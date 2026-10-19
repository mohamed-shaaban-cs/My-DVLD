using DVLD.Login;
using DVLD_BusinessLogic; 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Set the culture to invariant culture to avoid issues with number and date formatting
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            
            Application.Run(new frmLogin());

        }
    }
}
