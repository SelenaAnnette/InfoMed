<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Risk.aspx.cs" Inherits="MedProga.Risk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td> 
                <asp:Label ID="Label_distance" runat="server" 
                    Text="Пройденная дистанция за день (км)"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_distance" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr> 
            <td> 
                <asp:Label ID="Label_kkal" runat="server" 
                    Text="Количество поглощенных с пищей калорий (ккал)"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_kkal" runat="server" MaxLength="4"></asp:TextBox>
            </td>
        </tr>
         <tr> 
            <td> 
                <asp:Label ID="Label_alcohol" runat="server" 
                    Text="Количество алкоголя за сутки (мл)"></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_alcohol" runat="server" MaxLength="5"></asp:TextBox>
             </td>
        </tr>
        <tr> 
            <td> 
                <asp:Label ID="Label_alc_rate" runat="server" 
                    Text="Крепость принятого алкоголя (%)"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_alc_rate" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
         <tr> 
            <td> 
                <asp:Label ID="Label_smoking" runat="server" 
                    Text="Количество выкуренных сигарет  (шт в день)"></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_smoking" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
    </table>
    <asp:Button ID="Button_risk" runat="server" Text="Сохранить" 
        onclick="Button_risk_Click" />
</asp:Content>
