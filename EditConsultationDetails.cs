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
    public partial class EditConsultationDetails : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public EditConsultationDetails()
        {
            InitializeComponent();
        }

        private void EditConsultationDetails_Load(object sender, EventArgs e)
        {
            //Adding feet and inches to height
            comboBox2.Items.Add(1);
            comboBox2.Items.Add(2);
            comboBox2.Items.Add(3);
            comboBox2.Items.Add(4);
            comboBox2.Items.Add(5);
            comboBox2.Items.Add(6);
            comboBox2.Items.Add(7);
            comboBox2.Items.Add(8);
            comboBox2.Items.Add(9);
            comboBox2.Items.Add(10);
            comboBox2.Items.Add(11);
            comboBox1.Items.Add(1);
            comboBox1.Items.Add(2);
            comboBox1.Items.Add(3);
            comboBox1.Items.Add(4);
            comboBox1.Items.Add(5);
            comboBox1.Items.Add(6);

            textBox2.Text = DoctorView.Patient;
            textBox3.Text = DoctorView.MR.ToString();
            textBox12.Text = ConsultationForm.appt.ToString();
            textBox13.Text = ConsultationForm.doc;
            dateTimePicker1.Text = ConsultationForm.date;
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select PresentComplaints, Investigation, Heightf, Heighti, Weight, Temp, BP, Pulse, RespRate from Appointment, Doctors where Doctors.idDoctors = Appointment.idDoctors and Doctors.Name like '" + ConsultationForm.doc +"' and MR_No = " + DoctorView.MR.ToString() + " and Date = '" + ConsultationForm.date + "' and idAppointment = " + ConsultationForm.appt.ToString(), false, null);
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

        private void button1_Click(object sender, EventArgs e)
        {
            double pv;
            //when no details are mentioned and save is pressed
            if (comboBox1.Text == "" && comboBox2.Text == "" && textBox9.Text == "" && textBox8.Text == "" && textBox7.Text == "" && textBox6.Text == "" && textBox5.Text == "" && textBox1.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Please enter details to proceed.");
            }
            //when only vitals are being filled out
            else if (textBox1.Text == "" && textBox4.Text == "")
            {
                if (Double.TryParse(comboBox1.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv) && Double.TryParse(textBox9.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv) && Double.TryParse(textBox8.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv))
                {
                    int update = _sqlSetup.SetData("Update Appointment set Heightf = " + Convert.ToDouble(comboBox1.Text) + ", Heighti = " + Convert.ToDouble(comboBox2.Text) + ", Weight = " + Convert.ToDouble(textBox9.Text) + ", Temp = " + Convert.ToDouble(textBox8.Text) + ", BP = '" + textBox7.Text + "', Pulse = '" + textBox6.Text + "', RespRate = '" + textBox5.Text + "' where idDoctors in (select idDoctors from Doctors where Name like '" + ConsultationForm.doc + "') and MR_No = " + DoctorView.MR.ToString() + " and Date = '" + ConsultationForm.date + "' and idAppointment = " + ConsultationForm.appt.ToString(), false, null);
                    MessageBox.Show("Changes saved!");
                }
                else
                    MessageBox.Show("Height, Weight and Temp must be numbers");
            }
            //when consulting doctors enters all data
            else
            {
                if (Double.TryParse(comboBox1.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv) && Double.TryParse(textBox9.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv) && Double.TryParse(textBox8.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv))
                {
                    int update2 = _sqlSetup.SetData("Update Appointment set Investigation = '" + textBox4.Text + "', PresentComplaints = '" + textBox1.Text + "', Heightf = " + Convert.ToDouble(comboBox1.Text) + ", Heighti = " + Convert.ToDouble(comboBox2.Text) + ", Weight = " + Convert.ToDouble(textBox9.Text) + ", Temp = " + Convert.ToDouble(textBox8.Text) + ", BP = '" + textBox7.Text + "', Pulse = '" + textBox6.Text + "', RespRate = '" + textBox5.Text + "' where idDoctors in (select idDoctors from Doctors where Name like '" + ConsultationForm.doc + "') and MR_No = " + DoctorView.MR.ToString() + " and Date = '" + ConsultationForm.date + "' and idAppointment = " + ConsultationForm.appt.ToString(), false, null);
                    MessageBox.Show("Changes saved!");
                }                 
                else
                    MessageBox.Show("Height, Weight and Temp must be numbers");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            IssuePrescription frm = new IssuePrescription();
            frm.Show();
        }
    }
}
