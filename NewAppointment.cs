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
    public partial class NewAppointment : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public NewAppointment()
        {
            InitializeComponent();
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            textBox2.Text = SearchPatient.SetMrNo.ToString();
            textBox1.Text = SearchPatient.SetPatientName;
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select Age as age from Patient where Name like '" + textBox1.Text + "' and MR_No = " + textBox2.Text + "", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox4.Text = ds.Tables[0].Rows[0]["age"].ToString();
                }
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewAppointment_Load(object sender, EventArgs e)
        {
            Auto2();
            

        }
        public void Auto2()
        {
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Name from Doctors", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        coll.Add(ds.Tables[0].Rows[i]["Name"].ToString());
                    }

                }
                else
                {
                    MessageBox.Show("Doctor not found");
                }

            }

            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox3.AutoCompleteCustomSource = coll;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false))
            {
                MessageBox.Show("Invalid! Please enter all the information to proceed.");
            }
            else
            {
                DataSet ds = new DataSet();
                ds = _sqlSetup.GetData("Select top 1 idAppointment from Appointment where Date = '" +
                    dateTimePicker1.Value.Date + "' order by idAppointment desc", false, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet ds1 = new DataSet();
                        ds1 = _sqlSetup.GetData("select Name from Doctors where Name = '" + textBox3.Text + "'", false, null);
                        if (ds1 != null)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                if (radioButton1.Checked == true)
                                {

                                    
                                    int insert = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, " +
                                       "isSurgery) values ((Select top 1 idAppointment from Appointment where Date = '" + dateTimePicker1.Value.Date + "' order by idAppointment desc) +1 , " +
                                       "(select idDoctors from Doctors where Name like '" + textBox3.Text + "'), " +
                                       "(select MR_No from Patient where MR_No = " + Convert.ToInt32(textBox2.Text) + "),'" + dateTimePicker1.Value.Date +
                                       "','" + dateTimePicker2.Value.ToLongTimeString() + "', 0)", false, null);


                                    MessageBox.Show("Appointment Scheduled!");
                                    this.Close();
                                }
                                else if (radioButton2.Checked == true)
                                {
                                    int insert = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, " +
                                     "isSurgery) values ((Select top 1 idAppointment from Appointment where Date = '" + dateTimePicker1.Value.Date + "' order by idAppointment desc) +1 , " +
                                     "(select idDoctors from Doctors where Name like '" + textBox3.Text + "'), " +
                                     "(select MR_No from Patient where MR_No = " + Convert.ToInt32(textBox2.Text) + "),'" + dateTimePicker1.Value.Date +
                                     "','" + dateTimePicker2.Value.ToLongTimeString() + "', 1)", false, null);

                                    MessageBox.Show("Appointment Scheduled!");
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid doctor from the options available");

                            }
                        }
                    }
                    else
                    {
                        DataSet ds2 = new DataSet();
                        ds2 = _sqlSetup.GetData("select Name from Doctors where Name = '" + textBox3.Text + "'  ", false, null);
                        if (ds2 != null)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                if (radioButton1.Checked == true)
                                {
                                    int insert = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, " +
                                          "isSurgery) values (1, (select idDoctors from Doctors where Name like '" + textBox3.Text + "'), " +
                                          "(select MR_No from Patient where MR_No = " + Convert.ToInt32(textBox2.Text) + "),'" + dateTimePicker1.Value.Date +
                                          "','" + dateTimePicker2.Value.ToLongTimeString() + "', 0)", false, null);
                                    MessageBox.Show("Appointment Scheduled!");
                                    this.Close();
                                }
                                else if (radioButton2.Checked == true)
                                {
                                    int insert = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, " +
                                          "isSurgery) values (1, (select idDoctors from Doctors where Name like '" + textBox3.Text + "'), " +
                                          "(select MR_No from Patient where MR_No = " + Convert.ToInt32(textBox2.Text) + "), '" + dateTimePicker1.Value.Date +
                                          "','" + dateTimePicker2.Value.ToLongTimeString() + "', 1)", false, null);
                                    MessageBox.Show("Appointment Scheduled!");
                                    this.Close();
                                }

                            }

                            else
                            {
                                MessageBox.Show("Please enter a valid doctor from the options available");
                            }
                        }
                    }
                }
            }
        }
    }
}