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
    public partial class FMstudent : System.Windows.Forms.Form
    {
        string mail;
        public FMstudent(string loggedusermail)
        {
            InitializeComponent();
            mail = loggedusermail;
        }

        private void FMstudent_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = con.ReturningQuery("CALL getname ('" + mail + "')");
                LBname.Text = tab.Rows[0].ItemArray[0].ToString();
            }
            con.closeConnection();
        
        }

        private void BTlogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMlogin fm = new FMlogin();
            fm.ShowDialog();
            this.Close();
        }

        private void BTshschedule_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMscdlestudent fm = new FMscdlestudent(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTshabsence_Click(object sender, EventArgs e)
        {

        }

        private void BTshexams_Click(object sender, EventArgs e)
        {

        }

        private void BTchangepass_Click(object sender, EventArgs e)
        {

        }
    }
}
