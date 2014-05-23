<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_User" Title="Untitled Page" Codebehind="User.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <table>
        <tr>
            <td>Windows Account Name:</td>
            <td>
                <asp:TextBox ID="txtWindowsAccountName" runat="server"></asp:TextBox></td>
        </tr>    
        <tr>
            <td>First Name:</td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
        </tr>    
        <tr>
            <td>Last Name:</td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
        </tr>    
        <tr>
            <td>Email Address:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
        </tr>    
        <tr>
            <td>Active:</td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" /></td>
        </tr>
    </table>
</asp:Content>

