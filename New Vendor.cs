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
    public partial class New_Vendor : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public New_Vendor()
        {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void New_Vendor_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please insert vendor's name.");
            }
            else
            {
                DataSet ds = new DataSet();
                ds = _sqlSetup.GetData("select Name from Vendor where Name = '" + textBox1.Text + "'", false, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("This vendor already exists.");
                    }
                    else
                    {
                        int insert = _sqlSetup.SetData("Insert into Vendor(idVendor, Name) values " +
                            "((Select Top 1 idVendor from Vendor order by idVendor desc) +1,'"
                            + textBox1.Text + "')", false, null);
                        MessageBox.Show("Vendor Added.");
                        this.Close();
                        AddMedicineStock fm = new AddMedicineStock();
                        fm.Show();
                    }
                }

            }
        }
    }
}