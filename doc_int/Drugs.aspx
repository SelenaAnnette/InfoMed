<%@ Page Title="Выписать препарат" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Drugs.aspx.cs" Inherits="doc_int.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
        .style2
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
    <table class="style1">
        <tr>
            <td width="30%">
                1. Для начала консультации выберите пациента из списка и нажмите кнопку &quot;Начать 
                консультацию&quot;</td>
            <td width="40%">
                <asp:Label ID="Label3" runat="server" Text="2. Выберите причину консультации" 
                    Visible="False"></asp:Label>
            </td>
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
                <asp:Panel ID="Panel2" runat="server" Height="100%" Visible="False" 
                    Width="100%">
                    <asp:RadioButton ID="RadioButton3" runat="server" GroupName="consultationTypes" 
                        Text="Внеплановый визит" />
                    <br />
                    <asp:RadioButton ID="RadioButton4" runat="server" GroupName="consultationTypes"                        
                        Text="Контрольный визит по плану" />
                    <br />
                    <asp:RadioButton ID="RadioButton5" runat="server" GroupName="consultationTypes" 
                        Text="Первичное обследование" />
                    <br />
                    <asp:RadioButton ID="RadioButton6" runat="server" GroupName="consultationTypes" 
                    Text="Снатие с учета и прекращение наблюдения" />
                </asp:Panel>
            </td>
            <td style="text-align: center" width="30%">
                &nbsp;<br />
                &nbsp;<br />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td width="30%">
                &nbsp;</td>
            <td width="40%">
                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                    style="text-align: left" Text="Начать консультацию" Visible="False" />
            </td>
            <td width="30%">
                &nbsp;</td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="width: 60%">
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" Visible="False" 
                    Width="100%">
                    <table class="style1">
                        <tr>
                            <td>
                                2. Выберите препарат</td>
                            <td>
                                <asp:Label ID="Label2" runat="server" 
                                    Text="3. Укажите необходимые данные для ввыписки препарата."></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" 
                                    Text="4. Выберите дату начала приёма препарата"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" 
                                    GridLines="None" onrowcommand="GridView2_RowCommand" Width="350px">
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
                                Пациент<asp:TextBox ID="TextBox1" runat="server" Height="21px"></asp:TextBox>
                                <br />
                                Препарат<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                <br />
                                <br />
                                Принимать препарат по
                                <asp:TextBox ID="dosage" runat="server" Type="Number" Width="30px">1</asp:TextBox>
                                &nbsp; единице(ы)
                                <br />
                                <asp:RadioButton ID="RadioButton1" runat="server" Checked="True" 
                                    GroupName="choose" Text="N раз в день" />
                                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="choose" 
                                    Text="Раз в N дней" />
                                <br />
                                <asp:TextBox ID="choose_num" runat="server" Type="Number" Width="25px">2</asp:TextBox>
                                <br />
                                &nbsp;на продолжении
                                <asp:TextBox ID="dayCount" runat="server" Type="Number" Width="35px">14</asp:TextBox>
                                &nbsp;дня/дней.<br />
                                <br />
                                Способ приёма препарата:<br />
                                <asp:RadioButton ID="RadioButton7" runat="server" GroupName="way" 
                                    Text="Энтернальный (ингаляционный)" Checked="True" />
                                &nbsp;<asp:RadioButton ID="RadioButton8" runat="server" GroupName="way" 
                                    Text="Парентальный" />
                                <br />
                            </td>
                            <td>
                                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
                                    BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                                    DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                    ForeColor="#003399" Height="200px" ondayrender="Calendar1_DayRender" 
                                    onselectionchanged="Calendar1_SelectionChanged" Width="220px">
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="Measuring" runat="server" Text="Замеры" />
                                <br />
                                <br />
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <asp:ListItem Selected="True" Value="B2AB9366-3621-4067-B8F8-0741111D6B58">Вес</asp:ListItem>
                                    <asp:ListItem Value="061ECDED-8E47-4652-A0AC-2619D3DED3F0">Частота дыхательных движений</asp:ListItem>
                                    <asp:ListItem Value="7D48F462-B556-43C9-BDDC-2FA952A11337">Диастолическое АД</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                Сделать замер через<br />
                                <asp:TextBox ID="timeInterval" runat="server" Type="Number"  Width="35px">5</asp:TextBox>
                                &nbsp;минут после приёма препарата</td>
                            <td style="text-align: center">
                                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                    Text="Выписать" />
                                <br />
                                <asp:Button ID="Button4" runat="server" Enabled="False" onclick="Button4_Click" 
                                    Text="Завершить консультацию" />
                            </td>
                            <td>
                                ConId<asp:TextBox ID="TextBox10" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                <br />
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
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</p>
</asp:Content>
