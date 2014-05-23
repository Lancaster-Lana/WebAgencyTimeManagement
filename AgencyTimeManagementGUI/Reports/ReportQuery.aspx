<%@ Page Language="C#" MasterPageFile="~/PaidTimeOff.master" AutoEventWireup="true" Inherits="Reports_ReportQuery" Title="Untitled Page" Codebehind="ReportQuery.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <cc1:QueryBuilder ID="QueryBuilder1" runat="server" />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnPreview" runat="server" Text="Print Preview" 
                    onclick="btnPreview_Click" />
            </td>
            <td>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    onclick="btnCancel_Click" style="height: 26px" />
            </td>
        </tr>
    </table>
</asp:Content>

