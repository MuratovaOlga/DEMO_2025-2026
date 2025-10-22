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

namespace ShoeStore.Views
{
    /// <summary>
    /// Логика взаимодействия для Authorization1.xaml
    /// </summary>
    public partial class Authorization1 : Window
    {
        public Authorization1()
        {
            InitializeComponent();
        }



        private void SignInGuest(object sender, RoutedEventArgs e)
        {
            CurrentUser._CurrentUser = null;

            ListProducts1 listProducts1 = new ListProducts1();

            MessageBox.Show("Вы вошли как гость", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
            listProducts1.Show();

            Close();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(LoginTB.Text) || string.IsNullOrEmpty(PasswordBX.Password))
            {
                MessageBox.Show("Все поля должны быть заполнены", "Вход", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                using(ShoeStoreDbContext db =  new ShoeStoreDbContext())
                {
                    var currentUser = db.Users.FirstOrDefault(item=> item.LoginUser.Replace("\r\n", "") == (LoginTB.Text).Trim() 
                    && item.PasswordUser.Replace("\r\n", "") == (PasswordBX.Password).Trim());

                    if(currentUser == null)
                    {
                         MessageBox.Show("Пользователь не найден", "Вход", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    CurrentUser._CurrentUser = currentUser;

                }
                ListProducts1 listProducts1 = new ListProducts1();

                MessageBox.Show("Авторизация прошла успешно", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                listProducts1.Show();

                Close();

            }
        }
    }
}
