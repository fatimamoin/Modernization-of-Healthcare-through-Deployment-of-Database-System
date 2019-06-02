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
    public partial class AdminView : Form
    {
        public AdminView()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewUser frm1 = new NewUser();
            frm1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SearchPatient frm2 = new SearchPatient();
            frm2.Show();
            //disable add new appointment button
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InventoryView frm3 = new InventoryView();
            frm3.Show();
            //disable add new medicine button
        }

        private void AdminView_Load(object sender, EventArgs e)
        {

        }
    }
}
