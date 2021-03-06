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
    public partial class LookupItemAvailability : Form
    {
        private cashierForm parentCashierForm;
        private int logged_id;
        private string branch;

        private static LookupItemAvailability lookUpItemAvailability;
        public LookupItemAvailability(int logged_id, string branch)
        {
            this.logged_id = logged_id;
            this.branch = branch;
           
            InitializeComponent();
            ComboBoxPoductIDFill();
            ComboBoxSupplierFill();
            ComboBoxMedicalNameFill();
            ComboBOxBrandNameFill();
            selections(true);
        }

        public static LookupItemAvailability getCashierForm(int id, string branch)
        {
            if (lookUpItemAvailability == null)
            {
                lookUpItemAvailability = new LookupItemAvailability(id, branch);
                
            }
         
            return lookUpItemAvailability;

        }


        public void selections(bool activation)
        {
            cBoxBrandName.Checked = activation;
            cBoxExpiryDate.Checked = activation;
            cBoxInStockAmount.Checked = activation;
            //cBoxInStockAmount.Checked = activation;
            cBoxMedicalName.Checked = activation;
            cBoxProductID.Checked = activation;
            cBoxReoderLevel.Checked = activation;
            cBoxSupplierName.Checked = activation;
            cBoxUnitPrice.Checked = activation;
            radioButtonBothBranches.Checked = activation;
        }

        public void buttonDeactivate()
        {
            Button availabilityButton = this.btnShowAvailability;
            Button refreshButton = this.btnRefresh;
            
            refreshButton.Enabled = false;
            availabilityButton.Enabled = false;
            moreDetailActivation(false);
        }
        private void buttonActivate()
        {
            Button availabilityButton = this.btnShowAvailability;
            Button refreshButton = this.btnRefresh;

            refreshButton.Enabled = true;
            availabilityButton.Enabled = true;
        }

        void ComboBoxPoductIDFill()
        {
           string connectionString = "server=localhost;user id=root;database=pharmacy";
            string myQuery = "SELECT DISTINCT ProductID FROM pharmacy.inventory";
            MySqlConnection newConnnection = new MySqlConnection(connectionString);
            MySqlCommand newCommand = new MySqlCommand(myQuery, newConnnection);
            MySqlDataReader newDataReader;
            try
            {
                newConnnection.Open();
                newDataReader = newCommand.ExecuteReader();
                while (newDataReader.Read())
                {
                    string productID = newDataReader.GetInt16("ProductID").ToString();
                    cmbBoxProductID.Items.Add(productID);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void ComboBoxSupplierFill()
        {
            string connectionString = "server=localhost;user id=root;database=pharmacy";
            string myQuery = "SELECT DISTINCT supplier FROM pharmacy.inventory";
            MySqlConnection newConnnection = new MySqlConnection(connectionString);
            MySqlCommand newCommand = new MySqlCommand(myQuery, newConnnection);
            MySqlDataReader newDataReader;
            try
            {
                newConnnection.Open();
                newDataReader = newCommand.ExecuteReader();
                while (newDataReader.Read())
                {
                    string supplier = newDataReader.GetString("supplier");
                    cmbBoxSupplierName.Items.Add(supplier);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ComboBoxMedicalNameFill()
        {
            string connectionString = "server=localhost;user id=root;database=pharmacy";
            string myQuery = "SELECT DISTINCT medicalName FROM pharmacy.inventory";
            MySqlConnection newConnnection = new MySqlConnection(connectionString);
            MySqlCommand newCommand = new MySqlCommand(myQuery, newConnnection);
            MySqlDataReader newDataReader;
            try
            {
                newConnnection.Open();
                newDataReader = newCommand.ExecuteReader();
                while (newDataReader.Read())
                {
                    string medicalName = newDataReader.GetString("medicalName");
                    cmbBoxMedicalName.Items.Add(medicalName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ComboBOxBrandNameFill()
        {
            string connectionString = "server=localhost;user id=root;database=pharmacy";
            string myQuery = "SELECT DISTINCT brandname FROM pharmacy.inventory";
            MySqlConnection newConnnection = new MySqlConnection(connectionString);
            MySqlCommand newCommand = new MySqlCommand(myQuery, newConnnection);
            MySqlDataReader newDataReader;
            try
            {
                newConnnection.Open();
                newDataReader = newCommand.ExecuteReader();
                while (newDataReader.Read())
                {
                    string brandName = newDataReader.GetString("brandname");
                    cmbBoxBrandName.Items.Add(brandName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string setQuery()
        {
            string query = "SELECT ";
            if (cBoxBrandName.Checked)
            {
                query += " ,BrandName ";
            }
            if (cBoxExpiryDate.Checked)
            {
                query += " ,ExpiryDate ";
            }
            if (cBoxInStockAmount.Checked)
            {
                query += " ,InStock ";
            }
            if (cBoxMedicalName.Checked)
            {
                query += " ,MedicalName ";
            }
            if (cBoxProductID.Checked)
            {
                query += " ,ProductID ";
            }
            if (cBoxReoderLevel.Checked)
            {
                query += " ,Reorderlevel ";
            }
            if (cBoxSupplierName.Checked)
            {
                query += " ,Supplier ";
            }
            if (cBoxUnitPrice.Checked)
            {
                query += " ,UnitPrice ";
            }
            if (radioButtonBothBranches.Checked)
            {
                query += " ,Branch ";
            }
            if (radioButtonMelleriawa.Checked)
            {
                query += " ,Branch ";
            }
            if (radioButtonWelivita.Checked)
            {
                query += " ,Branch ";
            }
            query = query.Remove(query.IndexOf(","), 1);
            return query;
        }
        private string getQuery()
        {
            string myQuery = setQuery();
            //"SELECT InStock, UnitPrice, MedicalName, BrandName, Supplier, ExpiryDate FROM pharmacy.inventory WHERE AND InStock != 0 ";
            myQuery += " FROM pharmacy.inventory WHERE AND InStock != 0 ";

            if (cmbBoxProductID.Text.Length!=0)
            {
                myQuery += " AND ProductID = " + cmbBoxProductID.Text;
            }
            if (cmbBoxMedicalName.Text.Length != 0)
            {
                myQuery += " AND MedicalName = '" + cmbBoxMedicalName.Text+"'";
            }
            if (cmbBoxBrandName.Text.Length != 0)
            {
                myQuery += " AND BrandName = '" + cmbBoxBrandName.Text + "'";
            }
            if (cmbBoxSupplierName.Text.Length != 0)
            {
                myQuery += " AND Supplier = '" + cmbBoxSupplierName.Text + "'";
            }
            if (radioButtonBothBranches.Checked)
            {
                //myQuery += " AND Branch = '" + " + "'";
            }
            if (radioButtonMelleriawa.Checked)
            {
                myQuery += " AND Branch = '" +"Mulleriyawa"+ "'";
            }
            if (radioButtonWelivita.Checked)
            {
                myQuery += " AND Branch = '" + "Welivita" + "'";
            }
            myQuery = myQuery.Remove(myQuery.IndexOf("AND"), 4);

            //MessageBox.Show(myQuery,"MyQuery");
            return myQuery;
        }

        private void fillDataGridViewAvailability()
        {
            string connectionString = "server=localhost;user id=root;database=pharmacy";
            string myQuery = getQuery();
            MySqlConnection newConnnection = new MySqlConnection(connectionString);
            MySqlCommand newCommand = new MySqlCommand(myQuery, newConnnection);
            //MySqlDataReader newDataReader;
            try
            {
                MySqlDataAdapter myAdapter = new MySqlDataAdapter();
                myAdapter.SelectCommand = newCommand;
                DataTable myTable = new DataTable();
                myAdapter.Fill(myTable);
                BindingSource mySource = new BindingSource();
                mySource.DataSource = myTable;
                dataGridViewAvailabilityDetails.DataSource = mySource;
                myAdapter.Update(myTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Button myButton = this.btnShowAvailability;
            myButton.Enabled = false;

            Button refreshButton = this.btnRefresh;
            refreshButton.Enabled = true; ;

            ItemDetailActivation(false);
        }


        private void btnShowAvailability_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user id=root;database=pharmacy";
            string myQuery = getQuery();
            MySqlConnection newConnnection = new MySqlConnection(connectionString);
            MySqlCommand newCommand = new MySqlCommand(myQuery, newConnnection);
            MySqlDataReader newDataReader;
            Button showMoreBtn = this.btnShowMore;
            ComboBox brand = cmbBoxBrandName;
            ComboBox supplier = cmbBoxSupplierName;
            ComboBox medical = cmbBoxMedicalName;
            ComboBox product = cmbBoxProductID;
            
            brand.Enabled = false;
            supplier.Enabled = false;
            medical.Enabled = false;
            product.Enabled = false;


            try
            {
                newConnnection.Open();
                newDataReader = newCommand.ExecuteReader();
                if (newDataReader.Read())
                {
                    MessageBox.Show("Item(s) available", "Availability Message");
                    //string brandName = newDataReader.GetString("brandname");
                    //cmbBoxBrandName.Items.Add(brandName);
                    showMoreBtn.Enabled = true;
                    Button refreshBtn = this.btnRefresh;
                    refreshBtn.Enabled = true;
                    //refreshBtn.Enabled = true;
                    cBoxBrandName.Enabled = true;
                    cBoxExpiryDate.Enabled = true;
                    cBoxInStockAmount.Enabled = true;
                    //cBoxInStockAmount.Checked = true;
                    cBoxMedicalName.Enabled = true;
                    cBoxProductID.Enabled = true;
                    cBoxReoderLevel.Enabled = true;
                    cBoxSupplierName.Enabled = true;
                    cBoxUnitPrice.Enabled = true;
                    radioButtonBothBranches.Enabled = true;
                    radioButtonMelleriawa.Enabled = true;
                    radioButtonWelivita.Enabled = true;




                }
                else
                {
                    
                    MessageBox.Show("Item not available. Invalid Selection", "Availability Message");
                    Button refreshBtn = this.btnRefresh;
                    refreshBtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                bool checker = cmbBoxProductID.Text.ToString().All(char.IsNumber);// IsAllDigits(cmbBoxProductID.Text);
                if (!checker)
                {
                    MessageBox.Show("Wrong Primary ID. It should be an integer!!!", "Availablity Message");
                    btnRefresh.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error in Selected Data!!!");
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            parentCashierForm.parentButtonActivation(true);
            ItemDetailActivation(true);

            cmbBoxBrandName.Text = null;
            cmbBoxMedicalName.Text = null;
            cmbBoxProductID.Text = null;
            cmbBoxSupplierName.Text = null;
            dataGridViewAvailabilityDetails.DataSource = null;
            // dataGridViewAvailabilityDetails = new DataGridView();
            buttonDeactivate();

        }

        private void cmbBoxProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button myButton = this.btnShowAvailability;
            myButton.Enabled = true;

            Button showMore = this.btnShowMore;
            showMore.Enabled = false;


        }
        private void cmbBoxMedicalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button myButton = this.btnShowAvailability;
            myButton.Enabled = true;
            Button showMore = this.btnShowMore;
            showMore.Enabled = false;
        }
        private void cmbBoxSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button myButton = this.btnShowAvailability;
            myButton.Enabled = true;
            Button showMore = this.btnShowMore;
            showMore.Enabled = false;
        }
        private void cmbBoxBrandName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button myButton = this.btnShowAvailability;
            myButton.Enabled = true;
            Button showMore = this.btnShowMore;
            showMore.Enabled = false;
        }


        private void ItemDetailActivation(bool activation )
        {
            GroupBox itemGroup = grpBoxItemDetails;
            ComboBox brand = cmbBoxBrandName;
            ComboBox supplier = cmbBoxSupplierName;
            ComboBox medical = cmbBoxMedicalName;
            ComboBox product = cmbBoxProductID;

            brand.Enabled = activation;
            supplier.Enabled = activation;
            medical.Enabled = activation;
            product.Enabled = activation;
            itemGroup.Enabled = activation;
        }

        private void moreDetailActivation(bool activation)
        {
            Button btnShow = this.btnShowMore;
            DataGridView availabilityDataGridView = this.dataGridViewAvailabilityDetails;
            GroupBox availabilityGroup = this.grpBoxAvailabilityDetails;
            Button rfrsh = this.btnRefresh;


            rfrsh.Enabled = activation;
            availabilityGroup.Enabled = activation;
            availabilityDataGridView.Enabled = activation;
            btnShow.Enabled = activation;
            cBoxBrandName.Enabled = activation;
            cBoxExpiryDate.Enabled = activation;
            cBoxInStockAmount.Enabled = activation;
            //cBoxInStockAmount.Checked = activation;
            cBoxMedicalName.Enabled = activation;
            cBoxProductID.Enabled = activation;
            cBoxReoderLevel.Enabled = activation;
            cBoxSupplierName.Enabled = activation;
            cBoxUnitPrice.Enabled = activation;
            radioButtonBothBranches.Enabled = activation;
            radioButtonMelleriawa.Enabled = activation;
            radioButtonWelivita.Enabled = activation;
            

        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
             ItemDetailActivation(true);

             cmbBoxBrandName.Text = null;
             cmbBoxMedicalName.Text = null;
             cmbBoxProductID.Text = null;
             cmbBoxSupplierName.Text = null;
             dataGridViewAvailabilityDetails.DataSource = null;
            // dataGridViewAvailabilityDetails = new DataGridView();
             buttonDeactivate();
            
            
        }



        private void btnShowMore_Click(object sender, EventArgs e)
        {
            Button availabilityButton = this.btnShowAvailability;
            Button btnShow = this.btnShowMore;
            
            //Button refreshButton = this.btnRefresh;
            //DataGridView availabilityDataGridView = this.dataGridViewAvailabilityDetails;
            // GroupBox availabilityGroup = this.grpBoxAvailabilityDetails;

            //availabilityGroup.Enabled = true;
            //availabilityDataGridView.Enabled = true;
            //refreshButton.Enabled = true;
            moreDetailActivation(true);
            btnShow.Enabled = false;
            availabilityButton.Enabled = false;

            cBoxBrandName.Enabled = false;
            cBoxExpiryDate.Enabled = false;
            cBoxInStockAmount.Enabled = false;
            //cBoxInStockAmount.Checked = true;
            cBoxMedicalName.Enabled = false;
            cBoxProductID.Enabled = false;
            cBoxReoderLevel.Enabled = false;
            cBoxSupplierName.Enabled = false;
            cBoxUnitPrice.Enabled = false;
            radioButtonBothBranches.Enabled = false;
            radioButtonMelleriawa.Enabled = false;
            radioButtonWelivita.Enabled = false;


            fillDataGridViewAvailability();
        }

        public void setParentForm(cashierForm parentCashierForm)
        {
            this.parentCashierForm = parentCashierForm;
        }

        private void LookupItemAvailability_Load(object sender, EventArgs e)
        {

        }
    }
}
