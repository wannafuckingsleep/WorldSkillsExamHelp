using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data.Common;

namespace lab3
{
    /// <summary>
    /// Логика взаимодействия для ShowClubs.xaml
    /// </summary>
    public partial class ShowClubs : Window
    {
        public class Club
        {
            public string ID { get; set; }
            public string date { get; set; }
            public string name { get; set; }
            public string city { get; set; }
        }

        public void Update_Grid()
        {
            var db = new DBClass();
            db.Connect();
            var rows = db.Command("SELECT * FROM Clubs");
            var reader = rows.ExecuteReader();
            List<Club> clubs = new List<Club>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var name = reader.GetString(reader.GetOrdinal("Name"));
                    var city = reader.GetString(reader.GetOrdinal("City"));
                    var id = Convert.ToString(reader.GetValue(reader.GetOrdinal("ID")));
                    var date = reader.GetString(reader.GetOrdinal("Create_Date"));
                    clubs.Add(
                        new Club { city = city, name = name, ID=id, date=date }
                        );

                }

            }
            db.Disconnect();
            grid.ItemsSource = clubs;
        }
        public ShowClubs()
        {
            InitializeComponent();
            Update_Grid();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Insert_Info(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "" && City.Text != "" && Date.Text != "") 
            {
                var db = new DBClass();
                db.Connect();
                
                try
                {
                    var sql = $"INSERT INTO Clubs (Name, Create_Date, City) VALUES ('{Name.Text}', '{Date.Text}', '{City.Text}')";
                    var cmd = db.Command(sql);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Вставка данных произошла успешно!");
                    Update_Grid();
                }
                catch (Exception err) {
                    MessageBox.Show("Произошла ошибка.\nПроверьте данные для вставки" + e);
                }
                finally
                {
                    db.Disconnect();
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля ввода");
            }
        }

        private void Update_Info(object sender, RoutedEventArgs e)
        {
            if (ID.Text != "0" && ID.Text != "")
            {
                if (Name.Text != "" && City.Text != "" && Date.Text != "")
                {
                    var db = new DBClass();
                    db.Connect();

                    try
                    {
                        var sql = $"UPDATE Clubs SET Name = '{Name.Text}', Create_Date = '{Date.Text}', City = '{City.Text}' WHERE ID = {ID.Text}";
                        var cmd = db.Command(sql);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Вставка данных произошла успешно!");
                        Update_Grid();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Произошла ошибка.\nПроверьте данные для вставки" + e);
                    }
                    finally
                    {
                        db.Disconnect();
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля ввода");
                }
            }
            else
                MessageBox.Show("Вставьте корректное значение ID");
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (ID.Text != "0" && ID.Text != "")
            {
                var db = new DBClass();
                db.Connect();

                try
                {
                    var sql = $"DELETE FROM Clubs WHERE ID = {ID.Text}";
                    var cmd = db.Command(sql);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Удалено!");
                    Update_Grid();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Произошла ошибка.\nПроверьте ID" + e);
                }
                finally
                {
                    db.Disconnect();
                }
            }
            else
                MessageBox.Show("Вставьте корректное значение ID");
        }

        private void Get_Info(object sender, RoutedEventArgs e)
        {
            var db = new DBClass();
            
            if (ID.Text != "" && ID.Text != "0") {
                db.Connect();
                var rows = db.Command($"SELECT * FROM Clubs WHERE ID = {ID.Text}");
                var reader = rows.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(reader.GetOrdinal("Name"));
                        var city = reader.GetString(reader.GetOrdinal("City"));
                        var id = Convert.ToString(reader.GetValue(reader.GetOrdinal("ID")));
                        var date = reader.GetString(reader.GetOrdinal("Create_Date"));
                        Name.Text = name;
                        City.Text = city;
                        ID.Text = id;
                        Date.Text = date;
                    }

                }
                else
                    MessageBox.Show("Нет Поля с таким ID");
                db.Disconnect();
            }
            else
            {
                MessageBox.Show("Введите корректное значение ID");
            }
        }

        private void Grid_Info(object sender, RoutedEventArgs e)
        {
            Update_Grid();
        }
    }
}
