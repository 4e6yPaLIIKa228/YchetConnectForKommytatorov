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
    /// Логика взаимодействия для DellComponents.xaml
    /// </summary>
    public partial class DellComponents : Window
    {
        public DellComponents()
        {
            InitializeComponent();
        }
        private void CmbComponents_DropDownClosed(object sender, EventArgs e)
        {
            String CombBox = CmbComponents.Text;
            if (CombBox != null)
            {
                LoadCmb();
                CmbNameComponents.IsEnabled = true;
            }
            else
            {
                CmbNameComponents.IsEnabled = false;
            }
        }

        public void LoadCmb()
        {
            String CombBox = CmbComponents.Text;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {

                    if (CombBox == "Тип провода")
                    {
                        string querty = $@"Select * from TypeProvod";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("TypeProvod");
                        SDA.Fill(dt);
                        CmbNameComponents.ItemsSource = dt.DefaultView;
                        CmbNameComponents.DisplayMemberPath = "NameType";
                        CmbNameComponents.SelectedValuePath = "ID";
                    }
                    else if (CombBox == "Корпус")
                    {
                        string querty = $@"Select * from House";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("House");
                        SDA.Fill(dt);
                        CmbNameComponents.ItemsSource = dt.DefaultView;
                        CmbNameComponents.DisplayMemberPath = "Corpus";
                        CmbNameComponents.SelectedValuePath = "ID";

                    }
                    else if (CombBox == "Шкаф")
                    {
                        string querty = $@"Select ID,NumberBox from BoxInfo";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        DataTable dt = new DataTable("BoxInfo");
                        SDA.Fill(dt);
                        CmbNameComponents.ItemsSource = dt.DefaultView;
                        CmbNameComponents.DisplayMemberPath = "NumberBox";
                        CmbNameComponents.SelectedValuePath = "ID";

                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void DellComponent()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    String CombBox = CmbComponents.Text;
                    if (String.IsNullOrEmpty(CmbComponents.Text) || String.IsNullOrEmpty(CmbNameComponents.Text))
                    {
                        MessageBox.Show("Выберите критерий и название критерия.");
                    }
                    else
                    {
                        connection.Open();
                        if (CombBox == "Тип провода")
                        {
                            bool result1 = int.TryParse(CmbNameComponents.SelectedValue.ToString(), out int IDTypeProvod);
                            String qwerty = $@"Select COUNT() from InfoConnection where InfoConnection.IDProvod = '{IDTypeProvod}' ";
                            SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                            int ProverkaComponent = Convert.ToInt32(cmd.ExecuteScalar());
                            String CombBoxText = CmbNameComponents.Text;
                            if (ProverkaComponent == 0)
                            {                                
                                qwerty = $@"Delete from TypeProvod where TypeProvod.ID = {IDTypeProvod}";
                                cmd = new SQLiteCommand(qwerty, connection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show($@"Тип провода {CombBoxText} удален из базы.");
                                CmbNameComponents.SelectedIndex = -1;
                                LoadCmb();
                            }
                            else
                            {
                                
                                MessageBox.Show($@"Тип провода {CombBoxText} используется в подключениях!");
                            }
                        }
                        else if (CombBox == "Корпус")
                        {
                            bool result1 = int.TryParse(CmbNameComponents.SelectedValue.ToString(), out int IDCorpus);
                            String qwerty = $@"Select COUNT() from BoxInfo where BoxInfo.IDCorpus = '{IDCorpus}' ";
                            SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                            int ProverkaComponent = Convert.ToInt32(cmd.ExecuteScalar());
                            String CombBoxText = CmbNameComponents.Text;
                            if (ProverkaComponent == 0)
                            {                               
                                qwerty = $@"Delete from House where House.ID = {IDCorpus}";
                                cmd = new SQLiteCommand(qwerty, connection);
                                cmd.ExecuteNonQuery();
                                cmd.ExecuteNonQuery();
                                MessageBox.Show($@"Корпус {CombBoxText} удален из базы.");
                                CmbNameComponents.SelectedIndex = -1;
                                LoadCmb();
                            }
                            else
                            {
                               
                                MessageBox.Show($@"В корпусе {CombBoxText} есть используемые шкафы!");
                            }
                        }
                        else if (CombBox == "Шкаф")
                        {
                            bool result1 = int.TryParse(CmbNameComponents.SelectedValue.ToString(), out int IDBox);
                            String qwerty = $@"Select COUNT() from InfoConnection where InfoConnection.IDBox = '{IDBox}' ";
                            SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                            int ProverkaComponent = Convert.ToInt32(cmd.ExecuteScalar());
                            String CombBoxText = CmbNameComponents.Text;
                            if (ProverkaComponent == 0)
                            {
                                qwerty = $@"Delete from BoxInfo where BoxInfo.ID = {IDBox}";
                                cmd = new SQLiteCommand(qwerty, connection);
                                cmd.ExecuteNonQuery();
                                cmd.ExecuteNonQuery();
                                MessageBox.Show($@"Шкаф с номером {CombBoxText} удален из базы.");
                                CmbNameComponents.SelectedIndex = -1;
                                LoadCmb();
                            }
                            else
                            {                               
                                MessageBox.Show($@"Шкаф с номером {CombBoxText} имеет подключения!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BntBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddComponent_Click(object sender, RoutedEventArgs e)
        {
            DellComponent();
        }        
    }
}
