<%@ Page Language="C#" AutoEventWireup="true" Inherits="TestWebParts" Codebehind="TestWebParts.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:WebPartManager ID="WebPartManager1" runat="server">
        </asp:WebPartManager>
        
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Customize" />
        
        <table>
            <tr>
                <td>
                    <asp:WebPartZone ID="WebPartZone1" runat="server" BorderColor="#CCCCCC" 
                        Font-Names="Verdana" Padding="6">
                        <EmptyZoneTextStyle Font-Size="0.8em" />
                        <PartStyle Font-Size="0.8em" ForeColor="#333333" />
                        <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
                        <MenuLabelHoverStyle ForeColor="#E2DED6" />
                        <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" 
                            Font-Names="Verdana" Font-Size="0.6em" />
                        <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" 
                            ForeColor="White" />
                        <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" 
                            ForeColor="White" />
                        <ZoneTemplate>                            
                            <asp:Button ID="Button1" runat="server" Text="Button" Title="Button Web Part" />
                            <asp:TextBox ID="TextBox1" runat="server" Title="TextBox Web Part"></asp:TextBox>
                        </ZoneTemplate>
                        <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" 
                            BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" />
                        <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" 
                            ForeColor="White" />
                        <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                        <MenuLabelStyle ForeColor="White" />
                    </asp:WebPartZone>
                </td>
                <td>
                    <asp:WebPartZone ID="WebPartZone2" runat="server">
                        <ZoneTemplate>
                            <asp:Label ID="Label1" runat="server" Text="Label" Title="Label Web Part"></asp:Label>
                        </ZoneTemplate>
                    </asp:WebPartZone>
                </td>
                <td>
                    <asp:CatalogZone ID="CatalogZone1" runat="server">
                        <ZoneTemplate>
                            <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" />
                        </ZoneTemplate>
                    </asp:CatalogZone>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
