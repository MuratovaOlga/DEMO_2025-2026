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
        public ListProducts1()
        {
            InitializeComponent();

            using (ShoeStoreDbContext db = new ShoeStoreDbContext())
            {
                ListProduct = db.Products
                    .Include(c=>c.IdProductCategoryNavigation)
                    .Include(m=>m.IdManufacturerNavigation)
                    .Include(s=>s.IdSupplierNavigation)
                    .ToList();
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
            foreach(var item in _products)
            {
                listProduct.Items.Add(new ProductCard1(item));
            }
        }
    }
}
