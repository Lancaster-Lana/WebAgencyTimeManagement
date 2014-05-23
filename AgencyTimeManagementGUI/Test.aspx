<%@ Page Language="C#" MasterPageFile="~/PaidTimeOff.master" AutoEventWireup="true"
    Inherits="Test" Title="Untitled Page" CodeBehind="Test.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        // Function opens a new window when running reports.  Each window has the current date/time as the id so that
        // many can be opened at once.
        function postTo(formName, urlLocation, waitPage) {
            var currDate = 'ABC';
            window.open(waitPage, currDate, 'scrollbars=yes,status=no,resizable=yes');

            var objForm;
            objForm = document.forms[formName];

            // Cache previous action
            previousAction = objForm.action;

            // Set new parameters
            // objForm.action = urlLocation;
            objForm.target = currDate;
            objForm.method = "POST";
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox ID="TextBox1" runat="server">test</asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
</asp:Content>
