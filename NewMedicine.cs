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
    public partial class NewMedicine : Form
    {
        readonly SqlSetup _sqlSetup = new SqlSetup();
        public NewMedicine()
        {
            InitializeComponent();
            DataSet ds = new DataSet();
            ds = _sqlSetup.GetData("Select Name from Type", false, null);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Name";

                    comboBox1.DataSource = ds.Tables[0];
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Invalid! Please enter all the information to proceed.");
            }


            else
            {

                if (textBox3.Enabled == true && textBox3.Text == "")
                {
                    MessageBox.Show("Invalid! Please enter all the information to proceed.");
                }

                else
                {
                    double pv;


                    if (textBox3.Enabled == true && !(Double.TryParse(textBox3.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out pv)))
                    {
                        MessageBox.Show("Quantity has to be an integer only.");
                    }

                    else
                    {
                        DataSet ds = new DataSet();
                        ds = _sqlSetup.GetData("Select * from Medicine where Name like '" + textBox1.Text + "' and Type = (Select idType from Type " +
                            "where Name like '" + comboBox1.Text + "')", false, null);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                MessageBox.Show("This medicine already exists.");
                            }
                        }


                        else
                        {
                            if (textBox3.Text == "")
                            {
                                int insert = _sqlSetup.SetData("Set Identity_insert Medicine ON Insert into Medicine(idMedicine, Name, GenericChemName, Quantity, PcsPerPack, Type) values (((Select Top 1 idMedicine from Medicine order by idMedicine desc)+1), '" +
                                textBox1.Text + "', '" + textBox2.Text + "' , 0 ,  0 , (Select idType from Type where Name like '" + comboBox1.Text + "')) ", false, null);
                                MessageBox.Show("Medicine Added.");
                                this.Close();
                                InventoryView newfrm = new InventoryView();
                                newfrm.Show();

                            }
                            else
                            {
                                int insert = _sqlSetup.SetData("Set Identity_insert Medicine ON Insert into Medicine(idMedicine, Name, GenericChemName, Quantity, PcsPerPack, Type) values (((Select Top 1 idMedicine from Medicine order by idMedicine desc)+1), '" +
                                textBox1.Text + "', '" + textBox2.Text + "' , 0 , '" + textBox3.Text + "' , (Select idType from Type where Name like '" + comboBox1.Text + "')) ", false, null);
                                MessageBox.Show("Medicine Added.");
                                this.Close();
                                InventoryView newfrm = new InventoryView();
                                newfrm.Show();

                            }
                        }


                    }
                }


            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Capsule" || comboBox1.Text == "Tablet")
            {
                textBox3.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
            }
        }

        private void NewMedicine_Load(object sender, EventArgs e)
        {

        }
    }
}