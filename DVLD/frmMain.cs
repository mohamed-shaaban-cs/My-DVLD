using DVLD_BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModrenUI_Interface;

namespace DVLD
{
    public partial class frmDVLDMain : Form
    {
        private bool IsSidebarExpand = true;
        public frmDVLDMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            UIOptimizer.EnableDoubleBufferingInAllControlsInForm(this);

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(!timer1.Enabled)
                timer1.Start();

            this.PerformLayout();
            this.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(Animations.ExpandCollapseSidebarWidth(ref flowLayoutPanel1, ref IsSidebarExpand))
                timer1.Stop();
        }

        private void peopleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            People.frmManagePeople frm = new People.frmManagePeople();
            frm.ShowDialog();
        }

        private void hiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
