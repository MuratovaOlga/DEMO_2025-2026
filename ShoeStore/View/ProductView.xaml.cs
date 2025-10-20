using ShoeStore.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShoeStore.View
{
    /// <summary>
    /// Логика взаимодействия для ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        //олучаем продукт для отображения
        public ProductView(Product product)
        {
            InitializeComponent();

            //вызываем метод отчистки от \r\n, которые создаются при считывании с бд
            product.CleanField();

            //вызываем метод который изменяет дизайн формы в соответсвии со значениями продукта
            designForm(product);
            DataContext = product;
        }

        private void designForm(Product product)
        {
            //если Скидка больше  15% красим фон в зеленый цвет(ргб получили через конструктор)
            if (product.CurrentDiscountProduct > 15)
            {
                Back.Background = new SolidColorBrush(Color.FromRgb(46, 139, 87));
            }

            //Изменяем поле с ценой 
            if(product.CurrentDiscountProduct != 0)
            {
                Price.Foreground = new SolidColorBrush(Color.FromRgb(255,0,0));
                Price.TextDecorations = TextDecorations.Strikethrough;

                PriceCurrent.Text = ((float)(product.PriceProduct - (product.PriceProduct * (product.CurrentDiscountProduct / 100.0m)))).ToString("n2");
            }

            //если товара нет в наличи красим строку в голубой
            if(product.QuantityInStockProduct == 0)
            {
                QuantityInStock.Background = new SolidColorBrush(Color.FromRgb(18, 210, 241));
            }
        }
    }
}
