﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Star_Pharmacy
{
    public partial class addNewSuppliers : Form
    {

        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=pharmacy");

        

        public addNewSuppliers()
        {
            InitializeComponent();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlCon.con.Open();


            String gender = "";

            if(!(rd_btnMr.Checked || rd_btnMrs.Checked))
            {
                MessageBox.Show("Please specify the unchecked values.");
            }

            else if (rd_btnMr.Checked)
            {
                gender = "Mr. ";
            } else if (rd_btnMrs.Checked)
            {
                gender = "Mrs. ";
            }

            string mrOrMrs = gender  + txtContactPerson.Text;

            MySqlCommand cmd = new MySqlCommand(@"insert into pharmacy.supplierdetails (SupplierID,CompanyName,ContactPerson,ContactPersonTelephone,CompanyTelephone) values ('" + txtSupplierId.Text + "','" + txtCompanyName.Text + "','" +
                mrOrMrs + "','" + txtContactPersonTelephone.Text + "','" + txtCompanyTelphone.Text + "');", SqlCon.con);

            MySqlCommand cmd1 = new MySqlCommand(@"insert into pharmacy.supplier_transactions (PayingAmount,SupplierName,Date) values ('" + amnt.Text + "','" + txtCompanyName.Text + "','" +
               dtp.Value.ToString("yyyy-MM-dd") + "');", SqlCon.con);

            MySqlCommand cmd2 = new MySqlCommand("insert into pharmacy.credit_details (SupplierID,SupplierName,CreditAmount) values ('" + txtSupplierId.Text + "','" + txtCompanyName.Text + "','" + "0" + "');", SqlCon.con);

            try
            {
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Data Inserted!");
                    cmd2.ExecuteNonQuery();
                }

                else
                {
                    MessageBox.Show("Data Not Inserted!");
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            txtSupplierId.Text = "";
            txtContactPerson.Text = "";
            txtCompanyName.Text = "";
            txtContactPersonTelephone.Text = "";
            txtCompanyTelphone.Text = "";

            
            //You must get the same instance of supplier_details form and load the suppliers again (refresh it) here.
            SqlCon.con.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewSuppliers_Load(object sender, EventArgs e)
        {
            //Better if the txtSupplierID gets autofilled when the form loads. (Like auto incremented).

        }

        private void txtCompanyTelphone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
