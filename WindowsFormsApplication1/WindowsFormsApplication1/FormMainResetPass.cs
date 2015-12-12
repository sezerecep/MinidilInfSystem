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
        public FMresetpass(string email)
        {
            InitializeComponent();
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
                
                // yeni passwordun veritabanına kaydı yapılacak
                con.closeConnection();
                DialogResult res;
                res = MessageBox.Show("Your New Password Succcesfully Saved", "Password Reset Attempt Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
                if (res == System.Windows.Forms.DialogResult.OK)
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
