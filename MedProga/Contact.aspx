<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MedProga.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="TextBox_contact" runat="server" MaxLength="50" Height="79px" 
        Rows="5" Width="237px" TextMode="MultiLine"></asp:TextBox>
    <asp:Button ID="Button_contact" runat="server" Text="Отправить" 
        onclick="Button_contact_Click" />
</asp:Content>
