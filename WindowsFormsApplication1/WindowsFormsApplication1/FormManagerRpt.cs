using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinidilInformationSystem
{
    public partial class FMreport : Form
    {
        string com;
        public FMreport(string comes)
        {
            com = comes;
            InitializeComponent();
        }

        private void FMrepnotes_Load(object sender, EventArgs e)
        {
            if(com== "Average Note")
            {
                col_noteavg.HeaderText = com;
                //veritabanı çekimi
            }
            else
            {
                //veritabanı çekimi
                col_noteavg.HeaderText = com;
            }
        }
    }
}
