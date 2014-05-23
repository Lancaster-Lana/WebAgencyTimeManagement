using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Agency.PaidTimeOffBLL.Framework;

namespace Agency.FrameworkControls
{
    [ToolboxData("<{0}:WorkflowController runat=server></{0}:WorkflowController>")]
    public class WorkflowController : WebControl, INamingContainer
    {
        #region Members

        private HtmlTable tblWFUserGroups = new HtmlTable();
        private Label lblCurrentState = new Label();
        private Label lblActions = new Label();
        private DropDownList ddlTransitions = new DropDownList();
        private CustomGridView cgvWFStateHistory = new CustomGridView();

        #endregion Members

        #region Properties

        public ENTWorkflowEO Workflow { get; private set; }
        public ENTBaseEO BaseEOObject { get; set; }
        public string WorkflowObjectName { get; set; }

        #endregion Properties

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            var tblWorkflow = new HtmlTable();
            //Row 1
            var tr1 = new HtmlTableRow();
            tr1.Attributes.Add("class", "gridViewHeader");
            var tcR1C1 = new HtmlTableCell();
            tcR1C1.ColSpan = 2;
            tcR1C1.InnerText = "Workflow";
            tr1.Cells.Add(tcR1C1);
            tblWorkflow.Rows.Add(tr1);

            //Row 2
            var tr2 = new HtmlTableRow();
            var tcR2C1 = new HtmlTableCell();
            tcR2C1.InnerText = "Current State:";
            tr2.Cells.Add(tcR2C1);

            var tcR2C2 = new HtmlTableCell();
            lblCurrentState.ID = "lblCurrentState";
            tcR2C2.Controls.Add(lblCurrentState);
            tr2.Cells.Add(tcR2C2);
            tblWorkflow.Rows.Add(tr2);

            //Row 3
            var tr3 = new HtmlTableRow();
            var tcR3C1 = new HtmlTableCell();

            lblActions.ID = "lblActions";
            lblActions.Text = "Actions:";
            tcR3C1.Controls.Add(lblActions);
            tr3.Cells.Add(tcR3C1);

            var tcR3C2 = new HtmlTableCell();

            ddlTransitions.ID = "ddlTransitions";
            tcR3C2.Controls.Add(ddlTransitions);
            tr3.Cells.Add(tcR3C2);
            tblWorkflow.Rows.Add(tr3);
            Controls.Add(tblWorkflow);
            //Add the owners.            
            CreateWFGroupOwnersTable();
            //Add custom grid view control
            cgvWFStateHistory.ID = "cgvWFStateHistory";
            Controls.Add(cgvWFStateHistory);
        }

        private void CreateWFGroupOwnersTable()
        {
            var tr1 = new HtmlTableRow();
            var tc1 = new HtmlTableCell();
            tc1.Attributes.Add("class", "gridViewHeader");
            tc1.ColSpan = 2;
            tc1.InnerText = "Users";
            tr1.Cells.Add(tc1);
            tblWFUserGroups.Rows.Add(tr1);

            //Get the workflow associated with this object.
            Workflow = new ENTWorkflowEO();
            Workflow.LoadByObjectName(WorkflowObjectName);

            //Get the groups associated with this workflow
            var entWFOwnerGroups = new ENTWFOwnerGroupEOList();
            entWFOwnerGroups.Load(Workflow.ID);

            //Create the table for all the owner groups
            foreach (var wfOwnerGroup in entWFOwnerGroups)
            {
                var tr = new HtmlTableRow();
                var tcName = new HtmlTableCell();
                tcName.InnerText = wfOwnerGroup.OwnerGroupName + ":";
                tcName.Attributes.Add("ENTWFOwnerGroupId", wfOwnerGroup.ID.ToString());
                tr.Cells.Add(tcName);

                var tcUsers = new HtmlTableCell();
                var ddlUsers = new DropDownList();
                ddlUsers.DataSource = wfOwnerGroup.UserAccounts;
                ddlUsers.DataTextField = "UserName";
                ddlUsers.DataValueField = "ENTUserAccountId";
                ddlUsers.DataBind();
                ddlUsers.Items.Insert(0, new ListItem("", "0"));
                tcUsers.Controls.Add(ddlUsers);
                tr.Cells.Add(tcUsers);
                tblWFUserGroups.Rows.Add(tr);
            }
            Controls.Add(tblWFUserGroups);
        }

        public void LoadControlFromObject(ENTBaseWorkflowEO baseWorkflowEO, int currentUserId)
        {
            this.EnsureChildControls();

            //Select the user for the group
            for (int row = 1; row < tblWFUserGroups.Rows.Count; row++)
            {
                HtmlTableRow tr = tblWFUserGroups.Rows[row];

                //The owner group id is an attribute in the first cell
                int entWFOwnerGroupId = Convert.ToInt32(tr.Cells[0].Attributes["ENTWFOwnerGroupId"]);

                var ddlUsers = (DropDownList)tr.Cells[1].Controls[0];

                //Select the correct record.
                var itemOwner = baseWorkflowEO.WFOwners.GetByENTWFOwnerGroupId(entWFOwnerGroupId);
                if (itemOwner.ENTUserAccountId == null)
                {
                    ddlUsers.SelectedIndex = 0;
                }
                else
                {
                    ddlUsers.Items.FindByValue(itemOwner.ENTUserAccountId.ToString()).Selected = true;
                }
            }

            //Set the current state label.
            lblCurrentState.Text = baseWorkflowEO.CurrentState.StateName + " (" + baseWorkflowEO.CurrentOwnerUserName + ")";

            if (baseWorkflowEO.WFTransitions.Count == 0)
            {
                //Hide Transitions
                lblActions.Visible = false;
                ddlTransitions.Visible = false;
            }
            else
            {
                //Load the transition drop down        
                ddlTransitions.DataSource = baseWorkflowEO.WFTransitions;
                ddlTransitions.DataTextField = "DisplayText";
                ddlTransitions.DataValueField = "ID";
                ddlTransitions.DataBind();
            }

            //If this is a new item then there must be a transition picked.
            if (baseWorkflowEO.ID != 0)
            {
                ddlTransitions.Items.Insert(0, new ListItem("", "0"));

                //If this is an existing item and the current user is not the current owner then do not let them 
                //transition the item.
                if (currentUserId != baseWorkflowEO.CurrentOwnerENTUserAccountId)
                {
                    lblActions.Visible = false;
                    ddlTransitions.Visible = false;
                }
            }

            //Load the state history grid
            cgvWFStateHistory.ListClassName = typeof(ENTWFItemStateHistoryEOList).AssemblyQualifiedName;
            cgvWFStateHistory.LoadMethodName = "Load";
            cgvWFStateHistory.LoadMethodParameters.Add(baseWorkflowEO.WFItem.ID);
            cgvWFStateHistory.SortExpressionLast = "InsertDate";

            //Name
            cgvWFStateHistory.AddBoundField("StateName", "State", "");
            cgvWFStateHistory.AddBoundField("OwnerName", "Owner", "");
            cgvWFStateHistory.AddBoundField("InsertDate", "Date", "");
            cgvWFStateHistory.AddBoundField("InsertedBy", "Moved By", "");

            cgvWFStateHistory.DataBind();
        }

        public void LoadObjectFromControl(ENTBaseWorkflowEO baseWorkflowEO)
        {
            this.EnsureChildControls();

            baseWorkflowEO.WFItem.ItemId = baseWorkflowEO.ID;
            baseWorkflowEO.WFItem.ENTWorkflowId = baseWorkflowEO.Workflow.ID;

            //Get the users that have been selected to be owners for this workflow.
            for (int row = 1; row < tblWFUserGroups.Rows.Count; row++)
            {
                var tr = tblWFUserGroups.Rows[row];

                //Get selected user id
                var ddlOwner = (DropDownList)tr.Cells[1].Controls[0];
                int entUserAccountId = Convert.ToInt32(ddlOwner.SelectedValue);

                //Try to find the owner group 
                int entWFOwnerGroupId = Convert.ToInt32(tr.Cells[0].Attributes["ENTWFOwnerGroupId"]);
                var itemOwner = baseWorkflowEO.WFOwners.GetByENTWFOwnerGroupId(entWFOwnerGroupId);
                if (itemOwner == null)
                {
                    //This must be added to the object
                    baseWorkflowEO.WFOwners.Add(new ENTWFItemOwnerEO
                    {
                        ENTUserAccountId = entUserAccountId,
                        ENTWFItemId = baseWorkflowEO.ID,
                        ENTWFOwnerGroupId = entWFOwnerGroupId,
                    });
                }
                else
                {
                    //Set the id of the selected user
                    itemOwner.ENTUserAccountId = entUserAccountId;
                }
            }

            //Check if the user is transitioning this item
            baseWorkflowEO.ENTWFTransitionId = Convert.ToInt32(ddlTransitions.SelectedValue);
            if (baseWorkflowEO.ENTWFTransitionId != 0)
            {
                //Change the current state.
                var transition = new ENTWFTransitionEO();
                transition.Load(baseWorkflowEO.ENTWFTransitionId);

                baseWorkflowEO.WFItem.CurrentWFStateId = transition.ToENTWFStateId;

                //Add to state history
                baseWorkflowEO.WFStateHistory.Add(new ENTWFItemStateHistoryEO
                {
                    ENTWFStateId = baseWorkflowEO.WFItem.CurrentWFStateId,
                    ENTUserAccountId = baseWorkflowEO.CurrentOwnerENTUserAccountId
                });
            }
            else
            {
                //Check if the user change the owner by chaning, this should be shown in the state history also.
                if (baseWorkflowEO.CurrentOwnerENTUserAccountId != ((ENTBaseWorkflowEO)baseWorkflowEO.OriginalItem).CurrentOwnerENTUserAccountId)
                {
                    //Add to state history
                    baseWorkflowEO.WFStateHistory.Add(new ENTWFItemStateHistoryEO
                    {
                        ENTWFStateId = baseWorkflowEO.WFItem.CurrentWFStateId,
                        ENTUserAccountId = baseWorkflowEO.CurrentOwnerENTUserAccountId
                    });
                }
            }
            //Set the notification page
            baseWorkflowEO.NotificationPage = this.Context.Request.Url.AbsoluteUri;
        }
    }
}
