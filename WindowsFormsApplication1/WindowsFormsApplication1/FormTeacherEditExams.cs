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
    public partial class FMedtexams : Form
    {
        DataTable NDGV =new DataTable();
        string mail = null;
        public FMedtexams(string loggedmail)
        {
            InitializeComponent();
            mail = loggedmail;
        }


        private void FMedtexams_Load(object sender, EventArgs e)
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
            if (con.is_Connected())
            {
                DataTable tab = con.ReturningQuery("CALL tcfromemail ('" + mail + "')");
                DataTable tab1 = con.ReturningQuery("SELECT * FROM viewexam");
                NDGV = con.ReturningQuery("SELECT * FROM viewexam");
                if (tab1.TableName!="Connected but Empty")
                {
                    tab1.Columns[0].ColumnName = "Exam Name";
                    tab1.Columns[1].ColumnName = "Lesson";
                    tab1.Columns[2].ColumnName = "Class";
                    tab1.Columns[3].ColumnName = "Date/Time";
                    DGV1.DataSource = tab1;
                    DGVexams.DataSource = tab1;
                  
                }
            }
        }

        private void CBlevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = new DataTable();
                CBlessonclasses.Items.Clear();
                tab = con.ReturningQuery("CALL getlessonsandclasses ('" + CBlevel.SelectedItem.ToString() + "');");
                foreach (DataRow rw in tab.Rows)
                {
                    CBlessonclasses.Items.Add(rw[0].ToString()+" "+rw[1].ToString());
                }
            }

        }

        private void BTadd_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save New Exam?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                DatabaseConnection con = new DatabaseConnection();
                if (con.is_Connected())
                {
                    if (TBname.Text != "" && CBlevel.Text != "" && CBlessonclasses.Text != "" && DTP1.Text != "")
                    {
                        bool suc;
                        bool suc1;
                        
                        string[] bol = CBlessonclasses.SelectedItem.ToString().Split(' ');
                       
                        suc = con.NonReturnQuery("INSERT INTO exams VALUES('" + bol[0] + "','" + bol[1]+ "','" + TBname.Text + "','"
                            + DTP1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',NULL);");
                        DataTable tab = con.ReturningQuery("CALL studentsfromlesson('" + bol[0] + "','" + bol[1] + "');");
                        foreach (DataRow rw in tab.Rows)
                        {
                            suc1 = con.NonReturnQuery("INSERT INTO students_exams VALUES(" + rw.ItemArray[0].ToString() + ",'" + TBname.Text + "',NULL)");
                        }

                        if (suc)
                        {
                            DialogResult res1;
                            res1 = MessageBox.Show("Exam Saved Successfully", "Exam Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (res1 == DialogResult.OK)
                            {
                                this.Hide();
                                FMedtexams fm = new FMedtexams(mail);
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
        private void BTsave_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                if (con.is_Connected())
                {

                    if (true)
                    {
                        // Gride girilenlerin doğruluğu kontrol eedilmemiş
                        bool ret = false;
                        for (int i = 0; i < DGV1.Rows.Count; i++)
                        {

                            ret = con.NonReturnQuery("UPDATE exams SET exam_name='" + DGV1.Rows[i].Cells[0].Value.ToString() + "',lesson_name='" + DGV1.Rows[i].Cells[1].Value.ToString()
                               + "',class_name='" + DGV1.Rows[i].Cells[2].Value.ToString() +"',updated_at="+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',exam_date_time='" 
                               + Convert.ToDateTime(DGV1.Rows[i].Cells[3].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE (exam_name='" + NDGV.Rows[i].ItemArray[0].ToString() +"');");
                        }
                        
                        if (ret)
                        {
                            MessageBox.Show("Changes Saved Succesfully", "Save Succcesfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            FMteacher fm = new FMteacher(mail);
                            fm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Changes Cannot Saved", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }

        private void BTdelete_Click(object sender, EventArgs e)
        {
            DataTable tab2 = new DataTable();
            DataColumn[] dcs = new DataColumn[] { };
            foreach (DataGridViewColumn c in DGV1.Columns)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = c.Name;
                dc.DataType = c.ValueType;
                tab2.Columns.Add(dc);
            }
            foreach (DataGridViewRow r in DGV1.SelectedRows)
            {
                DataRow drow = tab2.NewRow();
                foreach (DataGridViewCell cell in r.Cells)
                {
                    drow[cell.OwningColumn.Name] = cell.Value;
                }
                tab2.Rows.Add(drow);
            }

            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                bool suc=false;
                foreach(DataRow dtRow in tab2.Rows)
                {
                    suc = con.NonReturnQuery("DELETE FROM exams WHERE(exam_name='" + dtRow.ItemArray[0].ToString()+"');"); 
                }
                if (suc)
                {
                    MessageBox.Show("Entry Succesfully Deleted", "Deletion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FMteacher fm = new FMteacher(mail);
                    fm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Entry Cannot Be Deleted", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BTcancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMteacher fm = new FMteacher(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void DGVexams_SelectionChanged(object sender, EventArgs e)
        {
            int index = DGVexams.CurrentCell.RowIndex;
            DataTable tab2 = new DataTable();
            DataColumn[] dcs = new DataColumn[] { };
            foreach (DataGridViewColumn c in DGVexams.Columns)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = c.Name;
                dc.DataType = c.ValueType;
                tab2.Columns.Add(dc);
            }

            tab2 = (DataTable)(DGVexams.DataSource);
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab;
                tab = con.ReturningQuery("CALL getstudentsofexams('" + tab2.Rows[index].ItemArray[0].ToString() + "');");
                DGVstudent.DataSource = tab;
                if (tab.TableName != "Connected but Empty")
                {
                    DGVstudent.Columns[0].ReadOnly = true;
                    DGVstudent.Columns[1].ReadOnly = true;
                }
            }
        }

        private void BTscorecancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMteacher fm = new FMteacher(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void BTscoresave_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            DialogResult res;
            res = MessageBox.Show("Are You Sure to Save ?", "Saving to Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                if (con.is_Connected())
                {
                    bool suc1 = false;
                    DataTable tab = con.ReturningQuery("CALL studentsfromlesson('" + DGVexams.SelectedRows[0].Cells[1].Value.ToString() + "','" + DGVexams.SelectedRows[0].Cells[2].Value.ToString() + "');");
                    foreach (DataRow rw in tab.Rows)
                    {
                        DataTable tab2 = con.ReturningQuery("CALL getnamesurname_tc("+ rw.ItemArray[0].ToString() + ");");
                        foreach(DataGridViewRow dgvr in DGVstudent.Rows )
                        {
                            if(dgvr.Cells[0].Value.ToString()==tab2.Rows[0].ItemArray[0].ToString()&& dgvr.Cells[1].Value.ToString() == tab2.Rows[0].ItemArray[1].ToString())
                            {
                                suc1 = con.NonReturnQuery("UPDATE students_exams SET exam_grade="+dgvr.Cells[2].Value.ToString()+ " WHERE student_tc=" + rw.ItemArray[0].ToString() +" AND exam_name='"+ DGVexams.SelectedRows[0].Cells[0].Value.ToString() + "';");
                            }
                        }
                        
                    }
                    if(suc1)
                    {
                        MessageBox.Show("Changes Saved Succesfully", "Save Succcesfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        FMteacher fm = new FMteacher(mail);
                        fm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Changes Cannot Saved", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                this.Hide();
                FMteacher fm = new FMteacher(mail);
                fm.ShowDialog();
                this.Close();
            }
        }
    }
}


    
