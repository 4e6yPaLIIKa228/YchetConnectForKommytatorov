using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EdditConnection.xaml
    /// </summary>
    public partial class EddConnection : Window
    {
        string IDInfoconnection, OldNumberKabel,OLdDopInfo;
        public EddConnection(DataRowView drv)
        {
            InitializeComponent();
            LoadTypeProvod();
            LoadBox();
            IDInfoconnection = drv["IDInfoConnection"].ToString();
            NumberKabela.Text = drv["NumberKabela"].ToString();
            OldNumberKabel = drv["NumberKabela"].ToString();
            NumberPatch.Text = drv["NumberPatch"].ToString();
            NumberPort.Text = drv["NumberPort"].ToString();
            OLdDopInfo = drv["NumberMesta"].ToString();
            NumberExit.Text = drv["NumberExit"].ToString();
            NumberPozetku.Text = drv["NumberMesta"].ToString();
            CmbTypeProvod.Text = drv["NameType"].ToString();
            CmbBox.Text = drv["NumberBox"].ToString();
            if (NumberExit.Text == "APLINK")
            {
                ChbxLunk.IsChecked = true;
                LoadCbmDopInfoCorpus();
                ExpndrLinkInfo.IsExpanded = false;
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
        public void LoadBox()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {

                    string querty1 = $@"Select BoxInfo.ID,NumberBox from BoxInfo
                                      Order by BoxInfo.NumberBox";
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
        }

        public void LoadCbmDopInfoCorpus()
        {

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {

                    string querty1 = $@"Select ID,Corpus from House
                                      Order by House.Corpus";
                    SQLiteCommand cmd1 = new SQLiteCommand(querty1, connection);
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("House");
                    SDA1.Fill(dt1);
                    CbmDopInfoCorpus.ItemsSource = dt1.DefaultView;
                    CbmDopInfoCorpus.DisplayMemberPath = "Corpus";
                    CbmDopInfoCorpus.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EddConnect()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    String textbox = CmbBox.Text;
                    bool result2 = int.TryParse(CmbTypeProvod.SelectedValue.ToString(), out int IDBox);
                    if (OldNumberKabel == NumberKabela.Text)
                    {
                        if (ChbxLunk.IsChecked == true)
                        {
                            if (String.IsNullOrEmpty(CbmDopInfoCorpus.Text) || String.IsNullOrEmpty(CbmDopInfoCorpus.Text) || String.IsNullOrEmpty(CbmDopInfoCorpus.Text) || String.IsNullOrEmpty(CbmDopInfoCorpus.Text))
                            {
                                MessageBox.Show("Заполните данные в Доп. Информации");
                            }
                            else
                            {
                                String textcorpus = CbmDopInfoCorpus.Text;
                                String textboxdop = CbmDopInfoBox.Text;
                                String textpathpanel = CbmDopInfoPathPanel.Text;
                                String textport = CbmDopInfoPathPort.Text;
                                string qwerty = $@"Update InfoConnection Set NumberKabela = '{NumberKabela.Text}',NumberExit = '{NumberExit.Text}', NumberMesta =  'Корпус: {textcorpus} Шкаф: {textboxdop} Патч-панель: {textpathpanel} Порт: {textport}' where InfoConnection.ID = '{IDInfoconnection}'";
                                SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                                cmd.ExecuteScalar();
                                MessageBox.Show("Данные обновлены!");
                            }

                        }
                        else
                        {
                            string qwerty = $@"Update InfoConnection Set NumberKabela = '{NumberKabela.Text}',NumberExit = '{NumberExit.Text}', NumberMesta = '{NumberPozetku.Text}' where InfoConnection.ID = '{IDInfoconnection}'";
                            SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                            cmd.ExecuteScalar();
                            MessageBox.Show("Данные обновлены!");
                        }
                        //string qwerty1 = $@"Update InfoConnection Set NumberKabela = {NumberKabela.Text},NumberExit = '{NumberExit.Text}', NumberMesta = '{NumberPozetku.Text}' where InfoConnection.ID = '{IDInfoconnection}'";
                        //SQLiteCommand cmd1 = new SQLiteCommand(qwerty1, connection);
                        //cmd1.ExecuteScalar();
                        //MessageBox.Show("Данные обновлены!");

                    }
                    else
                    {                       
                        string qwerty = $@"SELECT count() FROM InfoConnection
                                        JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                        WHERE InfoConnection.NumberKabela = '{NumberKabela.Text}' and InfoConnection.NumberPatch = '{NumberPatch.Text}' and BoxInfo.NumberBox = '{textbox}'";
                        SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                        int ProverkaPovtoraNumberKabeka = Convert.ToInt32(cmd.ExecuteScalar());
                        if (ProverkaPovtoraNumberKabeka == 0)
                        {
                            if (ChbxLunk.IsChecked == true)
                            {
                                if (String.IsNullOrEmpty(CbmDopInfoCorpus.Text) || String.IsNullOrEmpty(CbmDopInfoCorpus.Text) || String.IsNullOrEmpty(CbmDopInfoCorpus.Text) || String.IsNullOrEmpty(CbmDopInfoCorpus.Text))
                                {
                                    MessageBox.Show("Заполните данные в Доп. Информации");
                                }
                                else
                                {
                                    String textcorpus = CbmDopInfoCorpus.Text;
                                    String textboxdop = CbmDopInfoBox.Text;
                                    String textpathpanel = CbmDopInfoPathPanel.Text;
                                    String textport = CbmDopInfoPathPort.Text;
                                    qwerty = $@"Update InfoConnection Set NumberKabela = '{NumberKabela.Text}',NumberExit = '{NumberExit.Text}', NumberMesta =  'Корпус: {textcorpus} Шкаф: {textboxdop} Патч-панель: {textpathpanel} Порт: {textport}' where InfoConnection.ID = '{IDInfoconnection}'";
                                    SQLiteCommand cmd1 = new SQLiteCommand(qwerty, connection);
                                    cmd1.ExecuteScalar();
                                    MessageBox.Show("Данные обновлены!");
                                }
                               
                            }
                            else
                            {
                                qwerty = $@"Update InfoConnection Set NumberKabela = '{NumberKabela.Text}',NumberExit = '{NumberExit.Text}', NumberMesta = '{NumberPozetku.Text}' where InfoConnection.ID = '{IDInfoconnection}'";
                                SQLiteCommand cmd1 = new SQLiteCommand(qwerty, connection);
                                cmd1.ExecuteScalar();
                                MessageBox.Show("Данные обновлены!");
                            }
                           
                        }
                        else
                        {
                            MessageBox.Show($@"Номере кабеля {NumberKabela.Text} в патч-панели {NumberPatch.Text} в шкафу {textbox} уже используется в базе данных!");
                            NumberKabela.Text = OldNumberKabel;
                        }
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
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) //Ввод только цифр
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBykvlValidationTextBox(object sender, TextCompositionEventArgs e) //Ввод только цифр
        {
            Regex regex = new Regex("[^a-zA-Zа-яА-Я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnEdd_Click(object sender, RoutedEventArgs e)
        {
            EddConnect();
        }
        public void LunkChecked()
        {
            try
            {
                TxtBlExit.Text = "Доп. Инфо";
                TxtBlMesta.Text = "Доп. Инфо";
                NumberExit.Text = "APLINK";
                NumberPozetku.Text = OLdDopInfo;
                NumberExit.IsEnabled = false;
                NumberPozetku.IsEnabled = false;
                ExpndrLinkInfo.IsEnabled = true;
                ExpndrLinkInfo.IsExpanded = true;
                LoadCbmDopInfoCorpus();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LunkUnChecked()
        {
            try
            {
                TxtBlExit.Text = "Номер помещения";
                TxtBlMesta.Text = "Номер места";
                NumberExit.Text = null;
                NumberPozetku.Text = null;
                NumberExit.IsEnabled = true;
                NumberPozetku.IsEnabled = true;
                ExpndrLinkInfo.IsEnabled = false;
                ExpndrLinkInfo.IsExpanded = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ChbxLunk_Unchecked(object sender, RoutedEventArgs e)
        {
            LunkUnChecked();
        }
        private void ChbxLunk_Checked(object sender, RoutedEventArgs e)
        {
            LunkChecked();
        } 
        private void ExpndrLinkInfo_Expanded(object sender, RoutedEventArgs e)
        {
            if (ExpndrLinkInfo.IsExpanded == true)
            {
                this.Height = Height + 150;
            }            
        }  
        private void ExpndrLinkInfo_Collapsed(object sender, RoutedEventArgs e)
        {
            if (ExpndrLinkInfo.IsExpanded == false)
            {
                this.Height = Height - 150;
            }
        }
        public void LoadCbmDopInfoBox()
        {
            try
            {
                String textcorpus = CbmDopInfoCorpus.Text;
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    string querty1 = $@"SELECT BoxInfo.ID as IDBox, BoxInfo.NumberBox from BoxInfo
                                        LEFT JOIN House on BoxInfo.IDCorpus = House.ID
                                        WHERE House.Corpus = '{textcorpus}'
                                        Order by BoxInfo.NumberBox + 0 ASC";
                    SQLiteCommand cmd1 = new SQLiteCommand(querty1, connection);
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("BoxInfo");
                    SDA1.Fill(dt1);
                    CbmDopInfoBox.ItemsSource = dt1.DefaultView;
                    CbmDopInfoBox.DisplayMemberPath = "NumberBox";
                    CbmDopInfoBox.SelectedValuePath = "IDBox";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public void LoadCbmDopInfoPathPanel()
        {
            try
            {
                String textbox = CbmDopInfoBox.Text;
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    string querty1 = $@"SELECT InfoConnection.ID,InfoConnection.NumberPatch from InfoConnection
                                        LEFT JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                        LEFT JOIN House on BoxInfo.IDCorpus = House.ID
                                        WHERE BoxInfo.NumberBox = '{textbox}'
                                        GROUP by InfoConnection.NumberPatch + 0";
                    SQLiteCommand cmd1 = new SQLiteCommand(querty1, connection);
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("InfoConnection");
                    SDA1.Fill(dt1);
                    CbmDopInfoPathPanel.ItemsSource = dt1.DefaultView;
                    CbmDopInfoPathPanel.DisplayMemberPath = "NumberPatch ";
                    //CbmDopInfoPathPanel.SelectedValuePath = "InfoConnection.ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadCbmDopInfoPathPort()
        {
            try
            {
                String textpathpanel = CbmDopInfoPathPanel.Text;
                String textbox = CbmDopInfoBox.Text;
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    string querty1 = $@"SELECT InfoConnection.NumberPort from InfoConnection
                                        LEFT JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                        LEFT JOIN House on BoxInfo.IDCorpus = House.ID
                                        WHERE BoxInfo.NumberBox = '{textbox}' and InfoConnection.NumberPatch = '{textpathpanel}' and  InfoConnection.NumberKabela is NULL
                                        GROUP by InfoConnection.NumberPort +0";
                    SQLiteCommand cmd1 = new SQLiteCommand(querty1, connection);
                    SQLiteDataAdapter SDA1 = new SQLiteDataAdapter(cmd1);
                    DataTable dt1 = new DataTable("InfoConnection");
                    SDA1.Fill(dt1);
                    CbmDopInfoPathPort.ItemsSource = dt1.DefaultView;
                    CbmDopInfoPathPort.DisplayMemberPath = "NumberPort";
                    //CbmDopInfoBox.SelectedValuePath = "IDBox";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CbmDopInfoBox_DropDownClosed(object sender, EventArgs e)
        {
            String textcorpus = CbmDopInfoBox.Text;
            if (textcorpus != "" || textcorpus != null)
            {
                LoadCbmDopInfoPathPanel();
            }
        }
        private void CbmDopInfoPathPanel_DropDownClosed(object sender, EventArgs e)
        {
            String textcorpus = CbmDopInfoPathPanel.Text;
            if (textcorpus != "" && textcorpus != null)
            {
                LoadCbmDopInfoPathPort();
            }
        }

        private void CbmDopInfoCorpus_DropDownClosed(object sender, EventArgs e)
        {
            String textcorpus = CbmDopInfoCorpus.Text;
            if (textcorpus != "" || textcorpus != null)
            {
                LoadCbmDopInfoBox();
            }
        }
    }
}
