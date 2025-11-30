using ShoeStore.Models;
using System.Windows;

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для AddChangedProduct.xaml
    /// </summary>
    public partial class AddChangedProduct : Window
    {
        private Product updateProduct = null;

        public AddChangedProduct()
        {
            InitializeComponent();
            comboboxGet();
        }


        public AddChangedProduct(Product product)
        {
            InitializeComponent();
            updateProduct = product;

            ArticleProductTB.Text = product.ArticleProduct;
            NameProductTB.Text = product.NameProduct;
            UnitOfMeasurementProductTB.Text = product.UnitOfMeasurementProduct;
            PriceProductTB.Text = product.PriceProduct.ToString();
            CurrentDiscountProductTB.Text = product.CurrentDiscountProduct.ToString();
            QuantityInStockProductTB.Text = product.QuantityInStockProduct.ToString();
            DescriptionProductTB.Text = product.DescriptionProduct;
            comboboxGet();
            ManufacturerCB.SelectedValue = product.IdManufacturerNavigation.Name;
            CategoryCB.SelectedValue = product.IdProductCategoryNavigation.CategoryName;
            SupplierCB.SelectedValue = product.IdSupplierNavigation.NameSupplier;

        }

        private void comboboxGet()
        {
            using (ShoeStoreDbContext db = new ShoeStoreDbContext())
            {
                var listM = db.Manufacturers.ToList();
                foreach (var item in listM)
                {
                    ManufacturerCB.Items.Add(item.Name.Replace("\r\n", ""));
                }
                var listC = db.ProductCategories.ToList();
                foreach (var item in listC)
                {
                    CategoryCB.Items.Add(item.CategoryName.Replace("\r\n", ""));
                }
                var listS = db.Suppliers.ToList();
                foreach (var item in listS)
                {
                    SupplierCB.Items.Add(item.NameSupplier.Replace("\r\n", ""));
                }
            }
        }

        private void SaveProduct(object sender, RoutedEventArgs e)
        {

                if (!string.IsNullOrWhiteSpace(ArticleProductTB.Text)
                && !string.IsNullOrWhiteSpace(NameProductTB.Text)
                && !string.IsNullOrWhiteSpace(UnitOfMeasurementProductTB.Text)
                && !string.IsNullOrWhiteSpace(PriceProductTB.Text)
                && !string.IsNullOrWhiteSpace(CurrentDiscountProductTB.Text)
                && !string.IsNullOrWhiteSpace(QuantityInStockProductTB.Text)
                && !string.IsNullOrWhiteSpace(DescriptionProductTB.Text)
                && ManufacturerCB.SelectedItem != null
                && CategoryCB.SelectedItem != null
                && SupplierCB.SelectedItem != null
                )
                {
                    if (!decimal.TryParse(PriceProductTB.Text, out decimal price) || price < 0)
                    {
                        MessageBox.Show("Цена указана не верно", "Сохранение данных",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (!int.TryParse(QuantityInStockProductTB.Text, out int quantityInStock) || quantityInStock < 0)
                    {
                        MessageBox.Show("Количество на складе указана не верно", "Сохранение данных",
                           MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (!int.TryParse(CurrentDiscountProductTB.Text, out int сurrentDiscount) || сurrentDiscount < 0)
                    {
                        MessageBox.Show("Количество на складе указана не верно", "Сохранение данных",
                           MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if ((updateProduct == null))
                        {

                            var saveProduct = new Product()
                            {
                                ArticleProduct = ArticleProductTB.Text,
                                NameProduct = NameProductTB.Text,
                                UnitOfMeasurementProduct = UnitOfMeasurementProductTB.Text,
                                PriceProduct = decimal.Parse(PriceProductTB.Text),
                                CurrentDiscountProduct = int.Parse(CurrentDiscountProductTB.Text),
                                QuantityInStockProduct = int.Parse(QuantityInStockProductTB.Text),
                                DescriptionProduct = DescriptionProductTB.Text,
                            };

                            using (ShoeStoreDbContext db = new ShoeStoreDbContext())
                            {
                                var man = db.Manufacturers.FirstOrDefault(x => x.Name.Replace("\r\n", "") == ManufacturerCB.SelectedItem as string);
                                if (man != null)
                                {
                                    saveProduct.IdManufacturerNavigation = man;
                                }
                                else
                                {
                                    MessageBox.Show("Производитель не найден", "Сохранение данных",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                }


                                var cat = db.ProductCategories.FirstOrDefault(x => x.CategoryName.Replace("\r\n", "") == CategoryCB.SelectedItem as string);
                                if (man != null)
                                {
                                    saveProduct.IdProductCategoryNavigation = cat;
                                }
                                else
                                {
                                    MessageBox.Show("Категория не найдена", "Сохранение данных",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                }


                                var sup = db.Suppliers.FirstOrDefault(x => x.NameSupplier.Replace("\r\n", "") == SupplierCB.SelectedItem as string);
                                if (man != null)
                                {
                                    saveProduct.IdSupplierNavigation = sup;
                                }
                                else
                                {
                                    MessageBox.Show("Поставщик не найден", "Сохранение данных",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                db.Products.Add(saveProduct);
                                db.SaveChanges();
                            }
                        


                                
                        }
                        else
                        {
                            updateProduct.ArticleProduct = ArticleProductTB.Text;
                        updateProduct.NameProduct = NameProductTB.Text;
                        updateProduct.UnitOfMeasurementProduct = UnitOfMeasurementProductTB.Text;
                        updateProduct.PriceProduct = decimal.Parse(PriceProductTB.Text);
                        updateProduct.CurrentDiscountProduct = int.Parse(CurrentDiscountProductTB.Text);
                        updateProduct.QuantityInStockProduct = int.Parse(QuantityInStockProductTB.Text);
                        updateProduct.DescriptionProduct = DescriptionProductTB.Text;
                        using (ShoeStoreDbContext db = new ShoeStoreDbContext())
                        {
                            var man = db.Manufacturers.FirstOrDefault(x => x.Name.Replace("\r\n", "") == ManufacturerCB.SelectedItem as string);
                            if (man != null)
                            {
                                updateProduct.IdManufacturerNavigation = man;
                            }
                            else
                            {
                                MessageBox.Show("Производитель не найден", "Сохранение данных",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            }


                            var cat = db.ProductCategories.FirstOrDefault(x => x.CategoryName.Replace("\r\n", "") == CategoryCB.SelectedItem as string);
                            if (man != null)
                            {
                                updateProduct.IdProductCategoryNavigation = cat;
                            }
                            else
                            {
                                MessageBox.Show("Категория не найдена", "Сохранение данных",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            }


                            var sup = db.Suppliers.FirstOrDefault(x => x.NameSupplier.Replace("\r\n", "") == SupplierCB.SelectedItem as string);
                            if (man != null)
                            {
                                updateProduct.IdSupplierNavigation = sup;
                            }
                            else
                            {
                                MessageBox.Show("Поставщик не найден", "Сохранение данных",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            db.Products.Update(updateProduct);
                            db.SaveChanges();
                        }
                    }
                        MessageBox.Show("Данные успешно сохранены", "Сохранение данных",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                }
                }
                else
                {
                    MessageBox.Show("Заполните все поля", "Сохранение данных",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}
