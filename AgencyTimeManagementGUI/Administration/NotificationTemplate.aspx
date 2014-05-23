<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_NotificationTemplate" Title="Untitled Page" 
         ValidateRequest="false" Codebehind="NotificationTemplate.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<table>
    <tr>
        <td align="right">Notification:</td>
        <td><asp:Label runat="server" ID="lblDescription"></asp:Label></td>
        <td rowspan="4" valign="top">
            <table>
                <tr>
                    <td colspan="2">Enter the following tags into the subject or body to insert dynamic text.</td>
                </tr>
                <tr>
                    <td style="text-align: right">Item ID:</td>
                    <td>&lt;WFITEMID&gt;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Link to Item:</td>
                    <td>&lt;LINK&gt;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Current Owner:</td>
                    <td>&lt;WFOWNER&gt;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Current State:</td>
                    <td>&lt;WFSTATE&gt;</td>
                </tr>
                <tr>
                    <td style="text-align: right">Submit Date:</td>
                    <td>&lt;WFSUBMITDATE&gt;</td>
                </tr>                
            </table>
        </td>
    </tr>
    <tr>
        <td align="right">From Email Address:</td>
        <td><asp:TextBox runat="server" ID="txtFromEmailAddress" Width="450px" ></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right">Subject:</td>
        <td><asp:TextBox runat="server" ID="txtSubject" Width="450px"></asp:TextBox></td>
    </tr>
    <tr>
        <td valign="top" align="right">Body:</td>
        <td><asp:TextBox runat="server" ID="txtBody" Rows="25" TextMode="MultiLine" 
                Width="450px"></asp:TextBox></td>
    </tr>
</table>
</asp:Content>

