using System;

namespace DataProcessing.Model
{
    public class thietlaphesoModel
    {
        
        private static int colcount = 1;
        private static int rowcount = 1;       
        private static String[] color = new String[colcount - 1];
        private static int[] value = new int[colcount - 1];
        private static int[][] zeroOne = new int[colcount - 1][];
        private static int[] index = new int[colcount - 1];
        /// <summary>
        /// Set giá trị mảng cột
        /// </summary>
        /// <param name="colcount1"></param>
        public void setColCount(int colcount1)
        {
            colcount = colcount1;
        }
        /// <summary>
        /// Get giá trị mảng cột
        /// </summary>
        /// <returns></returns>
        public int getColCount()
        {
            return colcount;
        }
        /// <summary>
        /// Set giá trị mảng hàng
        /// </summary>
        /// <param name="colcount1"></param>
        public void setRowCount(int rowcount1)
        {
            rowcount = rowcount1;
        }
        /// <summary>
        /// Get giá trị mảng hàng
        /// </summary>
        /// <returns></returns>
        public int getRowCount()
        {
            return rowcount;
        }
        /// <summary>
        /// Set giá trị mảng tên màu
        /// </summary>
        /// <param name="color"></param>
        public void setColor(String[] color1)
        {
            color = color1;
        }
        /// <summary>
        /// Get giá trị mảng tên màu
        /// </summary>
        /// <returns></returns>
        public String[] getColor()
        {
            return color;
        }
        /// <summary>
        /// Set giá trị mảng tổng màu bán được theo cột
        /// </summary>
        /// <param name="value"></param>
        public void setValue(int[] value1)
        {
            value = value1;
        }
        /// <summary>
        /// Get giá trị tổng màu bán được theo cột
        /// </summary>
        /// <returns></returns>
        public int[] getValue()
        {
            return value;
        }
        /// <summary>
        /// Set giá trị mảng cell theo cột
        /// </summary>
        /// <param name="zeroOne"></param>
        public void setZeroOne(int[][] zeroOne1)
        {
            zeroOne = zeroOne1;
        }
        /// <summary>
        /// Get giá trị mảng cell theo cột
        /// </summary>
        /// <returns></returns>
        public int[][] getZeroOne()
        {
            return zeroOne;
        }
        /// <summary>
        /// Set giá trị mảng số thứ tự theo excel
        /// </summary>
        /// <param name="index"></param>
        public void setIndex(int[] index1)
        {
            index = index1;
        }
        /// <summary>
        /// Get giá trị mảng số thứ tự theo excel
        /// </summary>
        /// <returns></returns>
        public int[] getIndex()
        {
            return index;
        }
    }
}
