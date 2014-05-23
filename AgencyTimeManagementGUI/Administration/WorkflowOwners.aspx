<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="Administration_WorkflowOwners" Title="Untitled Page" Codebehind="WorkflowOwners.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

    <cc1:CustomGridView ID="cgvWorkflow" runat="server" onrowdatabound="cgvWorkflow_RowDataBound" 
        >
<RowStyle CssClass="gridViewRow"></RowStyle>

<PagerStyle CssClass="gridViewPager"></PagerStyle>

<HeaderStyle CssClass="gridViewHeader"></HeaderStyle>

<AlternatingRowStyle CssClass="gridViewAlternatingRow"></AlternatingRowStyle>
    </cc1:CustomGridView>
    
</asp:Content>

