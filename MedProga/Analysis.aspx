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
                    DataKeyNames="id_par" DataSourceID="SqlDataSource1">
                    <Columns>
                    <asp:BoundField DataField="id_par" HeaderText="id_par" SortExpression="id_par" 
                            InsertVisible="False" ReadOnly="True" />
                        <asp:BoundField DataField="id_пац" HeaderText="id_пац" 
                            SortExpression="id_пац" />
                        <asp:BoundField DataField="Дата" HeaderText="Дата" 
                            SortExpression="Дата" />
                        <asp:BoundField DataField="Время_суток" HeaderText="Время_суток" 
                            SortExpression="Время_суток" />
                        <asp:BoundField DataField="САД" HeaderText="САД" SortExpression="САД" />
                        <asp:BoundField DataField="ДАД" HeaderText="ДАД" 
                            SortExpression="ДАД" />
                        
                        <asp:BoundField DataField="ЧСС" HeaderText="ЧСС" 
                            SortExpression="ЧСС" />
                        
                        <asp:BoundField DataField="Вес" HeaderText="Вес" 
                            SortExpression="Вес" />
                        <asp:BoundField DataField="Окр_бедер" HeaderText="Окр_бедер" 
                            SortExpression="Окр_бедер" />
                        
                        <asp:BoundField DataField="Окр_талии" HeaderText="Окр_талии" 
                            SortExpression="Окр_талии" />
                        
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                    
                    
                    SelectCommand="SELECT * FROM [Parameters]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
