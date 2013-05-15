<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parameters.aspx.cs" Inherits="MedProga.Parameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 46px;
        }
        .style2
        {
            width: 140px;
        }
        .style3
        {
            height: 46px;
            width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td height="50" class="style2"> 
                <asp:Label ID="Label_date" runat="server" Text="Дата и время"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_DateTime" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
         <tr> 
            <td class="style3" height="50"> 
                Систолическое&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                артериальное<br />
                давление</td>
            <td class="style1"> 
                <asp:TextBox ID="TextBox_sad" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td height="50" class="style2"> 
                Диастолическое<br />
                артериальное<br />
                давление</td>
            <td> 
                <asp:TextBox ID="TextBox_dad" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td height="50" class="style2"> 
                Частота сердечных<br />
                сокращений</td>
            <td> 
                <asp:TextBox ID="TextBox_chss" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td height="50" class="style2"> 
                <asp:Label ID="Label_weight" runat="server" Text="Вес"></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_weight" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td class="style2" height="50"> 
                <asp:Label ID="Label_bedra" runat="server" Text="Окружность бедер (см)" ></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_okr_beder" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
         <tr> 
            <td class="style2" height="50"> 
                <asp:Label ID="Label_taliya" runat="server" Text="Окружность талии (см)" ></asp:Label>
             </td>
            <td> 
                <asp:TextBox ID="TextBox_okr_talii" runat="server" MaxLength="3"></asp:TextBox>
             </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="Button_parameters" runat="server" Text="Сохранить" 
        onclick="Button_parameters_Click" Height="25px" Width="100px" />
            </td>
            <td> 
                <asp:Button ID="Button_clear_parameters" runat="server" Height="25px" 
                    onclick="Button_clear_parameters_Click" Text="Очистить" Width="100px" />
            </td>
        </tr>
    </table>
    </asp:Content>
