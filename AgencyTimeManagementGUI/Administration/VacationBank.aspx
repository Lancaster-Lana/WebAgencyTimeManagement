<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_VacationBank" Title="Untitled Page" Codebehind="VacationBank.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <table>
        <tr>
            <td>Employee:</td>
            <td><asp:DropDownList runat="server" ID="ddlENTUserAccount"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Year:</td>            
            <td><asp:DropDownList runat="server" ID="ddlYear"></asp:DropDownList></td>            
        </tr>
        <tr>
            <td>Personal Days</td>
            <td><asp:TextBox runat="server" ID="txtPersonalDays"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Vacation Days</td>
            <td><asp:TextBox runat="server" ID="txtVacationDays"></asp:TextBox></td>
        </tr>        
    </table>
</asp:Content>

