<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Therapy.aspx.cs" Inherits="MedProga.Therapy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td> 
                <asp:Label ID="Label_date" runat="server" Text="Дата"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_date" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td> 
                <asp:Label ID="Label_time" runat="server" Text="Время"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_time" runat="server" MaxLength="5"></asp:TextBox>
            </td>     
        </tr>
        <tr>
            <td> 
                <asp:Label ID="Label_drugs" runat="server" Text="Препараты"></asp:Label>
            </td>
            <td> 
                <asp:CheckBoxList ID="CheckBoxList_drugs" runat="server" 
                    DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name">
                    <asp:ListItem>Препарат1</asp:ListItem>
                    <asp:ListItem>Препарат2</asp:ListItem>
                    <asp:ListItem>Препарат3</asp:ListItem>
                </asp:CheckBoxList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:InfoMedMainDBConnectionString2 %>" 
                    SelectCommand="SELECT [Name] FROM [Medicaments] ORDER BY [Name]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <asp:Button ID="Button_therapy" runat="server" Text="Сохранить" 
        onclick="Button_therapy_Click" />
</asp:Content>
