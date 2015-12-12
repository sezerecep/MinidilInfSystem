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
    public partial class FMteacher : System.Windows.Forms.Form
    {
        public FMteacher(string loggedusermail)
        {
            InitializeComponent();
        }

        private void FMteacher_Load(object sender, EventArgs e)
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
    }
}
