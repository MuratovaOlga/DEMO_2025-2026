using Microsoft.IdentityModel.Tokens;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Windows;

namespace ShoeStore.View
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void AuthorizationCommand(object sender, RoutedEventArgs e)
        {
            if(LoginTB.Text.IsNullOrEmpty() || PasswordTB.Password.IsNullOrEmpty())
            {
                MessageBox.Show("Заполните все поля", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
                using (ShoeStoreDbContext db = new ShoeStoreDbContext())
                {
                    var list = db.Users.ToList();
                    var users = db.Users.FirstOrDefault(item => item.LoginUser.Replace("\r\n", "") == LoginTB.Text && item.PasswordUser.Replace("\r\n", "") == PasswordTB.Password);

                    if (users == null)
                    {
                        MessageBox.Show("Данные не корректные. Проверьте Логин и Пароль.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        CurrentUser._CurrentUser = users;
                        ListOfProducts products = new ListOfProducts();
                        products.Show();
                        Close();
                        return;
                    }
                }
        }

        private void AuthorizationGuestCommand(object sender, RoutedEventArgs e)
        {
            CurrentUser._CurrentUser = null;
            ListOfProducts products = new ListOfProducts();
            products.Show();
            MessageBox.Show("Вы вошли как Гость");
            Close();
        }
    }
}
