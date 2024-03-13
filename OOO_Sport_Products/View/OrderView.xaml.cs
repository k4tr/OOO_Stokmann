using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;
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

namespace OOO_Sport_Products.View
{
    /// <summary>
    /// Логика взаимодействия для OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        //Список всех заказов из БД
        List<ProductInOrder> listOrder;


        

        public OrderView()
        {
            InitializeComponent();
            tbFIO.Text = "ФИО:" + Helper.User.UserName;
        }
        public OrderView(List<ProductInOrder> listOrder)
        {
            InitializeComponent();
            //this.DataContext = this;
            this.listOrder = listOrder;
            ShowInfo();
        }
        private void ShowInfo()
        {
            listBoxOrders.ItemsSource = listOrder;
            double sumTotal = 0;
            double sumTotalWithDiscount = 0;
            foreach (var item in listOrder)
            {
                sumTotal += (double)(item.ProductExtended.Product.ProductCost * item.CountProductInOrder);
                sumTotalWithDiscount += (double)(item.ProductExtended.ProductDiscountCost * item.CountProductInOrder);
            }
            tbSumTotal.Text = "Сумма заказа: " + sumTotal.ToString();
           
        }
        private void butDeleteProductInOrder_Click(object sender, RoutedEventArgs e)
        {

            int art = (int)(sender as Button).Tag;
            ProductInOrder selectProduct = listOrder.Find(pr => pr.ProductExtended.Product.ProductId == art);

            listOrder.Remove(selectProduct);
            ShowInfo();
        }
        private void buttunback_Click(object sender, RoutedEventArgs e)
        {
            this.Close();


        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem windowEditCatalog = new AddItem();
            this.Hide();
            windowEditCatalog.ShowDialog();
            this.ShowDialog();
        }
        private void btnMakeOrder_Click(object sender, RoutedEventArgs e)
        {

            if (listOrder.Count <= 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }
            else
            {
                
                //Создаём оьъект заказ
                Model.Order Order = new Model.Order();
                //Запполняем поля
                Order.OrderDate = DateTime.Now;
                Helper.DB.Order.Add(Order);
                //try
                //{
                    //Helper.DB.SaveChanges();
                    //foreach (Classes.ProductInOrder item in listOrder)
                    //{
                    //    Model.OrderProduct orderProduct = new Model.OrderProduct();


                    //    orderProduct.OrderProductQuantity = item.CountProductInOrder;
                    //    Helper.DB.OrderProduct.Add(orderProduct);
                    //    Helper.DB.SaveChanges();
                    //}
                    MessageBox.Show("Заказ оформлен");
                    listOrder.Clear();
                    this.Close();
                //}
                //catch
                //{
                //    MessageBox.Show("Произошёл сбой при сохранении");
                //}
            }
        }
        private void listBoxOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
