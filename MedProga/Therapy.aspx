<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Therapy.aspx.cs" Inherits="MedProga.Therapy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td> 
                <asp:Label ID="Label_date_time" runat="server" Text="Дата и время"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_date_time" runat="server" MaxLength="10" 
                    ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
       <tr>
            <td style="vertical-align: top"> 
                <asp:Label ID="Label_drugs" runat="server" Text="Препараты"></asp:Label>
            </td>
            <td> 
                <asp:GridView ID="GridView_drugs" runat="server">
                </asp:GridView>
            </td>
        </tr>
    </table>
    </asp:Content>
