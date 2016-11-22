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
        public void updateFoundedColor()
        {
            model.setFoundedColor();
        }

        public int getFoundedColorValue()
        {
            return model.getFoundedColor();
        }
    }
}
