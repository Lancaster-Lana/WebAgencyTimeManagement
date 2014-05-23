<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_AuditObject" Title="Untitled Page" Codebehind="AuditObject.aspx.cs" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>
<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

    <table>
        <tr>
            <td>Select an Object Name to Audit:</td>
            <td><asp:DropDownList ID="ddlObjectName" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlObjectName_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>        
        <tr>
            <td colspan="2"><asp:Table runat="server" ID="tblProperties"></asp:Table> </td>            
        </tr>
    </table>

</asp:Content>

