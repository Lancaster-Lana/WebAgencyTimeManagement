<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_Holiday" Title="Untitled Page" Codebehind="Holiday.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
<table>
        <tr>
            <td>Holiday Name:</td>
            <td><asp:TextBox ID="txtHolidayName" runat="server"></asp:TextBox></td>
        </tr>        
        <tr>
            <td valign="top">Date:</td>
            <td>
                <asp:Label runat="server" ID="lblSelectedDate"></asp:Label>
                <asp:Calendar ID="calHolidayDate" runat="server" 
                    ondayrender="calHolidayDate_DayRender" 
                    onselectionchanged="calHolidayDate_SelectionChanged"></asp:Calendar>
            </td>
        </tr>        
    </table>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

