using ShoeStore.Data;
using ShoeStore.Models;
using System.Windows;

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
            // сохранение информации о том, что пользователь вошёл как гость
            CurrentUser._CurrentUser = null;

            //вызываем окно списка товаров
            ListProducts1 listProducts1 = new ListProducts1();

            //уведомляем пользователя
            MessageBox.Show("Вы вошли как гость", "Вход", MessageBoxButton.OK, 
                MessageBoxImage.Information);
            //отображаем окно списка товаров
            listProducts1.Show();
            //закрываем окно авторизации
            Close();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            //Делаем проверку заполнения полей
            if(string.IsNullOrWhiteSpace(LoginTB.Text) 
                || string.IsNullOrWhiteSpace(PasswordBX.Password))
            {
                MessageBox.Show("Все поля должны быть заполнены", "Вход", MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
            else
            {
                //если поля заполнены, то открываем поток бд
                using(ShoeStoreDbContext db =  new ShoeStoreDbContext())
                {
                    // делаем провеку на существование пользователя с введенными данными в системе
                    var currentUser = db.Users.FirstOrDefault(item=> item.LoginUser.Replace("\r\n", "") == (LoginTB.Text).Trim() 
                    && item.PasswordUser.Replace("\r\n", "") == (PasswordBX.Password).Trim());
                    if(currentUser == null)
                    {
                        //если пользователь не найден, уведомляем 
                         MessageBox.Show("Пользователь не найден", "Вход", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    CurrentUser._CurrentUser = currentUser;
                }
                ListProducts1 listProducts1 = new ListProducts1();
                MessageBox.Show("Авторизация прошла успешно", "Вход", MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                listProducts1.Show();
                Close();

            }
        }
    }
}
