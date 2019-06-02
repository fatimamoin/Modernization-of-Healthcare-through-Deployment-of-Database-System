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
    public partial class DoctorView : Form
    {
        public static string Patient = "";
        public static int MR = 0;
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public DoctorView()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            
            ds = _sqlSetup.GetData("Select idAppointment AS 'Appointment ID', Appointment.MR_No, Patient.Name AS 'Patient Name', Doctors.[Name] AS 'Doctor', Convert( varchar, Time, 8) as 'Time' from Appointment join Patient on Appointment.MR_No = Patient.MR_No join Doctors on Appointment.idDoctors = Doctors.idDoctors where Convert(varchar, Date, 6) = Convert(varchar, GETDATE(), 6)  order by Time", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                { }

            }
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (!(dataGridView1.CurrentCell.Value is null))
                {
                    Patient = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    MR = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    ConsultationForm frm1 = new ConsultationForm();
                    frm1.Show();
                }
                else
                    MessageBox.Show("Please select a patient to proceed.");
            }
            else
                MessageBox.Show("No patient exists for these details. Please enter a valid search criteria.");
        }

        
        private void DoctorView_Load(object sender, EventArgs e)
        {
            
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
