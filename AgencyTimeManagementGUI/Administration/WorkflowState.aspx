<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditPage.master" AutoEventWireup="true" Inherits="Administration_WorkflowState" Title="Untitled Page" Codebehind="WorkflowState.aspx.cs" %>

<%@ MasterType virtualPath="~/PaidTimeOffEditPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">    
    <ContentTemplate>
    <table>
        <tr>
            <td style="text-align: right">State Name:</td>
            <td><asp:TextBox ID="txtStateName" runat="server" Width="300px"></asp:TextBox></td>
        </tr>        
        <tr>
            <td style="text-align: right">Select a workflow to associate with this state:</td>
            <td><asp:DropDownList runat="server" ID="ddlWorkflow" AutoPostBack="True" 
                    onselectedindexchanged="ddlWorkflow_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>        
        <tr>
            <td style="text-align: right">Description:</td>
            <td><asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" 
                    Width="300px"></asp:TextBox></td>
        </tr>        
        <tr>
            <td style="text-align: right">Select the group that owns the issue while in this state:</td>
            <td><asp:DropDownList runat="server" ID="ddlWFOwnerGroup" ></asp:DropDownList>
                <asp:CheckBox ID="chkIsSubmitter" runat="server" AutoPostBack="True" 
                    oncheckedchanged="chkIsSubmitter_CheckedChanged" 
                    Text="Check here if the submitter is the default owner." />
            </td>
        </tr>                
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Table runat="server" ID="tblProperties"></asp:Table> </td>            
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

