using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using csharp_oop_ecommerce_basic.model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http.Headers;

namespace csharp_oop_ecommerce_basic
{
    public partial class Form1 : Form
    {
        private Cart carr;

        public Form1()
        {
            InitializeComponent();

            carr = EcommerceFactory.getSampleCart();

            setHeaderCarrView();
            updateCarrView();
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                Product p = new Product(EcommerceFactory.getProductID(), textBoxName.Text, textBoxManifacturer.Text, textBoxDescription.Text, float.Parse(textBoxPrice.Text));
                carr.Add(p);
                updateCarrView();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

            Product p = new Product(labelID.Text, textBoxName.Text, textBoxManifacturer.Text, textBoxDescription.Text, float.Parse(textBoxPrice.Text));
            if(carr.IndexOf(p)<0)
            {
                MessageBox.Show("ID prodotto non valido");
                return;
            }

            carr.Modify(p);
            updateCarrView();

        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Product p = new Product(labelID.Text, textBoxName.Text, textBoxManifacturer.Text, textBoxDescription.Text, float.Parse(textBoxPrice.Text));
            if (carr.IndexOf(p) < 0)
            {
                MessageBox.Show("Invalid ID");
                return;
            }

            carr.Remove(p);
            updateCarrView();
        }


        public ListViewItem GetFocusedItem()
        {

            if (list.FocusedItem == null) return null;
            int listIndex = list.FocusedItem.Index;
            return list.Items[listIndex];
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {

            ListViewItem i = GetFocusedItem();

            if (i == null) return;

            ShowCurrentItemOnLabels(i);

        }

        public Product GetProductFromItem(ListViewItem item)
        {
            Product p = new Product(item.SubItems[0].Text);
            
            int i = carr.IndexOf(p);
            if (i >= 0)
                return carr.Products[i];
            else
                return null;
        }

        public void ShowCurrentItemOnLabels(ListViewItem i)
        {

            Product p = GetProductFromItem(i);
            if(p== null) return;

            labelID.Text=p.Id;
            textBoxName.Text = p.Name;
            textBoxDescription.Text = p.Description;
            textBoxManifacturer.Text = p.Manufacturer;
            textBoxPrice.Text = ""+p.Price;

        }


        private void setHeaderCarrView()
        {
            list.View = View.Details;
            list.FullRowSelect = true;
            string[] intestazione = new string[] { "ID", "NOME", "PRODUTTORE", "DESCRIZIONE", "PREZZO" };

            for (int i = 0; i < intestazione.Length; i++)
            {
                list.Columns.Add(intestazione[i]);
            }
        }

        private void updateCarrView()
        {

            list.Items.Clear();
            list.View = View.Details;
            list.FullRowSelect = true;

            Product[] prodotti = carr.Products;

            for (int i = 0; i < prodotti.Length; i++)
            {
                ListViewItem item = new ListViewItem(prodotti[i].Id);
                item.SubItems.Add(prodotti[i].Name);
                item.SubItems.Add(prodotti[i].Manufacturer);
                item.SubItems.Add(prodotti[i].Description);
                item.SubItems.Add(""+prodotti[i].Price);
                list.Items.Add(item);
            }

            list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            list.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

    }
}
