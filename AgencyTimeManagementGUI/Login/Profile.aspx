<%@ Page Language="C#" MasterPageFile="~/Layouts/MainMasterPage.master" AutoEventWireup="true" Inherits="Profile" Title="User Profile" Codebehind="Profile.aspx.cs" %>

<asp:Content ContentPlaceHolderID="phBodyContent" Runat="Server">
    <table width="100%">
        <tr class="styleDataGroups">           
            <td colspan="2">Profile settings</td>
            <td>
                <asp:Label ID="lblWindowsAccountName" runat="server"></asp:Label></td>
        </tr>    
        <tr>
            <td></td>
            <td>First Name:</td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
        </tr>    
        <tr>
            <td></td>
            <td>Last Name:</td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
        </tr>    
        <tr>
            <td></td>
            <td>Email Address:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
        </tr>    
        <tr>
            <td></td>
            <td>Active:</td>
            <td>
                <asp:CheckBox ID="chkActive" runat="server" /></td>
        </tr>        
         <tr class="styleDataGroups">           
            <td colspan="3"></td>
         </tr>   
    </table>
</asp:Content>