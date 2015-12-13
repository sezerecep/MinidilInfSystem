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
    public partial class FMstdntshabs : Form
    {
        string mail = null;
        public FMstdntshabs(string loggedmail)
        {
            InitializeComponent();
            mail = loggedmail;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMstudent fm = new FMstudent(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void FMstdntshabs_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = con.ReturningQuery("CALL tcfromemail ('" + mail + "')");
                DataTable tab1 = con.ReturningQuery("CALL getstudentabsence (" + tab.Rows[0].ItemArray[0].ToString() + ");");
                tab1.Columns[0].ColumnName = "Lesson";
                tab1.Columns[1].ColumnName = "Class";
                tab1.Columns[2].ColumnName = "Date/Time";
                DGV1.DataSource = tab1;
            }
        }
    }
}
