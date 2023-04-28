using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для AddComponents.xaml
    /// </summary>
    public partial class AddComponents : Window
    {
        public AddComponents()
        {
            InitializeComponent();
        }

        private void CmbComponents_DropDownClosed(object sender, EventArgs e)
        {
            String CombBox = CmbComponents.Text;
            if (CombBox != "Шкаф")
            {
                GrTextBox.Visibility = Visibility.Collapsed;
                GrCmbBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                GrTextBox.Visibility = Visibility.Visible;
                GrCmbBox.Visibility = Visibility.Visible;
                LoadCmbCorpus();
            }
        }        
        public void LoadCmbCorpus()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    string querty = $@"Select * from House";
                    SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable("House");
                    SDA.Fill(dt);
                    CmbCorpus.ItemsSource = dt.DefaultView;
                    CmbCorpus.DisplayMemberPath = "Corpus";
                    CmbCorpus.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddComponent()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    String CombBox = CmbComponents.Text;
                    if (CombBox != "Шкаф")
                    {
                        if (String.IsNullOrEmpty(CmbComponents.Text) || String.IsNullOrEmpty(TxtComponent.Text))
                        {
                            MessageBox.Show("Заполните данные.");
                        }
                        else
                        {
                            if (CombBox == "Тип провода")
                            {
                                String qwerty = $@"Select COUNT() from TypeProvod where TypeProvod.NameType = '{TxtComponent.Text}' ";
                                SQLiteCommand  cmd  = new SQLiteCommand(qwerty,connection);
                                int ProverkaComponent = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponent == 0)
                                {
                                    qwerty = $@"Insert Into TypeProvod ('NameType') Values ('{TxtComponent.Text}') ";
                                    cmd = new SQLiteCommand(qwerty, connection);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show($@"Компонет: {TxtComponent.Text} добавлен в базу {CombBox}.");
                                }
                                else
                                {
                                    MessageBox.Show($@"Компонет: {TxtComponent.Text} уже используется в базе.");
                                }

                            }
                            else if (CombBox == "Корпус")
                            {
                                String qwerty = $@"Select COUNT() from House where House.Corpus = '{TxtComponent.Text}' ";
                                SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                                int ProverkaComponent = Convert.ToInt32(cmd.ExecuteScalar());
                                if (ProverkaComponent == 0)
                                {
                                    qwerty = $@"Insert Into House ('Corpus') Values ('{TxtComponent.Text}') ";
                                    cmd = new SQLiteCommand(qwerty, connection);
                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show($@"Компонет: {TxtComponent.Text} добавлен в базу {CombBox}.");
                                }
                                else
                                {
                                    MessageBox.Show($@"Компонет: {TxtComponent.Text} уже используется в базе.");
                                }
                            }
                        }
                    }
                    else if (CombBox == "Шкаф")
                    {
                        if (String.IsNullOrEmpty(CmbComponents.Text) || String.IsNullOrEmpty(TxtComponent.Text) || String.IsNullOrEmpty(CmbCorpus.Text) || String.IsNullOrEmpty(CmbLVL.Text))
                        {
                            MessageBox.Show("Заполните данные.");
                        }
                        else
                        {
                            string qwerty = $@"Select COUNT() from BoxInfo where BoxInfo.NumberBox = '{TxtComponent.Text}' ";
                            SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                            int ProverkaComponent = Convert.ToInt32(cmd.ExecuteScalar());
                            if (ProverkaComponent == 0)
                            {
                                String CombLVL = CmbLVL.Text;
                                bool result3 = int.TryParse(CmbCorpus.SelectedValue.ToString(), out int IDTypeCorpus);
                                qwerty = $@"Insert Into BoxInfo ('NumberBox','LVLCorpus','IDCorpus') Values ('{TxtComponent.Text}','{CombLVL}','{IDTypeCorpus}') ";
                                cmd = new SQLiteCommand(qwerty, connection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show($@"Компонет: {TxtComponent.Text} добавлен в базу {CombBox}.");
                            }
                            else
                            {
                                MessageBox.Show($@"Компонет: {TxtComponent.Text} уже используется в базе.");
                            }
                        }
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void BtnAddComponent_Click(object sender, RoutedEventArgs e)
        {
            AddComponent();
        }
        private void BntBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
            {
                connection.Open();
                string qwerty = $@"SELECT count() from InfoConnection 
                                GROUP by NumberPatch ";
                SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                int ProverkaComponent = Convert.ToInt32(cmd.ExecuteScalar());
                MessageBox.Show(ProverkaComponent.ToString());
            }
        }
    }
}
