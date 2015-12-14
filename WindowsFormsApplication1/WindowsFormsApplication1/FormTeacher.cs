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
    public partial class FMteacher : System.Windows.Forms.Form
    {
        string mail;
        public FMteacher(string loggedusermail)
        {
            InitializeComponent();
            mail = loggedusermail;
        }

        private void FMteacher_Load(object sender, EventArgs e)
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
            FMschdleteacher fm = new FMschdleteacher(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTedtexms_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMedtexams fm = new FMedtexams(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTsshlessstudent_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMteacherswless fm = new FMteacherswless(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTchngpass_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMresetpass fm = new FMresetpass(mail);
            fm.ShowDialog();
            this.Close();
        }
    }
}
