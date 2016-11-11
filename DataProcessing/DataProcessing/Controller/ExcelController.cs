using System;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using app = Microsoft.Office.Interop.Excel.Application;
using DataProcessing.Model;

namespace DataProcessing.Controller
{
    class ExcelController
    {
        thietlaphesoModel model = new thietlaphesoModel();
        public static int ngaybatdau = 0, ngayketthuc = 0;
        public static bool check = false;
        public static int ncolor = 1;
        
        public void setNColor(int numberinputcolor)
        {
            ncolor = numberinputcolor;
        }
        
        public void readExcel(String path)
        {

            Excel.Application excel;
            excel = new Excel.Application();
            Excel.Workbook WB = excel.Workbooks.Open(path);
            WB = excel.ActiveWorkbook;
            Excel.Worksheet WS;
            WS = WB.ActiveSheet;

            int start = Environment.TickCount;

            model.setColCount(WS.UsedRange.Columns.Count);
            model.setRowCount(WS.UsedRange.Rows.Count);
            String[] color = new string[model.getColCount() - 1];
            int[] value = new int[model.getColCount() - 1];
            int[][] zeroOne = new int[model.getColCount() - 1][];
            int[] index = new int[model.getColCount() - 1];
            for (int i = 0; i < model.getColCount() - 1; i++)
            {
                index[i] = i;
            }
            model.setIndex(index);
            Excel.Range colornumber = WS.get_Range((Excel.Range)WS.Cells[2][1], (Excel.Range)WS.Cells[model.getColCount()][1]);
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
            for (int row = 2; row <= model.getRowCount(); row++)
            {
                string cell = (WS.Cells[1][row] as Excel.Range).Value == null ? "" : (WS.Cells[1][row] as Excel.Range).Value.ToString("M/dd/yyyy");
                if (cell == thietlapHeSo.startdatetime)
                    ngaybatdau = row;

                else if (cell == thietlapHeSo.enddatetime)
                {
                    ngayketthuc = row;
                    break;
                }

            }


            MessageBox.Show("Khoanh vùng hết: " + ((double)(Environment.TickCount - start) / 1000).ToString() + "s");

            //Tạo mảng 2 chiều zeroOne
            for (int i = 0; i < model.getColCount() - 1; i++)
            {
                zeroOne[i] = new int[ngayketthuc - ngaybatdau + 1];
            }

            //Tính tổng tất cả các cột theo thời gian đã định
            for (int i = 2; i <= model.getColCount(); i++)
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


            string tmp4 = "";
            int tmp5, tmp6, tmpindex0;
            if (ncolor == 1)
            {
                for (int i = 0; i < model.getColCount() - 1; i++)
                {
                    //if (color[i] == "BO")
                    //{
                    //    tmp4 = color[i];
                    //    tmp5 = value[i];
                    //    tmpindex0 = index[i];
                    //    color[i] = color[0];
                    //    value[i] = value[0];
                    //    index[i] = index[0];
                    //    color[0] = tmp4;
                    //    value[0] = tmp5;
                    //    index[0] = tmpindex0;
                    //    for (int n = 0; n < ngayketthuc - ngaybatdau + 1; n++)
                    //    {
                    //        tmp6 = zeroOne[i][n];
                    //        zeroOne[i][n] = zeroOne[0][n];
                    //        zeroOne[0][n] = tmp6;
                    //    }
                    //    break;
                    //}     
                }

            }


            //sắp xếp mảng
            int tmp1, tmp2, tmpindex;
            string tmp3 = "";
            for (int i = 0; i < value.Length; i++)
            {
                //if (check == true && i == 0)
                //{
                //    continue;
                //}
                for (int j = i + 1; j < value.Length; j++)
                {
                    if (value[i] < value[j])
                    {
                        tmp1 = value[i];
                        tmpindex = index[i];
                        tmp3 = color[i];
                        value[i] = value[j];
                        index[i] = index[j];
                        color[i] = color[j];
                        value[j] = tmp1;
                        index[j] = tmpindex;
                        color[j] = tmp3;
                        for (int n = 0; n < ngayketthuc - ngaybatdau + 1; n++)
                        {
                            tmp2 = zeroOne[i][n];
                            zeroOne[i][n] = zeroOne[j][n];
                            zeroOne[j][n] = tmp2;
                        }
                    }
                }
            }

            model.setColor(color);
            model.setValue(value);
            model.setZeroOne(zeroOne);
            MessageBox.Show(color[0] + " " + color[1]);
            excel.Quit();
        }
    }
}
