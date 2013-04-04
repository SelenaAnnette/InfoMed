<%@ Page Title="О нас" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Drugs.aspx.cs" Inherits="doc_int.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
        .style2
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
    <table class="style1">
        <tr>
            <td width="30%">
                <asp:Label ID="Label2" runat="server" 
                    Text="Укажите необходимые данные для ввыписки препарата."></asp:Label>
            </td>
            <td width="40%">
                &nbsp;</td>
            <td width="30%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center" width="30%">
                <asp:GridView ID="GridView1" runat="server" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    onrowcommand="GridView1_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
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
            </td>
            <td class="style2" width="40%">
                Пациент<asp:TextBox ID="TextBox1" runat="server" Height="21px"></asp:TextBox>
                <br />
                Препарат<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <br />
                <br />
                Принимать препарат
                по
                <asp:TextBox ID="dosage" runat="server" Type="Number" Width="30px">1</asp:TextBox>
&nbsp; единице(ы)
                <br />
                <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" 
                    GroupName="choose" Text="N раз в день" />
                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="choose" 
                    Text="Раз в N дней" />
                <br />
                <asp:TextBox ID="choose_num" runat="server" Width="25px" Type="Number">2</asp:TextBox>
                <br />
&nbsp;на продолжении
                <asp:TextBox ID="dayCount" runat="server" Type="Number" Width="35px">14</asp:TextBox>
                &nbsp;дня/дней.<br />
                <asp:Button ID="Button1" runat="server" Text="Выписать" 
                    onclick="Button1_Click" />
                <br />
            </td>
            <td style="text-align: center" width="30%">
                &nbsp;<br />
                &nbsp;<br />
                <asp:GridView ID="GridView2" runat="server" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    onrowcommand="GridView2_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
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
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="30%">
                &nbsp;</td>
            <td width="40%">
                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
                    BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                    DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                    ForeColor="#003399" Height="200px" Width="220px" 
                    ondayrender="Calendar1_DayRender"
                    onselectionchanged="Calendar1_SelectionChanged">
                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                        Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                    <WeekendDayStyle BackColor="#CCCCFF" />
                </asp:Calendar>
                <asp:Label ID="Label1" runat="server" 
                    Text="Выберите дату начала приёма препарата"></asp:Label>
            </td>
            <td width="30%">
                ID 1<asp:TextBox ID="TextBox3" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                ID 2<asp:TextBox ID="TextBox4" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                Доза<asp:TextBox ID="TextBox5" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                Начало<asp:TextBox ID="TextBox6" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                Кол-во дней<asp:TextBox ID="TextBox7" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                Сколько в день<asp:TextBox ID="TextBox8" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                Через сколько дней<asp:TextBox ID="TextBox9" runat="server" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                    Text="Проверка" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</p>
</asp:Content>
