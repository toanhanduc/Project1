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
using System.Data;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataProcessing
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class thietlapHeSo : Page
    {
        public thietlapHeSo()
        {
            InitializeComponent();
        }

        private void browseFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xls";

            openfile.Filter = "(.xls)|*.xls|(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
                txtFilePath.Text = openfile.FileName;
        }

        private void startSearch(object sender, RoutedEventArgs e)
        {
            Excel.Application excel;
            excel = new Excel.Application();
            Excel.Workbook WB = excel.Workbooks.Open(txtFilePath.Text);
            WB = excel.ActiveWorkbook;
            Excel.Worksheet WS;
            WS = WB.ActiveSheet;
            int start = Environment.TickCount;
            for (int i = 2; i <= WS.UsedRange.Columns.Count; i++)
            {

                int temp = 0;
                Excel.Range a = WS.get_Range((Excel.Range)WS.Cells[i][2], (Excel.Range)WS.Cells[i][WS.UsedRange.Rows.Count]);
                object arr = a.Value;
                foreach (object s in (Array)arr)
                {
                    string tmp = (string)s;
                    if (tmp != "1")
                        continue;
                    else
                        temp += int.Parse(tmp);
                }

            }
            MessageBox.Show("Đọc hết: " + ((double)(Environment.TickCount - start) / 1000).ToString() + "ms");
        }
    }
}
