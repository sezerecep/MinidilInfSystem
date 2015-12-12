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
    public partial class FMforgotpass : Form
    {
        string question = null;
        string answer = null;
        string email = null;
        public FMforgotpass()
        {
            InitializeComponent();
        }

        private void BTback_Click(object sender, EventArgs e)
        {
            this.Hide();
            FMlogin form2 = new FMlogin();
            form2.ShowDialog();
            this.Close();
        }

        private void BTnext_Click(object sender, EventArgs e)
        {
            DatabaseConnection con = new DatabaseConnection();
            if (label1.Text != "Answer :")
            {
                if (TBemail.Text == "")
                {
                    MessageBox.Show("Please Enter Your E-mail", "Forgot Password Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                   

                    // E mailin kontrolü yapılacak

                    //Burada veritabanınadan e maile göre reset sorusu ve cevabı çekilecek question ve answera yazılacak

                    label2.Text = question;
                    label1.Text = "Answer :";
                    pictureBox2.Hide();


                }
            }
            else
            {
                //Burada sorunun cevabı kontrol edilecek
                //if (Cevap doğru)
                
                this.Hide();
                FMresetpass fm = new FMresetpass(email);
                fm.ShowDialog();
                this.Close();
                //else 
               // MessageBox.Show("Password Reset Answer is Wrong", "Forgot PAssword Attempt Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.closeConnection();
        }

        
    }
}