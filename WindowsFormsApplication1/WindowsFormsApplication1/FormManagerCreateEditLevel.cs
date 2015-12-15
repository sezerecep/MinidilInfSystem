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
    public partial class FMcrtedtlevel : Form
    {
        string mail = null;
        public FMcrtedtlevel(string loggedmail)
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
                    if (TBlevelname.Text != "")
                    {
                        bool suc;
                        suc = con.NonReturnQuery("INSERT INTO levels VALUES('" + TBlevelname.Text + "','"
                            + DTPstart.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                            + DTPend.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','"
                            + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',NULL);");
                        if(suc)
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

        private void BTedtsave_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save Your Changes ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    if (TBedtlvlnam.Text!="")
                    {
                        bool suc;
                        suc = con.NonReturnQuery("UPDATE levels SET level_name ='"+TBedtlvlnam.Text+ "',start_date='"
                            +DTPedstart.Value.ToString("yyyy-MM-dd HH:mm:ss") +"',end_date='"+DTPedend.Value.ToString("yyyy-MM-dd HH:mm:ss") +"',updated_at='"
                            +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"' WHERE level_name='"
                            +CBlevels.SelectedItem.ToString()+"'");
                        if(suc)
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
                if(con.is_Connected())
                {
                    if (CBlevels.Text != "")
                    {
                        bool ret = con.NonReturnQuery("DELETE FROM levels WHERE level_name='" + CBlevels.SelectedItem.ToString() + "'");
                        if (ret)
                        {
                            DialogResult res1;
                            res1 = MessageBox.Show("Entry Succesfully Deleted", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show("Entry Cannot Be Deleted", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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

        private void FMcrtedtlevel_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if(con.is_Connected())
            {
                DataTable tab;
                tab = con.ReturningQuery("SELECT level_name FROM levels");
                foreach(DataRow rw in tab.Rows)
                {
                    CBlevels.Items.Add(rw[0].ToString());
                }
            }
            con.closeConnection();
        }

        private void CBlevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if(con.is_Connected())
            {
                TBedtlvlnam.Text = CBlevels.SelectedItem.ToString();
                DataTable tab;
                tab = con.ReturningQuery("SELECT start_date FROM levels WHERE level_name='" + CBlevels.SelectedItem.ToString() + "'");
                DTPstart.Value = Convert.ToDateTime(tab.Rows[0].ItemArray[0].ToString()).Date;

                tab.Clear();
                tab=con.ReturningQuery("SELECT end_date FROM levels WHERE level_name='" + CBlevels.SelectedItem.ToString() + "'");
                DTPend.Value = Convert.ToDateTime(tab.Rows[0].ItemArray[0].ToString()).Date;


            }
        }

    }
}
