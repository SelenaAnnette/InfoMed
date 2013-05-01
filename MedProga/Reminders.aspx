<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="MedProga.Reminders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
    <tr>
        <td> 
            <asp:CheckBoxList ID="CheckBoxList_nots" runat="server">
            </asp:CheckBoxList>
        </td>
        <td>
             
            &nbsp;</td>
    </tr>
    <tr>
        <td> 
            <asp:Button ID="Button_save" runat="server" onclick="Button_save_Click" 
                Text="Сохранить" Width="105px" />
        </td>
    </tr>
</table>
</asp:Content>
