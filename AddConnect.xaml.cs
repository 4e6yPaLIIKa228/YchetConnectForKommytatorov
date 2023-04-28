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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data.Common;
using Ychet.Connection;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;

namespace Ychet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AddConnect : Window
    {
        public AddConnect()
        {
            InitializeComponent();
            LoadCorpus();
            LoadTypeProvod();
        }

        public void LoadCorpus()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
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

        public void LoadTypeProvod()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    string querty = $@"Select * from TypeProvod";
                    SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable("TypeProvod");
                    SDA.Fill(dt);
                    CmbTypeProvod.ItemsSource = dt.DefaultView;
                    CmbTypeProvod.DisplayMemberPath = "NameType";
                    CmbTypeProvod.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddDBInfoConnect()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    bool result1 = int.TryParse(CmbCorpus.SelectedValue.ToString(), out int IDCorpus);
                    bool result2 = int.TryParse(CmbBox.SelectedValue.ToString(), out int IDBox);
                    string querty = $@"Insert into InfoConnection ('NumberKabela','NumberPatch','NumberPort','NumberExit','NumberMesta','IDBox','IDCorpus','LVLCorpus') 
                    values ('{NumberKabela.Text}','{NumberPatch.Text}','{NumberPort.Text}','{NumberExit.Text}','{NumberPozetku.Text}','{IDBox}','{IDCorpus}','{CmbLVLCorpus.Text}')";
                    SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Выполнено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {
            //AddDBInfoConnect();

            //OtchetToPrint otchet = new OtchetToPrint();
            //this.Close();
            //otchet.ShowDialog();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmbCorpus_DropDownClosed(object sender, EventArgs e)
        {
            String combtext = CmbCorpus.Text;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                   
                    string querty1 = $@"Select DISTINCT LVLCorpus from BoxInfo 
                                        JOIN House ON BoxInfo.IDCorpus = House.ID
                                        where House.Corpus = '{combtext}'
                                        Order by BoxInfo.LVLCorpus";
                    SQLiteCommand cmd1 = new SQLiteCommand(querty1, connection);
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("BoxInfo");
                    SDA1.Fill(dt1);
                    CmbLVLCorpus.ItemsSource = dt1.DefaultView;
                    CmbLVLCorpus.DisplayMemberPath = "LVLCorpus";
                    CmbLVLCorpus.SelectedValuePath = "LVLCorpus";                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (combtext == "")
            {
                CmbLVLCorpus.SelectedIndex = -1;
                CmbLVLCorpus.IsEnabled = false;
                CmbBox.SelectedIndex = -1;
                CmbBox.IsEnabled = false;
            }
            else
            {
                CmbLVLCorpus.IsEnabled = true;
            }
        }

        private void CmbLVLCorpus_DropDownClosed(object sender, EventArgs e)
        {
            String combtext1 = CmbCorpus.Text;
            String combtext2 = CmbLVLCorpus.Text;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    
                    string querty1 = $@"Select BoxInfo.ID,NumberBox from BoxInfo 
                                        JOIN House ON BoxInfo.IDCorpus = House.ID
                                        where House.Corpus = '{combtext1}' and LVLCorpus = '{combtext2}'
                                        Order by BoxInfo.LVLCorpus";
                    SQLiteCommand cmd1 = new SQLiteCommand(querty1, connection);
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("BoxInfo");
                    SDA1.Fill(dt1);
                    CmbBox.ItemsSource = dt1.DefaultView;
                    CmbBox.DisplayMemberPath = "NumberBox";
                    CmbBox.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (combtext2 == "")
            {                
                CmbBox.SelectedIndex = -1;
                CmbBox.IsEnabled = false;
            }
            else
            {
                CmbBox.IsEnabled = true;
            }
        }

        public void AddConnection()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    String CombBox = CmbBox.Text;
                    string querty = $@"Select count(NumberKabela) from InfoConnection
                                    join BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                    where  NumberKabela = '{NumberKabela.Text}' and BoxInfo.NumberBox = {CombBox} ";
                    SQLiteCommand cmd = new SQLiteCommand(querty, connection);  
                    int ProvekraNumberProvodaInBox = Convert.ToInt32(cmd.ExecuteScalar());
                    if (ProvekraNumberProvodaInBox == 0)
                    {
                        querty = $@"Select count() from InfoConnection
                                    join BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                    where  NumberPatch = '{NumberPatch.Text}' and NumberPort = '{NumberPort.Text}' and  BoxInfo.NumberBox = {CombBox} ";
                        cmd = new SQLiteCommand(querty, connection);
                        int ProvekraPatchPortInBox = Convert.ToInt32(cmd.ExecuteScalar());
                        if (ProvekraPatchPortInBox == 0)
                        {
                            querty = $@"Select count() from InfoConnection
                                    join BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                    where  NumberExit = '{NumberExit.Text}' and NumberMesta = '{NumberPozetku.Text}' and  BoxInfo.NumberBox = {CombBox} ";
                            cmd = new SQLiteCommand(querty, connection);
                            int ProvekraExitMestaInBox = Convert.ToInt32(cmd.ExecuteScalar());
                            if (ProvekraExitMestaInBox ==0)
                            {
                                //bool result1 = int.TryParse(CmbCorpus.SelectedValue.ToString(), out int IDCorpus);
                                bool result3 = int.TryParse(CmbTypeProvod.SelectedValue.ToString(), out int IDTypeProvod);
                                bool result2 = int.TryParse(CmbBox.SelectedValue.ToString(), out int IDBox);
                              
                                querty = $@"Insert into InfoConnection ('NumberKabela','NumberPatch','NumberPort','NumberExit','NumberMesta','IDBox','IDProvod') 
                                values ('{NumberKabela.Text}','{NumberPatch.Text}','{NumberPort.Text}','{NumberExit.Text}','{NumberPozetku.Text}','{IDBox}','{IDTypeProvod}')";
                                cmd = new SQLiteCommand(querty, connection);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Выполнено!");
                            }
                            else
                            {
                                MessageBox.Show($@"Помещение {NumberExit.Text} и местом {NumberPozetku.Text} уже используется в шкафу {CombBox}. Напишите другое помещение или место.");
                            }
                        }
                        else
                        {
                            MessageBox.Show($@"Патч-панель {NumberPatch.Text} с портом {NumberPort.Text} уже используется в шкафу {CombBox}. Напишите другой номер патч-панели или порта.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($@"Данный номер кабеля {NumberKabela.Text}, уже используется в шкафу {CombBox}. Напишите другой номер кабеля.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void TextValidationTextBox(object sender, KeyEventArgs e) //Невозможность ввести пробелы
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
        private void NumberValidationTextBox(object sender,TextCompositionEventArgs e) //Ввод тольок цифр
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBykvlValidationTextBox(object sender, TextCompositionEventArgs e) //Ввод тольок цифр
        {
            Regex regex = new Regex("[^a-zA-Zа-яА-Я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddConnection();
        }      
    }
}
