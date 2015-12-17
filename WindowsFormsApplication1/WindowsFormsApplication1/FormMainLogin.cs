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
    

    public partial class FMlogin : System.Windows.Forms.Form
    {
        DatabaseConnection con;
        public FMlogin()
        {
            InitializeComponent();
            
        }


   
        private void FMlogin_Load(object sender, EventArgs e)
        {
            
            con = new DatabaseConnection();
            if (con.is_Connected())
                this.toolStripStatusLabel1.Text = "Database Connection Established...";
            else
                this.toolStripStatusLabel1.Text = "Database Connection Failed...";

            BTlogin.Hide();

        }

        private void BTlogin_Click(object sender, EventArgs e)
        {
            if (!con.is_Connected())
            {
                DialogResult result = MessageBox.Show("Database Connection Failed", "Database Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (result == System.Windows.Forms.DialogResult.Retry)
                    con = new DatabaseConnection();
                else
                    this.Close();
            }
            else if (TBmail.Text == "" || TBpass.Text == "")
            {
                MessageBox.Show("Please Enter Your E-mail and Password", "Login Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                string userpass=null;
                string perm=null;
                DataTable retTab = new DataTable();
                DataTable retPermission = new DataTable();
                retTab = con.ReturningQuery("CALL getpassword('"+ TBmail.Text + "');");
                retPermission = con.ReturningQuery("CALL getpermissions('" + TBmail.Text + "');");
                if(retTab.TableName == "Connected but Empty")
                {
                    MessageBox.Show("Wrong E-Mail,Password Combination, Please Try Again", "Login Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(retTab.TableName == "I am Empty")
                {
                    MessageBox.Show("Database Connection Failed", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    foreach (DataRow rw in retTab.Rows)
                    {
                        userpass = rw[0].ToString();
                    }
                }
                foreach(DataRow rw in retPermission.Rows)
                {
                    perm = rw[0].ToString();
                }
                if(userpass==TBpass.Text&& perm =="Student")
                {
                    this.Hide();
                    FMstudent form = new FMstudent(TBmail.Text);
                    form.ShowDialog();
                    this.Close();
                }
                else if(userpass == TBpass.Text&&perm =="Admin")
                {
                    this.Hide();
                    FMyonetici form = new FMyonetici(TBmail.Text);
                    form.ShowDialog();
                    this.Close();
                }
                else if(userpass == TBpass.Text&&perm=="Teacher")
                {
                    this.Hide();
                    FMteacher form = new FMteacher(TBmail.Text);
                    form.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong E-Mail,Password Combination, Please Try Again", "Login Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }
            con.closeConnection();
        }

        private void BTforgotpass_Click(object sender, EventArgs e)
        {

            this.Hide();
            FMforgotpass form2 = new FMforgotpass();
            form2.ShowDialog();
            this.Close();
        }

        private void FMlogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            con.closeConnection();
         }
    }
}
