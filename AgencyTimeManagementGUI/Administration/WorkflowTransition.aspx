<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_WorkflowTransition" Title="Untitled Page" Codebehind="WorkflowTransition.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <table>
        <tr>
            <td style="text-align: right">Transition Name:</td>
            <td><asp:TextBox ID="txtTransitionName" runat="server"></asp:TextBox></td>
        </tr>        
        <tr>
            <td style="text-align: right">Workflow:</td>
            <td><asp:DropDownList runat="server" ID="ddlWorkflow" AutoPostBack="True" 
                    onselectedindexchanged="ddlWorkflow_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>        
        <tr>
            <td style="text-align: right">From State:</td>
            <td><asp:DropDownList runat="server" ID="ddlFromState"></asp:DropDownList></td>
        </tr>        
        <tr>
            <td style="text-align: right">To State:</td>
            <td><asp:DropDownList runat="server" ID="ddlToState"></asp:DropDownList></td>
        </tr>                
        <tr>
            <td style="text-align: right">Select a method to execute after a successful transition:</td>
            <td><asp:DropDownList runat="server" ID="ddlPostTransitionMethodName"></asp:DropDownList></td>
        </tr>        
    </table>

</asp:Content>

