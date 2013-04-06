<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="MedProga.Analysis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 559px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr> 
            <td class="style1"> 
                <asp:GridView ID="GridView_analysis" runat="server">
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
