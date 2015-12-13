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


        private void BTsave_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save Your Changes ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    if (TBmail.Text != "" && TBname.Text != "" && TBphone.Text != "" && TBresansw.Text != "" && TBresetpass.Text != "" && TBsurname.Text != "" && TBtc.Text != "" && (RBfem.Checked || RBmal.Checked))
                    {
                        bool suc;
                        bool suc1;
                        if (RBfem.Checked)
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','"
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "','" + TBphone.Text + "','f','" + TBtc.Text
                                                                                                + "','" + TBresetpass.Text + "','" + TBresansw.Text
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Teacher');");
                            suc1 = con.NonReturnQuery("INSERT INTO teachers VALUES(" + TBtc.Text + ");");
                        }
                        else
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','"
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "','" + TBphone.Text + "','m','" + TBtc.Text
                                                                                                + "','" + TBresetpass.Text + "','" + TBresansw.Text
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Teacher');");
                            suc1 = con.NonReturnQuery("INSERT INTO teachers VALUES(" + TBtc.Text + ",'"+TBskills.Text+"',0);");
                        }
                        if (suc && suc1)
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

        private void BTselect_Click(object sender, EventArgs e)
        {
            if (TBtcin.Text == "")
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
                    TBedtmail.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getpassresetanswerforteacheradmin (" + TBtcin.Text + ");");
                    TBeditpassrst.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getpassresetquestionforteacheradmin (" + TBtcin.Text + ");");
                    TBedtpassque.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getphone_tc (" + TBtcin.Text + ");");
                    TBedtphone.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    TBedttc.Text = TBtcin.Text;
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getdateofbirth_tc (" + TBtcin.Text + ");");
                    DTP2.Value = Convert.ToDateTime(tab.Rows[0].ItemArray[0].ToString()).Date;
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getteacherskills(" + TBtcin.Text + ");");
                    TBedtskills.Text= tab.Rows[0].ItemArray[0].ToString();
                    tab = con.ReturningQuery("CALL getgender_tc (" + TBtcin.Text + ");");
                    if (tab.Rows[0].ItemArray[0].ToString() == "m")
                    {
                        RBedtmal.Checked = true;
                        RBedtfem.Checked = false;
                    }
                    else
                    {
                        RBedtfem.Checked = true;
                        RBedtmal.Checked = false;
                    }
                }
                con.closeConnection();
            }
        }

        private void BTedtsave_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save Your Changes ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    if (TBedtmail.Text != "" && TBeditpassrst.Text != "" && TBedtphone.Text != "" && TBeditsurname.Text != "" && TBedttc.Text != "" && TBedtname.Text != "" && TBedtpassque.Text != "")
                    {
                        bool suc;
                        bool suc1;
                        if (RBedtfem.Checked)
                        {
                            suc = con.NonReturnQuery("UPDATE users SET tc=" + TBedttc.Text + ",name_of_user='"
                                 + TBedtname.Text + "',surname='" + TBeditsurname.Text + "',email='"
                                 + TBedtmail.Text + "',date_of_birth='" + DTP2.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                 + "',phone='" + TBedtphone.Text + "',gender='f',reset_pass_question='"
                                 + TBedtpassque.Text + "',reset_pass_answer='" + TBeditpassrst.Text + "',updated_at='"
                                 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE tc='" + TBtcin.Text + "'");
                            suc1 = con.NonReturnQuery("UPDATE teachers SET skills='"+TBedtskills.Text + "' WHERE teacher_tc="+TBedttc.Text+";");
                        }
                        else
                        {
                            suc = con.NonReturnQuery("UPDATE users SET tc=" + TBedttc.Text + ",name_of_user='"
                                + TBedtname.Text + "',surname='" + TBeditsurname.Text + "',email='"
                                + TBedtmail.Text + "',date_of_birth='" + DTP2.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                + "',phone='" + TBedtphone.Text + "',gender='m',reset_pass_question='"
                                + TBedtpassque.Text + "',reset_pass_answer='" + TBeditpassrst.Text + "',updated_at='"
                                + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE tc='" + TBtcin.Text + "'");
                            suc1 = con.NonReturnQuery("UPDATE teachers SET skills='" + TBedtskills.Text + "' WHERE teacher_tc=" + TBedttc.Text + ";");
                        }
                        if (suc&&suc1)
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

        private void BTedtcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTeditdel_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Delete ?", "Deleting from Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    bool ret = con.NonReturnQuery("UPDATE users SET email='-*-<->-r-d-',deleted_at='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE tc=" + TBtcin.Text + ";");
                    if (ret)
                    {

                        DialogResult res1;
                        res1 = MessageBox.Show("Entry Succesfully Deleted", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (res1 == DialogResult.OK)
                        {
                            string mmail = null;
                            DataTable tab;
                            tab = con.ReturningQuery("CALL getemail_tc (" + TBtcin.Text + ");");
                            mmail = tab.Rows[0].ItemArray[0].ToString();
                            if (mail == mmail)
                            {
                                con.NonReturnQuery("UPDATE users SET email='-*-<->-r-d-' WHERE email='" + mmail + "';");
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
    }
}
