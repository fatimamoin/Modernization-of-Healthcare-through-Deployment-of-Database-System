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
    public partial class InventoryView : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public static string SetID;
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        public InventoryView()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select idMedicine, Medicine.Name, Quantity, Type.Name from Medicine " +
                "join Type on Medicine.Type = Type.idType", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    dataGridView1.DataSource = ds.Tables[0];
                }
            }

        }
        public void Auto()
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("select Name from Medicine", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        coll.Add(ds.Tables[0].Rows[i]["Name"].ToString());

                    }
                }
                else
                { MessageBox.Show("Medicine not found"); }
            }

            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = coll;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select idMedicine, Medicine.Name as 'Medicine', Quantity, Type.Name AS 'Type' from Medicine join Type on Medicine.Type = Type.idType Where Medicine.Name like '" + textBox1.Text + "%'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
        }

        private void InventoryView_Load(object sender, EventArgs e)
        {
            Auto();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewMedicine frm2 = new NewMedicine();
            frm2.Show();
            this.Hide();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SetID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ViewMedicine newfrm = new ViewMedicine();
            newfrm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            int insert = _sqlSetup.SetData("Begin Transaction Delete from MedicineReceived where idMedicine = " +
                SetID + " Delete from Appointment_has_Medicine where idMedicine = " + SetID + " Delete from Medicine where idMedicine = "
                + SetID + " Commit", false, null);

            MessageBox.Show("Medicine Deleted");

            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select idMedicine, Medicine.Name as 'Medicine', Quantity, Type.Name AS 'Type' from Medicine join Type on Medicine.Type = Type.idType Where Medicine.Name like '" + textBox1.Text + "%'", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }

            }
        }
    }
}