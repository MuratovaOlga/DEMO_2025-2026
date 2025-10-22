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

namespace ShoeStore.View
{
    /// <summary>
    /// Логика взаимодействия для TestListProduct.xaml
    /// </summary>
    public partial class TestListProduct : Window
    {
        public List<Product> Products {  get; set; }   
        public TestListProduct()
        {
            InitializeComponent();
            using(ShoeStoreDbContext db = new ShoeStoreDbContext())
            {
                Products = db.Products
                    .Include(c => c.IdProductCategoryNavigation)
                    .Include(c => c.IdManufacturerNavigation)
                    .Include(c => c.IdSupplierNavigation)
                    .ToList();
            }
            foreach(var item in Products)
            {
                item.CleanField();
            }
            DataContext = this;
        }
    }
}
