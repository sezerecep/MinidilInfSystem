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
    public partial class FMedtabs : Form
    {
        string mail;
        public FMedtabs(string logged)
        {
            InitializeComponent();
            mail = logged;
        }

        private void FMedtabs_Load(object sender, EventArgs e)
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
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("CALL AbsenceLesClassDayTime('"+CBlevel.Text+"');");
                foreach (DataRow rw in tab.Rows)
                {
                    CBlesss.Items.Add(rw[0].ToString()+" "+ rw[1].ToString()+" " + rw[2].ToString()+" " + rw[3].ToString());
                }
                tab.Clear();
            }
            
        }

        private void CBlesss_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                string[] bol = CBlesss.Text.Split(' ');
                DataTable tab = new DataTable();
                tab = con.ReturningQuery("CALL getstudentsoflessons('" + bol[0] +"','"+bol[1]+"');");
                foreach (DataRow rw in tab.Rows)
                {
                    CBstudent.Items.Add(rw[0].ToString() + " " + rw[1].ToString());
                }
                tab.Clear();
            }
        }

        private void BTcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
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

                    if (CBstudent.Text!="")
                    {
                        string[] bol = CBlesss.Text.Split(' ');
                        string[] bol1 = CBstudent.Text.Split(' ');
                        DataTable tab = con.ReturningQuery("CALL tcfromname ('" + bol1[0] + "','" + bol1[1] + "');");
                        bool ret = con.NonReturnQuery("INSERT INTO students_lessons_classes_absence VALUES(" + tab.Rows[0].ItemArray[0].ToString()
                            + ",'" + bol[0] + "','" + bol[1] + "','" + DTP1.Value.Date.ToString("yyyy-MM-dd")+"');");
                        if (ret)
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
                        MessageBox.Show("Please Select The Sections", "Not Enough Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
