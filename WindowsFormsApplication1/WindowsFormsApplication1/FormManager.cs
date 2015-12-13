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
    public partial class FMyonetici : System.Windows.Forms.Form
    {
        string mail = null;

        public FMyonetici(string loggedusermail)
        {
            mail = loggedusermail;
            InitializeComponent();
        }

        private void FMyonetici_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if(con.is_Connected())
            {
                DataTable retTab=con.ReturningQuery("CALL getname ('" + mail + "')");
                string nam = null;
                foreach (DataRow rw in retTab.Rows)
                {
                     nam= rw[0].ToString();
                }
                this.label2.Text = nam;
            }
            else
            {
                MessageBox.Show("Connection to Database Failed", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                FMlogin fm = new FMlogin();
                fm.ShowDialog();
                this.Close();
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

        private void BTcgpass_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMresetpass fm = new FMresetpass(mail);
            fm.ShowDialog();
            this.Close();
        }


        private void BTcrtedtuser_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            if (RBadmin.Checked)
            {
                FMcrtedtmanager fm = new FMcrtedtmanager(mail);
                fm.ShowDialog();
                this.Close();
            }
            else if (RBstudent.Checked)
            {
                FMcrtedtstudent fm = new FMcrtedtstudent(mail);
                fm.ShowDialog();
                this.Close();
            }
            else if (RBteacher.Checked)
            {
                FMcrtedtteacher fm = new FMcrtedtteacher(mail);
                fm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Select a User Type", "User Type Selection Empty", MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Show();
            }
        }

        private void BTcrtedtclass_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMcrtedtclass fm = new FMcrtedtclass(mail);
            fm.ShowDialog();
            this.Close();
        }

        

        private void BTcrteditless_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMcrtedtlesson fm = new FMcrtedtlesson(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTcrteditlvls_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMcrtedtlevel fm = new FMcrtedtlevel(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTedtabs_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMedtabs fm = new FMedtabs();
            fm.ShowDialog();
            this.Close();
        }

        private void BTrptnot_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMreport fm = new FMreport("Average Note");
            fm.ShowDialog();
            this.Close();
        }

        private void BTrprtabs_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMreport fm = new FMreport("Average Absence");
            fm.ShowDialog();
            this.Close();
        }
    }
}
