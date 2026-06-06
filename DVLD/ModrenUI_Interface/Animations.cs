using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModrenUI_Interface
{
    public class Animations
    {
        public static bool ExpandCollapseSidebarWidth(ref FlowLayoutPanel flpSidebar, ref bool IsSidebarExpand)
        {


            // SET Minimum or Maximum size Of the Sidebar
            int TargetSidebarWidth = (IsSidebarExpand) ? /* Collapse */  flpSidebar.MinimumSize.Width : /* Expand */ flpSidebar.MaximumSize.Width;


            //Set Target Size of Sidebar.
            int diff = TargetSidebarWidth - flpSidebar.Width;
            int Sidebardelta = diff / 6;

            if (Sidebardelta == 0)
                Sidebardelta = (diff < 0) ? 1 : -1;

            //Apply width change
            flpSidebar.SuspendLayout();
            flpSidebar.Width += Sidebardelta;  //Ease
            flpSidebar.ResumeLayout(false);

            if ((Sidebardelta < 0 && flpSidebar.Width <= TargetSidebarWidth) ||
                (Sidebardelta > 0 && flpSidebar.Width >= TargetSidebarWidth))
            {
                flpSidebar.Width = TargetSidebarWidth;
                ////pnlEmployeeScreen.Width = TargetEmployeeScreenWidth;

                IsSidebarExpand = !IsSidebarExpand;
                return true;
            }
            return false;
        }
    }
}
