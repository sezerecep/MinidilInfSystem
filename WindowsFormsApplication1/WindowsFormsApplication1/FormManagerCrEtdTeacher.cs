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
    public partial class FMcrtedtteacher : Form
    {
        string mail = null;
        public FMcrtedtteacher(string lopggedmail)
        {
            InitializeComponent();
            mail = lopggedmail;
        }

        private void FMcrtedtteacher_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if(con.is_Connected())
            {
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("SELECT level_name FROM levels");
                foreach (DataRow rw in tab.Rows)
                {
                    CBlevels.Items.Add(rw[0].ToString());
                }
                
            }
            con.closeConnection();
            
        }

        private void CBlevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("CALL getlessonsandclasses ('" + CBlevels.SelectedItem.ToString() + "');");
                foreach (DataRow rw in tab.Rows)
                {
                    CBlessons.Items.Add(rw[0].ToString());
                }
            }
        }

        private void BTadd_Click(object sender, EventArgs e)
        {
            CLBlist.Items.Add(CBlessons.SelectedItem.ToString());
        }
    }
}
