using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace OOO_Sport_Products.View
{

    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class WindowsCatalog : Window
    {
        int filterDiscount, filterCategory; 		//Фильтр по скидке и категории
        string sort;					//Направление сортировки
        int countAll, countFilter;			//Количество всех записей и после фильтрации
        string searchProduct;           //Строка поиска
                                        //Массив диапазонов скидок
        private List<ProductInOrder> order = new List<ProductInOrder>();
       
        public WindowsCatalog()
        {
            InitializeComponent();
            if (Helper.User == null)
            {
                buttonOrder.Visibility = Visibility.Hidden;
                butAllOrders.Visibility = Visibility.Hidden;
                butAddProduct.Visibility = Visibility.Hidden;
            }
            else if(Helper.User.UserRole == 3 || Helper.User.UserRole == 2)
            {
                
                buttonOrder.Visibility = Visibility.Visible;
            }
            else if (Helper.User.UserRole == 1)
            {
                butAllOrders.Visibility = Visibility.Visible;
                butAddProduct.Visibility = Visibility.Visible;
            }

        }
        //Возврат на авторизацию
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void buttonAllOrder_Click(object sender, RoutedEventArgs e)
        {
            AllOrder windowOrder = new AllOrder();
            this.Hide();
            windowOrder.ShowDialog();
            this.Show();
        }





        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Информация о пользователе
            if (Helper.User != null)//Зарегистрированный пользователь
            {
                tbFIO.Text = Helper.User.UserName;
            }
            else
            {
                tbFIO.Text = "Гость";
            }
            comboSale.Items.Clear();
            comboSale.Items.Add("Все диапазоны");
            comboSale.Items.Add("0-5%");
            comboSale.Items.Add("5-10%");
            comboSale.Items.Add("15% и более");
            comboSale.SelectedIndex = 0;

            comboSort.Items.Clear();
            comboSort.Items.Add("Любая цена");
            comboSort.Items.Add("По возрастанию");
            comboSort.Items.Add("По убыванию");
            comboSort.SelectedIndex = 0;

        }
        
        private void rbSortAsc_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void rbSortDesc_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void cbDiscount_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        private void cbCategory_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        private void tbSearch_TextChanged(object sender, RoutedEventArgs e)
        {

        }
        List<Model.Product> listProducts;
        List<Classes.ProductExtended> productExtendeds;
        /// Получить данные из БД по условия фильтра и отобразить их
        private void ShowProduct()
        {
            List<Product> products = new List<Model.Product>();
            products = Helper.DB.Product.ToList();
            int totalCount = products.Count;
            double maxDiscount = 15;
            double minDiscount = 0;
            switch (comboSale.SelectedIndex)
            {
                case 1:
                    maxDiscount = 5; break;
                case 2:
                    minDiscount = 10; maxDiscount = 15; break;
                case 3:
                    minDiscount = 15; break;
            }
            //Фильтрация по скидке
            products = products.Where(x => x.ProductDiscount <= maxDiscount && x.ProductDiscount >= minDiscount).ToList();

            
            //Общее количество товаров в таблице товаров
            countAll = products.Count();
            //***********Создание сложного запроса из отдельных элементов
            //Сортировка от состояния радиокнопки
            if (comboSort.SelectedIndex == 1)       //По возрастанию
            {
                products = products.OrderBy(pr => pr.ProductCost).ToList();
            }
            else if (comboSort.SelectedIndex == 2)
            {
                products = products.OrderByDescending(pr => pr.ProductCost).ToList();
            }

            //Поиск по названию
            if (!String.IsNullOrEmpty(searchProduct))
            {
                products = products.Where(x => x.ProductName.Contains(searchProduct)).ToList();
            }
            //Сортировка
            if (sort == "ASC")
            {
                products = products.OrderBy(x => x.ProductCost).ToList();
            }
            else
            {
                products = products.OrderByDescending(x => x.ProductCost).ToList();
            }
            //Количество товаров после фильтрации
            int filterCount = products.Count;
            tbCount.Text = filterCount + " Из " + countAll;

            List<Classes.ProductExtended> productExtendeds = new List<Classes.ProductExtended>();
            foreach (Product product in products)//Все записи
            {
                //Создать отдельный элемент списка
                Classes.ProductExtended productExtended = new Classes.ProductExtended();
                //Перенести в него данные о товаре из БД
                productExtended.Product = product;
                //Добавить этот элемент в строящейся список
                productExtendeds.Add(productExtended);
            }
            //Привязка – отобразить построенный список в ЭУ
            listBoxProducts.ItemsSource = productExtendeds;


            //Все продукты без слежения
        }
       
        /// Выбор направления сортировки
        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboSort.SelectedIndex == 0) sort = "ASC";
            else sort = "DESC";
            ShowProduct();
        }

        /// Выбор скидки
        private void comboSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterDiscount = comboSale.SelectedIndex;
            ShowProduct();
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem windowOrder = new AddItem();
            this.Hide();
            windowOrder.ShowDialog();
            this.Show();
        }
        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderView windowOrder = new OrderView(order);
            this.Hide();
            windowOrder.ShowDialog();
            this.Show();

        }

        private void listBoxProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// Выбор категории
        private void comboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        /// Ввод строки поиска
        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchProduct = search.Text;
            ShowProduct();
        }
        private void miAddOrder_Click(object sender, RoutedEventArgs e)
        {
            

            ProductExtended productExtendedSelect = listBoxProducts.SelectedItem as ProductExtended;

            
            //Поиск выбранного товара в заказе
            ProductInOrder productInOrderFind = order.Find(x => x.ProductExtended.Product.ProductId == productExtendedSelect.Product.ProductId);
            //Нашли
            if (productInOrderFind != null)
            {
                productInOrderFind.CountProductInOrder++;
            }
            else  //Нет - добавялем новую поззицию в заказ
            {
                ProductInOrder productNew = new ProductInOrder(productExtendedSelect);
                order.Add(productNew);
            }
            ////Поиск выбранного товара по артикле в заказе
            //Classes.ProductInOrder productInOrderFind = order.Find;

        }
        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderView windowOrder = new OrderView(order);
            this.Hide();
            windowOrder.ShowDialog();
            this.Show();
        }

        private void butDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаём контекст базы данных
                Model.OOOStokmanEntities context = new Model.OOOStokmanEntities();

                ProductExtended productExtendedSelect = listBoxProducts.SelectedItem as ProductExtended;
                // Получаем ссылку на объект товара, который нужно удалить (например, по артиклю товара)
                Model.Product productToRemove = context.Product.FirstOrDefault(p => p.ProductId == productExtendedSelect.Product.ProductId);

                // Проверяем, был ли найден объект товара
                if (productToRemove != null)
                {
                    // Удаляем объект товара из базы данных
                    context.Product.Remove(productToRemove);

                    //Удалеяем фото из папки если оно есть
                    //if(!(productExtendedSelect.Product.ProductPhoto == null))
                    //{
                    // Имя файла
                    string fileName = $"{productExtendedSelect.Product.ProductId.ToString()}.jpg"; //название файла с фото

                    //Путь файла
                    string folderPath = @"C:\Users\katrk\Desktop\матыга\OOO_Sport_Products\OOO_Sport_Products\Resources\";

                    // Полный путь к файлу
                    string delPath = System.IO.Path.Combine(folderPath, fileName);

                    if (File.Exists(delPath))
                    {
                        try
                        {
                            File.Delete(delPath);
                            // Вывод сообщения об успешном удалении файла
                        }
                        catch (Exception ex)
                        {
                            // Обработка исключения, если удаление файла не удалось
                            System.Windows.MessageBox.Show("Произошёл сбой при удалении");
                        }
                    }
                    else
                    {
                        // Вывод сообщения, если файл не найден
                    }
                    //}                    
                    try
                    {
                        // сохраняем изменения в базе данных
                        context.SaveChanges();
                        listBoxProducts.ItemsSource = listProducts;
                        listBoxProducts.ItemsSource = productExtendeds;
                        System.Windows.MessageBox.Show("Товар удален");


                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("Произошёл сбой при удалении");
                    }
                }
                else
                {
                    // Обработка ситуации, когда объект товара не был найден
                    System.Windows.MessageBox.Show("Товара нет товара");

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Произошёл сбой при удалении");
            }
        }

    }
}
