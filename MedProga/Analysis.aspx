<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="MedProga.Analysis" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_from" runat="server" Text="C"></asp:Label>
                <asp:TextBox ID="TextBox_from" runat="server" MaxLength="19"></asp:TextBox>
                <asp:Label ID="Label_to" runat="server" Text="По"></asp:Label>
                <asp:TextBox ID="TextBox_to" runat="server" MaxLength="19"></asp:TextBox>
            </td> 
        </tr>
         <tr>
            <td>
                <asp:PlaceHolder ID="PlaceHolder_analysis" runat="server"></asp:PlaceHolder>
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
                <asp:Button ID="Button_analysis" runat="server" Text="Проанализировать" 
                    onclick="Button_analysis_Click" Width="130px" Height="25px"/>
                <asp:Button ID="Button_clear_selection" runat="server" 
                    onclick="Button_clear_selection_Click" Text="Очистить выделение" 
                    Width="130px" Height="25px"/>
            </td>
        </tr>
        <tr> 
            <td class="style1"> 
                <asp:GridView ID="GridView_analysis" runat="server">
                </asp:GridView>
                <asp:Chart ID="Chart_analysis" runat="server" Height="200" Width="616px" 
                    Visible="False">
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </chartareas>
                </asp:Chart>
            </td>
        </tr>
    </table>
</asp:Content>
