using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
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
        public string Name { get; set; }
        //Основной список продуктов
        public List<Product> Products { get; set; }

        //Свойства для фильтрации/сортировки/поиска
        public List<string> Filtration { get; set; }
        public string SelectedFiltration { get; set; }
        public List<Product> ProductsFiltered { get; set; }



        public ListOfProducts()
        {
           
            InitializeComponent();
            
            using (ShoeStoreDbContext dbContext = new ShoeStoreDbContext())
            {
                Products = dbContext.Products
                    .Include(c => c.IdProductCategoryNavigation)
                    .Include(c => c.IdManufacturerNavigation)
                    .Include(c => c.IdSupplierNavigation)
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

            if (CurrentUser._CurrentUser != null)
            {
                NameUser.Text = CurrentUser._CurrentUser.Fiouser;
            }
            else
            {
                NameUser.Text = "Гость";
            }

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

        //Фильтрация/сортировка/поиск

        private void Apply()
        {
            string searchText = SearchText.Text.ToLower() ?? "";
            ProductsFiltered = Products.Where(p =>
                    p.NameProduct?.ToLower().Contains(searchText) == true ||
                    p.UnitOfMeasurementProduct?.ToLower().Contains(searchText) == true ||
                    p.DescriptionProduct?.ToLower().Contains(searchText) == true ||
                    p.IdSupplierNavigation.NameSupplier.ToLower().Contains(searchText) == true ||
                    p.IdManufacturerNavigation.Name.ToLower().Contains(searchText) == true ||
                    p.IdProductCategoryNavigation.CategoryName.ToLower().Contains(searchText) == true
                ).ToList();

            if (SelectedFiltration == Filtration[0])
            {
                ProductsFiltered = ProductsFiltered.Where(x => true).ToList();
            }
            else
            {
                ProductsFiltered = ProductsFiltered.Where(p => p.IdSupplierNavigation.NameSupplier == SelectedFiltration).ToList();
            }

            if(Ascending.IsChecked == true)
            {
                ProductsFiltered = ProductsFiltered.OrderBy(item => item.QuantityInStockProduct).ToList();
            }
            else if(Descending.IsChecked == true)
            {
                ProductsFiltered = ProductsFiltered.OrderBy(item => item.QuantityInStockProduct).ToList();
                ProductsFiltered.Reverse();
                
            }
            DrawingProducts(ProductsFiltered);
        }


        private void ApplyFilter(object sender, TextChangedEventArgs e)
        {
            Apply();
        }

        private void SelectedFiltrationCommand(object sender, SelectionChangedEventArgs e)
        {
            Apply();
        }

        private void ResetCommand(object sender, RoutedEventArgs e)
        {
            DrawingProducts(Products);
            Ascending.IsChecked = false;
            Descending.IsChecked = false;

        }
        private void ascendingCommand(object sender, RoutedEventArgs e)
        {
            Apply();
        }
    }
}
