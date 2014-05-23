using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Agency.PaidTimeOffDAL.Framework;
using Agency.Common;

namespace Agency.PaidTimeOffDAL.Reports
{
    [QueryField("CurrentOwner.LastName + ', ' + CurrentOwner.FirstName", "Current Owner", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    [QueryField("ENTWFState.StateName", "Current State", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    [QueryField("PTODayType.PTODayTypeName", "Day Type", QueryFieldAttribute.QueryFieldTypeEnum.String)]
    [QueryField("convert(varchar, PTORequest.RequestDate, 101)", "Request Date", QueryFieldAttribute.QueryFieldTypeEnum.Date)]
    [QueryField("PTORequestType.PTORequestTypeName", "Request Type", QueryFieldAttribute.QueryFieldTypeEnum.String)]       
    [QueryField("ENTUserAccount.LastName + ', ' + ENTUserAccount.FirstName", "Requestor", QueryFieldAttribute.QueryFieldTypeEnum.String)]        
    [RequiredQueryField("ENTWorkflowObjectName = 'Agency.PaidTimeOffBLL.PTORequestEO, Agency.PaidTimeOffBLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'")]
    public class ReportPTORequestsQueryData : ENTBaseQueryData<ReportMyPTORequestsResult>
    {
        protected override string SelectClause()
        {            
            return "SELECT ENTUserAccount.LastName, ENTUserAccount.FirstName, PTORequest.RequestDate, " +
                          "PTODayType.PTODayTypeName, PTORequestType.PTORequestTypeName, ENTWFState.StateName AS CurrentState, " +
                          "CurrentOwner.LastName AS CurrentOwnerLastName, CurrentOwner.FirstName AS CurrentOwnerFirstName ";             
        }

        protected override string FromClause()
        {
            return "FROM ENTWorkflow " +
                 "INNER JOIN ENTWFItem ON ENTWorkflow.ENTWorkflowId = ENTWFItem.ENTWorkflowId " +
                 "INNER JOIN PTORequest " +
                 "INNER JOIN PTODayType ON PTORequest.PTODayTypeId = PTODayType.PTODayTypeId " +
                 "INNER JOIN PTORequestType ON PTORequest.PTORequestTypeId = PTORequestType.PTORequestTypeId " +
                         "ON ENTWFItem.ItemId = PTORequest.PTORequestId " +
                 "INNER JOIN ENTUserAccount ON PTORequest.ENTUserAccountId = ENTUserAccount.ENTUserAccountId " +
                 "INNER JOIN ENTWFState ON ENTWFItem.CurrentWFStateId = ENTWFState.ENTWFStateId " +
                 "INNER JOIN ENTWFItemOwner ON ENTWFState.ENTWFOwnerGroupId = ENTWFItemOwner.ENTWFOwnerGroupId " +
                        "AND ENTWFItem.ENTWFItemId = ENTWFItemOwner.ENTWFItemId " +
                 "INNER JOIN ENTUserAccount AS CurrentOwner ON ENTWFItemOwner.ENTUserAccountId = CurrentOwner.ENTUserAccountId ";            
        }                
    }        
}
