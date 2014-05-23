<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true"
    Inherits="Administration_Role" Title="Untitled Page" CodeBehind="Role.aspx.cs" %>

<%@ MasterType VirtualPath="~/PaidTimeOffEditPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        Role Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Grant rights for this role.
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Table ID="tblCapabilities" runat="server">
                        </asp:Table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lblUserHeader" runat="server" Text="Select the users for this role."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUsers" runat="server" Text="Users"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Users In Role"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lstUnselectedUsers" runat="server" Rows="10" SelectionMode="Multiple">
                                    </asp:ListBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnMoveToSelected" runat="server" Text=">" OnClick="btnMoveToSelected_Click" /><br />
                                    <asp:Button ID="btnMoveAllToSelected" runat="server" Text=">>" OnClick="btnMoveAllToSelected_Click" /><br />
                                    <br />
                                    <asp:Button ID="btnMoveToUnselected" runat="server" Text="<" OnClick="btnMoveToUnselected_Click" /><br />
                                    <asp:Button ID="btnMoveAllToUnselected" runat="server" Text="<<" OnClick="btnMoveAllToUnselected_Click" /><br />
                                </td>
                                <td>
                                    <asp:ListBox ID="lstSelectedUsers" runat="server" Rows="10" SelectionMode="Multiple">
                                    </asp:ListBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
