<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="MedProga.Reminders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<table>
    <tr>
        <td> 
            <asp:Label ID="Label_text" runat="server" Text="Текст напоминания"></asp:Label>
        </td>
    </tr>
    <tr>
        <td> 
            <asp:TextBox ID="TextBox_reminder" runat="server" TextMode="MultiLine" 
                Height="51px"></asp:TextBox>
        </td>
    </tr>
    <tr> 
        <td> 
            <asp:CheckBox ID="CheckBox_activation" runat="server" Text="Активировано" />
        </td>
    </tr>
    <tr> 
        <td> 
            <asp:Label ID="Label_time" runat="server" Text="Время отправки"></asp:Label>
            <asp:CheckBoxList ID="CheckBoxList_time" runat="server" RepeatColumns="4" 
                CssClass="no">
                <asp:ListItem>00:00</asp:ListItem>
                <asp:ListItem>01:00</asp:ListItem>
                <asp:ListItem>02:00</asp:ListItem>
                <asp:ListItem>03:00</asp:ListItem>
                <asp:ListItem>04:00</asp:ListItem>
                <asp:ListItem>05:00</asp:ListItem>
                <asp:ListItem>06:00</asp:ListItem>
                <asp:ListItem>07:00</asp:ListItem>
                <asp:ListItem>08:00</asp:ListItem>
                <asp:ListItem>09:00</asp:ListItem>
                <asp:ListItem>10:00</asp:ListItem>
                <asp:ListItem>11:00</asp:ListItem>
                <asp:ListItem>12:00</asp:ListItem>
                <asp:ListItem>13:00</asp:ListItem>
                <asp:ListItem>14:00</asp:ListItem>
                <asp:ListItem>15:00</asp:ListItem>
                <asp:ListItem>16:00</asp:ListItem>
                <asp:ListItem>17:00</asp:ListItem>
                <asp:ListItem>18:00</asp:ListItem>
                <asp:ListItem>19:00</asp:ListItem>
                <asp:ListItem>20:00</asp:ListItem>
                <asp:ListItem>21:00</asp:ListItem>
                <asp:ListItem>22:00</asp:ListItem>
                <asp:ListItem>23:00</asp:ListItem>
            </asp:CheckBoxList>        
        </td>
    </tr>
    <tr>
        <td>         
            <asp:Label ID="Label_days_of_week" runat="server" Text="Дни недели"></asp:Label>
            <asp:CheckBoxList ID="CheckBoxList_days" runat="server" 
                RepeatDirection="Horizontal" CssClass="no">
                <asp:ListItem>Пн</asp:ListItem>
                <asp:ListItem>Вт</asp:ListItem>
                <asp:ListItem>Ср</asp:ListItem>
                <asp:ListItem>Чт</asp:ListItem>
                <asp:ListItem>Пт</asp:ListItem>
                <asp:ListItem>Сб</asp:ListItem>
                <asp:ListItem>Вс</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td> 
            <asp:Button ID="Button_save" runat="server" Text="Сохранить" 
                onclick="Button_save_Click" />
        </td>
    </tr>
</table>
</asp:Content>
