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
    public partial class FMresetpass : System.Windows.Forms.Form
    {
        string mail;
        public FMresetpass(string email)
        {
            InitializeComponent();
            mail = email;
        }

       

        private void BTsubmit_Click_1(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();

            if (TBnewfirst.Text == "" || TBnewsecond.Text == "")
                MessageBox.Show("Please Fill Both Of The Password Lines", "Password Reset Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (TBnewfirst.Text != TBnewsecond.Text)
                MessageBox.Show("Passwords Do Not Match, Please Try Again", "Password Reset Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {

                bool ret = con.NonReturnQuery("UPDATE users SET password_of_user='" + TBnewfirst.Text + "' WHERE email='" + mail + "';");
                con.closeConnection();
                DialogResult res;
                if (ret)
                {
                    res = MessageBox.Show("Your New Password Succcesfully Saved", "Password Reset Attempt Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        this.Hide();
                        FMlogin form = new FMlogin();
                        form.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        this.Hide();
                        FMlogin form = new FMlogin();
                        form.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Can Not Save New Password", "Password Reset Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BTback_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMlogin form = new FMlogin();
            form.ShowDialog();
            this.Close();
        }

        
    }
}
