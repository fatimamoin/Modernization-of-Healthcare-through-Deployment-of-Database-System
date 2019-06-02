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
    public partial class NewUser : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public NewUser()
        {
            InitializeComponent();
        }

        private void NewUser_Load(object sender, EventArgs e)
        {

        }

        private void Reg_Click_1(object sender, EventArgs e)
        {
            if (uname.Text == "" || pass.Text == "" || cpass.Text == "")
            {
                MessageBox.Show("Incomplete Information!");
            }
            else if (pass.Text.Length < 8)
            {
                MessageBox.Show("Password needs more characters");
            }
            else if (pass.Text != cpass.Text)
            {
                MessageBox.Show("Passwords don't match");
            }

            else
            {
                DataSet ds = new DataSet();
                ds = _sqlSetup.GetData("select username from Users where username ='" + uname.Text + "'", false, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        MessageBox.Show("A user already exists with these credentials. Please enter a different username and password.");

                    }
                    else
                    {
                        string command;

                        if (radioButton1.Checked == true)
                        {
                            command = "Insert into dbo.Users(username,pw,isDoctor,isReceptionist,isPharmacist,isAdmin) " +
                                "values( '" + uname.Text + "', '" + pass.Text + "', 0,0,1,0)";
                            int update = _sqlSetup.SetData(command, false, null);
                            MessageBox.Show("User Details Added!");
                            this.Close();

                        }
                        else if (radioButton2.Checked == true)
                        {
                            command = "Insert into dbo.Users(username,pw,isDoctor,isReceptionist,isPharmacist,isAdmin) " +
                                "values( '" + uname.Text + "', '" + pass.Text + "',  0,1,0,0)";
                            int update = _sqlSetup.SetData(command, false, null);
                            MessageBox.Show("User Details Added!");
                            this.Close();

                        }

                        else if (radioButton3.Checked == true)
                        {
                            command = "Insert into dbo.Users(username,pw,isDoctor,isReceptionist,isPharmacist,isAdmin) " +
                                "values( '" + uname.Text + "', '" + pass.Text + "',  0,0,0,1)";
                            int update = _sqlSetup.SetData(command, false, null);
                            MessageBox.Show("User Details Added!");
                            this.Close();

                        }
                        else if (radioButton4.Checked == true)
                        {
                            command = "Insert into dbo.Users(username,pw,isDoctor,isReceptionist,isPharmacist,isAdmin) " +
                                "values( '" + uname.Text + "', '" + pass.Text + "', 1,0,0,0)";

                            int add = _sqlSetup.SetData("Insert into dbo.Doctors(Name) values('" + uname.Text + "')", false, null);
                            int update = _sqlSetup.SetData(command, false, null);
                            MessageBox.Show("User Details Added!");
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Please select your position!");
                        }


                    }
                }

            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}