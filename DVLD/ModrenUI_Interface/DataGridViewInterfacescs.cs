using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModrenUI_Interface
{
    internal class DataGridViewInterfacescs
    {
        static internal void DataGridViewModrenStayle(DataGridView dgvModrenStayle)
        {
            // عام (General)
            dgvModrenStayle.Dock = DockStyle.Fill;
            dgvModrenStayle.BackgroundColor = Color.White;
            dgvModrenStayle.BorderStyle = BorderStyle.FixedSingle;

            // الأعمدة (Columns)
            dgvModrenStayle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvModrenStayle.ColumnHeadersHeight = 60;
            dgvModrenStayle.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // هيدر الأعمدة (Headers)
            dgvModrenStayle.EnableHeadersVisualStyles = false;
            dgvModrenStayle.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvModrenStayle.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvModrenStayle.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 12, FontStyle.Bold);

            // الصفوفdgvModrenStayle (Rows)
            dgvModrenStayle.DefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Regular);
            dgvModrenStayle.DefaultCellStyle.ForeColor = Color.Black;
            dgvModrenStayle.DefaultCellStyle.BackColor = Color.White;
            dgvModrenStayle.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 240, 255);
            dgvModrenStayle.DefaultCellStyle.SelectionForeColor = Color.Black;

            // صفوف متبادلة (Alternating Rows)
            dgvModrenStayle.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(250, 250, 250);

            // الشبكة (Grid)
            dgvModrenStayle.GridColor = Color.FromArgb(220, 220, 220);
        }
    }
}
