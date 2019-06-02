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
using System.Drawing.Printing;
using BBHU.DataClasses;

namespace BBHU
{
    public partial class NewPatient : Form
    {
        
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        AutoCompleteStringCollection coll2 = new AutoCompleteStringCollection();
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public NewPatient()
        {
            InitializeComponent();
            dateTimePicker3.Format = DateTimePickerFormat.Time;
            dateTimePicker3.ShowUpDown = true;

            
        }
        
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        public void Auto()
        {
            DataSet ds2 = new DataSet();
            ds2 = _sqlSetup.GetData("select City.Name as CName from City", false, null);
            if (ds2 != null)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        coll.Add(ds2.Tables[0].Rows[i]["CName"].ToString());
                    }
                }
                else
                {
                    textBox7.Clear();
                    MessageBox.Show("City not found");
                }
            }
            textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.AutoCompleteCustomSource = coll;
        }
        public void Auto2()
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Name from Doctors", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        coll2.Add(ds.Tables[0].Rows[i]["Name"].ToString());
                    }
                }
                else
                {
                    textBox8.Clear();
                    MessageBox.Show("Doctor not found");
                }
            }
            textBox8.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox8.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox8.AutoCompleteCustomSource = coll2;
        }


        private void NewPatient_Load(object sender, EventArgs e)
        {
            Auto();
            Auto2();
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select Name from Province", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "PName";
                    comboBox1.DataSource = ds.Tables[0];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double pv;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || (radioButton1.Checked == false && radioButton2.Checked == false))
            {
                MessageBox.Show("Invalid! Please enter all the information to proceed.");
            }
            else
            {
                if (Double.TryParse(textBox3.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv) && Double.TryParse(textBox4.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv) && Double.TryParse(textBox6.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv) && textBox4.TextLength == 11 && textBox3.TextLength == 13)
                {
                    DataSet ds = new DataSet();
                    ds = _sqlSetup.GetData("select Name from City where Name = '" + textBox7.Text + "'", false, null);
                    if (ds != null)
                    {
                        if (!(ds.Tables[0].Rows.Count > 0))
                        {
                            MessageBox.Show("Please enter a valid city from the options available");
                        }
                        else
                        {
                            DataSet ds2 = new DataSet();
                            ds2 = _sqlSetup.GetData("Select top 1 MR_No from Patient", false, null);
                            if (ds2 != null)
                            {
                                if (!(ds2.Tables[0].Rows.Count > 0))
                                {
                                    int insert = _sqlSetup.SetData("Set Identity_insert Patient ON insert into Patient (MR_No, idCity, Name, FatherOrHusband, Age, CNIC, Address, Cell_No) values (" +
                                    "1, (select idCity from City where Name like '" + textBox7.Text + "'), '" + textBox1.Text + "', '" + textBox2.Text + "', " + Convert.ToInt32(textBox6.Text) + ", '" + textBox3.Text + "', '" + textBox5.Text + "', '" + textBox4.Text + "')", false, null);
                                }
                                else
                                {
                                    int insert = _sqlSetup.SetData("Set Identity_insert Patient ON insert into Patient (MR_No, idCity, Name, FatherOrHusband, Age, CNIC, Address, Cell_No) values (" +
                                    "(Select top 1 MR_No from Patient order by MR_No desc) + 1, (select idCity from City where Name like '" + textBox7.Text + "'), '" + textBox1.Text + "', '" + textBox2.Text + "', " + Convert.ToInt32(textBox6.Text) + ", '" + textBox3.Text + "', '" + textBox5.Text + "', '" + textBox4.Text + "')", false, null);

                                }

                            }

                            DataSet ds4 = new DataSet();
                            ds4 = _sqlSetup.GetData("Select top 1 idAppointment from Appointment where Date = '" + dateTimePicker2.Value.Date + "' order by idAppointment desc", false, null);
                            if (ds4 != null)
                            {
                                if (ds4.Tables[0].Rows.Count > 0) //if an appointment exists for that date
                                {
                                    DataSet ds6 = new DataSet();
                                    ds6 = _sqlSetup.GetData("select Name from Doctors where Name = '" + textBox8.Text + "'", false, null); 
                                    if (ds6 != null)
                                    {
                                        if (ds6.Tables[0].Rows.Count > 0) //if the doctor is correct
                                        {
                                            if (radioButton1.Checked == true) //if it is a consultation
                                            {
                                                int insert4 = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, isSurgery)  values " +
                                                "((Select top 1 idAppointment from Appointment where Date = '" + dateTimePicker2.Value.Date + "'order by idAppointment desc) +1," +
                                                " (select idDoctors from Doctors where Name like '" + textBox8.Text + "'), (select TOP 1 MR_No from Patient order by MR_No desc), '" + dateTimePicker2.Value.Date + "', '" + dateTimePicker3.Value.ToLongTimeString() + "', 0)", false, null);

                                                MessageBox.Show("Patient added and Appointment Scheduled!");
                                                this.Close();
                                            }
                                            else if (radioButton2.Checked == true)
                                            {
                                                int insert4 = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, isSurgery)  values " +
                                                "((Select top 1 idAppointment from Appointment where Date = '" + dateTimePicker2.Value.Date + "'order by idAppointment desc) +1," +
                                                " (select idDoctors from Doctors where Name like '" + textBox8.Text + "'), (select TOP 1 MR_No from Patient order by MR_No desc), '" + dateTimePicker2.Value.Date + "', '" + dateTimePicker3.Value.ToLongTimeString() + "', 1)", false, null);

                                                MessageBox.Show("Patient added and Appointment Scheduled!");
                                                this.Close();
                                            }

                                        }
                                        else
                                            MessageBox.Show("Please enter a valid doctor from the options available");
                                    }

                                }


                                else
                                {
                                    DataSet ds5 = new DataSet();
                                    ds5 = _sqlSetup.GetData("select Name from Doctors where Name = '" + textBox8.Text + "'", false, null);
                                    if (ds5 != null)
                                    {
                                        if (ds5.Tables[0].Rows.Count > 0)
                                        {
                                            if (radioButton1.Checked == true)
                                            {
                                                int insert3 = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, isSurgery) values (1, (select idDoctors from Doctors where Name like '" + textBox8.Text + "'), (select TOP 1 MR_No from Patient order by MR_No desc), '" + dateTimePicker2.Value.Date + "', '" + dateTimePicker3.Value.ToLongTimeString() + "', 0)", false, null);
                                                MessageBox.Show("Patient added and Appointment Scheduled!");
                                                this.Close();
                                            }
                                            else if (radioButton2.Checked == true)
                                            {
                                                int insert3 = _sqlSetup.SetData("insert into Appointment (idAppointment, idDoctors, MR_No, Date, Time, isSurgery) values (1, (select idDoctors from Doctors where Name like '" + textBox8.Text + "'), (select TOP 1 MR_No from Patient order by MR_No desc), '" + dateTimePicker2.Value.Date + "',  '" + dateTimePicker3.Value.ToLongTimeString() + "', 1)", false, null);
                                                MessageBox.Show("Patient added and Appointment Scheduled!");
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
                else
                    MessageBox.Show("CNIC must have 13 digits, Phone Number must have 11 digits. Age, CNIC and Phone Number must only be numeric.");
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}