<%@ Page Title="Правка, добавление пациента" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="patient_info_input.aspx.cs" Inherits="doc_int.patient_info_input" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 225px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td class="style2" rowspan="13">
                <asp:GridView ID="GridView1" runat="server" onrowcommand="GridView1_RowCommand">
                      <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
            </td>
            <td width="350">
                Информация о пациенте</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="350">
                Фамилия, Имя, Отчество</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="350">
                Телефон</td>
            <td>
                <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="350">
                Мобильный телефон</td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="350">
                E-mail</td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="350">
                Домашний адрес</td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="350">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="350">
                Номер карточки пациента</td>
            <td>
                <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="350">
                Номер полиса медицинского страхования</td>
            <td>
                <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="350">
                Госпитализации в прошлом</td>
            <td>
                <asp:GridView ID="GridView2" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="350">
                Заболевания</td>
            <td>
                <asp:GridView ID="GridView3" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="350">
                Операции</td>
            <td>
                <asp:GridView ID="GridView4" runat="server">
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="350">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

