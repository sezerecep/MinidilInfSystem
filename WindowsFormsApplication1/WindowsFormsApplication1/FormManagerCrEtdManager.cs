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
    public partial class FMcrtedtmanager : Form
    {
        string mail = null;
        public FMcrtedtmanager(string loggedmail)
        {
            mail = loggedmail;
            InitializeComponent();
        }

        private void BTsave_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res =MessageBox.Show("Are You Sure to Save Your Changes ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(res==DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if(con.is_Connected())
                {
                    if (TBmail.Text != "" && TBname.Text != "" && TBphone.Text != "" && TBresetansw.Text != "" && TBresetpass.Text != "" && TBsurname.Text != "" && TBtc.Text != "" && (RBfem.Checked || RBmal.Checked))
                    {
                        bool suc;
                        bool suc1;
                        if (RBfem.Checked)
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','" 
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") 
                                                                                                + "','" + TBphone.Text + "','f','" + TBtc.Text 
                                                                                                + "','" + TBresetpass.Text + "','" + TBresetansw.Text 
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Admin');");
                            suc1=con.NonReturnQuery("INSERT INTO admin VALUES(" + TBtc.Text + ");");
                        }
                        else
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','"
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "','" + TBphone.Text + "','m','" + TBtc.Text
                                                                                                + "','" + TBresetpass.Text + "','" + TBresetansw.Text
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Admin');");
                            suc1=con.NonReturnQuery("INSERT INTO admin VALUES(" + TBtc.Text + ");");
                        }
                        if(suc&&suc1)
                        {
                            DialogResult res1;
                            res1= MessageBox.Show("Changes Saved Successfully", "Changes Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if(res1==DialogResult.OK)
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

        private void BTselect_Click(object sender, EventArgs e)
        {
            
            if(TBtcin.Text=="")
            {
                MessageBox.Show("Please Enter The TC Number of User To Edit", "Parameters Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DatabaseConnection con = new DatabaseConnection();
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("CALL pass_tc (" + TBtcin.Text + ");");
                if (tab.TableName == "Connected but Empty")
                {
                    MessageBox.Show("Wrong TC, Please Try Again", "Select Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (tab.TableName == "I am Empty")
                {
                    MessageBox.Show("Database Connection Failed", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getname_tc (" + TBtcin.Text + ");");
                    TBedtname.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getsurname_tc (" + TBtcin.Text + ");");
                    TBeditsurname.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getemail_tc (" + TBtcin.Text + ");");
                    TBeditmail.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getpassresetanswerforteacheradmin (" + TBtcin.Text + ");");
                    TBeditpassanw.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getpassresetquestionforteacheradmin (" + TBtcin.Text + ");");
                    TBedtpassreset.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getphone_tc (" + TBtcin.Text + ");");
                    TBeditphone.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    TBedittc.Text = TBtcin.Text;
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getdateofbirth_tc (" + TBtcin.Text + ");");
                    DTP2.Value = Convert.ToDateTime(tab.Rows[0].ItemArray[0].ToString()).Date;
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getgender_tc (" + TBtcin.Text + ");");
                    if (tab.Rows[0].ItemArray[0].ToString() == "m")
                    {
                        RBeditmal.Checked = true;
                        RBeditfem.Checked = false;
                    }
                    else
                    {
                        RBeditfem.Checked = true;
                        RBeditmal.Checked = false;
                    }
                }
                con.closeConnection();
            }
        }

        private void BTdelete_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Delete ?", "Deleting from Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    bool ret = con.NonReturnQuery("UPDATE users SET password_of_user='-*-<->-r-d-',deleted_at='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE tc=" + TBtcin.Text + ";");
                    if (ret)
                    {
                        MessageBox.Show("Entry Succesfully Deleted", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            string mmail = null;
                            DataTable tab;
                            tab = con.ReturningQuery("CALL getemail_tc (" + TBtcin.Text + ");");
                            mmail = tab.Rows[0].ItemArray[0].ToString();
                            if (mail == mmail)
                            {
                                con.NonReturnQuery("UPDATE users SET password_of_user='-*-<->-r-d-' WHERE email='" + mmail + "';");
                                this.Hide();
                                FMlogin fm1 = new FMlogin();
                                fm1.ShowDialog();
                                this.Close();

                            }
                            else
                            {
                                this.Hide();
                                FMyonetici fm = new FMyonetici(mail);
                                fm.ShowDialog();
                                this.Close();
                            }
                        
                    }
                    else
                    {
                        MessageBox.Show("Entry Cannot Be Deleted", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                this.Hide();
                FMyonetici fm = new FMyonetici(mail);
                fm.ShowDialog();
                this.Close();
            }
        }

        private void BTeditcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTeditsave_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save Your Changes ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    if (TBeditmail.Text != "" && TBeditpassanw.Text != "" && TBeditphone.Text != "" && TBeditsurname.Text != "" && TBedittc.Text != "" && TBedtname.Text != "" && TBedtpassreset.Text != "")
                    {
                        bool suc;
                        if (RBeditfem.Checked)
                        {
                            suc = con.NonReturnQuery("UPDATE users SET tc=" + TBedittc.Text + ",name_of_user='"
                                 + TBedtname.Text + "',surname='" + TBeditsurname.Text + "',email='"
                                 + TBeditmail.Text + "',date_of_birth='" + DTP2.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                 + "',phone='" + TBeditphone.Text + "',gender='f',reset_pass_question='"
                                 + TBedtpassreset.Text + "',reset_pass_answer='" + TBeditpassanw.Text + "',updated_at='"
                                 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE tc='" + TBtcin.Text + "'");
                        }
                        else
                        {
                            suc = con.NonReturnQuery("UPDATE users SET tc=" + TBedittc.Text + ",name_of_user='"
                                + TBedtname.Text + "',surname='" + TBeditsurname.Text + "',email='"
                                + TBeditmail.Text + "',date_of_birth='" + DTP2.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                + "',phone='" + TBeditphone.Text + "',gender='m',reset_pass_question='"
                                + TBedtpassreset.Text + "',reset_pass_answer='" + TBeditpassanw.Text + "',updated_at='"
                                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE tc='" + TBtcin.Text + "'");
                        }
                        if (suc)
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
                        MessageBox.Show("Please Do Not Leave any Sections Empty", "Not Enough Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void TBtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TBedittc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TBeditphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        
    }
}
