<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="Administration_VacationBanks" Title="Untitled Page" Codebehind="VacationBanks.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <asp:Button runat="server" ID="btnCopy" Text="Copy Year to Year" 
        onclick="btnCopy_Click" />
    <cc1:CustomGridView ID="cgvVacationBanks" runat="server" 
        onrowdatabound="cgvVacationBanks_RowDataBound">
    </cc1:CustomGridView>
</asp:Content>

