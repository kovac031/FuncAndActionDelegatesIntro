using DemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    // kao primjer za delegate ali na winformUI primjeru 

    public partial class Dashboard : Form
    {
        ShoppingCartModel cart = new ShoppingCartModel();

        public Dashboard()
        {
            InitializeComponent();
            PopulateCartWithDemoData();
        }

        private void PopulateCartWithDemoData()
        {
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 3.63M });
            cart.Items.Add(new ProductModel { ItemName = "Milk", Price = 2.95M });
            cart.Items.Add(new ProductModel { ItemName = "Strawberries", Price = 7.51M });
            cart.Items.Add(new ProductModel { ItemName = "Blueberries", Price = 8.84M });
        }

        // klikom na gumb Message Box vrti ovo:
        private void messageBoxDemoButton_Click(object sender, EventArgs e)
        {
            decimal total = cart.GenerateTotal(SubTotalAlert, CalculateLeveledDiscount, PrintOutDiscountAlert);

            MessageBox.Show($"The total is {total:C2}");
        }       

        // isto ali s lambdom za drugi gumb
        // iako, taj gumb popunjava prazna polja iznosima subTotal i total, a message je ostavljen prazan kao
        private void textBoxDemoButton_Click(object sender, EventArgs e)
        {
            decimal total = cart.GenerateTotal(
                (subTotal) => subTotalTextBox.Text = $"{subTotal:C2}",
                (products, subTotal) => subTotal - products.Count(),
                (message) => { }
                );
            totalTextBox.Text = $"{total:C2}";
        }

        // metode, ali osim sto su parametri za GenerateTotal metodu, ne vidim po cemu su delegati
        private void SubTotalAlert(decimal subTotal)
        {
            MessageBox.Show($"The subtotal is {subTotal:C2}");
        }
        private decimal CalculateLeveledDiscount(List<ProductModel> products, decimal subTotal)
        {
            return subTotal - products.Count();
        }
        private void PrintOutDiscountAlert(string message)
        {
            MessageBox.Show(message);
        }        
    }
}
