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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ychet.Connection;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace Ychet
{
    /// <summary>
    /// Логика взаимодействия для AddNewConnectionBox.xaml
    /// </summary>
    public partial class AddNewConnectionBox : Window
    {
        public AddNewConnectionBox()
        {
            InitializeComponent();
            LoadBox();
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
                    CmbNumberBox.ItemsSource = dt1.DefaultView;
                    CmbNumberBox.DisplayMemberPath = "NumberBox";
                    CmbNumberBox.SelectedValuePath = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CheckUnit()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    int summ = 0, exitfor = 1;
                    String textCmbNumberBox = CmbNumberBox.Text;
                    string qwerty = $@"SELECT BoxInfo.Unit from BoxInfo 
                                        where BoxInfo.NumberBox = {textCmbNumberBox}";
                    SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                    int KollUnit = Convert.ToInt32(cmd.ExecuteScalar());
                    for (int i = 1;exitfor != 0;i++)
                    {
                        qwerty = $@"SELECT count() from InfoConnection 
                                       JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                        WHERE NumberPatch = '{i}' and BoxInfo.NumberBox = '{textCmbNumberBox}'";
                        cmd = new SQLiteCommand(qwerty, connection);
                        int KollConnect = Convert.ToInt32(cmd.ExecuteScalar());
                        if (KollConnect == 24)
                        {
                            summ += 1;
                        }
                        else if (KollConnect == 48) 
                        {
                            summ += 2;
                        } 
                        else if (KollConnect == 0 && i == 45)
                        {
                            exitfor = 0;
                        }                      
                    }
                    if (summ >= KollUnit) //42>=42 нет мест
                    {
                        MessageBox.Show($@"В шкафу {textCmbNumberBox} нет свободных мест под патч-панель.");
                    }
                    else if (summ < KollUnit) //1...41 < 42 
                    {
                        if (summ == KollUnit-1 && TxBxKollPortov.Text == "48") //41 == 41 48 не влезает
                        {
                            MessageBox.Show($@"Патч-панель на 48 не может быть добавленна, так как в шкафу {textCmbNumberBox} нет места. Есть возможность добавить на 24 патч-панель.");
                            TxBxKollPortov.Text = "24";
                            MessageBox.Show(Convert.ToString(summ));
                        }
                        else if (summ <= KollUnit-2 && TxBxKollPortov.Text == "48")//1...40 может 48 
                        {
                            MessageBox.Show(Convert.ToString(summ));
                            AddMoreConnection();
                        }
                        else if (summ <= KollUnit-1 && TxBxKollPortov.Text == "24")//1..41 может 24
                        {
                            MessageBox.Show(Convert.ToString(summ));
                            AddMoreConnection();
                        }                        
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void AddMoreConnection()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    String textCmbNumberBox = CmbNumberBox.Text;
                    bool result2 = int.TryParse(CmbNumberBox.SelectedValue.ToString(), out int IDBox);
                    int NumberPatchPanel = Convert.ToInt32(TxBxNamePatchPanel.Text);
                    int Kollconnect = Convert.ToInt32(TxBxKollPortov.Text);
                    for (int i = 1; i <= Kollconnect; i++)
                    {
                        string qwerty = $@"SELECT count() FROM InfoConnection
                                        JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                        WHERE InfoConnection.NumberPatch = '{NumberPatchPanel}' and InfoConnection.NumberPort = '{i}' and BoxInfo.NumberBox = '{textCmbNumberBox}'";
                        SQLiteCommand cmd = new SQLiteCommand(qwerty, connection);
                        int ProvekraPovtoraPorta = Convert.ToInt32(cmd.ExecuteScalar());
                        if (ProvekraPovtoraPorta == 0)
                        {
                            qwerty = $@"Insert into InfoConnection ('NumberKabela','NumberPatch','NumberPort','NumberExit','NumberMesta','IDBox','IDProvod') 
                                values (null,'{NumberPatchPanel}','{i}',null,null,{IDBox},'3')";
                            cmd = new SQLiteCommand(qwerty, connection);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Выполнено");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CheckTextTest()
        {
            try
            {
                if (String.IsNullOrEmpty(CmbNumberBox.Text) || String.IsNullOrEmpty(TxBxNamePatchPanel.Text)  || String.IsNullOrEmpty(TxBxKollPortov.Text))
                {
                    MessageBox.Show("Заполните данные.");
                }
                else
                {
                    CheckUnit();
                    //AddMoreConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CheckTextTest();
        }

        public void TextValidationTextBox(object sender, KeyEventArgs e) //Невозможность ввести пробелы
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) //Ввод тольок цифр
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }       

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Check()
        {
            //SELECT count() from InfoConnection WHERE NumberPatch = '1' and InfoConnection.IDBox = '3'
            //UPDATE sqlite_sequence set seq = 0 WHERE name = 'InfoConnection'
        }
    }
}
