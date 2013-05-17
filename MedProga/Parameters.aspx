<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parameters.aspx.cs" Inherits="MedProga.Parameters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2
        {
            width: 140px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
       <tr>
            <td class="style2">
                <asp:PlaceHolder ID="PlaceHolder_parameters" runat="server"></asp:PlaceHolder>
            </td>
       </tr>
       <tr>
            <td class="style2"> 
                <asp:Button ID="Button_parameters" runat="server" Text="Сохранить" 
                            onclick="Button_parameters_Click" Height="25px" Width="100px"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_clear_parameters" runat="server" Height="25px" 
                    onclick="Button_clear_parameters_Click" Text="Очистить" Width="100px" />
            </td>
        </tr>
    </table>
    </asp:Content>
