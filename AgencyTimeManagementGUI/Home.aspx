<%@ Page Language="C#" MasterPageFile="~/PaidTimeOff.master" AutoEventWireup="true" Inherits="Home" Title="Untitled Page" Codebehind="Home.aspx.cs" %>

<%@ Register src="Controls/PTORequests.ascx" tagname="PTORequests" tagprefix="uc1" %>

<%@ Register src="Controls/PTORequestsCalendar.ascx" tagname="PTORequestsCalendar" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:WebPartManager ID="WebPartManager1" runat="server">
    </asp:WebPartManager>
    <table>
        <tr>
            <td colspan="2" align="right">
                <asp:LinkButton runat="server" ID="lbtnCustomize" Text="Customize" 
                    onclick="lbtnCustomize_Click"></asp:LinkButton> | 
                    <asp:LinkButton runat="server" ID="lbtnReset" Text="Reset" 
                    onclick="lbtnReset_Click" ></asp:LinkButton> 
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CatalogZone ID="CatalogZone1" runat="server" BackColor="#F7F6F3" 
                    BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana" Padding="6">
                    <HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" 
                        ForeColor="#333333" />
                    <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" 
                        ForeColor="White" />
                    <FooterStyle BackColor="#E2DED6" HorizontalAlign="Right" />
                    <PartChromeStyle BorderColor="#E2DED6" BorderStyle="Solid" BorderWidth="1px" />
                    <PartLinkStyle Font-Size="0.8em" />
                    <InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
                    <ZoneTemplate>
                        <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" />
                    </ZoneTemplate>
                    <LabelStyle Font-Size="0.8em" ForeColor="#333333" />
                    <SelectedPartLinkStyle Font-Size="0.8em" />
                    <VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                    <HeaderStyle BackColor="#E2DED6" Font-Bold="True" Font-Size="0.8em" 
                        ForeColor="#333333" />
                    <EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                    <PartStyle BorderColor="#F7F6F3" BorderWidth="5px" />
                    <EmptyZoneTextStyle Font-Size="0.8em" ForeColor="#333333" />
                </asp:CatalogZone>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:WebPartZone ID="WebPartZone1" runat="server" 
                    PartChromeType="TitleAndBorder" BorderColor="#CCCCCC" Font-Names="Verdana" 
                    Padding="6" >
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
                    <uc1:PTORequests ID="requestsToApprove" runat="server" 
                        Filter="RequestsToApprove" title="Requests To Approve" />                    
                    <uc2:PTORequestsCalendar ID="PTORequestsCalendar1" runat="server" title="My Requests Calendar" />                    
                </ZoneTemplate>                
                    <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" 
                        BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" />
                    <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" 
                        ForeColor="White" />
                    <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                    <MenuLabelStyle ForeColor="White" />
                </asp:WebPartZone>

            </td>
            <td valign="top">
                <asp:WebPartZone ID="WebPartZone2" runat="server" 
                    PartChromeType="TitleAndBorder" BorderColor="#CCCCCC" Font-Names="Verdana" 
                    Padding="6">
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
                <uc1:PTORequests ID="myRequests" runat="server" Filter="MyRequests" title="My Requests" />                                        
                </ZoneTemplate>
                    <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" 
                        BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" />
                    <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" 
                        ForeColor="White" />
                    <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
                    <MenuLabelStyle ForeColor="White" />
                </asp:WebPartZone>
            </td>            
        </tr>
    </table>
</asp:Content>

