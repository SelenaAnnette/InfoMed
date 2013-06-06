<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Therapy.aspx.cs" Inherits="MedProga.Therapy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td> 
                <asp:Label ID="Label_date_time" runat="server" Text="Дата и время"></asp:Label>
            </td>
        </tr>
       <tr>
            <td style="vertical-align: top"> 
                <asp:TextBox ID="TextBox_date_time" runat="server" MaxLength="19" Width="135px" 
                    ToolTip="Введите дату и время, для которых необходимо просмотреть назначенные препараты"></asp:TextBox>
            </td>
        </tr>
       <tr>
            <td style="vertical-align: top"> 
                <asp:Button ID="Button_show_drugs" runat="server" Height="28px" 
                    onclick="Button_show_drugs_Click" Text="Показать назначенные препараты" 
                    Width="203px" />
            </td>
        </tr>
       <tr>
            <td style="vertical-align: top"> 
                <asp:Label ID="Label_drugs" runat="server" Visible="False"></asp:Label>
                <asp:GridView ID="GridView_drugs" runat="server">
                </asp:GridView>
            </td>
        </tr>
    </table>
    </asp:Content>
