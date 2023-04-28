using Microsoft.Office.Interop.Excel;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ychet.Connection;
using static System.Net.Mime.MediaTypeNames;
using DataTable = System.Data.DataTable;
using Excel = Microsoft.Office.Interop.Excel;


namespace Ychet
{
    /// <summary>
    /// Логика взаимодействия для DataBaseConnection.xaml
    /// </summary>
    public partial class DataBaseConnection : System.Windows.Window
    {
        public DataBaseConnection()
        {
            InitializeComponent();
            LoadDB();
        }

        public void LoadDB()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    //               string querty = $@"SELECT InfoConnection.ID as IDInfoConnection,*  from InfoConnection
                    //               JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                    //               JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
                    //JOIN House on BoxInfo.IDCorpus = House.ID
                    //ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC
                    //               ";
                    string querty = $@"SELECT * from InfoConnectionView";
                    SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                    DataTable DT = new DataTable("InfoConnection");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    DataGridDB.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SearchInfo()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    String combtext = CmbSearch.Text;
                    if(combtext == "Номер шкафа")
                    {
                        string querty = $@"SELECT InfoConnection.ID as IDInfoConnection, *  from InfoConnection
                        JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                        JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					    JOIN House on BoxInfo.IDCorpus = House.ID					    
                        where BoxInfo.NumberBox like '{TBSearch.Text}%'
                        ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        DataTable DT = new DataTable("InfoConnection");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridDB.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                        String combtext2 = CmbSearchDop.Text;
                        if (combtext2 == "Начало")
                        {
                            if (TBSearchDop1.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberPatch like '{TBSearchDop1.Text}'
                                ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(DT);
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                            if(TBSearchDop2.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberPort like '{TBSearchDop2.Text}'
                                ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(DT);
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                            if (TBSearchDop1.Text != "" && TBSearchDop2.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberPatch like '{TBSearchDop1.Text}'
                                and InfoConnection.NumberPort like '{TBSearchDop2.Text}'
                                ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(DT);
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (combtext2 == "Конец")
                        {
                            if (TBSearchDop1.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberExit like '{TBSearchDop1.Text}'";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(DT);
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                            if (TBSearchDop2.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberMesta like '{TBSearchDop2.Text}'";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(DT);
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                            if (TBSearchDop1.Text != "" && TBSearchDop2.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberExit like '{TBSearchDop1.Text}'
                                and InfoConnection.NumberMesta like '{TBSearchDop2.Text}' ";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(DT);
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (combtext2 == "№ Кабеля")
                        {
                            if (TBSearchDop1.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberKabela like '{TBSearchDop1.Text}'";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(DT);
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }                            
                        }
                    }
                    if (combtext == "Корпус")
                    {
                        string querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                        JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                        JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					    JOIN House on BoxInfo.IDCorpus = House.ID                               
                        where House.Corpus = '{TBSearch.Text}'
                        ORDER by BoxInfo.LVLCorpus";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        DataTable DT = new DataTable("InfoConnection");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridDB.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                        String combtext2 = CmbSearchDop.Text;                        
                    }
                    if (combtext == "Этаж")
                    {
                        string querty = $@"SELECT InfoConnection.ID as IDInfoConnection,* from InfoConnection
                        JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                        JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					    JOIN House on BoxInfo.IDCorpus = House.ID
                        where BoxInfo.LVLCorpus = '{TBSearch.Text}'
                        Order by BoxInfo.NumberBox ";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        DataTable DT = new DataTable("InfoConnection");
                        SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(DT);
                        DataGridDB.ItemsSource = DT.DefaultView;
                        cmd.ExecuteNonQuery();
                    }
                    if (TBSearch.Text == "")
                    {
                        LoadDB();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchInfo();
        }

        private void CmbSearch_DropDownClosed(object sender, EventArgs e)
        {
            String combtext = CmbSearch.Text;
            if (combtext == "Номер шкафа")
            {
                
                CmbSearchDop.Visibility = Visibility.Visible;
                CmbSearchDop.SelectedIndex = -1;
                TbDopSearch.Visibility = Visibility.Visible;
                TBSearch.Text = null;
                
            }
            else if (combtext == "Корпус")
            {
                //TbPort.Visibility = Visibility.Collapsed;
                //TbConnect.Visibility = Visibility.Collapsed;
                //Tbkabel.Visibility = Visibility.Collapsed;
                //TbLVL.Visibility = Visibility.Visible;
                CmbSearchDop.Visibility = Visibility.Collapsed;
                CmbSearchDop.SelectedIndex = -1;
                TBDop1.Visibility = Visibility.Collapsed;
                TBSearchDop1.Visibility = Visibility.Collapsed;
                TBDop2.Visibility = Visibility.Collapsed;
                TBSearchDop2.Visibility = Visibility.Collapsed;
                TbDopSearch.Visibility = Visibility.Collapsed;
                TBSearch.Text = null;
            }
            else if (combtext == "Этаж")
            {
                CmbSearchDop.Visibility = Visibility.Collapsed;
                CmbSearchDop.SelectedIndex = -1;
                TBDop1.Visibility = Visibility.Collapsed;
                TBSearchDop1.Visibility = Visibility.Collapsed;
                TBDop2.Visibility = Visibility.Collapsed;
                TBSearchDop2.Visibility = Visibility.Collapsed;
                TbDopSearch.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbSearchDop_DropDownClosed(object sender, EventArgs e)
        {
            String combtext1 = CmbSearchDop.Text;
            if (combtext1 == "Начало")
            {
                TBDop1.Text = "Патч-панель ";
                TBDop1.Visibility = Visibility.Visible;
                TBSearchDop1.Visibility = Visibility.Visible;
                TBDop2.Text = "Порт";
                TBDop2.Visibility = Visibility.Visible;
                TBSearchDop2.Visibility = Visibility.Visible;
                
            }
            if (combtext1 == "Конец")
            {
                TBDop1.Text = "Помещение";
                TBDop1.Visibility = Visibility.Visible;
                TBSearchDop1.Visibility = Visibility.Visible;
                TBDop2.Text = "Место";
                TBDop2.Visibility = Visibility.Visible;
                TBSearchDop2.Visibility = Visibility.Visible;
            }
            if (combtext1 == "№ Кабеля")
            {
                TBDop1.Text = "№ Кабеля";
                TBDop1.Visibility = Visibility.Visible;
                TBSearchDop1.Visibility = Visibility.Visible;
                TBDop2.Text = "";
                TBDop2.Visibility = Visibility.Collapsed;
                TBSearchDop2.Visibility = Visibility.Collapsed;
            }
        }

        public void ExcelEx()
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];
            sheet1.Name = "Подключения";
            Excel.Range myRang1 = sheet1.get_Range("A1", "G1");
            myRang1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang1.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang1.Merge(Type.Missing);
            myRang1.Font.Name = "Times New Roman";
            myRang1.Font.Bold = true;
            myRang1.Cells.Font.Size = 16;
            for (int j = 0; j < DataGridDB.Columns.Count; j++) //Столбцы
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[2, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 20;
                //sheet1.Columns[j + 1].ColumnFamily = "Times New Roman";
                sheet1.Columns[j + 1].NumberFormat = "@";
                myRange.Value2 = DataGridDB.Columns[j].Header;
            }
            for (int i = 0; i < DataGridDB.Columns.Count; i++) //Строки
            {
                for (int j = 0; j < DataGridDB.Items.Count; j++)
                {
                    TextBlock b = DataGridDB.Columns[i].GetCellContent(DataGridDB.Items[j]) as TextBlock;
                    if (b == null)
                        continue;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 3, i + 1];
                    myRange.Value2 = b.Text;
                    myRange.Font.Name = "Times New Roman";
                    myRange.Font.Bold = true;
                    myRange.Cells.Font.Size = 16;
                    
                    
                }
            }
        }
        public void ExcporToExcel()
        {
            try           
            {   
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                for (int j = 0; j < DataGridDB.Columns.Count; j++) //Столбцы
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;
                    sheet1.Columns[j + 1].ColumnWidth = 20;
                    //sheet1.Columns[j + 1].ColumnFamily = "Times New Roman";
                    sheet1.Columns[j + 1].NumberFormat = "@";
                    myRange.Value2 = DataGridDB.Columns[j].Header;
                }
                for (int i = 0; i < DataGridDB.Columns.Count; i++) //Строки
                {
                    for (int j = 0; j < DataGridDB.Items.Count; j++) 
                    {
                        TextBlock b = DataGridDB.Columns[i].GetCellContent(DataGridDB.Items[j]) as TextBlock;
                        if (b == null)
                            continue;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2,i + 1];
                        myRange.Value2 = b.Text;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
        public void ExcelTest1()
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
            excel.Visible = true;
            excel.Interactive = false;
            excel.ScreenUpdating = false;
            excel.UserControl = false;
            excel.DisplayAlerts = false;                
            Excel.Range myRang1 = sheet1.get_Range("B1", "C1");
            myRang1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang1.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang1.Merge(Type.Missing);
            myRang1.Font.Name = "Times New Roman";
            myRang1.Font.Bold = true;
            myRang1.Cells.Font.Size = 16;
            myRang1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang1.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang1.Font.Name = "Times New Roman";
            myRang1.Borders.LineStyle = XlLineStyle.xlContinuous;
            sheet1.Range["B1"].Value = "НАЧАЛО";
            Excel.Range myRang2 = sheet1.get_Range("D1", "E1");
            myRang2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang2.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang2.Merge(Type.Missing);
            myRang2.Font.Name = "Times New Roman";
            myRang2.Font.Bold = true;
            myRang2.Cells.Font.Size = 16;
            myRang2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang2.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
            myRang2.Font.Name = "Times New Roman";
            sheet1.Range["D1"].Value = "КОНЕЦ";
            sheet1.get_Range("A2","I2").Borders.LineStyle = XlLineStyle.xlContinuous;
            for (int j = 0; j < DataGridDB.Columns.Count; j++) //Столбцы
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[2, j + 1];
                sheet1.Columns[j + 1].NumberFormat = "@";
                myRange.Value2 = DataGridDB.Columns[j].Header;
                myRange.Font.Name = "Times New Roman";
                myRange.Cells.Font.Size = 16;

            }
            for (int i = 0; i < DataGridDB.Columns.Count; i++) //Строки
            {
                for (int j = 0; j < DataGridDB.Items.Count; j++)
                {
                    TextBlock b = DataGridDB.Columns[i].GetCellContent(DataGridDB.Items[j]) as TextBlock;
                    if (b == null)
                        continue;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 3, i + 1];                    
                    myRange.Value2 = b.Text;
                    myRange.Font.Name = "Times New Roman";
                    myRange.Cells.Font.Size = 14;
                    myRange = sheet1.UsedRange;
                    myRange.Borders.LineStyle = XlLineStyle.xlContinuous;


                }
            }
            
            sheet1.Columns.AutoFit();
            sheet1.Rows.AutoFit();
            excel.Interactive = true;
            excel.ScreenUpdating = true;
            excel.UserControl = true;
        }

        public void ExcelForSQLLite()
        {

            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                excel.Visible = true;
                excel.Interactive = false;
                excel.ScreenUpdating = false;
                excel.UserControl = false;
                excel.DisplayAlerts = false;
                Excel.Range myRang1 = sheet1.get_Range("B1", "C1");
                myRang1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang1.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang1.Merge(Type.Missing);
                myRang1.Font.Name = "Times New Roman";
                myRang1.Font.Bold = true;
                myRang1.Cells.Font.Size = 16;
                myRang1.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang1.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang1.Font.Name = "Times New Roman";
                myRang1.Borders.LineStyle = XlLineStyle.xlContinuous;
                sheet1.Range["B1"].Value = "НАЧАЛО";
                Excel.Range myRang2 = sheet1.get_Range("D1", "E1");
                myRang2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang2.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang2.Merge(Type.Missing);
                myRang2.Font.Name = "Times New Roman";
                myRang2.Font.Bold = true;
                myRang2.Cells.Font.Size = 16;
                myRang2.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang2.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                myRang2.Font.Name = "Times New Roman";
                sheet1.Range["D1"].Value = "КОНЕЦ";
                sheet1.get_Range("A2", "I2").Borders.LineStyle = XlLineStyle.xlContinuous;
                for (int j = 0; j < DataGridDB.Columns.Count; j++) //Столбцы
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[2, j + 1];
                    sheet1.Columns[j + 1].NumberFormat = "@";
                    myRange.Value2 = DataGridDB.Columns[j].Header;
                    myRange.Font.Name = "Times New Roman";
                    myRange.Cells.Font.Size = 16;

                }
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    connection.Open();
                    string querty = $@"SELECT InfoConnection.NumberKabela,InfoConnection.NumberPatch,InfoConnection.NumberPort,InfoConnection.NumberExit,
		                InfoConnection.NumberMesta,TypeProvod.NameType, BoxInfo.NumberBox,House.Corpus,BoxInfo.LVLCorpus from InfoConnection
                        JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                        JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					    JOIN House on BoxInfo.IDCorpus = House.ID
                        where BoxInfo.NumberBox like '{TBSearch.Text}%'
					    ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC
                        ";
                    SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                    DataTable DT = new DataTable("InfoConnection");
                    DataSet ds = new DataSet();
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(ds);
                    SDA.Fill(DT);
                    DT = ds.Tables[0];
                    DataTable dt = DT;
                    cmd.ExecuteNonQuery();
                    int collInd = 0;
                    int rowInd = 0;
                    string data = "";
                    String combtext = CmbSearch.Text;
                    if (combtext == "Номер шкафа")
                    {
                        querty = $@"SELECT InfoConnection.NumberKabela,InfoConnection.NumberPatch,InfoConnection.NumberPort,InfoConnection.NumberExit,
		                InfoConnection.NumberMesta,TypeProvod.NameType, BoxInfo.NumberBox,House.Corpus,BoxInfo.LVLCorpus from InfoConnection
                        JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                        JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					    JOIN House on BoxInfo.IDCorpus = House.ID
                        where BoxInfo.NumberBox like '{TBSearch.Text}%'
					    ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC
                        ";
                        cmd = new SQLiteCommand(querty, connection);
                        DT = new DataTable("InfoConnection");
                        ds = new DataSet();
                        SDA = new SQLiteDataAdapter(cmd);
                        SDA.Fill(ds);
                        SDA.Fill(DT);
                        DT = ds.Tables[0];
                        dt = DT;
                        cmd.ExecuteNonQuery();
                        String combtext2 = CmbSearchDop.Text;
                        if (combtext2 == "Начало")
                        {
                            if (TBSearchDop1.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.NumberKabela,InfoConnection.NumberPatch,InfoConnection.NumberPort,InfoConnection.NumberExit,
		                        InfoConnection.NumberMesta,TypeProvod.NameType, BoxInfo.NumberBox,House.Corpus,BoxInfo.LVLCorpus from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberPatch like '{TBSearchDop1.Text}'
                                ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                ds = new DataSet();
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(ds);
                                SDA.Fill(DT);
                                DT = ds.Tables[0];
                                dt = DT;
                                cmd.ExecuteNonQuery();
                            }
                            if (TBSearchDop2.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.NumberKabela,InfoConnection.NumberPatch,InfoConnection.NumberPort,InfoConnection.NumberExit,
		                        InfoConnection.NumberMesta,TypeProvod.NameType, BoxInfo.NumberBox,House.Corpus,BoxInfo.LVLCorpus from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberPort like '{TBSearchDop2.Text}'
                                ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                ds = new DataSet();
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(ds);
                                SDA.Fill(DT);
                                DT = ds.Tables[0];
                                dt = DT;
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                            if (TBSearchDop1.Text != "" && TBSearchDop2.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.NumberKabela,InfoConnection.NumberPatch,InfoConnection.NumberPort,InfoConnection.NumberExit,
		                        InfoConnection.NumberMesta,TypeProvod.NameType, BoxInfo.NumberBox,House.Corpus,BoxInfo.LVLCorpus from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberPatch like '{TBSearchDop1.Text}'
                                and InfoConnection.NumberPort like '{TBSearchDop2.Text}'
                                ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                ds = new DataSet();
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(ds);
                                SDA.Fill(DT);
                                DT = ds.Tables[0];
                                dt = DT;
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (combtext2 == "№ Кабеля")
                        {
                            if (TBSearchDop1.Text != "")
                            {
                                querty = $@"SELECT InfoConnection.NumberKabela,InfoConnection.NumberPatch,InfoConnection.NumberPort,InfoConnection.NumberExit,
		                        InfoConnection.NumberMesta,TypeProvod.NameType, BoxInfo.NumberBox,House.Corpus,BoxInfo.LVLCorpus from InfoConnection
                                JOIN BoxInfo on InfoConnection.IDBox = BoxInfo.ID
                                JOIN TypeProvod on InfoConnection.IDProvod = TypeProvod.ID
					            JOIN House on BoxInfo.IDCorpus = House.ID
                                where BoxInfo.NumberBox like '{TBSearch.Text}%' and InfoConnection.NumberKabela like '{TBSearchDop1.Text}'
                                ORDER by BoxInfo.NumberBox + 0 ASC,InfoConnection.NumberPatch + 0 ASC ,InfoConnection.NumberPort + 0 ASC";
                                cmd = new SQLiteCommand(querty, connection);
                                DT = new DataTable("InfoConnection");
                                ds = new DataSet();
                                SDA = new SQLiteDataAdapter(cmd);
                                SDA.Fill(ds);
                                SDA.Fill(DT);
                                DT = ds.Tables[0];
                                dt = DT;
                                DataGridDB.ItemsSource = DT.DefaultView;
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    for (rowInd = 0; rowInd < dt.Rows.Count; rowInd++)
                    {
                        for (collInd = 0; collInd < dt.Columns.Count; collInd++)
                        {
                            data = dt.Rows[rowInd].ItemArray[collInd].ToString();
                            sheet1.Cells[rowInd + 3, collInd + 1] = data;
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[rowInd + 3, collInd + 1];
                            myRange.Font.Name = "Times New Roman";
                            myRange.Cells.Font.Size = 14;
                            myRange = sheet1.UsedRange;
                            myRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                        }
                    }
                    sheet1.Columns.AutoFit();
                    sheet1.Rows.AutoFit();
                    excel.Interactive = true;
                    excel.ScreenUpdating = true;
                    excel.UserControl = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnExcportExcel_Click(object sender, RoutedEventArgs e)
        {
            //ExcporToExcel();
            //ExcelEx();
            // ExcelTest1();
            //Test1();
            ExcelForSQLLite();
        }

        private void BtnAddConnect_Click(object sender, RoutedEventArgs e)
        {
            AddConnect NewWind = new AddConnect();
            NewWind.Owner = this;
            bool? result = NewWind.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB();
                    break;
            }
        }

        //private void ScrlVrBDLineUP(object sender, RoutedEventArgs e)
        //{
        //    ((IScrollInfo)ScrlVrBD).LineUp();
        //}
        //private void ScrlVrBDLineDown(object sender, RoutedEventArgs e)
        //{
        //    ((IScrollInfo)ScrlVrBD).LineDown();
        //}
        private void ScrlVrBD_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }


        public void DellConnect()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DBconnection.myConn))
                {
                    if (DataGridDB.SelectedIndex != -1 )
                    {
                        connection.Open();
                        string ID;
                        DataRowView drv = DataGridDB.SelectedItem as DataRowView;
                        ID = drv["IDInfoConnection"].ToString();
                        string querty = $@"Delete from InfoConnection Where InfoConnection.ID = '{ID}'";
                        SQLiteCommand cmd = new SQLiteCommand(querty, connection);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Подключение удалено.");
                        LoadDB();
                    }
                    else
                    {
                        MessageBox.Show("Выберите только одно подключение.");
                    }
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnDell_Click(object sender, RoutedEventArgs e)
        {
            DellConnect();
        }

        private void BtnAddComponent_Click(object sender, RoutedEventArgs e)
        {
            AddComponents addComponents = new AddComponents();
            addComponents.Owner = this;
            bool? result = addComponents.ShowDialog();
            switch(result)
            {
                default:
                    LoadDB(); 
                break;
            }
        }

        private void BtnDellComponent_Click(object sender, RoutedEventArgs e)
        {
            DellComponents dellComponents = new DellComponents();
            dellComponents.Owner = this;
            bool? result = dellComponents.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB();
                    break;
            }
        }

        private void BtnAddMoreConn_Click(object sender, RoutedEventArgs e)
        {
            AddNewConnectionBox addnewcon = new AddNewConnectionBox();
            addnewcon.Owner = this;
            bool? result = addnewcon.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB();
                    break;
            }
        }

        public void EddConnect()
        {
            try
            {
                if (DataGridDB.SelectedIndex != -1)
                {
                    EddConnection eddConnect = new EddConnection((DataRowView)DataGridDB.SelectedItem);
                    eddConnect.Owner = this;
                    bool? result = eddConnect.ShowDialog();
                    switch (result)
                    {
                        default:
                            LoadDB();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Выберите строку для изменения данных");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void BtnEddconnect_Click(object sender, RoutedEventArgs e)
        {
            EddConnect();
        }

        private void DataGridDB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EddConnect();
        }

        private void DataGridDB_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //e.Row.Header = (e.Row.GetIndex()+1).ToString();
           // if (e.Row.GetIndex() > 100) e.Row.Visibility = Visibility.Hidden;
        }

        private void BtnEddComponent_Click(object sender, RoutedEventArgs e)
        {
            EddComponent eddConnect = new EddComponent();
            eddConnect.Owner = this;
            bool? result = eddConnect.ShowDialog();
            switch (result)
            {
                default:
                    LoadDB();
                break;
            }
        }
    }
}
