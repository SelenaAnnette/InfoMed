<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Patients.aspx.cs" Inherits="doc_int._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        &nbsp;</h2>
    <p>
        &nbsp;<table class="style1">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:CheckBox ID="name_c" runat="server" Text="Имя" Checked="True" />
                    <asp:CheckBox ID="mname_c" runat="server" Text="Отчество" Checked="True" />
                    <asp:CheckBox ID="surname_c" runat="server" Text="Фамилия" Checked="True" />
                    <asp:CheckBox ID="risk_c" runat="server" Text="Факторы риска" />
                    <asp:CheckBox ID="prep_c" runat="server" Text="Принимаемые препараты" />
                    <asp:CheckBox ID="symp_c" runat="server" Text="Симптомы" />
                    <asp:CheckBox ID="notif_c" runat="server" Text="Напоминания" />
                    <asp:CheckBox ID="phone_c" runat="server" Text="Телефон" />
                    <asp:CheckBox ID="email_c" runat="server" Text="E-mail" />
                    <br />
                    <br />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:GridView ID="grid1" runat="server"  Width="100%" CellPadding="4" 
                        ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
                    <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                        Text="Показать отмеченные" />
                    <br />
                    <br />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <br />
                    Поиск по фамилии<br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="id" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                                ReadOnly="True" SortExpression="id" />
                            <asp:BoundField DataField="Imya" HeaderText="Imya" SortExpression="Imya" />
                            <asp:BoundField DataField="Otchestvo" HeaderText="Otchestvo" 
                                SortExpression="Otchestvo" />
                            <asp:BoundField DataField="Familiya" HeaderText="Familiya" 
                                SortExpression="Familiya" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:DbConn %>" 
                        SelectCommand="SELECT [id], [Imya], [Otchestvo], [Familiya] FROM [Patient] WHERE ([Familiya] LIKE '%' +@Familiya+ '%')">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="TextBox1" Name="Familiya" PropertyName="Text" 
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
