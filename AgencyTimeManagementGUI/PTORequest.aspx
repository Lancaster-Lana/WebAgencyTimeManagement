<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="PTORequest" Title="Untitled Page" Codebehind="PTORequest.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>            
    <table>
        <tr class="gridViewHeader">
            <td>PTO Bank</td>
            <td>This Year Bank</td>
            <td>Carried From Last Year</td>
            <td>Approved\Waiting Approval</td>
            <td>Balance</td>
        </tr>
        <tr>
            <td>Personal Days</td>
            <td><asp:Label runat="server" ID="lblPersonalDaysBank"></asp:Label></td>
            <td></td>
            <td><asp:Label runat="server" ID="lblPersonalDaysUsed"></asp:Label></td>
            <td><asp:Label runat="server" ID="lblPersonalBalance"></asp:Label></td>
        </tr>
        <tr>
            <td>Vacation Days</td>
            <td><asp:Label runat="server" ID="lblVacationDaysBank"></asp:Label></td>
            <td><asp:Label runat="server" ID="lblVacationCarry"></asp:Label></td>
            <td><asp:Label runat="server" ID="lblVacationDaysUsed"></asp:Label></td>
            <td><asp:Label runat="server" ID="lblVacationBalance"></asp:Label></td>
        </tr>
        <tr>
            <td>Unpaid</td>
            <td></td>
            <td></td>
            <td><asp:Label runat="server" ID="lblUnpaidUsed"></asp:Label></td>
            <td></td>
        </tr>
    </table>
    <table> 
        <tr class="gridViewHeader">
            <td colspan="2">Requested</td>
        </tr>   
        <tr> 
            <td runat="server">Request Type:</td>                       
            <td runat="server"><asp:DropDownList runat="server" ID="ddlPTORequestType" 
                    AutoPostBack="True"></asp:DropDownList></td>                       
        </tr>
        <tr>                        
            <td runat="server">Full or Half Day:</td>                       
            <td runat="server"><asp:DropDownList runat="server" ID="ddlPTODayType" 
                    AutoPostBack="True"></asp:DropDownList></td>                       
        </tr>        
        <tr>                        
            <td runat="server" valign="top">Date:</td>                       
            <td><asp:Label runat="server" ID="lblRequestDate"></asp:Label> 
                <asp:Calendar ID="calFullDay" runat="server" ondayrender="calFullDay_DayRender" 
                    BackColor="White" BorderColor="#999999" CellPadding="4" 
                    DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                    Height="250px" Width="300px" 
                    onselectionchanged="calFullDay_SelectionChanged" 
                    onvisiblemonthchanged="calFullDay_VisibleMonthChanged" >
                <SelectedDayStyle Font-Bold="False" ForeColor="Black" />
                <SelectorStyle BackColor="#CCCCCC" />
                <WeekendDayStyle BackColor="#FFFFCC" />
                <TodayDayStyle ForeColor="Black" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <DayStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                </asp:Calendar></td>            
        </tr>
        </table>       
        <hr />        
                
        <cc1:WorkflowController runat="server" ID="wfcPTORequest">
        </cc1:WorkflowController>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

