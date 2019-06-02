using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BBHU.DataClasses;

namespace BBHU
{
    public partial class Login : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void doctor_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            { MessageBox.Show("Information Missing!"); }

            string userid = textBox1.Text;
            string password = textBox2.Text;
            

            DataSet ds = new DataSet();
            
            ds = _sqlSetup.GetData("Select username, pw, isDoctor, isReceptionist, isAdmin, isPharmacist from Users where username='" + textBox1.Text + "'and pw='" + textBox2.Text + "'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["isDoctor"].ToString() == "True")
                    {
                        this.Hide();
                        DoctorView fm = new DoctorView();
                        fm.Show();

                    }

                    else if (ds.Tables[0].Rows[0]["isReceptionist"].ToString() == "True")
                    {
                        this.Hide();
                        ReceptionView fm = new ReceptionView();
                        fm.Show();

                    }

                    else if (ds.Tables[0].Rows[0]["isAdmin"].ToString() == "True")
                    {
                        this.Hide();
                        AdminView fm = new AdminView();
                        fm.Show();

                    }

                    else if (ds.Tables[0].Rows[0]["isPharmacist"].ToString() == "True")
                    {
                        this.Hide();
                        PharmacyView fm = new PharmacyView();
                        fm.Show();

                    }
                }
                else
                {
                    MessageBox.Show("Invalid Login. Please check username and password");
                }
            }
            else
            {
                MessageBox.Show("Invalid Login. Please check username and password");
            }


            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}