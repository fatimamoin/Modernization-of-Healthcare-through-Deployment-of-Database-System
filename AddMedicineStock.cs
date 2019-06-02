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

    public partial class AddMedicineStock : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        public delegate void DoEvent();
        public event DoEvent RefreshDgv;
        public AddMedicineStock()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Auto()
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Name from Vendor where Name like '" + textBox1.Text + "%'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        coll.Add(ds.Tables[0].Rows[i]["Name"].ToString());
                    }

                }
            }
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = coll;

        }
        private void AddMedicineStock_Load(object sender, EventArgs e)
        {
            Auto();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please insert Vendor Name");
            }

            else if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Please insert quantity");
            }
            else
            {
                DataSet ds = new DataSet();
                ds = _sqlSetup.GetData("select Name from Vendor where Name = '" + textBox1.Text + "'", false, null);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string cmd;
                        if (textBox2.Text == "")
                        {

                            cmd = "insert into MedicineReceived(idMedicine, RecvdDate, ExpiryDate, QtyRcvd, idVendor) values" +
                                "(" + InventoryView.SetID + ", '" + dateTimePicker2.Value.ToShortDateString() + "', '" + dateTimePicker1.Value.ToShortDateString() + "', " + numericUpDown1.Value + ",(Select idVendor from Vendor where Name like '" + textBox1.Text + "'))";

                        }
                        else
                        {
                            cmd = "insert into MedicineReceived(idMedicine, RecvdDate, ExpiryDate, Comments, QtyRcvd, idVendor) values" +
                           "(" + InventoryView.SetID + ",'" + dateTimePicker2.Value.ToShortDateString() + "', '" + dateTimePicker1.Value.ToShortDateString() + "', '" +
                           textBox2.Text + "', " + numericUpDown1.Value + ",(Select idVendor from Vendor where Name like '" + textBox1.Text + "'))";

                        }

                        int insert = _sqlSetup.SetData(cmd, false, null);


                        int update = _sqlSetup.SetData("Update Medicine Set Quantity = Quantity + ((Select PcsPerPack from Medicine where idMedicine = " +
                            InventoryView.SetID + ") * " + numericUpDown1.Value +
                            " ) where idMedicine = " + InventoryView.SetID + " ", false, null);

                        MessageBox.Show("Stock Updated.");
                        this.RefreshDgv();
                        this.Close();

                    }
                    else
                    { MessageBox.Show("The vendor does not exist."); }
                }


            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            New_Vendor fm = new New_Vendor();
            fm.Show();
            this.Close();
        }
    }
}