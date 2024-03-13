using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;
using OOO_Sport_Products.View;
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

namespace OOO_Sport_Products
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        System.Windows.Threading.DispatcherTimer timer;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Подключение к БД
            Classes.Helper.DB = new Model.OOOStokmanEntities();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Password.Text = PasswordDot.Password;
            string login = Login.Text;
            //string password = Password.Text;
            string password = PasswordDot.Password;
            StringBuilder sb = new StringBuilder();
            //Обработка пустоты
            if (login == "") {
                sb.Append("Вы не введи логин.\n");
            }
            if (password == "") {
                sb.Append("Вы не ввели пароль.\n");
            }
            if (sb.Length > 0) {
                MessageBox.Show(sb.ToString());
                return;
            }
            ////Работа с БД
            //List<Model.User> users = new List<Model.User>();
            ////Все записи БД
            //users = Classes.Helper.DB.Users.ToList();
            ////Получить первую запись таблицы
            //Model.User user = users.Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();
            Classes.Helper.User = Classes.Helper.DB.User.ToList().Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();
            if (Helper.User != null)
            {
                string userName = Helper.User.UserName;
                int userRoleId = Helper.User.UserRole;
                string userRoleName = Helper.User.Role.RoleName;
                MessageBox.Show("Здравствуйте, "+userName + "\n"+ "Ваша роль: " +"\n" + userRoleName);
                GoToCatalog();
            }
           
            //Доступ по навигационному свуйству в полю связвнной таблицы
            //string userRoleName = user.Role.RoleName;
            //MessageBox.Show(userName + " " + userRoleId + " " + userRoleName);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        //При отображении пароля
        private void isVisiblePassword_Checked(object sender, RoutedEventArgs e)
        {
            Password.Visibility = Visibility.Visible;
            PasswordDot.Visibility = Visibility.Hidden;
            Password.Text = PasswordDot.Password;
        }

        private void isVisiblePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            Password.Visibility = Visibility.Hidden;
            PasswordDot.Visibility = Visibility.Visible;
            PasswordDot.Password = Password.Text;
        }
        //Начальные настройки при отображении окна
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private async void timerTick(object sender, EventArgs e) {
            Start.IsEnabled = true;
        }
        //Переход в каталог Гостем
        private void Gost_Click(object sender, RoutedEventArgs e)
        {
            GoToCatalog();
        }


        //Метод перехода в каталог
        private void GoToCatalog() {
            WindowsCatalog windowsCatalog = new WindowsCatalog();
            this.Hide();
            windowsCatalog.ShowDialog();
            this.Show();
        }
    }
}
