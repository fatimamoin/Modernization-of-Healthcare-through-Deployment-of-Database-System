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
    public partial class DoctorPresc : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public static int AppMed = 0;
        public DoctorPresc()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            dataGridView1.Enabled = false;
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select MR_No, Doctors.Name as DName from Appointment " +
                "join Doctors on Appointment.idDoctors = Doctors.idDoctors where idAppointment = " + ConsultationForm.appt + " and MR_No = " +
                DoctorView.MR + " and Date = '" + ConsultationForm.date + "'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox1.Text = ds.Tables[0].Rows[0]["MR_No"].ToString();
                    textBox2.Text = ds.Tables[0].Rows[0]["DName"].ToString();
                }
            }
            DisplayData();
        }

        private void DoctorPresc_Load(object sender, EventArgs e)
        {

        }

        private void DisplayData()
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select  issued, idAppMed, Medicine.Name, QtyIssued, Appointment.Date from Appointment_has_Medicine join Appointment on Appointment.idAppointment = Appointment_has_Medicine.idAppointment join Medicine on " +
                "Medicine.idMedicine =  Appointment_has_Medicine.idMedicine where Mr_No ="
                + DoctorView.MR.ToString() + " and Appointment.idAppointment = " + ConsultationForm.appt +
                "and Appointment.Date = '" + ConsultationForm.date + "'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[1].Visible = false;
                }
            }
        }

 

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

