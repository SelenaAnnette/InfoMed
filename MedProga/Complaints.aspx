<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Complaints.aspx.cs" Inherits="MedProga.Complaints" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            height: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table> 
        <tr> 
            <td> 
                <asp:Label ID="Label_date" runat="server" Text="Дата"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_date" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr> 
            <td> 
                <asp:Label ID="Label_time" runat="server" Text="Время"></asp:Label>
            </td>
            <td> 
                <asp:TextBox ID="TextBox_time" runat="server" MaxLength="5"></asp:TextBox>
            </td>
        </tr>
        <tr> 
            <td class="style1"> 
                <asp:Label ID="Label_symptoms" runat="server" Text="Симптомы"></asp:Label>
            </td>
            <td class="style1"> 
                <asp:TextBox ID="TextBox_complaints" runat="server" Height="50px" 
                    TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td> </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList_symptoms" runat="server" AutoPostBack="True" 
                    RepeatColumns="4" DataSourceID="SqlDataSource1" DataTextField="Name" 
                    DataValueField="Name">
                    <asp:ListItem>Апатия</asp:ListItem>
                    <asp:ListItem>Беспокойство, тревога</asp:ListItem>
                    <asp:ListItem>Бессонница (Нарушение сна)</asp:ListItem>
                    <asp:ListItem>Болезненное мочеиспускание</asp:ListItem>
                    <asp:ListItem>Боль в груди</asp:ListItem>
                    <asp:ListItem>Боль внизу живота</asp:ListItem>
                    <asp:ListItem>Вздутие живота</asp:ListItem>
                    <asp:ListItem>Выраженная мышечная слабость</asp:ListItem>
                    <asp:ListItem>Вялость</asp:ListItem>
                    <asp:ListItem>Газы (Метеоризм)</asp:ListItem>
                    <asp:ListItem>Головная боль</asp:ListItem>
                    <asp:ListItem>Головокружение</asp:ListItem>
                    <asp:ListItem>Горечь во рту</asp:ListItem>
                    <asp:ListItem>Двоение перед глазами</asp:ListItem>
                    <asp:ListItem>Дезориентация и спутанность сознания</asp:ListItem>
                    <asp:ListItem>Диспепсия (Несварение)</asp:ListItem>
                    <asp:ListItem>Дисфагия (Нарушение глотания)</asp:ListItem>
                    <asp:ListItem>Жажда</asp:ListItem>
                    <asp:ListItem>Жар</asp:ListItem>
                    <asp:ListItem>Заторможенность</asp:ListItem>
                    <asp:ListItem>Изжога</asp:ListItem>
                    <asp:ListItem>Изменение речи</asp:ListItem>
                    <asp:ListItem>Изменения цвета мочи</asp:ListItem>
                    <asp:ListItem>Икота</asp:ListItem>
                    <asp:ListItem>Кашель</asp:ListItem>
                    <asp:ListItem>Кашель с кровью</asp:ListItem>
                    <asp:ListItem>Кровь в стуле</asp:ListItem>
                    <asp:ListItem>Лихорадка</asp:ListItem>
                    <asp:ListItem>Моча с кровью</asp:ListItem>
                    <asp:ListItem>Мышечные судороги</asp:ListItem>
                    <asp:ListItem>Носовое кровотечение</asp:ListItem>
                    <asp:ListItem>Обмороки</asp:ListItem>
                    <asp:ListItem>Общая слабость</asp:ListItem>
                    <asp:ListItem>Одышка</asp:ListItem>
                    <asp:ListItem>Онемение пальцев рук</asp:ListItem>
                    <asp:ListItem>Онемение пальцев ног</asp:ListItem>
                    <asp:ListItem>Острая задержка мочи</asp:ListItem>
                    <asp:ListItem>Отеки ступней, лодыжек</asp:ListItem>
                    <asp:ListItem>Отеки нижних конечностей</asp:ListItem>
                    <asp:ListItem>Отечность губ, век, лица, ушных раковин</asp:ListItem>
                    <asp:ListItem>Отрыжка</asp:ListItem>
                    <asp:ListItem>Перебои в работе сердца</asp:ListItem>
                    <asp:ListItem>Приступы сердцебиения</asp:ListItem>
                    <asp:ListItem>Перемежающая хромота</asp:ListItem>
                    <asp:ListItem>Потемнение в глазах</asp:ListItem>
                    <asp:ListItem>Потеря голоса</asp:ListItem>
                    <asp:ListItem>Потеря слуха</asp:ListItem>
                    <asp:ListItem>Прилив крови (Ощущение жара)</asp:ListItem>
                    <asp:ListItem>Рвота</asp:ListItem>
                    <asp:ListItem>Синяки</asp:ListItem>
                    <asp:ListItem>Слезоточивость</asp:ListItem>
                    <asp:ListItem>Снижение (отсутствие) аппетита</asp:ListItem>
                    <asp:ListItem>Снижение настроения</asp:ListItem>
                    <asp:ListItem>Снижение остроты зрения</asp:ListItem>
                    <asp:ListItem>Сухость кожи</asp:ListItem>
                    <asp:ListItem>Сыпь</asp:ListItem>
                    <asp:ListItem>Сыпь с температурой</asp:ListItem>
                    <asp:ListItem>Тошнота, рвота</asp:ListItem>
                    <asp:ListItem>Тремор</asp:ListItem>
                    <asp:ListItem>Удушье</asp:ListItem>
                    <asp:ListItem>Усталость</asp:ListItem>
                    <asp:ListItem>Ухудшение памяти</asp:ListItem>
                    <asp:ListItem>Учащенное дыхание</asp:ListItem>
                    <asp:ListItem>Учащенное мочеиспускание</asp:ListItem>
                    <asp:ListItem>Хрипы в груди</asp:ListItem>
                    <asp:ListItem>Чихание</asp:ListItem>
                    <asp:ListItem>Шум (звон) в ушах (Снижение слуха)</asp:ListItem>
                    <asp:ListItem>Эмоциональная лабильность (Слабость)</asp:ListItem>
                </asp:CheckBoxList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:InfoMedMainDBConnectionString %>" 
                    SelectCommand="SELECT [Name] FROM [Symptoms] ORDER BY [Name]">
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <asp:Button ID="Button_complaints" runat="server" Text="Сохранить" 
        onclick="Button_complaints_Click" />
</asp:Content>
