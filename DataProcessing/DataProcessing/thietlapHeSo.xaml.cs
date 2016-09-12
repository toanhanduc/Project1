using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using System.Collections.Generic;
using System.Linq;

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
        Boolean group2 = false, group3 = false, group4 = false, group5 = false;
        /// <summary>
        /// Mở đường dẫn đến file xls, xlsx và điền đường dẫn vào textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Bắt đầu tìm kiếm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startSearch(object sender, RoutedEventArgs e)
        {

            string startdatetime = startd.SelectedDate == null ? "" : startd.SelectedDate.Value.ToString("dd/M/yyyy");
            string enddatetime = endd.SelectedDate == null ? "" : endd.SelectedDate.Value.ToString("dd/M/yyyy");
            if (txtFilePath.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn đường dẫn!");
            }
            else if (startdatetime.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn ngày bắt đầu");
            }
            else if (enddatetime.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn ngày kết thúc");
            }
            else if (!group2 && !group3 && !group4 && !group5)
            {
                MessageBox.Show("Bạn chưa chọn nhóm màu");
            }
            else
            {
                int ngaybatdau = 0, ngayketthuc = 0;
                Excel.Application excel;
                excel = new Excel.Application();
                Excel.Workbook WB = excel.Workbooks.Open(txtFilePath.Text);
                WB = excel.ActiveWorkbook;
                Excel.Worksheet WS;
                WS = WB.ActiveSheet;
                int start = Environment.TickCount;
                String[] color = new string[WS.UsedRange.Columns.Count - 1];
                int[] value = new int[WS.UsedRange.Columns.Count - 1];
                int[][] zeroOne = new int[WS.UsedRange.Columns.Count - 1][];

                Excel.Range colornumber = WS.get_Range((Excel.Range)WS.Cells[2][1], (Excel.Range)WS.Cells[WS.UsedRange.Columns.Count][1]);
                object mamau = colornumber.Value;
                //Lấy mã màu vào mảng
                int colorcount = 0;
                foreach (object objcolor in (Array)mamau)
                {
                    string colorname = (string)objcolor;
                    color[colorcount] = colorname;
                    colorcount++;
                }
                MessageBox.Show("Lưu hết: " + ((double)(Environment.TickCount - start) / 1000).ToString() + "s");
                //Khoanh vùng ngày bắt đầu và kết thúc
                for (int row = 2; row <= WS.UsedRange.Rows.Count; row++)
                {

                    string cell = (WS.Cells[1][row] as Excel.Range).Value == null ? "" : (WS.Cells[1][row] as Excel.Range).Value.ToString("dd/M/yyyy");
                    if (cell == startdatetime)
                        ngaybatdau = row;

                    else if (cell == enddatetime)
                    {
                        ngayketthuc = row;
                        break;
                    }

                }
                MessageBox.Show("Khoanh vùng hết: " + ((double)(Environment.TickCount - start) / 1000).ToString() + "s");

                //Tạo mảng 2 chiều zeroOne
                for (int i = 0; i < WS.UsedRange.Columns.Count - 1; i++)
                {
                    zeroOne[i] = new int[ngayketthuc - ngaybatdau + 1];
                }

                //Tính tổng tất cả các cột theo thời gian đã định
                for (int i = 2; i <= WS.UsedRange.Columns.Count; i++)
                {

                    int temp = 0;
                    Excel.Range b = WS.get_Range((Excel.Range)WS.Cells[i][ngaybatdau], (Excel.Range)WS.Cells[i][ngayketthuc]);
                    object arr = b.Value;
                    int j = 0;
                    foreach (object s in (Array)arr)
                    {

                        string tmp = s == null ? "" : "1";
                        if (tmp != "1")
                        {
                            zeroOne[i - 2][j] = 0;
                            j++;
                            continue;
                        }

                        else
                        {
                            zeroOne[i - 2][j] = 1;
                            j++;
                            temp += int.Parse(tmp);

                        }

                    }
                    value[i - 2] = temp;

                }
                MessageBox.Show("Đọc hết: " + ((double)(Environment.TickCount - start) / 1000).ToString() + "s");

                if (group2)
                {
                    int timestart = Environment.TickCount;
                    processGroup2(color, value, zeroOne, WS.UsedRange.Columns.Count, WS.UsedRange.Rows.Count, ngaybatdau, ngayketthuc);
                    MessageBox.Show("Mau 2 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                }
                else if (group3)
                {
                    int timestart = Environment.TickCount;
                    processGroup3(color, value, zeroOne, WS.UsedRange.Columns.Count, WS.UsedRange.Rows.Count, ngaybatdau, ngayketthuc);
                    MessageBox.Show("Mau 3 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                }
                else if (group4)
                {
                    int timestart = Environment.TickCount;
                    processGroup4(color, value, zeroOne, WS.UsedRange.Columns.Count, WS.UsedRange.Rows.Count, ngaybatdau, ngayketthuc);
                    MessageBox.Show("Mau 4 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                }
                else if (group5)
                {
                    int timestart = Environment.TickCount;
                    processGroup5(color, value, zeroOne, WS.UsedRange.Columns.Count, WS.UsedRange.Rows.Count, ngaybatdau, ngayketthuc);
                    MessageBox.Show("Mau 5 het: " + ((double)(Environment.TickCount - timestart) / 1000).ToString() + "s");
                }
                else
                {
                    MessageBox.Show("Chưa xong");
                }

            }
        }


        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            group2 = true;
        }

        private void RadioButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            group2 = false;
        }

        private void RadioButton3_Checked(object sender, RoutedEventArgs e)
        {
            group3 = true;
        }

        private void RadioButton3_Unchecked(object sender, RoutedEventArgs e)
        {
            group3 = false;
        }

        private void RadioButton4_Checked(object sender, RoutedEventArgs e)
        {
            group4 = true;
        }

        private void RadioButton4_Unchecked(object sender, RoutedEventArgs e)
        {
            group4 = false;
        }

        private void RadioButton5_Checked(object sender, RoutedEventArgs e)
        {
            group5 = true;
        }

        private void RadioButton5_Unchecked(object sender, RoutedEventArgs e)
        {
            group5 = false;
        }

        /// <summary>
        /// Hàm xử lý nhóm 2 màu
        /// </summary>
        /// <param name="color"></param>
        /// <param name="value"></param>
        /// <param name="zeroOne"></param>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void processGroup2(String[] color, int[] value, int[][] zeroOne, int col, int row, int start, int end)
        {
            int currentColumnValue; // giá trị cột làm mốc
            int biggestValue = 0; // giá trị lớn nhất khi gộp 2 cột
            int biggestCosts = 0; // trọng số lớn nhất


            for (int i = 0; i < col - 2; i++) // chọn từng cột mốc trong 9 colors ( do không chọn đến cột cuối cùng làm mốc )
            {

                List<int> checkList = new List<int>(); // list so sánh theo ngày không bán được
                currentColumnValue = value[i]; // giá trị cột làm mốc

                for (int j = 0; j < end - start + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList.Add(j);
                    }
                }

                for (int q = i + 1; q < col - 1; q++) // duyệt các màu tiếp theo, có 10 màu
                {
                    //                    if (value[q] < biggestValue - currentColumnValue) // giá trị cột hiện tại + giá trị cột mốc < giá trị sau khi ghép lớn nhất -> dừng lại
                    //                    {
                    //                            Console.WriteLine("Dung lai duoc roi");
                    //                            Console.WriteLine(q);

                    //            break;
                    //                    }
                    List<int> checkListTemp = new List<int>(checkList); // list so sánh theo ngày không bán được
                    int currentCosts = 0; // Trọng số cột đang xét
                                          //                    List<int> removeList = new List<int>();

                    foreach (int temp in checkListTemp) // duyệt từng ngày của màu để đánh trọng số
                    {
                        if (zeroOne[q][temp] == 1)
                        {
                            currentCosts++;
                            //                            removeList.Add(temp);
                        }

                    }

                    if (currentCosts + currentColumnValue > biggestValue)
                    {
                        //                            Console.WriteLine("> bigvalue roi");
                        biggestValue = currentCosts + currentColumnValue;
                        Console.WriteLine("CLEAR");
                        Console.WriteLine(color[i] + "-" + color[q]);
                        Console.WriteLine(biggestValue);
                        biggestCosts = currentCosts;
                        Console.WriteLine();
                    }
                    else if (currentCosts + currentColumnValue == biggestValue)
                    {
                        //                            Console.WriteLine("giong nhau roi");
                        Console.WriteLine(color[i] + "-" + color[q]);
                        Console.WriteLine(biggestValue);
                        Console.WriteLine();

                    }
                    /*         else
                             {
                                 Console.WriteLine("next");
                             }
                             */
                }
                //                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Hàm xử lý nhóm 3 màu
        /// </summary>
        /// <param name="color"></param>
        /// <param name="value"></param>
        /// <param name="zeroOne"></param>
        private void processGroup3(String[] color, int[] value, int[][] zeroOne, int col, int row, int start, int end)
        {
            int currentValue1; // giá trị ở vòng 1
            int currentValue2; // giá trị cột mốc thứ 2
            int biggestValue = 0;

            for (int i = 0; i < col - 3; i++) // chọn từng cột làm mốc vòng 1 trong 8 màu ( để lại 2 màu đế ghép )
            {
                //                if (value[i] < biggestValue / 3)
                //                {
                //                    Console.WriteLine("Xong game !");
                //                    break;
                //                }

                //                Console.WriteLine("Start round 1");
                //                Console.WriteLine();
                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];

                for (int j = 0; j < end - start + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList1.Add(j);
                    }
                }

                for (int q = i + 1; q < col - 2; q++) // chọn từng cột trong vòng 2
                {
                    // THÊM ĐIỀU KIỆN DỪNG !
                    //                    if (value[i] + value[q] + value[q + 1] < biggestValue)
                    //                    {
                    //                        Console.WriteLine("chon mau khac lam moc di");
                    //                        break;
                    //                    }

                    List<int> checkList2 = new List<int>(checkList1);

                    int currentCosts = 0; // trọng số cột hiện tại

                    foreach (int temp in checkList1)
                    {
                        if (zeroOne[q][temp] == 1)
                        {
                            currentCosts++;
                            checkList2.Remove(temp);
                        }
                    }
                    currentValue2 = currentValue1 + currentCosts;
                    //biggestValue = currentValue2;
                    if (!checkList2.Any()) // full 1
                    {
                        Console.WriteLine("2 cột đã Full 1 roi !");
                        for (int k = q + 1; k < col - 2; k++)
                        {
                            Console.WriteLine(color[i] + "-" + color[q] + "-" + color[k]);
                        }
                    }
                    else
                    {
                        for (int k = q + 1; k < col - 2; k++)
                        {
                            if (value[k] + value[q] + value[i] < biggestValue)
                            {
                                Console.WriteLine("Chon mau khac lam cot 2 di");
                            }

                            currentCosts = 0; // trọng số cột hiện tại

                            foreach (int temp in checkList2)
                            {
                                if (zeroOne[k][temp] == 1)
                                {
                                    currentCosts++;
                                }
                            }

                            if (currentCosts + currentValue2 > biggestValue)
                            {
                                Console.WriteLine();
                                Console.WriteLine("> biggest value roi");
                                biggestValue = currentValue2 + currentCosts;
                                Console.WriteLine(biggestValue);
                                Console.WriteLine("CLEAR");
                                Console.WriteLine(color[i] + "-" + color[q] + "-" + color[k]);
                            }
                            else if (currentCosts + currentValue2 == biggestValue)
                            {
                                Console.WriteLine("giong nhau roi");
                                Console.WriteLine(currentValue2 + currentCosts);
                                Console.WriteLine(color[i] + "-" + color[q] + "-" + color[k]);
                            }

                        }
                    }


                }

            }
        }

        private void processGroup4(String[] color, int[] value, int[][] zeroOne, int col, int row, int start, int end)
        {
            int currentValue1; // giá trị ở vòng 1
            int currentValue2; // giá trị ở vòng 2
            int currentValue3; // giá trị ở vòng 3
            int biggestValue = 0;

            for (int i = 0; i < col - 4; i++) // để lại 3 cột để ghép màu
            {
                // THÊM ĐIỀU KIỆN ĐỂ KẾT THÚC CHƯƠNG TRÌNH

                //                if (biggestValue % 4 == 0 && value[i] < biggestValue / 4)
                //                {
                //                    Console.WriteLine("Xong game");
                //                    break;
                //                }
                //               else if (biggestValue % 4 != 0 && value[i] == biggestValue / 4)
                //               {
                //                    Console.WriteLine("Xong game");
                //                    break;
                //                }

                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];

                for (int j = 0; j < end - start + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList1.Add(j);
                    }
                }

                if (!checkList1.Any()) // full 1
                {
                    Console.WriteLine("ngay mốc 1 đã full 1 rồi");
                    if (biggestValue < currentValue1)
                    {
                        Console.WriteLine("CLEAR");
                        biggestValue = currentValue1;
                    }
                    for (int j = i + 1; j < col - 3; j++)
                    {
                        for (int q = j + 1; q < col - 2; q++)
                        {
                            for (int k = q + 1; q < col - 1; q++)
                            {
                                Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                            }
                        }
                    }
                    continue;
                }

                for (int j = i + 1; j < col - 3; j++) // chọn từng cột ở vòng 2, để lại 2 cột để ghép với 2 cột đã chọn
                {
                    // THÊM ĐIỀU KIỆN DỪNG
                    //                    if ((biggestValue - currentValue1) % 3 == 0 && value[i] < (biggestValue - currentValue1) / 3)
                    //                    {
                    //                        Console.WriteLine("xong mau nay roi chon mau khac di !");
                    //                        break;
                    //                    }
                    //                    if ((biggestValue - currentValue1) % 3 != 0 && value[i] == (biggestValue - currentValue1) / 3)
                    //                    {
                    //                        Console.WriteLine("xong mau nay roi chon mau khac di !");
                    //                        break;
                    //                    }
                    List<int> checkList2 = new List<int>(checkList1);

                    int currentCosts = 0; // trọng số cột hiện tại ở vòng 2

                    foreach (int temp in checkList1)
                    {
                        if (zeroOne[j][temp] == 1)
                        {
                            currentCosts++;
                            checkList2.Remove(temp);
                        }
                    }

                    currentValue2 = currentValue1 + currentCosts;

                    if (!checkList2.Any()) // full 1
                    {
                        Console.WriteLine("2 cot đa Full 1 roi !");
                        if (biggestValue < currentValue2)
                        {
                            Console.WriteLine("CLEAR");
                            biggestValue = currentValue2;
                        }
                        for (int q = j + 1; j < col - 2; j++)
                        {
                            for (int k = q + 1; q < col - 1; q++)
                            {
                                Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                            }
                        }
                        continue;
                    }
                    else
                    {
                        for (int q = j + 1; q < col - 2; q++) // chọn từng cột ở vòng 3, để lại 1 cột để ghép màu
                        {
                            //THÊM ĐIỀU KIỆN DỪNG


                            List<int> checkList3 = new List<int>(checkList2);

                            currentCosts = 0;

                            foreach (int temp in checkList2)
                            {
                                if (zeroOne[q][temp] == 1)
                                {
                                    currentCosts++;
                                    checkList3.Remove(temp);
                                }
                            }

                            currentValue3 = currentValue2 + currentCosts;

                            if (!checkList3.Any()) // full 1
                            {
                                Console.WriteLine("3 cot đa Full 1 roi !");
                                if (biggestValue < currentValue3)
                                {

                                    Console.WriteLine("CLEAR");
                                    biggestValue = currentValue3;
                                }
                                for (int k = q + 1; k < col - 1; k++)
                                {
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                                }
                                continue;
                            }

                            for (int k = q + 1; k < col - 1; k++) // chọn màu ở vòng 4
                            {
                                //THÊM ĐIỀU KIỆN DỪNG

                                List<int> checkList4 = new List<int>(checkList3);

                                currentCosts = 0;

                                foreach (int temp in checkList3)
                                {
                                    if (zeroOne[k][temp] == 1)
                                    {
                                        currentCosts++;
                                        checkList4.Remove(temp);
                                    }
                                }

                                if (currentValue3 + currentCosts > biggestValue)
                                {
                                    Console.WriteLine("> biggest value roi");
                                    Console.WriteLine("CLEAR");
                                    biggestValue = currentValue3 + currentCosts;
                                    Console.WriteLine(biggestValue);
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                                }
                                else if (currentValue3 + currentCosts == biggestValue)
                                {
                                    Console.WriteLine("giong nhau roi");
                                    Console.WriteLine(biggestValue);
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k]);
                                }


                            }

                        }
                    }


                }

            }
        }


        private void processGroup5(String[] color, int[] value, int[][] zeroOne, int col, int row, int start, int end)
        {
            int currentValue1; // giá trị ở vòng 1
            int currentValue2; // giá trị ở vòng 2
            int currentValue3; // giá trị ở vòng 3
            int currentValue4; // giá trị ở vòng 4
            int biggestValue = 0;

            for (int i = 0; i < col - 5; i++) // để lại 4 cột để ghép màu
            {
                // THÊM ĐIỀU KIỆN ĐỂ KẾT THÚC CHƯƠNG TRÌNH

                List<int> checkList1 = new List<int>(); // list so sánh theo ngày không bán được sau vòng 1
                currentValue1 = value[i];

                for (int j = 0; j < end - start + 1; j++) // duyệt từng ngày của màu
                {
                    if (zeroOne[i][j] == 0) // tìm ngày không bán được để so
                    {
                        checkList1.Add(j);
                    }
                }

                if (!checkList1.Any()) // full 1
                {
                    Console.WriteLine("ngay mốc 1 đã full 1 rồi");
                    if (biggestValue < currentValue1)
                    {
                        Console.WriteLine("CLEAR");
                        biggestValue = currentValue1;
                    }
                    for (int j = i + 1; j < col - 4; j++)
                    {
                        for (int q = j + 1; q < col - 3; q++)
                        {
                            for (int k = q + 1; q < col - 2; q++)
                            {
                                for (int l = k + 1; l < col - 1; l++)
                                {
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);
                                }
                            }
                        }
                    }
                    continue;
                }

                for (int j = i + 1; j < col - 4; j++) // chọn từng cột ở vòng 2, để lại 3 cột để ghép với 2 cột đã chọn
                {

                    // THÊM ĐIỀU KIỆN ĐỂ SANG MÀU KHÁC

                    List<int> checkList2 = new List<int>(checkList1);

                    int currentCosts = 0; // trọng số cột hiện tại ở vòng 2

                    foreach (int temp in checkList1)
                    {
                        if (zeroOne[j][temp] == 1)
                        {
                            currentCosts++;
                            checkList2.Remove(temp);
                        }
                    }

                    currentValue2 = currentValue1 + currentCosts;

                    if (!checkList2.Any()) // full 1
                    {
                        Console.WriteLine("2 cot đa Full 1 roi !");

                        if (biggestValue < currentValue2)
                        {
                            Console.WriteLine("CLEAR");
                            biggestValue = currentValue2;
                        }

                        for (int q = j + 1; j < col - 2; j++)
                        {
                            for (int k = q + 1; q < col - 1; q++)
                            {
                                for (int l = k + 1; l < col - 1; l++)
                                {
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);
                                }
                            }
                        }
                        continue;
                    }

                    for (int q = j + 1; q < col - 3; q++) // chọn từng cột ở vòng 3, để lại 2 cột để ghép màu
                    {
                        //THÊM ĐIỀU KIỆN DỪNG


                        List<int> checkList3 = new List<int>(checkList2);

                        currentCosts = 0;

                        foreach (int temp in checkList2)
                        {
                            if (zeroOne[q][temp] == 1)
                            {
                                currentCosts++;
                                checkList3.Remove(temp);
                            }
                        }

                        currentValue3 = currentValue2 + currentCosts;

                        if (!checkList3.Any()) // full 1
                        {
                            Console.WriteLine("3 cot đa Full 1 roi !");
                            if (biggestValue < currentValue3)
                            {

                                Console.WriteLine("CLEAR");
                                biggestValue = currentValue3;
                            }
                            for (int k = q + 1; k < col - 2; k++)
                            {
                                for (int l = k + 1; l < col - 1; l++)
                                {
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);
                                }
                            }
                            continue;
                        }

                        for (int k = q + 1; k < col - 2; k++) // chọn màu ở vòng 4, để lại 1 màu để ghép
                        {
                            //THÊM ĐIỀU KIỆN DỪNG

                            List<int> checkList4 = new List<int>(checkList3);

                            currentCosts = 0;

                            foreach (int temp in checkList3)
                            {
                                if (zeroOne[k][temp] == 1)
                                {
                                    currentCosts++;
                                    checkList4.Remove(temp);
                                }
                            }

                            currentValue4 = currentValue3 + currentCosts;

                            if (!checkList4.Any()) // full 1
                            {
                                Console.WriteLine("4 cot đa Full 1 roi !");
                                if (biggestValue < currentValue3)
                                {

                                    Console.WriteLine("CLEAR");
                                    biggestValue = currentValue4;
                                }
                                for (int l = k + 1; l < col - 1; l++)
                                {
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);

                                }
                                continue;
                            }

                            for (int l = k + 1; l < col - 1; l++)
                            {
                                //THÊM ĐIỀU KIỆN DỪNG

                                List<int> checkList5 = new List<int>(checkList4);

                                currentCosts = 0;

                                foreach (int temp in checkList4)
                                {
                                    if (zeroOne[l][temp] == 1)
                                    {
                                        currentCosts++;
                                        checkList5.Remove(temp);
                                    }
                                }

                                if (currentValue4 + currentCosts > biggestValue)
                                {
                                    Console.WriteLine("> biggest value roi");
                                    Console.WriteLine("CLEAR");
                                    biggestValue = currentValue4 + currentCosts;
                                    Console.WriteLine(biggestValue);
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);
                                }
                                else if (currentValue4 + currentCosts == biggestValue)
                                {
                                    Console.WriteLine("giong nhau roi");
                                    Console.WriteLine(biggestValue);
                                    Console.WriteLine(color[i] + "-" + color[j] + "-" + color[q] + "-" + color[k] + "-" + color[l]);
                                }

                            }


                        }




                    }

                }

            }
        }


    }
}