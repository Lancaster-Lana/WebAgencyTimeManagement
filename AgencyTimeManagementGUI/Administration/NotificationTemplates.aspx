<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="Administration_NotificationTemplates" Title="Untitled Page" Codebehind="NotificationTemplates.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <cc1:CustomGridView ID="cgvNotificationTemplates" runat="server" 
        onrowdatabound="cgvNotificationTemplates_RowDataBound" >
    </cc1:CustomGridView>
</asp:Content>

