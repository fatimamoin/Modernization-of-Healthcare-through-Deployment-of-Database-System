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
    public partial class DosageHistory : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        SqlConnection cnn = new SqlConnection(@"Data Source=192.168.0.100;Initial Catalog=BBHU;User ID=sa;Password =Hello4366@;Integrated Security=False");
        public static string SetApp;
        public static string SetDate;
        public DosageHistory()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select idAppointment, Name, Date  from Appointment join Doctors on " +
                "Doctors.idDoctors = Appointment.idDoctors where idAppointment like '" + textBox1.Text +
                "%' and MR_No = " + PharmacySearch.SetMR + " and Datepart(dayofyear,Date) = " +
                "DatePart(dayofyear,GetDate())", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }

        }

        private void DosageHistory_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select idAppointment from Appointment where MR_No = " + PharmacySearch.SetMR + " ", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        coll.Add(ds.Tables[0].Rows[i]["idAppointment"].ToString());
                    }

                }
            }

            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = coll;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select idAppointment, Name, Date  from Appointment join Doctors on " +
                "Doctors.idDoctors = Appointment.idDoctors where idAppointment like '" + textBox1.Text
                + "%' and MR_No = " + PharmacySearch.SetMR + "and Datepart(dayofyear, Date) = " +
                "DatePart(dayofyear, GetDate())", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (!(dataGridView1.CurrentCell.Value is null))
                {
                    SetApp = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    SetDate = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    PharmacyPrescription frm = new PharmacyPrescription();
                    frm.Show();
                }
                else
                    MessageBox.Show("Please select a patient to proceed.");
            }
            else
                MessageBox.Show("No patient exists for these details. Please enter a valid search criteria.");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}