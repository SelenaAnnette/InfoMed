<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Complaints.aspx.cs" Inherits="MedProga.Complaints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td> 
                <asp:Label ID="Label_date" runat="server" Text="Дата и время"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_date_time" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr> 
            <td class="style1" style="vertical-align: top"> 
                <asp:Label ID="Label_symptoms" runat="server" Text="Симптомы"></asp:Label>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList_symptoms" runat="server" RepeatColumns="4">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    <asp:Button ID="Button_complaints" runat="server" Text="Сохранить" 
        onclick="Button_complaints_Click" />
</asp:Content>
