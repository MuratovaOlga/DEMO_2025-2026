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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductCard1.xaml
    /// </summary>
    public partial class ProductCard1 : UserControl
    {
        private BrushConverter bc = new BrushConverter();
        public ProductCard1(Product product)
        {
            InitializeComponent();
            pictureProduct(product);
            DataContext = product;
        }
        private void pictureProduct(Product product)
        {
            if (product.CurrentDiscountProduct > 15)
            {
                Back.Background = (Brush)bc.ConvertFrom("#2E8B57");
            }
            if(product.CurrentDiscountProduct != 0)
            {
                Price.Foreground = new SolidColorBrush(Colors.Red);
                Price.TextDecorations = TextDecorations.Strikethrough;

                DiscountPrice.Text = (product.PriceProduct - (product.PriceProduct * (product.CurrentDiscountProduct / 100.0m))).ToString();
            }
            if(product.QuantityInStockProduct == 0)
            {
                QuantityInStock.Background = new SolidColorBrush(Colors.LightSkyBlue);
            }
        }
    }
}
