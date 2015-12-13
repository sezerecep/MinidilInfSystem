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
    public partial class FMcrtedtstudent : System.Windows.Forms.Form
    {
        string mail;
        public FMcrtedtstudent(string loggedmail)
        {
            mail = loggedmail;
            InitializeComponent();
        }



        private void BTsave_Click(object sender, EventArgs e)
        {

            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save Your Changes ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    if (TBmail.Text != "" && TBname.Text != "" && TBphone.Text != "" && TBparname.Text != "" && TBblood.Text != "" && TBparsurname.Text != "" && TBsurname.Text != "" && CBlevel.Text != "" && TBparmail.Text != "" && TBparphone.Text != "" && TBtc.Text != "" && (RBfem.Checked || RBmal.Checked))
                    {
                        bool suc;
                        bool suc1;
                        bool suc2 = false;
                        if (RBfem.Checked)
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','"
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "','" + TBphone.Text + "','f','" + TBtc.Text
                                                                                                + "','What is your parent name?','" + TBparname.Text
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Student');");
                            suc1 = con.NonReturnQuery("INSERT INTO students VALUES(" + TBtc.Text + ",'" + CBlevel.Text + "','" + TBparname.Text + "','" + TBparsurname.Text + "','" + TBparphone.Text + "','" + TBparmail.Text + "','" + TBallerg.Text + "','" + TBblood.Text + "');");
                        }
                        else
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','"
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "','" + TBphone.Text + "','m','" + TBtc.Text
                                                                                                + "','What is your parent name?','" + TBparname.Text
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Student');");
                            suc1 = con.NonReturnQuery("INSERT INTO students VALUES(" + TBtc.Text + ",'" + CBlevel.Text + "','" + TBparname.Text + "','" + TBparsurname.Text + "','" + TBparphone.Text + "','" + TBparmail.Text + "','" + TBallerg.Text + "','" + TBblood.Text + "');");
                        }
                        foreach (string item in CLB1.Items)
                        {
                            string[] bol = item.Split(' ');
                            suc2 = con.NonReturnQuery("INSERT INTO students_lessons_classes VALUES(" + TBtc.Text + ",'" + bol[1] + "','" + bol[0]+"','" + CBlevel.SelectedItem.ToString() + "');");
                        }
                        if (suc && suc1 && suc2)
                        {
                            DialogResult res1;
                            res1 = MessageBox.Show("Changes Saved Successfully", "Changes Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (res1 == DialogResult.OK)
                            {
                                this.Hide();
                                FMyonetici fm = new FMyonetici(mail);
                                fm.ShowDialog();
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Changes Cannot Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please Fill The (*) Sections", "Not Enough Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                con.closeConnection();
            }
            else
            {
                this.Hide();
                FMyonetici fm = new FMyonetici(mail);
                fm.ShowDialog();
                this.Close();
            }
        }

        private void BTcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void FMcrtedtstudent_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("SELECT level_name FROM levels");
                foreach (DataRow rw in tab.Rows)
                {
                    CBlevel.Items.Add(rw[0].ToString());
                }
                tab.Clear();
                
            }
        }

        private void CBlevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBlevel.Enabled = false;
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("CALL getlessonsandclasses ('" + CBlevel.SelectedItem.ToString() + "');");
                foreach (DataRow rw in tab.Rows)
                {
                    CBlesson.Items.Add(rw[0].ToString()+" "+ rw[1].ToString());
                }
            }
            con.closeConnection();
        }

        private void BTclear_Click(object sender, EventArgs e)
        {
            CBlevel.Enabled = true;
            CLB1.Items.Clear();
            CBlesson.Items.Clear();
            CBlesson.Text = "";
        }

        private void BTadd_Click(object sender, EventArgs e)
        {
            CLB1.Items.Add(CBlesson.SelectedItem.ToString());
        }

        private void BTremove_Click(object sender, EventArgs e)
        {
            CLB1.CheckedItems.OfType<string>().ToList().ForEach(CLB1.Items.Remove);
        }
    }
}
