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
    public partial class IssuePrescription : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        public static string SetAppMed;
        public IssuePrescription()
        {
            InitializeComponent();          
            DisplayData();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Type.Name from Type where idType in (select Type from Medicine where Medicine.Name like '" + textBox4.Text + "')", false, null);
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Name";
                    comboBox1.DataSource = ds.Tables[0];
                }
            }
        }

        private void IssuePrescription_Load(object sender, EventArgs e)
        {
            Auto();
        }
        public void Auto()
        {

            DataSet ds = new DataSet();    
            ds = _sqlSetup.GetData("select Name from Medicine where Quantity > 0", false, null);
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        coll.Add(dt.Rows[i]["Name"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No medicine available by this name");
                }
                textBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox4.AutoCompleteCustomSource = coll;

            }
        }
        


        private void button1_Click_1(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Name from Medicine where Name like '" + textBox4.Text + "'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (numericUpDown1.Value == 0)
                    {
                        MessageBox.Show("The Quantity must be greater than 0");
                    }
                    else
                    {
                        DataSet ds1 = new DataSet();
                        ds1 = _sqlSetup.GetData("Select * from Appointment_has_Medicine", false, null);
                        if (ds1 != null)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                int insert = _sqlSetup.SetData("Insert into Appointment_has_Medicine(idAppMed, idMedicine,idAppointment, QtyIssued, Date)  values " +
                                    "((Select Top 1 idAppMed from Appointment_has_Medicine order by idAppMed desc) +1,(select idMedicine from Medicine where Name like '"
                            + textBox4.Text + "' and Type = (Select idType from Type where Name like '" + comboBox1.Text + "')), (Select idAppointment from Appointment where idAppointment = " + ConsultationForm.appt.ToString() + " and Date = '" +
                            ConsultationForm.date + "'), " + numericUpDown1.Value + ", (Select Date from Appointment where idAppointment = " + ConsultationForm.appt.ToString() + " and Date = '" +
                            ConsultationForm.date + "'))", false, null);
                                                          }
                            else
                            {
                                int insert = _sqlSetup.SetData("Insert into Appointment_has_Medicine(idAppMed, idMedicine, idAppointment, QtyIssued, Date)  values(1, (select idMedicine from Medicine where Name like '"
                             + textBox4.Text + "' and Type = (Select idType from Type where Name like '" + comboBox1.Text + "')), (Select idAppointment from Appointment where idAppointment = " + ConsultationForm.appt.ToString() + " and Date = '" +
                             ConsultationForm.date + "'), " + numericUpDown1.Value + ", (Select Date from Appointment where idAppointment = " + ConsultationForm.appt.ToString() + " and Date = '" +
                             ConsultationForm.date + "'))", false, null);
                              
                             MessageBox.Show("Dosage Added");
                            }
                        }                        
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid medicine from the options available");
                
                }
        

            }
            DisplayData();
        }

        private void DisplayData()
        {
            DataSet ds = new DataSet();            
            ds = _sqlSetup.GetData("Select idAppMed, Medicine.Name, QtyIssued, Appointment.Date from Appointment_has_Medicine join Appointment on Appointment.idAppointment = Appointment_has_Medicine.idAppointment join Medicine on " +
                "Medicine.idMedicine =  Appointment_has_Medicine.idMedicine where Mr_No ="
                + DoctorView.MR.ToString() + " and Appointment.idAppointment = " + ConsultationForm.appt.ToString() + " and Appointment.Date = '" + ConsultationForm.date.ToString() + "'", false, null);
            if (ds != null)
            {
                
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].Visible = false;

            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedCells.Count > 0)
            {
                SetAppMed = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                int update = _sqlSetup.SetData("Update Medicine Set Quantity = Quantity + " +
                    "(Select QtyIssued from Appointment_has_Medicine where idAppMed =" + SetAppMed + ") where idMedicine = (Select idMedicine from " +
                    "Appointment_has_Medicine where idAppMed =" + SetAppMed + ")", false, null);
                    int delete = _sqlSetup.SetData("Delete from Appointment_has_Medicine where idAppMed =" + SetAppMed + "", false, null);
                    MessageBox.Show("Dosage removed");

            }
            else
            {
                MessageBox.Show("Please select a medicine to remove.");
            }
            DisplayData();          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
