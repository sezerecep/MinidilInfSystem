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
    public partial class FMteacherswless : Form
    {
        string mail;
        public FMteacherswless(string logged)
        {
            InitializeComponent();
            mail = logged;
        }

        private void FMteacherswless_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = con.ReturningQuery("CALL tcfromemail ('" + mail + "')");
                DataTable tab1 = con.ReturningQuery("CALL getlessonsofteacher (" + tab.Rows[0].ItemArray[0].ToString() + ");");
                tab1.Columns[0].ColumnName = "Level";
                tab1.Columns[1].ColumnName = "Lesson";
                tab1.Columns[2].ColumnName = "Class";
                tab1.Columns[3].ColumnName = "Day";
                tab1.Columns[4].ColumnName = "Time";
                DGVlessons.DataSource = tab1;
            }
        }

        private void DGVlessons_SelectionChanged(object sender, EventArgs e)
        {
            int index = DGVlessons.CurrentCell.RowIndex;
            DataTable tab2 = new DataTable();
            DataColumn[] dcs = new DataColumn[] { };
            foreach (DataGridViewColumn c in DGVlessons.Columns)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = c.Name;
                dc.DataType = c.ValueType;
                tab2.Columns.Add(dc);
            }

            tab2 = (DataTable)(DGVlessons.DataSource);
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab;
                tab = con.ReturningQuery("CALL getstudentsoflessons('"+tab2.Rows[index].ItemArray[0].ToString()+"','" + tab2.Rows[index].ItemArray[1].ToString() +"','"+tab2.Rows[index].ItemArray[2].ToString() 
                    +"','"+ tab2.Rows[index].ItemArray[3].ToString() + "','" + tab2.Rows[index].ItemArray[4].ToString() + "');");
                if (tab.TableName != "Connected but Empty")
                {
                    tab.Columns[0].ColumnName = "Name";
                    tab.Columns[1].ColumnName = "Surname";
                }
                DGVstudent.DataSource = tab;
                if (tab.TableName != "Connected but Empty")
                {
                    DGVstudent.Columns[0].ReadOnly = true;
                    DGVstudent.Columns[1].ReadOnly = true;

                }
            }
        }

        private void BTback_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMteacher fm = new FMteacher(mail);
            fm.ShowDialog();
            this.Close();
        }
    }
}
