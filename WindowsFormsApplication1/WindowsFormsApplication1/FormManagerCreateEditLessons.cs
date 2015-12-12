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
    public partial class FMcrtedtlesson : Form
    {
        public FMcrtedtlesson()
        {
            InitializeComponent();
        }

        private void FMcrtedtlesson_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if(con.is_Connected())
            {
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("SELECT level_name FROM levels");
                foreach(DataRow rw in tab.Rows)
                {
                    CBlevel.Items.Add(rw[0].ToString());
                }
            }
        }
    }
}
