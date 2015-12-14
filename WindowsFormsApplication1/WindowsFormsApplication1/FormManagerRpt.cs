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
    public partial class FMreport : Form
    {
        string com;
        string mail;
        public FMreport(string comes,string logged)
        {
            com = comes;
            mail = logged;
            InitializeComponent();
        }

        private void FMrepnotes_Load(object sender, EventArgs e)
        {
            if(com== "Average Note")
            {
                DatabaseConnection con = new DatabaseConnection();
                DataTable tab = con.ReturningQuery("SELECT * FROM reportaverageexam");
                if (tab.TableName != "Connected but Empty")
                {
                    tab.Columns[0].ColumnName = "Teacher Name";
                    tab.Columns[1].ColumnName = "Teacher Surname";
                    tab.Columns[2].ColumnName = "Exam";
                    tab.Columns[3].ColumnName = "Lesson";
                    tab.Columns[4].ColumnName = "Class";
                    tab.Columns[5].ColumnName = "Average Grade";
                }
                DGV1.DataSource = tab;
            }
            else
            {
                DatabaseConnection con = new DatabaseConnection();
                DataTable tab = con.ReturningQuery("SELECT * FROM reportcountabsence");
                if (tab.TableName != "Connected but Empty")
                {
                    tab.Columns[0].ColumnName = "Teacher Name";
                    tab.Columns[1].ColumnName = "Teacher Surname";
                    tab.Columns[2].ColumnName = "Lesson";
                    tab.Columns[3].ColumnName = "Class";
                    tab.Columns[4].ColumnName = "Absence Count";
                }
                DGV1.DataSource = tab;
            }
        }

        private void BTback_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMyonetici fm = new FMyonetici(mail);
            fm.ShowDialog();
            this.Close();
        }
    }
}
