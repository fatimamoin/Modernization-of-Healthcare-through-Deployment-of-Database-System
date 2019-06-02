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
    public partial class ViewTodaysAppointment : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public ViewTodaysAppointment()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ViewTodaysAppointment_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select idAppointment, Appointment.MR_No, Patient.Name as 'Patient', Doctors.[Name] as 'Doctor', " +
                "Convert( varchar, Time, 8) as 'Time' from Appointment join Patient on Appointment.MR_No =" +
                " Patient.MR_No join Doctors on Appointment.idDoctors = Doctors.idDoctors where " +
                "Convert(varchar, Date, 6) = Convert(varchar, GETDATE(), 6)  order by Time", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}