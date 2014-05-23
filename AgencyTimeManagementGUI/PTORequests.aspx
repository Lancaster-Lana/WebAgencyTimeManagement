<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="PTORequests" Title="Untitled Page" Codebehind="PTORequests.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<cc1:CustomGridView ID="cgvPTORequests" runat="server" 
        onrowdatabound="cgvPTORequests_RowDataBound" >
    </cc1:CustomGridView>
</asp:Content>

