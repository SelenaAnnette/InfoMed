<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="doc_int.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table class="style1">
    <tr>
        <td width="150">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td width="150">
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Height="100%" ReadOnly="True" 
                Width="100%"></asp:TextBox>
        </td>
    </tr>
</table>

</asp:Content>
