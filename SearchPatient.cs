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
    public partial class SearchPatient : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public static string SetPatientName = "";
        public static int SetMrNo = 0;
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        public SearchPatient()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select MR_No, Patient.Name, FatherOrHusband from Patient", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchPatient_Load(object sender, EventArgs e)
        {
            Auto(); //calls the Auto function
        }
        public void Auto()
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Name from City", false, null);
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
                { MessageBox.Show("City not found"); }

            }
            textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox5.AutoCompleteCustomSource = coll;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (!(dataGridView1.CurrentCell.Value is null))
                {
                    SetPatientName = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    SetMrNo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    NewAppointment frm1 = new NewAppointment();
                    frm1.Show();
                }
                else
                    MessageBox.Show("Please select a patient to proceed.");
            }
            else
                MessageBox.Show("No patient exists for these details. Please enter a valid search criteria.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (!(dataGridView1.CurrentCell.Value is null))
                {
                    SetPatientName = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    SetMrNo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    ViewPatient frm6 = new ViewPatient();
                    frm6.Show();
                }
                else
                    MessageBox.Show("Please select a patient to proceed.");
            }
            else
                MessageBox.Show("No patient exists for these details. Please enter a valid search criteria.");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlDataAdapter da = new SqlDataAdapter();
            double pv;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
            {
                MessageBox.Show("Please enter a valid search criteria!");
            }
            if (textBox4.Text != "")  //When only Mr_No is inserted
            {
                if (Double.TryParse(textBox4.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv))
                    if (textBox4.TextLength < 8)
                    {
                        DataSet ds = new DataSet();
                        ds = _sqlSetup.GetData("Select MR_No, Name, FatherOrHusband from Patient where MR_No = "
                            + Convert.ToInt32(textBox4.Text) + "", false, null);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dataGridView1.DataSource = ds.Tables[0];
                            }
                        }
                    }

                    else
                        MessageBox.Show("Please enter a valid MR Number");
                else
                {
                    MessageBox.Show("MR Number must only have digits");
                }
            }
            else if (textBox2.Text != "") //When only CNIC is inserted
            {
                if (Double.TryParse(textBox2.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv))
                    if (textBox2.TextLength == 13)
                    {
                        DataSet ds = new DataSet();
                        ds = _sqlSetup.GetData("Select MR_No, Name, FatherOrHusband from Patient where CNIC = '" +
                            textBox2.Text + "'", false, null);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dataGridView1.DataSource = ds.Tables[0];
                            }
                        }
                    }
                    else
                        MessageBox.Show("CNIC must be 13 digits only");
                else
                {
                    MessageBox.Show("CNIC must only have digits");
                }

            }
            else if (textBox6.Text != "") //When only Cell_No is inserted
            {
                if (Double.TryParse(textBox6.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv))
                    if (textBox6.TextLength == 11)
                    {
                        DataSet ds = new DataSet();
                        ds = _sqlSetup.GetData("Select MR_No, Name, FatherOrHusband from Patient where Cell_No = '" + textBox6.Text
                            + "'"
                            , false, null);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dataGridView1.DataSource = ds.Tables[0];
                            }
                            else
                            {
                                MessageBox.Show("No Patient exists for these details.");
                            }

                        }
                    }
                    else
                        MessageBox.Show("Phone Number must be 11 digits only");
                else
                {
                    MessageBox.Show("Number must only have digits");
                }
            }
            else if (textBox1.Text != "") //when name is inserted
            {
                if (textBox3.Text != "")
                {
                    DataSet ds = new DataSet();
                    ds = _sqlSetup.GetData("Select MR_No, Name, FatherOrHusband from Patient where Name like '" + textBox1.Text +
                        "' and FatherOrHusband like '" + textBox3.Text + "'", false, null);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dataGridView1.DataSource = ds.Tables[0];
                        }
                        else
                        {
                            MessageBox.Show("No Patient exists for these details.");
                        }
                    }
                }
                else if (textBox5.Text != "")
                {
                    DataSet ds = new DataSet();
                    ds = _sqlSetup.GetData("Select MR_No, Name, FatherOrHusband from Patient where Name like '" +
                        textBox1.Text + "' and idCity in (select idCity from City where Name like '" + textBox5.Text + "')",
                        false, null);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dataGridView1.DataSource = ds.Tables[0];
                        }
                        else
                        {
                            MessageBox.Show("No Patient exists for these details.");
                        }
                    }
                }
                else if (textBox5.Text == "" & textBox3.Text == "")
                {
                    DataSet ds = new DataSet();
                    ds = _sqlSetup.GetData("Select MR_No, Name, FatherOrHusband from Patient where Name like '" +
                        textBox1.Text + "'", false, null);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dataGridView1.DataSource = ds.Tables[0];
                        }
                        else
                        {
                            MessageBox.Show("No Patient exists for these details.");
                        }
                    }
                }
            }
            else if (textBox5.Text != "") //When only City is inserted
            {
                DataSet ds = new DataSet();
                ds = _sqlSetup.GetData("Select MR_No, Name, FatherOrHusband from Patient where idCity in" +
                    " (select idCity from City where Name like '" + textBox5.Text + "')", false, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dataGridView1.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        MessageBox.Show("No Patient exists for these details.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please either search by MR_No, or by Cell_No, or by CNIC, or by the Patient Name with either City or Last Name.");
                
            }


        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}