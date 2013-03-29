<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analysis.aspx.cs" Inherits="MedProga.Analysis" %>
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
            <td class="style1"> 
                <asp:GridView ID="GridView_analysis" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="Id" DataSourceID="SqlDataSource1">
                    <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" 
                            ReadOnly="True" />
                        <asp:BoundField DataField="PersonId" HeaderText="PersonId" 
                            SortExpression="PersonId" />
                        <asp:BoundField DataField="MeasuringTypeId" HeaderText="MeasuringTypeId" 
                            SortExpression="MeasuringTypeId" />
                        <asp:BoundField DataField="Value" HeaderText="Value" 
                            SortExpression="Value" />
                        <asp:BoundField DataField="MeasuringDate" HeaderText="MeasuringDate" 
                            SortExpression="MeasuringDate" />
                        
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:InfoMedTrashDBConnectionString %>" 
                    
                    
                    SelectCommand="SELECT * FROM [PersonMeasuring]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
