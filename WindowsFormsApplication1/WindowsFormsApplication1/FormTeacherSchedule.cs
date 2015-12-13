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
    public partial class FMschdleteacher : Form
    {
        string mail;
        public FMschdleteacher(string logged)
        {
            InitializeComponent();
            mail = logged;
        }

        private void BTback_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMteacher fm = new FMteacher(mail);
            fm.ShowDialog();
            this.Close();
        }

        private void FMschdleteacher_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                DataTable tab = con.ReturningQuery("CALL tcfromemail ('" + mail + "')");
                DataTable tab1 = con.ReturningQuery("CALL getschedulesofteacher (" + tab.Rows[0].ItemArray[0].ToString() + ");");
                tab1.Columns[0].ColumnName = "Day";
                tab1.Columns[1].ColumnName = "Time";
                tab1.Columns[2].ColumnName = "Lesson";
                tab1.Columns[3].ColumnName = "Class";

                DGV1.DataSource = tab1;
            }
        }
    }
}
