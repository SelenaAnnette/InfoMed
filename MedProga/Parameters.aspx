<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parameters.aspx.cs" Inherits="MedProga.Parameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 46px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td> 
                <asp:Label ID="Label_date" runat="server" Text="Дата"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_DateTime" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
         <tr> 
            <td class="style1"> 
                Систолическое&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                артериальное<br />
                давление</td>
            <td class="style1"> 
                <asp:TextBox ID="TextBox_sad" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td> 
                Диастолическое<br />
                артериальное<br />
                давление</td>
            <td> 
                <asp:TextBox ID="TextBox_dad" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td> 
                Частота сердечных<br />
                сокращений</td>
            <td> 
                <asp:TextBox ID="TextBox_chss" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td> 
                <asp:Label ID="Label_weight" runat="server" Text="Вес"></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_weight" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td> 
                <asp:Label ID="Label_bedra" runat="server" Text="Окружность бедер (см)"></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_okr_beder" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td> 
                <asp:Label ID="Label_taliya" runat="server" Text="Окружность талии (см)"></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_okr_talii" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
    </table>
    <asp:Button ID="Button_parameters" runat="server" Text="Сохранить" 
        onclick="Button_parameters_Click" />
</asp:Content>
