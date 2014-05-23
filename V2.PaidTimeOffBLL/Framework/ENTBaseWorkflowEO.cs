using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Agency.PaidTimeOffDAL.Framework;
using Agency.PaidTimeOffDAL;

namespace Agency.PaidTimeOffBLL.Framework
{
    [Serializable]
    public abstract class ENTBaseWorkflowEO : ENTBaseEO
    {
        #region Members

        private ENTWFStateEO _currentState = new ENTWFStateEO();

        #endregion Members

        #region Constructor

        public ENTBaseWorkflowEO()
            : base()
        {
            Workflow = new ENTWorkflowEO();
            WFItem = new ENTWFItemEO();
            WFOwnerGroups = new ENTWFOwnerGroupEOList();
            WFOwners = new ENTWFItemOwnerEOList();
            WFStateHistory = new ENTWFItemStateHistoryEOList();
            WFTransitions = new ENTWFTransitionEOList();
        }

        #endregion Constructor

        #region Properties

        public ENTBaseEO OriginalItem { get; set; }

        public ENTWorkflowEO Workflow { get; private set; }

        public ENTWFItemEO WFItem { get; private set; }

        public ENTWFOwnerGroupEOList WFOwnerGroups { get; private set; }

        public ENTWFItemOwnerEOList WFOwners { get; private set; }

        public ENTWFItemStateHistoryEOList WFStateHistory { get; private set; }

        public ENTWFTransitionEOList WFTransitions { get; private set; }

        public int ENTWFTransitionId { get; set; }

        public string NotificationPage { get; set; }

        public ENTWFStateEO CurrentState
        {
            get
            {
                if (WFItem.CurrentWFStateId != _currentState.ID)
                {
                    _currentState = new ENTWFStateEO();
                    _currentState.Load(WFItem.CurrentWFStateId);
                    return _currentState;
                }
                else
                {
                    return _currentState;
                }
            }
        }

        public int CurrentOwnerENTUserAccountId
        {
            get
            {
                //Determine the current owner by the current state
                if (WFItem.CurrentWFStateId != 0)
                {
                    if (CurrentState.IsOwnerSubmitter)
                    {
                        return WFItem.SubmitterENTUserAccountId;
                    }
                    else
                    {
                        return Convert.ToInt32(WFOwners.GetByENTWFOwnerGroupId(Convert.ToInt32(CurrentState.ENTWFOwnerGroupId)).ENTUserAccountId);
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public string CurrentOwnerUserName
        {
            get
            {
                //Determine the current owner by the current state
                if (WFItem.CurrentWFStateId != 0)
                {
                    if (CurrentState.IsOwnerSubmitter)
                    {
                        return WFItem.SubmitterUserName;
                    }
                    else
                    {
                        return WFOwners.GetByENTWFOwnerGroupId(Convert.ToInt32(CurrentState.ENTWFOwnerGroupId)).UserName;
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        #endregion Properties

        #region Public Methods

        public bool SaveWorkflow(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors,
            ENTBaseEO item, int userAccountId)
        {
            WFItem.ItemId = item.ID;

            ValidateWorkflow(db, ref validationErrors, item);

            if (validationErrors.Count == 0)
            {
                //Set the ID for all the child owner objects
                foreach (ENTWFItemOwnerEO entWFItemOwner in WFOwners)
                {
                    entWFItemOwner.ENTWFItemId = item.ID;
                }

                foreach (ENTWFItemStateHistoryEO entWFItemStateHistory in WFStateHistory)
                {
                    entWFItemStateHistory.ENTWFItemId = item.ID;
                }

                if (WFItem.Save(db, ref validationErrors, userAccountId))
                {
                    foreach (ENTWFItemOwnerEO wfItemOwner in WFOwners)
                    {
                        wfItemOwner.ENTWFItemId = WFItem.ID;

                        if (wfItemOwner.Save(db, ref validationErrors, userAccountId) == false)
                        {
                            return false;
                        }
                    }

                    foreach (ENTWFItemStateHistoryEO wfItemStateHistory in WFStateHistory)
                    {
                        if (wfItemStateHistory.IsNewRecord())
                        {
                            //A state history is only added if an item changes state or a different person becomes the owner.
                            //Send notification if user became owner, Chapter 8.
                            //Check if the new owner is registered to recieve a notification when they become the owner of an item.
                            ENTNotificationEO myNotification = new ENTNotificationEO();
                            if (myNotification.Load(db, ENTNotificationEO.NotificationType.IBecameOwnerOfIssue, wfItemStateHistory.ENTUserAccountId))
                            {
                                //Get the new owner's email address
                                ENTUserAccountEO newOwner = new ENTUserAccountEO();
                                newOwner.Load(db, wfItemStateHistory.ENTUserAccountId);

                                ENTEmailEO email = new ENTEmailEO
                                {
                                    FromEmailAddress = myNotification.FromEmailAddress,
                                    Subject = ReplaceTokens(myNotification.Subject, item),
                                    Body = ReplaceTokens(myNotification.Body, item),
                                    EmailStatusFlag = ENTEmailEO.EmailStatusFlagEnum.NotSent,
                                    ToEmailAddress = newOwner.Email
                                };

                                email.Save(db, ref validationErrors, userAccountId);
                            }
                        }

                        wfItemStateHistory.ENTWFItemId = WFItem.ID;

                        if (wfItemStateHistory.Save(db, ref validationErrors, userAccountId) == false)
                        {
                            return false;
                        }
                    }

                    //Call any methods the transition requires
                    if (ENTWFTransitionId != 0)
                    {
                        ENTWFTransitionEO entWFTransition = WFTransitions.Get(ENTWFTransitionId);

                        if (entWFTransition.PostTransitionMethodName != null)
                        {
                            //Create an instance of the object
                            Type objectType = Type.GetType(Workflow.ENTWorkflowObjectName);
                            //object listObject = Activator.CreateInstance(objectType);

                            //Call the method to load the object
                            objectType.InvokeMember(entWFTransition.PostTransitionMethodName, BindingFlags.InvokeMethod, null, item, new object[] { db });
                        }

                        //Send notifications if user requests to be notified when their issue changes state, Chapter 8.
                        ENTNotificationEO issueChangedStateNotification = new ENTNotificationEO();
                        if (issueChangedStateNotification.Load(db, ENTNotificationEO.NotificationType.MyRequestChangedState, WFItem.SubmitterENTUserAccountId))
                        {
                            //Get the submitters email address.
                            ENTUserAccountEO submitter = new ENTUserAccountEO();
                            submitter.Load(db, WFItem.SubmitterENTUserAccountId);

                            ENTEmailEO email = new ENTEmailEO
                            {
                                FromEmailAddress = issueChangedStateNotification.FromEmailAddress,
                                Subject = ReplaceTokens(issueChangedStateNotification.Subject, item),
                                Body = ReplaceTokens(issueChangedStateNotification.Body, item),
                                EmailStatusFlag = ENTEmailEO.EmailStatusFlagEnum.NotSent,
                                ToEmailAddress = submitter.Email
                            };

                            email.Save(db, ref validationErrors, userAccountId);
                        }

                        //Check if anyone registered for this notification for the current state.
                        ENTNotificationENTUserAccountEOList goesToStateNotification = new ENTNotificationENTUserAccountEOList();
                        goesToStateNotification.Load(db, WFItem.CurrentWFStateId, ENTNotificationEO.NotificationType.IssueIOwnedGoesToState);

                        if (goesToStateNotification.Count > 0)
                        {
                            //Get the notification details to send the email.
                            ENTNotificationEO notification = new ENTNotificationEO();
                            notification.Load(db, (int)ENTNotificationEO.NotificationType.IssueIOwnedGoesToState);

                            //Send notifications if user requests to be notified if they owned an issue an it reaches a specific state.
                            foreach (ENTWFItemOwnerEO owner in WFOwners)
                            {
                                ENTNotificationENTUserAccountEO notifyForState = goesToStateNotification.GetByENTUserAccountId((int)owner.ENTUserAccountId);
                                if (notifyForState != null)
                                {
                                    //Get the owner's email address.
                                    ENTUserAccountEO ownerUserAccount = new ENTUserAccountEO();
                                    ownerUserAccount.Load(db, (int)owner.ENTUserAccountId);

                                    ENTEmailEO email = new ENTEmailEO
                                    {
                                        FromEmailAddress = notification.FromEmailAddress,
                                        Subject = ReplaceTokens(notification.Subject, item),
                                        Body = ReplaceTokens(notification.Body, item),
                                        EmailStatusFlag = ENTEmailEO.EmailStatusFlagEnum.NotSent,
                                        ToEmailAddress = ownerUserAccount.Email
                                    };

                                    email.Save(db, ref validationErrors, userAccountId);
                                }
                            }
                        }
                    }

                    return true;
                }
                else
                {
                    //Failed item save.
                    return false;
                }
            }
            else
            {
                //Failed Validation
                return false;
            }
        }

        private string ReplaceTokens(string text, ENTBaseEO baseEO)
        {
            List<Token> tokens = new List<Token>();

            //state
            tokens.Add(new Token { TokenString = "<WFSTATE>", Value = CurrentState.StateName });

            //owner            
            tokens.Add(new Token { TokenString = "<WFOWNER>", Value = CurrentOwnerUserName });

            //itemid            
            tokens.Add(new Token { TokenString = "<WFITEMID>", Value = baseEO.ID.ToString() });

            //submit date            
            tokens.Add(new Token { TokenString = "<WFSUBMITDATE>", Value = (WFItem.InsertDate == DateTime.MinValue ? DateTime.Now.ToStandardDateFormat() : WFItem.InsertDate.ToStandardDateFormat()) });

            //link, get the page from the workflow.            
            tokens.Add(new Token { TokenString = "<LINK>", Value = "<a href='" + NotificationPage + StringHelpers.EncryptQueryString("id=" + WFItem.ItemId.ToString()) + "'>Click here to view the item.</a>" });

            return ENTNotificationEO.ReplaceTokens(tokens, text);
        }

        public void InitWorkflow(string className)
        {
            //Get the workflow object by class name            
            if (Workflow.LoadByObjectName(className))
            {
                //Get all unique owners for this workflow                
                WFOwnerGroups.Load(Workflow.ID);

                //Add an owner group to the work flow owners
                foreach (ENTWFOwnerGroupEO entWFOwnerGroup in WFOwnerGroups)
                {
                    Nullable<int> entUserAccountId = null;

                    if (entWFOwnerGroup.IsDefaultSameAsLast)
                    {
                        //Get this user's last request and set it as the default.
                        var lastUser = new ENTWFItemOwnerData().SelectLastUserByGroupId(entWFOwnerGroup.ID, WFItem.SubmitterENTUserAccountId);
                        if ((lastUser != null) && (lastUser.ENTUserAccountId != null))
                        {
                            entUserAccountId = lastUser.ENTUserAccountId;
                        }
                    }
                    else
                    {
                        //set the owner to the default one selected for this group.
                        entUserAccountId = entWFOwnerGroup.DefaultENTUserAccountId;
                    }

                    string userName = "";
                    if (entUserAccountId != null)
                    {
                        //get the user's name
                        var userAccount = new ENTUserAccountEO();
                        userAccount.Load((int)entUserAccountId);
                        userName = userAccount.DisplayText;
                    }

                    //Add this item owner with the default user.
                    WFOwners.Add(new ENTWFItemOwnerEO { ENTUserAccountId = entUserAccountId, ENTWFOwnerGroupId = entWFOwnerGroup.ID, UserName = userName });
                }

                //Load the transitions based on the current state.                
                WFTransitions.Load(WFItem.CurrentWFStateId);
            }
            else
            {
                throw new Exception("Workflow not set correctly.  Please associate this item with a workflow.");
            }
        }

        /// <summary>
        /// Retrieves all the workflow data associated with this item.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="itemId"></param>
        public void LoadWorkflow(string className, int itemId)
        {
            //Get the workflow object by class name            
            if (Workflow.LoadByObjectName(className))
            {
                //Load the WFItem using the itemId, this is the not the same as the primary key for the WFItem.                
                WFItem.LoadByItemId(Workflow.ID, itemId);

                //Get all owner groups for this workflow                
                WFOwnerGroups.Load(Workflow.ID);

                //Get the owners for this item                
                WFOwners.Load(WFItem.ID);

                //Add any owner groups that aren't in the list
                foreach (ENTWFOwnerGroupEO wfOwnerGroup in WFOwnerGroups)
                {
                    ENTWFItemOwnerEO wfItemOwner = WFOwners.SingleOrDefault(o => o.ENTWFOwnerGroupId == wfOwnerGroup.ID);

                    if (wfItemOwner == null)
                    {
                        //Add this with a blank user
                        WFOwners.Add(new ENTWFItemOwnerEO { ENTWFItemId = itemId, ENTWFOwnerGroupId = wfOwnerGroup.ID });
                    }
                }

                //Get all the state histories
                WFStateHistory.Load(WFItem.ID);

                //Load the transitions based on the current state.                
                WFTransitions.Load(WFItem.CurrentWFStateId);

                //Load the current state.
                _currentState = new ENTWFStateEO();
                _currentState.Load(WFItem.CurrentWFStateId);

            }
            else
            {
                throw new Exception("Workflow not set correctly.  Please associate this item with a workflow.");
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void ValidateWorkflow(HRPaidTimeOffDataContext db, ref ENTValidationErrors validationErrors,
            ENTBaseEO item)
        {
            //If the current owner is required.
            if (CurrentOwnerENTUserAccountId == 0)
            {
                validationErrors.Add("Please select the " + WFOwnerGroups.GetByENTWFOwnerGroupId(Convert.ToInt32(CurrentState.ENTWFOwnerGroupId)).OwnerGroupName + ".");
            }

            if (OriginalItem == null)
            {
                throw new Exception("The original item was not sent to the workflow save method.");
            }
            else
            {
                //Check required fields.
                ENTWFStatePropertyEOList entWFStateProperties = new ENTWFStatePropertyEOList();
                entWFStateProperties.Load(db, WFItem.CurrentWFStateId);

                Type objectType = Type.GetType(Workflow.ENTWorkflowObjectName);

                foreach (ENTWFStatePropertyEO entWFStateProperty in entWFStateProperties)
                {
                    if (entWFStateProperty.Required)
                    {
                        PropertyInfo property = objectType.GetProperty(entWFStateProperty.PropertyName);
                        string errorMessage = "The " + entWFStateProperty.PropertyName + " is required.";

                        if (property.PropertyType.IsEnum)
                        {
                            Array a = Enum.GetValues(property.PropertyType);
                            int value = Convert.ToInt32(property.GetValue(item, null));
                            bool isValid = false;
                            foreach (int i in a)
                            {
                                if (i == value)
                                {
                                    isValid = true;
                                    break;
                                }
                            }

                            if (isValid == false)
                            {
                                validationErrors.Add(errorMessage);
                            }
                        }
                        else
                        {
                            switch (property.PropertyType.Name)
                            {
                                case "Int32":
                                    if (Convert.ToInt32(property.GetValue(item, null)) == 0)
                                    {
                                        validationErrors.Add(errorMessage);
                                    }
                                    break;
                                case "String":
                                    if ((property.GetValue(item, null) == null) || (property.GetValue(item, null).ToString() == string.Empty))
                                    {
                                        validationErrors.Add(errorMessage);
                                    }
                                    break;
                                case "DateTime":
                                    if ((property.GetValue(item, null) == null) || (Convert.ToDateTime(property.GetValue(item, null)) == DateTime.MinValue))
                                    {
                                        validationErrors.Add(errorMessage);
                                    }
                                    break;
                                case "Nullable`1":
                                    if (property.GetValue(item, null) == null)
                                    {
                                        validationErrors.Add(errorMessage);
                                    }
                                    break;
                                default:
                                    throw new Exception("Property type unknown.");
                            }
                        }
                    }

                    //Check if this field is read only.  Only check read only fields if the record was already submitted.
                    if (((ENTBaseWorkflowEO)OriginalItem).CurrentState.ID != 0)
                    {
                        if (entWFStateProperty.ReadOnly)
                        {
                            PropertyInfo property = objectType.GetProperty(entWFStateProperty.PropertyName);

                            if (property.GetValue(item, null).ToString() != property.GetValue(OriginalItem, null).ToString())
                            {
                                validationErrors.Add("The " + entWFStateProperty.PropertyName + " can not be changed.");
                            }
                        }
                    }
                }
            }
        }

        #endregion Private Methods
    }
}
