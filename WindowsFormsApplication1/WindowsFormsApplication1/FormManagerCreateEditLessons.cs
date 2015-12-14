using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MinidilInformationSystem
{
    public partial class FMcrtedtlesson : Form
    {
        DataTable NDGV;
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
                tab = con.ReturningQuery("SELECT name_of_user,surname FROM users WHERE (permissions='Teacher' AND password_of_user<>'-*-<->-r-d-');");
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

                DataTable tab1 = new DataTable();
                tab = con.ReturningQuery("SELECT level_name FROM levels");
                foreach (DataRow rw in tab.Rows)
                {
                    CBedtlevel.Items.Add(rw[0].ToString());
                    CBedtpicklvl.Items.Add(rw[0].ToString());
                }
                tab.Clear();


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
                    bool ret1 = false;
                    if (TBname.Text != "" && TBbooks.Text != "" && CBlevel.SelectedItem.ToString() != "" && CLB1.Items[0].ToString() != "")
                    {
                        ret = con.NonReturnQuery("INSERT INTO lessons VALUES('" + TBname.Text + "','"
                            + TBbooks.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',NULL,NULL);");
                        foreach (string item in CLB1.Items)
                        {
                            
                            string[] bol = item.Split(' ');
                            DataTable tab = con.ReturningQuery("CALL tcfromname('" + bol[3] + "','" + bol[4] + "');");
                            string tc = tab.Rows[0].ItemArray[0].ToString();
                            DataTable numless = con.ReturningQuery("CALL getnumoflessteacher_tc (" + tc + ");");
                            if (numless.Rows[0].ItemArray[0].ToString() == "20")
                            {
                                MessageBox.Show("Teacher: " + bol[3] + " " + bol[4] + " Has 20 Lessons, No More Lessons Cannot Be Assigned", "Teacher Number Of Lessons Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ret1 = false;
                            }
                            else
                            {
                                ret1 = con.NonReturnQuery("INSERT INTO lessons_classes VALUES('" + TBname.Text + "','" + bol[0]
                                    + "'," + tc + ",'" + bol[1] + "','" + bol[2] + ":00','" + CBlevel.SelectedItem.ToString() + "');");
                                con.NonReturnQuery("INSERT INTO lessons_classes_size VALUES('" + TBname.Text + "','" + bol[0] + "',0);");
                                con.NonReturnQuery("UPDATE teachers SET number_of_lessons=number_of_lessons+1 WHERE teacher_tc="+tc+";");
                            }

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

        private void BTremove_Click(object sender, EventArgs e)
        {
            CLB1.CheckedItems.OfType<string>().ToList().ForEach(CLB1.Items.Remove);
        }

        private void BTselect_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                if (TBedtlessnamein.Text != "" && CBedtpicklvl.SelectedItem.ToString() != null)
                {

                    DataTable tab = con.ReturningQuery("CALL getdatetimelessonsclasses('" + TBedtlessnamein.Text + "','" + CBedtpicklvl.SelectedItem.ToString() + "');");
                    NDGV = con.ReturningQuery("CALL getdatetimelessonsclasses('" + TBedtlessnamein.Text + "','" + CBedtpicklvl.SelectedItem.ToString() + "');");
                    tab.Columns[0].ColumnName = "Class";
                    tab.Columns[1].ColumnName = "Day";
                    tab.Columns[2].ColumnName = "Hour";
                    tab.Columns[3].ColumnName = "Teacher Name";
                    tab.Columns[4].ColumnName = "Teacher Surname";
                    DGV1.DataSource = tab;
                    DataTable tab1 = new DataTable();
                    tab1 = con.ReturningQuery("CALL getbooks('" + TBedtlessnamein.Text + "');");
                    TBedtbooks.Text = tab1.Rows[0].ItemArray[0].ToString();
                    TBedtname.Text = TBedtlessnamein.Text;
                    CBedtlevel.SelectedItem = CBedtpicklvl.SelectedItem;


                }
                else
                {
                    MessageBox.Show("Please Enter The Name and Level of Lesson to Edit", "Parameters Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
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
                    DataTable rett = con.ReturningQuery("SELECT teacher_tc FROM lessons_classes WHERE lesson_name='" + TBedtlessnamein.Text + "';");
                    foreach(DataRow rw in rett.Rows)
                    {
                        con.NonReturnQuery("UPDATE teachers SET number_of_lessons=number_of_lessons-1 WHERE teacher_tc=" + rw.ItemArray[0] + ";");
                    }
                    bool ret = con.NonReturnQuery("DELETE FROM lessons WHERE lesson_name='" + TBedtlessnamein.Text + "';");
                    if (ret)
                    {
                        MessageBox.Show("Entry Succesfully Deleted", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        FMyonetici fm = new FMyonetici(mail);
                        fm.ShowDialog();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Entry Cannot Be Deleted", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void BTedtcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTedtsave_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                if (con.is_Connected())
                {
                    if (TBedtname.Text != "" && TBedtbooks.Text != "" && CBedtlevel.SelectedItem.ToString() != "")
                    {

                        DataTable tab = con.ReturningQuery("SELECT class_name FROM classes");
                        DataTable tab1 = con.ReturningQuery("SELECT name_of_user,surname FROM users WHERE (permissions='Teacher' AND password_of_user<>'-*-<->-r-d-');");
                        
                        if (true)
                        {
                            // Gride girilenlerin doğruluğu kontrol eedilmemiş
                            bool ret = false;
                            for(int i=0;i<DGV1.Rows.Count-1;i++)
                            {
                                DataTable tab2 = con.ReturningQuery("CALL tcfromname ('" + NDGV.Rows[i].ItemArray[3].ToString() + "','" + NDGV.Rows[i].ItemArray[4].ToString()+"');");
                                DataTable tab3 = con.ReturningQuery("CALL tcfromname ('" + DGV1.Rows[i].Cells[3].Value.ToString() + "','" + DGV1.Rows[i].Cells[4].Value.ToString() + "');");
                                if (tab3.Rows[0].ItemArray[0].ToString() == tab2.Rows[0].ItemArray[0].ToString())
                                {
                                    ret = con.NonReturnQuery("UPDATE lessons_classes SET lesson_name='" + TBedtname.Text + "',class_name ='" + DGV1.Rows[i].Cells[0].Value.ToString() + "',weekday='" + DGV1.Rows[i].Cells[1].Value.ToString()
                                    + "',lessons_classes_time='" + DGV1.Rows[i].Cells[2].Value.ToString() + "',level_name='" + CBedtlevel.SelectedItem.ToString() + "',teacher_tc=" + tab3.Rows[0].ItemArray[0].ToString() + " WHERE (lesson_name='" + TBedtlessnamein.Text + "' AND class_name='" + NDGV.Rows[i].ItemArray[0].ToString() +
                                    "' AND weekday='" + NDGV.Rows[i].ItemArray[1].ToString() + "' AND lessons_classes_time='" + NDGV.Rows[i].ItemArray[2].ToString() + "' AND level_name='" + CBedtpicklvl.Text + "' AND teacher_tc=" + tab2.Rows[0].ItemArray[0].ToString() + ");");
                                    con.NonReturnQuery("UPDATE lessons_classes SET lesson_name='" + TBedtname.Text + "',class_name ='" + DGV1.Rows[i].Cells[0].Value.ToString() + "' WHERE (lesson_name='" + TBedtlessnamein.Text + "' AND class_name='" + NDGV.Rows[i].ItemArray[0].ToString() + ");");
                                }
                                else
                                {
                                    DataTable numless = con.ReturningQuery("CALL getnumoflessteacher_tc (" + tab3.Rows[0].ItemArray[0].ToString() + ");");
                                    if (numless.Rows[0].ItemArray[0].ToString() == "20")
                                    {
                                        MessageBox.Show("Teacher Has 20 Lessons, No More Lessons Cannot Be Assigned", "Teacher Number Of Lessons Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        ret = false;
                                    }
                                    else
                                    {
                                        ret = con.NonReturnQuery("UPDATE lessons_classes SET lesson_name='" + TBedtname.Text + "',class_name ='" + DGV1.Rows[i].Cells[0].Value.ToString() + "',weekday='" + DGV1.Rows[i].Cells[1].Value.ToString()
                                        + "',lessons_classes_time='" + DGV1.Rows[i].Cells[2].Value.ToString() + "',level_name='" + CBedtlevel.SelectedItem.ToString() + "',teacher_tc=" + tab3.Rows[0].ItemArray[0].ToString() + " WHERE (lesson_name='" + TBedtlessnamein.Text + "' AND class_name='" + NDGV.Rows[i].ItemArray[0].ToString() +
                                        "' AND weekday='" + NDGV.Rows[i].ItemArray[1].ToString() + "' AND lessons_classes_time='" + NDGV.Rows[i].ItemArray[2].ToString() + "' AND level_name='" + CBedtpicklvl.Text + "' AND teacher_tc=" + tab2.Rows[0].ItemArray[0].ToString() + ");");
                                        con.NonReturnQuery("UPDATE teachers SET number_of_lessons=number_of_lessons+1 WHERE teacher_tc=" + tab3.Rows[0].ItemArray[0].ToString() + ";");
                                        con.NonReturnQuery("UPDATE teachers SET number_of_lessons=number_of_lessons-1 WHERE teacher_tc=" + tab2.Rows[0].ItemArray[0].ToString() + ";");
                                        con.NonReturnQuery("UPDATE lessons_classes SET lesson_name='" + TBedtname.Text + "',class_name ='" + DGV1.Rows[i].Cells[0].Value.ToString()+ "' WHERE (lesson_name='" + TBedtlessnamein.Text + "' AND class_name='" + NDGV.Rows[i].ItemArray[0].ToString()+");");
                                    }
                                }
                            }
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
                            MessageBox.Show("Please Enter Fields Correctly", "Fields Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
                            