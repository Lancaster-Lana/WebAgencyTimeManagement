<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="Administration_Holidays" Title="Untitled Page" Codebehind="Holidays.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <cc1:CustomGridView ID="cgvHolidays" runat="server" 
        onrowdatabound="cgvHolidays_RowDataBound" >
    </cc1:CustomGridView>
</asp:Content>

