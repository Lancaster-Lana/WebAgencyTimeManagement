<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="Administration_AuditObjects" Title="Untitled Page" Codebehind="AuditObjects.aspx.cs" %>
<%@ Register TagPrefix="cc1" Namespace="Agency.FrameworkControls" Assembly="Agency.FrameworkControls" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <cc1:CustomGridView ID="cgvAuditObjects" runat="server" 
        onrowdatabound="cgvAuditObjects_RowDataBound" >
    </cc1:CustomGridView>
</asp:Content>

