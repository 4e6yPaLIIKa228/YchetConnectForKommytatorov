using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using Ychet.Connection;

namespace Ychet
{
    /// <summary>
    /// Логика взаимодействия для EddComponent.xaml
    /// </summary>
    public partial class EddComponent : Window
    {
        public EddComponent()
        {
            InitializeComponent();
        }
        private void CmbComponents_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    String txtcmbSelect = CmbComponents.Text;
                    if (txtcmbSelect == "Тип провода")
                    {
                        string querty = $@"Select * from TypeProvod";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("TypeProvod");
                        SDA.Fill(dt);
                        CmbComponent.ItemsSource = dt.DefaultView;
                        CmbComponent.DisplayMemberPath = "Nametype";
                        CmbComponent.SelectedValuePath = "ID";
                        CmbComponent.IsEnabled = true;
                    }
                    else if (txtcmbSelect == "Номер шкафа")
                    {
                        string querty = $@"Select ID,NumberBox from BoxInfo";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("BoxInfo");
                        SDA.Fill(dt);
                        CmbComponent.ItemsSource = dt.DefaultView;
                        CmbComponent.DisplayMemberPath = "NumberBox";
                        CmbComponent.SelectedValuePath = "ID";
                        CmbComponent.IsEnabled = true;
                    }
                    else if (txtcmbSelect == "Этаж")
                    {
                        string querty = $@"Select ID,LVLCorpus from BoxInfo";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("BoxInfo");
                        SDA.Fill(dt);
                        CmbComponent.ItemsSource = dt.DefaultView;
                        CmbComponent.DisplayMemberPath = "LVLCorpus";
                        CmbComponent.SelectedValuePath = "ID";
                        CmbComponent.IsEnabled = true;
                    }
                    else if (txtcmbSelect == "Unit")
                    {
                        string querty = $@"Select ID,Unit from BoxInfo";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("BoxInfo");
                        SDA.Fill(dt);
                        CmbComponent.ItemsSource = dt.DefaultView;
                        CmbComponent.DisplayMemberPath = "Unit";
                        CmbComponent.SelectedValuePath = "ID";
                        CmbComponent.IsEnabled = true;
                    }
                    else if (txtcmbSelect == "Корпус")
                    {
                        string querty = $@"Select * from House";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("House");
                        SDA.Fill(dt);
                        CmbComponent.ItemsSource = dt.DefaultView;
                        CmbComponent.DisplayMemberPath = "Corpus";
                        CmbComponent.SelectedValuePath = "ID";
                        CmbComponent.IsEnabled = true;
                    }
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
