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
    public partial class PharmacyView : Form
    {
        public PharmacyView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PharmacySearch newfrm = new PharmacySearch();
            newfrm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InventoryView frm1 = new InventoryView();
            frm1.Show();
        }

        private void PharmacyView_Load(object sender, EventArgs e)
        {

        }
    }
}
