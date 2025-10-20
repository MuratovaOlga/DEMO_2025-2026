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
    /// Логика взаимодействия для ListOfProducts.xaml
    /// </summary>
    public partial class ListOfProducts : Window
    {
        //Основной список продуктов
        public List<Product> Products { get; set; }

        //Свойства для фильтрации/сортировки/поиска
        public List<string> Filtration { get; set; }
        public string SelectedFiltration { get; set; }
        public List<Product> ProductsFiltered { get; set; }



        public ListOfProducts()
        {
            InitializeComponent();
            using(ShoeStoreDbContext dbContext = new ShoeStoreDbContext())
            {
                Products = dbContext.Products
                    .Include(c=>c.IdProductCategoryNavigation)
                    .Include(c=>c.IdManufacturerNavigation)
                    .Include(c=>c.IdSupplierNavigation)
                    .ToList();
            }
            Filtration = new List<string>()
            {
                "Все поставщики"
            };
            using (ShoeStoreDbContext db = new ShoeStoreDbContext())
            {
                foreach(var item in db.Suppliers.ToList())
                {
                    Filtration.Add(item.NameSupplier.Replace("\r\n", ""));
                }
            }
            ProductsFiltered = new List<Product>(Products);
            SelectedFiltration = Filtration[0];

            //Вызываем метод отрисовки UserControl
            DrawingProducts(Products);
            DataContext = this;
        }

        private void DrawingProducts(List<Product> products)
        {
            listBoxProducts.Items.Clear();
            foreach (var item in products)
            {
                //В ListBox помещаем все UС 
                listBoxProducts.Items.Add(new ProductView(item));
            }
        }

        //Фильтрация данных (на данный момент работает не полностью)
        private void ApplyFilter(object sender, TextChangedEventArgs e)
        {
            SelectedFiltrationCommand(null, null);
            string searchText = (sender as TextBox).Text.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                ProductsFiltered = Products.Where(p =>
                    p.NameProduct?.ToLower().Contains(searchText) == true ||
                    p.UnitOfMeasurementProduct?.ToLower().Contains(searchText) == true ||
                    p.DescriptionProduct?.ToLower().Contains(searchText) == true ||
                    p.IdSupplierNavigation.NameSupplier.ToLower().Contains(searchText) == true ||
                    p.IdManufacturerNavigation.Name.ToLower().Contains(searchText) == true ||
                    p.IdProductCategoryNavigation.CategoryName.ToLower().Contains(searchText) == true
                ).ToList();

                DrawingProducts(ProductsFiltered);
            }
            else
            {
                DrawingProducts(Products);
            }
        }

        private void SelectedFiltrationCommand(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedFiltration == Filtration[0])
            {
                ProductsFiltered = ProductsFiltered.Where(x => true).ToList();
                DrawingProducts(ProductsFiltered);
            }
            else
            {

                ProductsFiltered = ProductsFiltered.Where(p => p.IdSupplierNavigation.NameSupplier == SelectedFiltration).ToList();
                DrawingProducts(ProductsFiltered);
            }
        }

        private void ResetCommand(object sender, RoutedEventArgs e)
        {
            listBoxProducts.ItemsSource = Products;
        }
    }
}
