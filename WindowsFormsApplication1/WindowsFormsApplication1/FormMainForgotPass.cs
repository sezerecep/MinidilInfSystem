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
    public partial class FMforgotpass : Form
    {
        string question = null;
        string answer = null;
        string email = null;
        public FMforgotpass()
        {
            InitializeComponent();
        }

        private void BTback_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMlogin form2 = new FMlogin();
            form2.ShowDialog();
            this.Close();
        }

        private void BTnext_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (label1.Text != "Answer :")
            {
                if (TBemail.Text == "")
                {
                    MessageBox.Show("Please Enter Your E-mail", "Forgot Password Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable retTab = new DataTable();
                    retTab = con.ReturningQuery("CALL getpassresetquestion('" + TBemail.Text + "');");
                    email = TBemail.Text;
                    if (retTab.TableName == "Connected but Empty")
                    {
                        MessageBox.Show("Wrong E-Mail,Password Combination, Please Try Again", "Login Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (retTab.TableName == "I am Empty")
                    {
                        MessageBox.Show("Database Connection Failed", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        label2.Text = retTab.Rows[0].ItemArray[0].ToString();
                        label1.Text = "Answer :";
                        pictureBox2.Hide();
                    }
                }
            }
            else
            {
                DataTable retTab = new DataTable();
                retTab = con.ReturningQuery("CALL getpassresetanswer('" + email + "');");
                if (TBemail.Text == "")
                {
                    MessageBox.Show("Please Enter Your Answer", "Forgot Password Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if(TBemail.Text==retTab.Rows[0].ItemArray[0].ToString())
                    {
                        this.Hide();
                        FMresetpass form2 = new FMresetpass(email);
                        form2.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Answer, Please Try Again", "Login Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            con.closeConnection();
        }
    }
}