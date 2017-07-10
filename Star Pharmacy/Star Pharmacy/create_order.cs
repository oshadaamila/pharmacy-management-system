﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Star_Pharmacy
{
    public partial class create_order : Form
    {
        public DataTable dt = new DataTable();
        private static create_order inst4;

        public static create_order getCreateOrder(SplitContainer s, Form f)
        {
            if (inst4 == null || inst4.IsDisposed)
            {
                inst4 = new create_order();
                
                inst4.MdiParent = f;
                s.Panel2.Controls.Add(inst4);
                return inst4;
            }
            else
            {
                return inst4;
            }
        }
        public create_order()
        {
            InitializeComponent();
        }

        private void create_order_Load(object sender, EventArgs e)
        {
            load_dgv1();
            
        }
        private void load_dgv1()
        {
            String query = "Select ProductID,BrandName,MedicalName,UnitPrice,ExpiryDate,InStock from pharmacy.inventory;";
            SqlCon.updateDataGridView(query, dataGridView1);
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0)
            {
                MySqlDataAdapter sAdapter2 = new MySqlDataAdapter("Select ProductID,BrandName,MedicalName,UnitPrice,ExpiryDate,InStock from pharmacy.inventory where ProductID like '" + numericUpDown1.Value.ToString() + "%" + "';", SqlCon.con);
                DataTable dt2 = new DataTable();
                sAdapter2.Fill(dt2);
                dataGridView1.DataSource = dt2;
                dataGridView1.Update();
                dataGridView1.Refresh();
            }
            else
            {
                String query = "Select ProductID,BrandName,MedicalName,UnitPrice,ExpiryDate,InStock from pharmacy.inventory;";
                SqlCon.updateDataGridView(query, dataGridView1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MySqlDataAdapter sAdapter3 = new MySqlDataAdapter("Select ProductID,BrandName,MedicalName,UnitPrice,ExpiryDate,InStock from pharmacy.inventory where BrandName like '" + "%" + textBox1.Text + "%" + "';", SqlCon.con);
            DataTable dt3 = new DataTable();
            sAdapter3.Fill(dt3);
            dataGridView1.DataSource = dt3;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            MySqlDataAdapter sAdapter4 = new MySqlDataAdapter("Select ProductID,BrandName,MedicalName,UnitPrice,ExpiryDate,InStock from pharmacy.inventory where MedicalName like '" + "%" + textBox2.Text + "%" + "';", SqlCon.con);
            DataTable dt4 = new DataTable();
            sAdapter4.Fill(dt4);
            dataGridView1.DataSource = dt4;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void numericUpDown1_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown1.ResetText();
            textBox2.Text = null;
        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown1.ResetText();
            textBox1.Text = null;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private int selectedRowIndex;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
        }
        private decimal total;
        private void button1_Click(object sender, EventArgs e)
        {
            int newInstock;
            int oldStock;
            oldStock = System.Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[5].Value);
            newInstock = oldStock - System.Convert.ToInt32(numericUpDown2.Value);
            if (numericUpDown2.Value == 0)
            {
                MessageBox.Show("Enter a quantity!");
            }
            else if (newInstock<0)
            {
                MessageBox.Show("Low Stock!");
            }
            else
            {
                String unitPrice, ProductName, Quantity, Amount,productId;
                
                productId = dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString();
                ProductName = dataGridView1.Rows[selectedRowIndex].Cells[2].Value.ToString();
                unitPrice = dataGridView1.Rows[selectedRowIndex].Cells[3].Value.ToString();
                Quantity = numericUpDown2.Value.ToString();
                Amount = (System.Convert.ToDecimal(unitPrice) * numericUpDown2.Value).ToString();
                dataGridView2.Rows.Add(new String [] {productId,ProductName,unitPrice,Quantity,Amount});
                total = total + System.Convert.ToDecimal(Amount);
                total_lbl.Text = total.ToString();

                SqlCon.con.Open();
                MySqlCommand com = new MySqlCommand("update pharmacy.inventory  set InStock = '" + newInstock.ToString() + "' where ProductID = '" + productId + "' ;", SqlCon.con);
                com.ExecuteNonQuery();
                SqlCon.con.Close();
                load_dgv1();
                MySqlDataAdapter sAdapter5 = new MySqlDataAdapter("Select ProductID,BrandName,MedicalName,UnitPrice,ExpiryDate,InStock from pharmacy.inventory;", SqlCon.con);
                DataTable dt5 = new DataTable();
                sAdapter5.Fill(dt5);
                dataGridView1.DataSource = dt5;
                dataGridView1.Refresh();

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int selIndex = dataGridView2.CurrentCell.RowIndex;
                int newStock, oldStock;
                MySqlDataAdapter sda = new MySqlDataAdapter("select InStock from pharmacy.inventory where ProductID='" + dataGridView2.Rows[selIndex].Cells[0].Value.ToString() + "'; ", SqlCon.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 1)
                {
                    MessageBox.Show("Error! Check inventory for duplicate entries!");
                }
                else
                {
                    oldStock = System.Convert.ToInt32(dt.Rows[0][0].ToString());
                    newStock = oldStock + System.Convert.ToInt32(dataGridView2.Rows[selIndex].Cells[3].Value.ToString());
                    SqlCon.con.Open();
                    MySqlCommand com = new MySqlCommand("update pharmacy.inventory  set InStock = '" + newStock.ToString() + "' where ProductID = '" + dataGridView2.Rows[selIndex].Cells[0].Value.ToString() + "' ;", SqlCon.con);
                    com.ExecuteNonQuery();
                    SqlCon.con.Close();
                    MySqlDataAdapter sAdapter5 = new MySqlDataAdapter("Select ProductID,BrandName,MedicalName,UnitPrice,ExpiryDate,InStock from pharmacy.inventory;", SqlCon.con);
                    DataTable dt5 = new DataTable();
                    sAdapter5.Fill(dt5);
                    dataGridView1.DataSource = dt5;
                    dataGridView1.Refresh();
                    total = total - System.Convert.ToDecimal(dataGridView2.Rows[selIndex].Cells[4].Value.ToString());
                    total_lbl.Text = total.ToString();
                    dataGridView2.Rows.RemoveAt(selIndex);

                }
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("No Items to remove", "No Items", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            while (dataGridView2.Rows.Count > 0)
            {
                button4_Click(sender, e);
            }
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            change_lbl.Text = (numericUpDown3.Value - Convert.ToDecimal(total_lbl.Text)).ToString();
        }

       

        

    }
}
