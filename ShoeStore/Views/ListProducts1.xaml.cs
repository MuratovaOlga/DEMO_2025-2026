using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для ListProducts1.xaml
    /// </summary>
    public partial class ListProducts1 : Window
    {
        public List<Product> ListProduct { get; set; }

        public List<string> SuppliersList { get; set; }
        public ListProducts1()
        {
            InitializeComponent();
            SuppliersList = new List<string>();
            SuppliersList.Add("Все поставщики");
            using (ShoeStoreDbContext db = new ShoeStoreDbContext())
            {
                ListProduct = db.Products
                    .Include(c=>c.IdProductCategoryNavigation)
                    .Include(m=>m.IdManufacturerNavigation)
                    .Include(s=>s.IdSupplierNavigation)
                    .ToList();

                var list = db.Suppliers.ToList();
                foreach (var item in list)
                {
                    SuppliersList.Add(item.NameSupplier.Replace("\r\n", ""));
                }
            }

            

            foreach(var item in ListProduct)
            {
                item.CleanField();
            }

            drawinProduct(ListProduct);


            DataContext = this;
        }


        private void drawinProduct(List<Product> _products)
        {
            listProduct.Items.Clear();

            foreach (var item in _products)
            {
                listProduct.Items.Add(new ProductCard1(item));
            }
            listProduct.Items.Refresh();

        }

        private void SearchCommand(object sender, TextChangedEventArgs e)
        {
            if(((TextBox)sender).Text != null)
            {
                var searchStr = ((TextBox)sender).Text.ToLower();
                var list = ListProduct.Where(item => item.NameProduct.ToLower().Contains(searchStr) || 
                item.UnitOfMeasurementProduct.ToLower().Contains(searchStr) ||
                item.DescriptionProduct.ToLower().Contains(searchStr) ||
                item.IdManufacturerNavigation.Name.ToLower().Contains(searchStr) ||
                item.IdProductCategoryNavigation.CategoryName.ToLower().Contains(searchStr) ||
                item.IdSupplierNavigation.NameSupplier.ToLower().Contains(searchStr))
                    .ToList();
                drawinProduct(list);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBox)sender).SelectedIndex == 0)
            {
                drawinProduct(ListProduct);
            }
            else if(((ComboBox)sender).SelectedIndex == 1)
            {
                var list = ListProduct.OrderBy(item => item.QuantityInStockProduct).ToList();
                drawinProduct(list);
            }
            else
            {

                var list = ListProduct.OrderByDescending(item => item.QuantityInStockProduct).ToList();
                drawinProduct(list);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBox)sender).SelectedIndex == 0)
            {
                drawinProduct(ListProduct);
            }
            else
            {
                var list = ListProduct.Where(item => item.IdSupplierNavigation.NameSupplier 
                == ((ComboBox)sender).SelectedValue.ToString()).ToList();
                drawinProduct(list);
            }
        }
    }
}
