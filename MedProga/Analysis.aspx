<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="MedProga.Analysis" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 559px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_from" runat="server" Text="C"></asp:Label>
                <asp:TextBox ID="TextBox_from" runat="server"></asp:TextBox>
                <asp:Label ID="Label_to" runat="server" Text="По"></asp:Label>
                <asp:TextBox ID="TextBox_to" runat="server"></asp:TextBox>
            </td> 
        </tr>
        <tr>
            <td>
                <asp:CheckBoxList ID="CheckBoxList_Parameters" runat="server" RepeatColumns="3">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td> 
                <asp:Button ID="Button_show_table" runat="server" Height="25px" 
                    Text="Вывести таблицу" Width="250px" onclick="Button_show_table_Click" />
                <asp:Button ID="Button_create_chart" runat="server" Height="25px" 
                    Text="Построить график" Width="250px" 
                    onclick="Button_create_chart_Click" />
            </td>
        </tr>
        <tr> 
            <td class="style1"> 
                <asp:GridView ID="GridView_analysis" runat="server" Height="200px">
                </asp:GridView>
            </td>
            </tr>
            <tr>
            <td>
                <asp:Chart ID="Chart_analysis" runat="server" Width="616px" 
                    AlternateText="Построение графика невозможно">
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </chartareas>
                </asp:Chart>
            </td>
        </tr>
    </table>
</asp:Content>
