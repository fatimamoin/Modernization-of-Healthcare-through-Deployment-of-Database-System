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
    public partial class ConsultationForm : Form
    {
        public static int appt = 0;
        public static string date = "";
        public static string doc = "";
        public static bool flag = false;
        readonly SqlSetup _sqlSetup = new SqlSetup();

        public ConsultationForm()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (!(dataGridView1.CurrentCell.Value is null))
                {
                    doc = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    date = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    appt = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    EditConsultationDetails newfrm = new EditConsultationDetails();
                    newfrm.Show();
                }
                else
                    MessageBox.Show("Please select a valid consultation to proceed.");
            }
            else
                MessageBox.Show("No consultations exist for this patient.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (!(dataGridView1.CurrentCell.Value is null))
                {
                    date = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    appt = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    doc = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    ViewConsultationDetails newfrm = new ViewConsultationDetails();
                    newfrm.Show();
                }
                else
                    MessageBox.Show("Please select a valid consultation to proceed.");
            }
            else
                MessageBox.Show("No consultations exist for this patient.");
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void ConsultationForm_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select idAppointment AS 'Appointment ID', Date AS 'Date', Doctors.Name AS 'Doctor', Investigation AS 'Diagnosis' from Appointment, Doctors where Doctors.idDoctors = Appointment.idDoctors and MR_No = " + DoctorView.MR, false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    dataGridView1.Columns[3].Width = 480;
                }

            }



        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }
    }
}
