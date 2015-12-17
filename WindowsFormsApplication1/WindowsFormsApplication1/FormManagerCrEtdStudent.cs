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
        CheckedListBox oldless=new CheckedListBox();
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
                    if (TBmail.Text != "" && TBname.Text != "" && TBphone.Text != "" && TBparname.Text != "" && CBblood1.Text != "" && TBparsurname.Text != "" && TBsurname.Text != "" && CBlevel.Text != "" && TBparmail.Text != "" && TBparphone.Text != "" && TBtc.Text != "" && (RBfem.Checked || RBmal.Checked))
                    {
                        bool suc;
                        bool suc1 = false;
                        bool suc2 = false;
                        if (RBfem.Checked)
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','"
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "','" + TBphone.Text + "','f','" + TBtc.Text
                                                                                                + "','What is your parent name?','" + TBparname.Text
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Student');");
                            if(suc)
                            suc1 = con.NonReturnQuery("INSERT INTO students VALUES(" + TBtc.Text + ",'" + CBlevel.Text + "','" + TBparname.Text + "','" + TBparsurname.Text + "','" + TBparphone.Text + "','" + TBparmail.Text + "','" + TBallerg.Text + "','" + CBblood1.Text + "');");
                        }
                        else
                        {
                            suc = con.NonReturnQuery("INSERT INTO users VALUES(" + TBtc.Text + ",'" + TBname.Text + "','" + TBsurname.Text + "','"
                                                                                                + TBmail.Text + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "','" + TBphone.Text + "','m','" + TBtc.Text
                                                                                                + "','What is your parent name?','" + TBparname.Text
                                                                                                + "',NULL,NULL,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                                                                                + "',NULL,NULL,'Student');");
                            if (suc)
                                suc1 = con.NonReturnQuery("INSERT INTO students VALUES(" + TBtc.Text + ",'" + CBlevel.Text + "','" + TBparname.Text + "','" + TBparsurname.Text + "','" + TBparphone.Text + "','" + TBparmail.Text + "','" + TBallerg.Text + "','" + CBblood1.Text + "');");
                        }
                        if (suc1)
                        {
                            foreach (string item in CLB1.Items)
                            {
                                string[] bol = item.Split(' ');
                                DataTable clsize = con.ReturningQuery("CALL classsizefromnamelesson('" + bol[0] + "','" + bol[1] + "');");
                                if (clsize.Rows[0].ItemArray[0].ToString() == "15")
                                {
                                    DialogResult res1 = MessageBox.Show("This Class Has 15 Students ", "Class Size Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    suc2 = false;
                                }
                                else
                                {
                                    suc2 = con.NonReturnQuery("INSERT INTO students_lessons_classes VALUES(" + TBtc.Text + ",'" + bol[1] + "','" + bol[0] + "','" + CBlevel.SelectedItem.ToString() + "');");
                                    if (suc2)
                                    {
                                        con.NonReturnQuery("UPDATE lessons_classes_size SET class_size=class_size+1 WHERE lesson_name='" + bol[0] + "', AND class_name='" + bol[1] + "';");
                                    }
                                }
                            }
                        }
                        if (suc2)
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
            DTP1.MaxDate = DateTime.Now;
            DTP2.MaxDate = DateTime.Now;
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
            if(CBlesson.Text!="")
            CLB1.Items.Add(CBlesson.SelectedItem.ToString());
        }

        private void BTremove_Click(object sender, EventArgs e)
        {
            CLB1.CheckedItems.OfType<string>().ToList().ForEach(CLB1.Items.Remove);
        }

        private void BTedtcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTedtselect_Click(object sender, EventArgs e)
        {
            if (TBedttcin.Text == "")
            {
                MessageBox.Show("Please Enter The TC Number of User To Edit", "Parameters Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DatabaseConnection con = new DatabaseConnection();
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("CALL pass_tc (" + TBedttcin.Text + ");");
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
                    tab = con.ReturningQuery("CALL getname_tc (" + TBedttcin.Text + ");");
                    TBedtname.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getsurname_tc (" + TBedttcin.Text + ");");
                    TBedtsurname.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getemail_tc (" + TBedttcin.Text + ");");
                    TBedtmail.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getphone_tc (" + TBedttcin.Text + ");");
                    TBedtphone.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    TBedttc.Text = TBedttcin.Text;
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getdateofbirth_tc (" + TBedttcin.Text + ");");
                    DTP2.Value = Convert.ToDateTime(tab.Rows[0].ItemArray[0].ToString()).Date;
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getgender_tc (" + TBedttcin.Text + ");");
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
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getparentname_tc (" + TBedttcin.Text + ");");
                    TBedtparname.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getparentsurname_tc (" + TBedttcin.Text + ");");
                    TBedtparsurname.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getparentemail_tc (" + TBedttcin.Text + ");");
                    TBedtparmail.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getparentphone_tc (" + TBedttcin.Text + ");");
                    TBedtparmobile.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getallergy_tc (" + TBedttcin.Text + ");");
                    TBedtallergies.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getbloodtype_tc (" + TBedttcin.Text + ");");
                    CBblood2.Text = tab.Rows[0].ItemArray[0].ToString();
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getstudentlevel_tc(" + TBedttcin.Text + "); ");
                    CBedtlvl.Text= tab.Rows[0].ItemArray[0].ToString();
                    CBedtlvl.Enabled = false;
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getlessonsandclasses ('" + CBedtlvl.Text + "');");
                        foreach (DataRow rw in tab.Rows)
                        {
                            CBedtlesson.Items.Add(rw[0].ToString() + " " + rw[1].ToString());
                        }
                    tab.Clear();
                    tab = con.ReturningQuery("CALL getstudentlessonsandclasses (" + TBedttcin.Text + ");");
                   foreach (DataRow rw in tab.Rows)
                        {
                            CLB2.Items.Add(rw[0].ToString() + " " + rw[1].ToString());
                            oldless.Items.Add(rw[0].ToString() + " " + rw[1].ToString());
                        }
                    
                }
                con.closeConnection();
            }
        }

        private void BTedtclear_Click(object sender, EventArgs e)
        {
            CBedtlvl.Enabled = true;
            CLB2.Items.Clear();
            CBedtlesson.Items.Clear();
            CBedtlesson.Text = "";
        }

        private void Btedtadd_Click(object sender, EventArgs e)
        {
            if (CBedtlesson.Text != "")
                CLB2.Items.Add(CBedtlesson.SelectedItem.ToString());
        }

        private void BTedtremove_Click(object sender, EventArgs e)
        {
            CLB2.CheckedItems.OfType<string>().ToList().ForEach(CLB2.Items.Remove);
        }

        private void BTedtdelete_Click(object sender, EventArgs e)
        {   
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Delete ?", "Deleting from Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {   

                    bool ret = con.NonReturnQuery("UPDATE users SET password_of_user='-*-<->-r-d-',deleted_at='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  WHERE tc=" + TBedttcin.Text + ";");
                    bool ret2 = con.NonReturnQuery("DELETE FROM students WHERE student_tc=" + TBedttcin.Text + ";");
                    foreach(string item in CLB2.Items)
                    {
                        string[] bol = item.Split(' ');
                        con.NonReturnQuery("UPDATE lessons_classes_size SET class_size=class_size-1 WHERE lesson_name='" + bol[0] + "',class_name='" + bol[1] + "';");
                    }
                    if (ret&&ret2)
                    {
                        MessageBox.Show("Entry Succesfully Deleted", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string mmail = null;
                        DataTable tab;
                        tab = con.ReturningQuery("CALL getemail_tc (" + TBedttcin.Text + ");");
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

        private void BTedtsave_Click(object sender, EventArgs e)
        {

            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save Your Changes ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    if (TBedtmail.Text != "" && TBedtname.Text != "" && TBedtphone.Text != "" && TBedtparname.Text != "" && CBblood2.Text != "" && TBedtparsurname.Text != "" && TBedtsurname.Text != "" && CBedtlvl.Text != "" && TBedtparmail.Text != "" && TBedtparmobile.Text != "" && TBedttc.Text != "")
                    {
                        bool suc;
                        bool suc1;
                        bool suc3;
                        bool suc4;
                        if (RBedtfem.Checked)
                        {
                            suc = con.NonReturnQuery("UPDATE users SET tc=" + TBedttc.Text + ",name_of_user='"
                                 + TBedtname.Text + "',surname='" + TBedtsurname.Text + "',email='"
                                 + TBedtmail.Text + "',date_of_birth='" + DTP2.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                 + "',phone='" + TBedtphone.Text + "',gender='f',reset_pass_question='What is your parent name?',reset_pass_answer='" 
                                 + TBedtparname.Text + "',updated_at='"
                                 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE tc='" + TBedttcin.Text + "'");
                            suc1 = con.NonReturnQuery("UPDATE students SET level_name='" + CBedtlvl.Text + "',parent_name='"
                                + TBedtparname.Text + "',parent_surname='" + TBedtparsurname.Text + "',parent_phone='" + TBedtparmobile.Text + "',parent_email='"
                                + TBedtparmail.Text + "',allergy='" + TBedtallergies.Text + "',blood_type='" + CBblood2.Text + "' WHERE student_tc='" + TBedttc.Text + "'");
                            
                        }
                        else
                        {
                            suc = con.NonReturnQuery("UPDATE users SET tc=" + TBedttc.Text + ",name_of_user='"
                                 + TBedtname.Text + "',surname='" + TBedtsurname.Text + "',email='"
                                 + TBedtmail.Text + "',date_of_birth='" + DTP2.Value.Date.ToString("yyyy-MM-dd HH:mm:ss")
                                 + "',phone='" + TBedtphone.Text + "',gender='m',reset_pass_question='What is your parent name?',reset_pass_answer='"
                                 + TBedtparname.Text + "',updated_at='"
                                 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE tc='" + TBedttcin.Text + "'");
                            suc1 = con.NonReturnQuery("UPDATE students SET level_name='" + CBedtlvl.Text + "',parent_name='"
                                + TBedtparname.Text + "',parent_surname='" + TBedtparsurname.Text + "',parent_phone='" + TBedtparmobile.Text + "',parent_email='"
                                + TBedtparmail.Text + "',allergy='" + TBedtallergies.Text + "',blood_type='" + CBblood2.Text + "' WHERE student_tc='" + TBedttc.Text + "'");
                        }
                        for (int i = 0; i < CLB2.Items.Count; i++)
                        {
                            if (oldless.Items.Contains(CLB2.Items[i]))
                            {
                                //do nothing
                            }
                            else
                            {
                                string[] bol = CLB2.Items[i].ToString().Split(' ');
                                DataTable clsize = con.ReturningQuery("CALL classsizefromnamelesson('" + bol[0] + "','" + bol[1] + "');");
                         
                                if (clsize.Rows[0].ItemArray[0].ToString() == "15"&&clsize.TableName!="Connected but Empty")
                                {
                                    DialogResult res1 = MessageBox.Show("This Class Has 15 Students ", "Class Size Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    suc3 = false;
                                }
                                else
                                {
                                    suc3 = con.NonReturnQuery("INSERT INTO students_lessons_classes VALUES(" + TBedttc.Text + ",'" + bol[1] + "','" + bol[0] + "','" + CBedtlvl.Text + "');");
                                    con.NonReturnQuery("UPDATE lessons_classes_size SET class_size=class_size+1 WHERE lesson_name='" + bol[0] + "'AND class_name='" + bol[1] + "';");
                                }
                                
                            }
                        }
                        for (int i = 0; i < oldless.Items.Count; i++)
                        {
                            if (CLB2.Items.Contains(oldless.Items[i]))
                            {
                                //do nothing
                            }
                            else
                            {
                                string[] bol = oldless.Items[i].ToString().Split(' ');
                                suc4 = con.NonReturnQuery("DELETE FROM students_lessons_classes WHERE (student_tc=" + TBedttc.Text + " AND class_name='" + bol[1] + "' AND lesson_name='" + bol[0] + "' AND level_name='" + CBedtlvl.Text + "');");
                                con.NonReturnQuery("UPDATE lessons_classes_size SET class_size=class_size-1 WHERE lesson_name='" + bol[0] + "' AND class_name='" + bol[1] + "';");
                            }
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

        private void TBtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

     
    }
}
