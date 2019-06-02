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
    public partial class ViewMedicine : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        bool stock_added = false;
        public ViewMedicine()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            dataGridView1.DataSource = GetData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stock_added = true;
            AddMedicineStock frm1 = new AddMedicineStock();
            frm1.RefreshDgv += new AddMedicineStock.DoEvent(fm_RefreshDgv);

            frm1.Show();
        }
        private DataTable GetData()
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("(Select  Vendor.Name, RecvdDate, ExpiryDate, QtyRcvd, Comments from MedicineReceived join Vendor on " +
                "Vendor.idVendor = MedicineReceived.idVendor where idMedicine = " + InventoryView.SetID + ")", false, null);

            return ds.Tables[0];
        }

        void fm_RefreshDgv()
        {
            dataGridView1.DataSource = GetData();
            textBox4.Clear();
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select Quantity as qty from Medicine join Type on Medicine.Type = " +
                "Type.idType where idMedicine = " + Convert.ToInt32(InventoryView.SetID) + " ", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox4.Text = ds.Tables[0].Rows[0]["qty"].ToString();
                }
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ViewMedicine_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select Medicine.Name as med , GenericChemName as chem, Type.Name as type," +
                " PcsPerPack as pcs, Quantity as qty from " +
                "Medicine join Type on Medicine.Type = Type.idType where idMedicine = " +
                Convert.ToInt32(InventoryView.SetID) + " ", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textBox1.Text = ds.Tables[0].Rows[0]["med"].ToString();
                    textBox2.Text = ds.Tables[0].Rows[0]["chem"].ToString();
                    comboBox1.Text = ds.Tables[0].Rows[0]["type"].ToString();
                    textBox3.Text = ds.Tables[0].Rows[0]["pcs"].ToString();
                    textBox4.Text = ds.Tables[0].Rows[0]["qty"].ToString();

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            InventoryView newf = new InventoryView();
            newf.Show();
        }
    }
}