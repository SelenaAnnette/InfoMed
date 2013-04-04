<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="MedProga.Reminders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
    <tr>
        <td> 
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
            </asp:CheckBoxList>
        </td>
    </tr>
    <tr>
        <td> 
            <asp:Button ID="Button_save" runat="server" onclick="Button_save_Click" 
                Text="Сохранить" />
        </td>
    </tr>
    <tr>
        <td> 
            <asp:Button ID="Button_close" runat="server" onclick="Button_close_Click" 
                Text="Закрыть" />
        </td>
    </tr>
</table>
</asp:Content>
