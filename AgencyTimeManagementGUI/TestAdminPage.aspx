<%@ Page Language="C#" AutoEventWireup="true" Inherits="TestAdminPage" Codebehind="TestAdminPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btnInsert" runat="server" Text="Insert" onclick="btnInsert_Click" />
        <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
            Text="Update" />
        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
            Text="Delete" />
        <asp:Button ID="btnSelect" runat="server" onclick="btnSelect_Click" 
            Text="Select" />
        <asp:Button ID="btnCreateError" runat="server" Text="Create Error" 
            onclick="btnCreateError_Click" />
    </div>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    </form>
    
</body>
</html>
