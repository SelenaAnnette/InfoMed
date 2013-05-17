<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Risk.aspx.cs" Inherits="MedProga.Risk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2
        {
            width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td class="style2"> 
                <asp:PlaceHolder ID="PlaceHolder_risk" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_risk" runat="server" Text="Сохранить" 
                onclick="Button_risk_Click" Height="25px" Width="100px" />
            </td>
        </tr>
        <tr>
            <td>
               <asp:Button ID="Button_clear_risk" runat="server" Height="25px" 
                    onclick="Button_clear_risk_Click" Text="Очистить" Width="100px" />
            </td>
        </tr>
    </table>
    </asp:Content>
