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
    public partial class ViewPatient : Form
    {
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        readonly SqlSetup _sqlSetup = new SqlSetup();

        public ViewPatient()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            textBox1.Text = SearchPatient.SetMrNo.ToString();
            textBox2.Text = SearchPatient.SetPatientName;
            ds = _sqlSetup.GetData("Select Patient.Age as PAge, Patient.FatherOrHusband as PFa, Patient.CNIC as PCNIC, Patient.Address as PAdd, Patient.Cell_No as PCell, City.Name as CName, Province.Name as ProvName from Patient, City, Province where Patient.idCity = City.idCity and City.idProvince = Province.idProvince and Patient.Name like '" + textBox2.Text + "' and Patient.MR_No = " + Convert.ToInt32(textBox1.Text), false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox6.Text = ds.Tables[0].Rows[0]["PAge"].ToString();
                    textBox3.Text = ds.Tables[0].Rows[0]["PFa"].ToString();
                    textBox4.Text = ds.Tables[0].Rows[0]["PCNIC"].ToString();
                    textBox5.Text = ds.Tables[0].Rows[0]["PAdd"].ToString();
                    textBox8.Text = ds.Tables[0].Rows[0]["PCell"].ToString();
                    textBox7.Text = ds.Tables[0].Rows[0]["CName"].ToString();
                    comboBox1.Text = ds.Tables[0].Rows[0]["ProvName"].ToString();
                }
            }            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Province.Name as PName from Province, City where City.idProvince = Province.idProvince and City.Name like '" + textBox7.Text + "'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    comboBox1.Text = ds.Tables[0].Rows[0]["PName"].ToString();
                }
            }

        }

        private void ViewPatient_Load(object sender, EventArgs e)
        {
            Auto();
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
                    MessageBox.Show("City not found");
                }
            }
            textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.AutoCompleteCustomSource = coll;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double pv;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Invalid! Please enter all the information to proceed.");
               
            }
            else
            {
                if (!(Double.TryParse(textBox4.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv)) || !(Double.TryParse(textBox6.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv)) || !(Double.TryParse(textBox5.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv)))
                {
                    MessageBox.Show("Phone Number , CNIC, and age must only be integer values.");
                }

                else
                {
                    if (!(textBox5.TextLength == 11))
                    {
                        MessageBox.Show("Phone No. must be 11 digits.");
                    }

                    else
                    {
                        if (!(textBox4.TextLength == 13))
                        {
                            MessageBox.Show("CNIC must be 13 digits.");
                        }

                        else if (textBox4.TextLength == 13)
                        {
                            DataSet ds = new DataSet();
                            ds = _sqlSetup.GetData("select Name from City where Name = '" + textBox7.Text + "'", false, null);
                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    int update = _sqlSetup.SetData("Update Patient set idCity = (select idCity from City where Name like '" + textBox7.Text + "'), FatherOrHusband = '" + textBox3.Text + "', Age = " + Convert.ToInt32(textBox6.Text) + ", CNIC = '" + textBox4.Text + "', Address = '" + textBox8.Text + "', Cell_No = '" + textBox5.Text + "' where Patient.Name = '" + textBox2.Text + "' and Patient.MR_No = " + Convert.ToInt32(textBox1.Text), false, null);
                                    MessageBox.Show("Patient details updated! Please click the Close button to exit.");
                                }
                                else
                                {
                                    MessageBox.Show("Please enter a valid city from the options available");
                                }
                            }                            
                        }
                    }
                        
                }
                    
           
                
            }
        }
    }
}
