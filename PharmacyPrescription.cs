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
    public partial class PharmacyPrescription : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public static int AppMed = 0;
        public PharmacyPrescription()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select MR_No, Doctors.Name as DName from Appointment " +
                "join Doctors on Appointment.idDoctors = Doctors.idDoctors where idAppointment = " + DosageHistory.SetApp + " and MR_No = " +
                PharmacySearch.SetMR + " and Date = '" + DosageHistory.SetDate + "'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox1.Text = ds.Tables[0].Rows[0]["MR_No"].ToString();
                    textBox2.Text = ds.Tables[0].Rows[0]["DName"].ToString();
                }
                else
                {
                    MessageBox.Show("No appointments found.");
                }
            }
            DisplayData();
        }

        private void PharmacyPrescription_Load(object sender, EventArgs e)
        {

        }

        private void DisplayData()
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select  issued, idAppMed, Medicine.Name, QtyIssued, Appointment.Date from Appointment_has_Medicine join Appointment on Appointment.idAppointment = Appointment_has_Medicine.idAppointment join Medicine on " +
                "Medicine.idMedicine =  Appointment_has_Medicine.idMedicine where Mr_No ="
                + PharmacySearch.SetMR.ToString() + " and Appointment.idAppointment = " + DosageHistory.SetApp +
                "and Appointment.Date = '" + DosageHistory.SetDate + "'", false, null);
            if (ds != null)
            {
                //if (ds.Tables[0].Rows.Count > 0)
                //{ }
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[1].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                AppMed = Convert.ToInt32(row.Cells[1].Value.ToString());
                bool isSelected = Convert.ToBoolean(row.Cells[0].Value);
                DataSet ds = new DataSet();
                ds = _sqlSetup.GetData("Select * from Appointment_has_Medicine where idAppMed = " + AppMed + " and issued = 1", false, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show(row.Cells[2].Value.ToString() + " has already been issued.");
                    }
                    else if (isSelected && ds.Tables[0].Rows.Count == 0)
                    {
                        DataSet ds2 = new DataSet();
                        ds2 = _sqlSetup.GetData("Select * from Medicine where Name like '" + row.Cells[2].Value.ToString() + "'and Quantity -" +
                            Convert.ToInt32(row.Cells[3].Value.ToString()) + "> 0", false, null);
                        if (ds2 != null)
                        {
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                int update = _sqlSetup.SetData("Update Medicine Set Quantity = Quantity -" +
                        Convert.ToInt32(row.Cells[3].Value.ToString()) +
                        " where idMedicine = (Select idMedicine from Medicine where Name like '"
                        + row.Cells[2].Value.ToString() + "')", false, null);


                                int update2 = _sqlSetup.SetData("Update Appointment_has_Medicine Set issued = 1 where idAppMed = " + AppMed + "", false, null);
                                MessageBox.Show("Medicine has been issued.");
                                this.Close();
                            }

                            else
                            {
                                MessageBox.Show("Not enough quantity in stock for " + dataGridView1.CurrentRow.Cells[2].Value.ToString());
                            }
                        }                     
                    }
                }
            }
        }
    }
}


