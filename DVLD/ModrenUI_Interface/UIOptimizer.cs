using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModrenUI_Interface
{
    public class UIOptimizer
    {
        public static void EnableDoubleBufferingInAllControlsInForm(Control root)
        {
            if (root == null) return;

            var prop = typeof(Control).GetProperty(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance
            );

            prop?.SetValue(root, true, null);

            foreach (Control child in root.Controls)
            {
                EnableDoubleBufferingInAllControlsInForm(child);
            }
        }
    }
}