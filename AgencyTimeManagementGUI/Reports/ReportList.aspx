<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="Reports_ReportList" Title="Untitled Page" Codebehind="ReportList.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <cc1:CustomGridView ID="cgvReports" runat="server" 
        onrowdatabound="cgvReports_RowDataBound" >
    </cc1:CustomGridView>

</asp:Content>

