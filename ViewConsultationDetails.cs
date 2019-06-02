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
    public partial class ViewConsultationDetails : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public ViewConsultationDetails()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ConsultationDetails_Load(object sender, EventArgs e)
        {
                dateTimePicker1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                

            textBox2.Text = DoctorView.Patient;
            textBox3.Text = DoctorView.MR.ToString();
            textBox12.Text = ConsultationForm.appt.ToString();
            textBox13.Text = ConsultationForm.doc;
            dateTimePicker1.Text = ConsultationForm.date;

            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select PresentComplaints, Investigation, Heightf,Heighti, Weight, Temp, BP, Pulse, RespRate from Appointment, Doctors where Doctors.idDoctors = Appointment.idDoctors and Doctors.Name like '" + ConsultationForm.doc + "' and MR_No = " + DoctorView.MR.ToString() + "and Date = '" + ConsultationForm.date + "' and idAppointment = " + ConsultationForm.appt.ToString(), false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox1.Text = ds.Tables[0].Rows[0]["PresentComplaints"].ToString();
                    textBox4.Text = ds.Tables[0].Rows[0]["Investigation"].ToString();
                    comboBox1.Text = ds.Tables[0].Rows[0]["Heightf"].ToString();
                    comboBox2.Text = ds.Tables[0].Rows[0]["Heighti"].ToString();
                    textBox9.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
                    textBox8.Text = ds.Tables[0].Rows[0]["Temp"].ToString();
                    textBox7.Text = ds.Tables[0].Rows[0]["BP"].ToString();
                    textBox6.Text = ds.Tables[0].Rows[0]["Pulse"].ToString();
                    textBox5.Text = ds.Tables[0].Rows[0]["RespRate"].ToString();
                }

            }


        }


        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DoctorPresc frm1 = new DoctorPresc();
            frm1.Show();
        }
    }
}
