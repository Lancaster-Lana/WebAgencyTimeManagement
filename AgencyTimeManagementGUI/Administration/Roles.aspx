﻿<%@ Page Language="C#" MasterPageFile="~/PaidTimeOffEditGrid.master" AutoEventWireup="true" Inherits="Administration_Roles" Title="Untitled Page" Codebehind="Roles.aspx.cs" %>

<%@ Register Assembly="Agency.FrameworkControls" Namespace="Agency.FrameworkControls" TagPrefix="cc1" %>
<%@ MasterType virtualPath="~/PaidTimeOffEditGrid.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
    <cc1:CustomGridView ID="cgvRoles" runat="server" 
        onrowdatabound="cgvRoles_RowDataBound" >
    </cc1:CustomGridView>
</asp:Content>

