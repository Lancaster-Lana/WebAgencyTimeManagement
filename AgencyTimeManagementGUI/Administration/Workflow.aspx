<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_Workflow" Title="Untitled Page" Codebehind="Workflow.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <table>
        <tr>
            <td>Workflow Name:</td>
            <td><asp:TextBox ID="txtWorkflowName" runat="server" Width="200px"></asp:TextBox></td>
        </tr>        
        <tr>
            <td>Select a class name.:</td>
            <td><asp:DropDownList runat="server" ID="ddlObjectName"></asp:DropDownList></td>
        </tr>                
    </table>

</asp:Content>

