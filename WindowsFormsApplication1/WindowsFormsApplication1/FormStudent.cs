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
    public partial class FMstudent : System.Windows.Forms.Form
    {
        public FMstudent(string loggedusermail)
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (con.is_Connected())
            {
                //Tc ye göre kullanıcı ismi getirilip aşağıdaki değişkene atılacak
                string loggedusername = null;
                this.label2.Text = loggedusername;
            }
            else
            {
                MessageBox.Show("Connection to Database Failed", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                FMlogin fm = new FMlogin();
                fm.ShowDialog();
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
