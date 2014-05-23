<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Please, login</title>
    <link href="../CSS/SiteStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="mainContainer" class="page">
        <tr class="header">
            <td style="width: 300px">
            </td>
            <td>
                <h1>
                     Industries Deals
                </h1>
            </td>
            <td class="loginDisplay">
                <asp:LoginView ID="LoginView2" runat="server" EnableViewState="false">
                    <AnonymousTemplate>
                        [<a href="~/Startup.aspx" id="HeadLoginStatus" runat="server">Log In</a> ]
                    </AnonymousTemplate>
                    <LoggedInTemplate>  Welcome 
                        <span class="bold">
                            <asp:LoginName ID="HeadLoginName" runat="server" />
                        </span>! [
                        <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out"
                            LogoutPageUrl="~/" /> ]
                    </LoggedInTemplate>
                </asp:LoginView>
            </td>
        </tr>
        <tr style="background-color: InactiveBorder">
            <td colspan="3" valign="top">
                <center>
                    <div class="content" align="left">
                        <h3> Registration Page</h3>
                        <asp:Label ID="lblUserName" Width="150px" runat="server" Text="User Name: " />
                        <asp:TextBox ID="txtUserName" runat="server" />
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="RegisterValidate">*</asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Label ID="lblPassword" runat="server" Width="150px" Text="Password: " />
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="RegisterValidate">*</asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Label ID="lblConfirmPassword" runat="server" Width="150px" Text="Confirm Password: " />
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" />
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Password is required." ToolTip="Confirm Password is required."
                            ValidationGroup="RegisterValidate">*</asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"  />
                        <br />
                        <br />
                        <asp:Button ID="btnAddUser" OnClick="btnAddUser_Click" ValidationGroup="RegisterValidate"
                            runat="server" Text="Registrate" />
                    </div>
                </center>
            </td>
        </tr>
        <tr class="footer">
            <td colspan="3">
                <asp:Label ID="lblCopyright" runat="server">Copyright © LANA-SOFT</asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
