using System;
using System.Collections.Generic;
using System.Linq;
using Agency.PaidTimeOffDAL;
using Agency.PaidTimeOffDAL.Framework;

namespace Agency.PaidTimeOffBLL.Framework
{
    #region ENTMenuItemBO

    [Serializable]
    public class ENTMenuItemBO : ENTBaseBO
    {
        #region Constructor

        public ENTMenuItemBO()
        {
            ChildMenuItems = new ENTMenuItemBOList();
        }

        #endregion Constructor

        #region Properties

        public string MenuItemName { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Nullable<int> ParentMenuItemId { get; set; }
        public short DisplaySequence { get; set; }
        public bool IsAlwaysEnabled { get; set; }
        public ENTMenuItemBOList ChildMenuItems { get; private set; }

        #endregion Properties

        #region Overrides

        public override bool Load(int id)
        {
            throw new NotImplementedException();
        }

        protected override void MapEntityToCustomProperties(IENTBaseEntity entity)
        {
            var menuItem = (ENTMenuItem)entity;

            ID = menuItem.ENTMenuItemId;
            MenuItemName = menuItem.MenuItemName;
            Description = menuItem.Description;
            Url = menuItem.Url;
            ParentMenuItemId = menuItem.ParentENTMenuItemId;
            DisplaySequence = menuItem.DisplaySequence;
            IsAlwaysEnabled = menuItem.IsAlwaysEnabled;
        }

        protected override string GetDisplayText()
        {
            throw new NotImplementedException();
        }

        #endregion Overrides

        #region Public Methods

        public bool HasAccessToMenu(ENTUserAccountEO userAccount, ENTRoleEOList roles)
        {
            if (IsAlwaysEnabled)
            {
                return true;
            }
            //Loop through all the roles this user is in.  The first time the user has
            //access to the menu item then return true.  If you get through all the
            //roles then the user does not have access to this menu item.
            foreach (var role in roles)
            {
                //Check if this user is in this role
                if (role.RoleUserAccounts.IsUserInRole(userAccount.ID))
                {
                    //Try to find the capability with the menu item Id.
                    IEnumerable<ENTRoleCapabilityEO> capabilities = role.RoleCapabilities.GetByMenuItemId(ID);

                    if (capabilities.Any(capability => (capability != null) 
                        && (capability.AccessFlag != ENTRoleCapabilityEO.CapabiiltyAccessFlagEnum.None)))
                    {
                        return true;
                    }
                }
            }

            //If it gets here then the user didn't have access to this menu item.  BUT they may have access
            //to one of its children, now check the children and if they have access to any of  them  then 
            //return true.
            if (ChildMenuItems.Count > 0)
            {
                return ChildMenuItems.Any(child => child.HasAccessToMenu(userAccount, roles));
            }

            //If it never found a role with any capability then return false.
            return false;
        }

        #endregion Public Methods
    }

    #endregion ENTMenuItemBO

    #region ENTMenuItemBOList

    [Serializable]
    public class ENTMenuItemBOList : ENTBaseBOList<ENTMenuItemBO>
    {
        #region Overrides

        /// <summary>
        /// This will load up the object with the correct parent\child relationships within the menu structure.
        /// Any parent menu item will have its child menu items loaded in it's ChildMenuItems property.
        /// </summary>
        public override void Load()
        {
            //Load the list from the database.  This will then be traversed to create the 
            //parent child relationships in for each menu item.
            List<ENTMenuItem> menuItems = new ENTMenuItemData().Select();

            //Traverse through the list to create the parent child relationships                                                    
            foreach (ENTMenuItem menuItem in menuItems)
            {
                var menuItemBO = new ENTMenuItemBO();
                menuItemBO.MapEntityToProperties(menuItem);

                // Check if the menu already exists in this object.
                if (MenuExists(menuItemBO.ID) == false)
                {
                    //Doesn't exist so now check if this is a top level item.
                    if (menuItemBO.ParentMenuItemId == null)
                    {
                        //Top level item so just add it.
                        this.Add(menuItemBO);
                    }
                    else
                    {
                        // Get the parent menu item from this object if it exists.
                        ENTMenuItemBO parent = GetByMenuItemId(Convert.ToInt32(menuItemBO.ParentMenuItemId));

                        if (parent == null)
                        {
                            // If it gets here then the parent isn't in the list yet.
                            // Find the parent in the list.                            
                            ENTMenuItemBO newParentMenuItem = FindOrLoadParent(menuItems, Convert.ToInt32(menuItemBO.ParentMenuItemId));

                            // Add the current child menu item to the newly added parent
                            newParentMenuItem.ChildMenuItems.Add(menuItemBO);
                        }
                        else
                        {
                            // Parent already existed in this object.
                            // Add this menu to the child of the parent
                            parent.ChildMenuItems.Add(menuItemBO);
                        }
                    }
                }
            }
        }

        #endregion Overrides

        #region Public Methods
        /// <summary>
        /// Checks if the menu item exists.  This will look at the child object of the menu also.
        /// </summary>
        public bool MenuExists(int menuItemId)
        {
            return (GetByMenuItemId(menuItemId) != null);
        }

        /// <summary>
        /// Returns the menu item for the specified id.  This will search through all child items in the list.
        /// </summary>
        public ENTMenuItemBO GetByMenuItemId(int menuItemId)
        {
            foreach (ENTMenuItemBO menuItem in this)
            {
                // Check if this is the item we are looking for
                if (menuItem.ID == menuItemId)
                {
                    return menuItem;
                }
                else
                {
                    // Check if this menu has children
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        // Search the children for this item.
                        ENTMenuItemBO childMenuItem = menuItem.ChildMenuItems.GetByMenuItemId(menuItemId);

                        // If the menu is found in the children then it won't be null
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }

            //It wasn't found so return null.
            return null;
        }

        //Return the menu item that is at the top most level that does not have a parent.
        public ENTMenuItemBO GetTopMenuItem(string menuItemName)
        {
            //Find the menu item by it name.
            ENTMenuItemBO menuItem = GetByMenuItemName(menuItemName);

            while (menuItem.ParentMenuItemId != null)
            {
                menuItem = GetByMenuItemId(Convert.ToInt32(menuItem.ParentMenuItemId));
            }

            return menuItem;
        }

        public ENTMenuItemBO GetByMenuItemName(string menuItemName)
        {
            foreach (ENTMenuItemBO menuItem in this)
            {
                // Check if this is the item we are looking for
                if (menuItem.MenuItemName == menuItemName)
                {
                    return menuItem;
                }
                else
                {
                    // Check if this menu has children
                    if (menuItem.ChildMenuItems.Count > 0)
                    {
                        // Search the children for this item.
                        ENTMenuItemBO childMenuItem = menuItem.ChildMenuItems.GetByMenuItemName(menuItemName);

                        // If the menu is found in the children then it won't be null
                        if (childMenuItem != null)
                        {
                            return childMenuItem;
                        }
                    }
                }
            }

            //It wasn't found so return null.
            return null;
        }

        #endregion Public Methods

        #region Private Methods

        private ENTMenuItemBO FindOrLoadParent(List<ENTMenuItem> menuItems, int parentMenuItemId)
        {
            // Find the menu item in the entity list.
            ENTMenuItem parentMenuItem = menuItems.Single(m => m.ENTMenuItemId == parentMenuItemId);

            // Load this into the business object.
            var menuItemBO = new ENTMenuItemBO();
            menuItemBO.MapEntityToProperties(parentMenuItem);

            // Check if it has a parent
            if (parentMenuItem.ParentENTMenuItemId == null)
            {
                this.Add(menuItemBO);
            }
            else
            {
                // Since this has a parent it should be added to its parent's children.
                // Try to find the parent in the list already.
                ENTMenuItemBO parent = GetByMenuItemId(Convert.ToInt32(parentMenuItem.ParentENTMenuItemId));

                if (parent == null)
                {
                    // This one's parent wasn't found.  So add it.
                    ENTMenuItemBO newParent = FindOrLoadParent(menuItems, Convert.ToInt32(parentMenuItem.ParentENTMenuItemId));
                    newParent.ChildMenuItems.Add(menuItemBO);
                }
                else
                {
                    // Add this menu to the child of the parent
                    parent.ChildMenuItems.Add(menuItemBO);
                }
            }

            return menuItemBO;
        }


        public ENTMenuItemBOList GetSubMenuItems(ENTMenuItemBO topMenuItem)
        {
            return topMenuItem.ChildMenuItems;
        }
    }

        #endregion Private Methods

    #endregion ENTMenuItemBOList
}
