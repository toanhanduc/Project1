using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataProcessing.Model;
namespace DataProcessing.Controller
{
    class MiddlewareController
    {
        MiddlewareModel model = new MiddlewareModel();
        thietlaphesoModel tlhs = new thietlaphesoModel();
        public void updateFoundedColor()
        {
            model.setFoundedColor();
        }

        public int getFoundedColorValue()
        {
            return model.getFoundedColor();
        }

        public int estimateTime(int n, int k)
        {
            if (k == 0 || k == n)
                return 1;
            return (estimateTime(n - 1, k - 1) + estimateTime(n - 1, k));
        }

        public int getExcelCol()
        {
            return tlhs.getColCount() - 1;
        }
    }
}
