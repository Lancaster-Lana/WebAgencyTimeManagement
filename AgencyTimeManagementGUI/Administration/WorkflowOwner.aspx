<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_WorkflowOwner" Title="Untitled Page" Codebehind="WorkflowOwner.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
    <table>
        <tr>
            <td style="text-align: right">Owner Group Name:</td>
            <td><asp:TextBox ID="txtOwnerGroupName" runat="server" Width="300px"></asp:TextBox></td>
        </tr>        
        <tr>
            <td style="text-align: left">Select a workflow to associate with this group:</td>
            <td><asp:DropDownList runat="server" ID="ddlWorkflow"></asp:DropDownList></td>
        </tr>        
        <tr>
            <td style="text-align: right">Description:</td>
            <td><asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" 
                    Width="300px"></asp:TextBox></td>
        </tr>        
        <tr>
            <td style="text-align: right">Select the default owner for this group:</td>
            <td><asp:DropDownList runat="server" ID="ddlDefaultOwner"></asp:DropDownList>
                <asp:CheckBox ID="chkSameAsLast" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chkSameAsLast_CheckedChanged" 
                    
                    Text="Check here to default to the same user as the user's last request." />
            </td>
        </tr>        
        <tr>
            <td colspan="2"><hr /></td>
        </tr>        
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblUserHeader" runat="server" 
                                Text="Select the users for this group."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblUsers" runat="server" Text="Available Users"></asp:Label></td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Users In Group"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstUnselectedUsers" runat="server" Rows="10" SelectionMode="Multiple"></asp:ListBox>
                        </td>
                        <td>
                            
                            <asp:Button ID="btnMoveToSelected" runat="server" Text=">" onclick="btnMoveToSelected_Click" /><br />
                            <asp:Button ID="btnMoveAllToSelected" runat="server" Text=">>" 
                                onclick="btnMoveAllToSelected_Click" /><br />
                            <br />
                            <asp:Button ID="btnMoveToUnselected" runat="server" Text="<" onclick="btnMoveToUnselected_Click" /><br />
                            <asp:Button ID="btnMoveAllToUnselected" runat="server" Text="<<" 
                                onclick="btnMoveAllToUnselected_Click" /><br />
                        </td>
                        <td>
                            <asp:ListBox ID="lstSelectedUsers" runat="server" Rows="10" SelectionMode="Multiple"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

