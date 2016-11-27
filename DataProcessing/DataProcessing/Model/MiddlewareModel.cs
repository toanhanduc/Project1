using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.Model
{
    class MiddlewareModel
    {
        private static int foundedColor = 0;
        private static int foundedColor_MaxValue = 0;
        public void setFoundedColor()
        {
            foundedColor += 1;
        }
        /// <summary>
        /// Get giá trị mảng cột
        /// </summary>
        /// <returns></returns>
        public int getFoundedColor()
        {
            return foundedColor;
        }


        public void setFoundedColorMaxValue()
        {
            foundedColor_MaxValue += 1;
        }
        public int getFoundedColorMaxValue()
        {
            return foundedColor_MaxValue;
        }

    }
}
