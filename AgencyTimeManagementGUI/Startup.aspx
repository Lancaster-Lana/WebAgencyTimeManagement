<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Startup.aspx.cs" Inherits="AgencyTimeManagementGUI.Startup" %>

<center>
    <asp:login id="ctrlLogin" runat="server" bordercolor="Black" borderwidth="1" backcolor="AliceBlue"
        onloggedin="ctrlLogin_LoggedIn" onloginerror="ctrlLogin_LoginError" onauthenticate="ctrlLogin_Authenticate" />
    <p />
    <asp:hyperlink id="lnkRegister" navigateurl="~/Login/Registration.aspx" runat="server">Registration</asp:hyperlink>
    <p />
    <asp:textbox id="txtStatus" runat="server" textmode="MultiLine" style="width: 280px;
        height: 100px; background-color: Transparent; border-style: none; overflow: auto;
        color: Red;" />
</center>
