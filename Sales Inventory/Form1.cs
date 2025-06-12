using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Sales_Inventory
{
    public partial class Form1 : Form
    {
        // Item class definition  
        public class Item
        {
            public string Name { get; set; }
            public string SKU { get; set; }
            public decimal CostPrice { get; set; }
            public decimal SellPrice { get; set; }
            public int CurrentStock { get; set; }
        }

        // In-memory item list  
        private List<Item> items = new List<Item>();

        // Declare the missing DataGridView control  
        private DataGridView dataGridView1;

        public Form1()
        {
            InitializeComponent();
            InitializeItemList();
        }

        private void InitializeItemList()
        {
            // Initialize the DataGridView control  
            dataGridView1 = new DataGridView
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(dataGridView1);

            // Setup DataGridView columns  
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("SKU", "SKU/Barcode");
            dataGridView1.Columns.Add("CostPrice", "Cost Price");
            dataGridView1.Columns.Add("SellPrice", "Sell Price");
            dataGridView1.Columns.Add("CurrentStock", "Current Stock");
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var item = new Item
            {
                Name = txtName.Text,
                SKU = txtSKU.Text,
                CostPrice = decimal.TryParse(txtCostPrice.Text, out var cp) ? cp : 0,
                SellPrice = decimal.TryParse(txtSellPrice.Text, out var sp) ? sp : 0,
                CurrentStock = int.TryParse(txtCurrentStock.Text, out var cs) ? cs : 0
            };
            items.Add(item);
            RefreshGrid();
            ClearInputs();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idx = dataGridView1.SelectedRows[0].Index;
                if (idx >= 0 && idx < items.Count)
                {
                    items[idx].Name = txtName.Text;
                    items[idx].SKU = txtSKU.Text;
                    items[idx].CostPrice = decimal.TryParse(txtCostPrice.Text, out var cp) ? cp : 0;
                    items[idx].SellPrice = decimal.TryParse(txtSellPrice.Text, out var sp) ? sp : 0;
                    items[idx].CurrentStock = int.TryParse(txtCurrentStock.Text, out var cs) ? cs : 0;
                    RefreshGrid();
                    ClearInputs();
                }
            }
        }

        private void RefreshGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in items)
            {
                dataGridView1.Rows.Add(item.Name, item.SKU, item.CostPrice, item.SellPrice, item.CurrentStock);
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtSKU.Clear();
            txtCostPrice.Clear();
            txtSellPrice.Clear();
            txtCurrentStock.Clear();
        }
    }
}
