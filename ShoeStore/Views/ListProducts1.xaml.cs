using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для ListProducts1.xaml
    /// </summary>
    public partial class ListProducts1 : Window
    {
        //Свойство для списка товаров
        public List<Product> ListProduct { get; set; }
        public List<Product> SortListProduct { get; set; }
        public List<string> SuppliersList { get; set; }
        public ListProducts1()
        {
            InitializeComponent();
            if(CurrentUser._CurrentUser == null)
            {

                Visib.Visibility = Visibility.Collapsed;
                Visib1.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (CurrentUser._CurrentUser.IdRole == 3)
                {
                    Visib.Visibility = Visibility.Collapsed;
                    Visib1.Visibility = Visibility.Collapsed;
                }
            }

            if (CurrentUser._CurrentUser != null)
                NameUser.Text = CurrentUser._CurrentUser.Fiouser;
            else
                NameUser.Text = "Гость";

            //Получения списка товаров из бд, включая категории, производителей и поставщиков для каждого товара
            using (ShoeStoreDbContext db = new ShoeStoreDbContext())
            {
                ListProduct = db.Products
                    .Include(c=>c.IdProductCategoryNavigation)
                    .Include(m=>m.IdManufacturerNavigation)
                    .Include(s=>s.IdSupplierNavigation)
                    .ToList();
            }
            //отчиска строковых полей от посторонних элементов
            foreach(var item in ListProduct)
            {
                item.CleanField();
            }
            //вызов метода отрисовки списка карточек товаров
            drawinProduct(ListProduct);
            //привязка контекста
            DataContext = this;


            SuppliersList = new List<string>()
            {
                "Все поставщики"
            };
            using (ShoeStoreDbContext db = new ShoeStoreDbContext())
            {
                foreach (var item in db.Suppliers.ToList())
                {
                    SuppliersList.Add(item.NameSupplier.Replace("\r\n", ""));
                }
            }

            SortBox.SelectedIndex = 0;
            FilterBox.SelectedIndex = 0;



        }

        //метод отрисовки списка карточек товаров
        private void drawinProduct(List<Product> _products)
        {
            //отчиска списка
            listProduct.Items.Clear();
            foreach (var item in _products)
            {
                //Создание UserControl для каждого товара
                listProduct.Items.Add(new ProductCard1(item));
            }
            //обновляем отображение списка в ListBox
            listProduct.Items.Refresh();
        }

        private void listProduct_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var UserControl = ((sender as ListBox).SelectedItem as ProductCard1).DataContext;
                MessageBox.Show("");
        }

        private void BackCommand(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messege = MessageBox.Show("Вы действительно хотите выйти", "Выход",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(messege == MessageBoxResult.Yes)
            {
                CurrentUser._CurrentUser = null;
                Authorization1 authorization1 = new Authorization1();
                authorization1.Show();
                Close();
            }
        }

        private void workWithData()
        {
            var textSearch = SearchText.Text.ToLower();
            if (textSearch != null)
            {
                SortListProduct = ListProduct.Where(item => item.NameProduct.ToLower().Contains(textSearch)
                || item.ArticleProduct.ToLower().Contains(textSearch)
                || item.UnitOfMeasurementProduct.ToLower().Contains(textSearch)
                || item.DescriptionProduct.ToLower().Contains(textSearch)
                || item.IdManufacturerNavigation.Name.ToLower().Contains(textSearch)
                || item.IdProductCategoryNavigation.CategoryName.ToLower().Contains(textSearch)
                || item.IdSupplierNavigation.NameSupplier.ToLower().Contains(textSearch)
                ).ToList();
            }

            var index = SortBox.SelectedIndex;

            if (index == 0)
            {
                SortListProduct = SortListProduct;
            }
            else if (index == 1)
            {
                SortListProduct = SortListProduct.OrderBy(item => item.QuantityInStockProduct).ToList();
            }
            else
            {
                SortListProduct = SortListProduct.OrderByDescending(item => item.QuantityInStockProduct).ToList();
            }

            var textFilter = FilterBox.SelectedItem as string;
            if (FilterBox.SelectedIndex == 0)
            {
                SortListProduct = SortListProduct;
            }
            else
            {
                SortListProduct = SortListProduct.Where(item => item.IdSupplierNavigation.NameSupplier == textFilter).ToList();
            }
            drawinProduct(SortListProduct);
        }

        private void SearchCommand(object sender, TextChangedEventArgs e)
        {
            workWithData();
        }

        private void SortCommand(object sender, SelectionChangedEventArgs e)
        {
            workWithData();
        }

        private void FiltrationCoomand(object sender, SelectionChangedEventArgs e)
        {
            workWithData();
            
        }

        private void AddProductCommand(object sender, RoutedEventArgs e)
        {
            AddChangedProduct addChanged = new AddChangedProduct();
            addChanged.Show();
        }

        private void UpdateProduct(object sender, MouseButtonEventArgs e)
        {
            var selectedProduct = (listProduct.SelectedItem as ProductCard1).DataContext as Product;
            AddChangedProduct addChanged = new AddChangedProduct(selectedProduct);
            addChanged.Show();

        }
    }
}
