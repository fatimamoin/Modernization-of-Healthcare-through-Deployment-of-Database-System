using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BBHU
{
    public partial class ReceptionView : Form
    {
        public ReceptionView()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewTodaysAppointment frm2 = new ViewTodaysAppointment();
            frm2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SearchPatient frm1 = new SearchPatient();
            frm1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewPatient frm5 = new NewPatient();
            frm5.Show();
        }

        private void ReceptionView_Load(object sender, EventArgs e)
        {

        }
    }
}
