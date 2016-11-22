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

    }
}
