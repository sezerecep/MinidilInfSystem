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
    public partial class FMcrtedtlesson : Form
    {
        string mail;
        public FMcrtedtlesson(string loggedmail)
        {
            InitializeComponent();
            mail = loggedmail;
        }

        private void FMcrtedtlesson_Load(object sender, EventArgs e)
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
                tab = con.ReturningQuery("SELECT name_of_user,surname FROM users WHERE (permissions='Teacher' AND email<>'-*-<->-r-d-');");
                foreach (DataRow rw in tab.Rows)
                {
                    CBteacher.Items.Add(rw[0].ToString() + " " + rw[1].ToString());
                }
                tab.Clear();
                tab = con.ReturningQuery("SELECT class_name FROM classes");
                foreach (DataRow rw in tab.Rows)
                {
                    CBclass.Items.Add(rw[0].ToString());
                }
                
               
            }
        }

        private void BTsave_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                if (con.is_Connected())
                {
                    bool ret;
                    bool ret1=false;
                    if (TBname.Text != "" && TBbooks.Text != "" && CBlevel.SelectedItem.ToString() != "" && CLB1.Items[0].ToString() != "")
                    {
                        ret = con.NonReturnQuery("INSERT INTO lessons VALUES('" + TBname.Text + "','"
                            + TBbooks.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',NULL,NULL);");
                        foreach (string item in CLB1.Items)
                        {
                            string[] bol = item.Split(' ');
                            DataTable tab = con.ReturningQuery("CALL tcfromname('" + bol[3] + "','" + bol[4] + "');");
                            string tc = tab.Rows[0].ItemArray[0].ToString();

                            ret1 = con.NonReturnQuery("INSERT INTO lessons_classes VALUES('" + TBname.Text + "','" + bol[0] 
                                + "',"+tc+",'"+bol[1]+"','"+bol[2]+":00','"+CBlevel.SelectedItem.ToString()+"');");
                           
                        }
                        if (ret && ret1)
                        {
                            MessageBox.Show("Changes Saved Succesfully", "Save Succcesfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            FMyonetici fm = new FMyonetici(mail);
                            fm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Changes Cannot Saved", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please Do Not Leave any Sections Empty", "Not Enough Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void BTcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTadd_Click(object sender, EventArgs e)
        {
            CLB1.Items.Add(CBclass.SelectedItem.ToString() + " " + CBday.SelectedItem.ToString() + " " + CBhour.SelectedItem.ToString() + " " + CBteacher.SelectedItem.ToString());
        }
    }
}