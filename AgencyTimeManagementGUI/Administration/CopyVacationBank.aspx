<%@ Page Language="C#" MasterPageFile="~/PaidTimeOff.master" AutoEventWireup="true" Inherits="Administration_CopyVacationBank" Title="Untitled Page" Codebehind="CopyVacationBank.aspx.cs" %>
<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table>
    <tr>
        <td>This screen allows you to copy all the users and their personal and vacation days to another year.</td>
    </tr>
    
</table>
<table>    
    <tr>
        <td colspan="2"><cc1:ValidationErrorMessages ID="ValidationErrorMessages1" runat="server" /></td>
    </tr>
    <tr>
        <td>Select the year to copy from:</td>
        <td><asp:DropDownList runat="server" ID="ddlFrom"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Select the year to copy to:</td>
        <td><asp:DropDownList runat="server" ID="ddlTo"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>            
            <asp:Button runat="server" ID="btnCopy" Text="Copy" onclick="btnCopy_Click" /></td>
        <td><asp:Button runat="server" ID="btnCancel" Text="Cancel" 
                onclick="btnCancel_Click" /> </td>
    </tr>
</table>
</asp:Content>

