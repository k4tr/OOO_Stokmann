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
    /// Логика взаимодействия для AllOrder.xaml
    /// </summary>
    public partial class AllOrder : Window
    {
        List<Model.Order> listOrder = new List<Model.Order>();
        List<Model.Product> listOrderInfo = new List<Model.Product>();
        List<OrderExtended> listOrderExtended;
        public AllOrder()
        {
            InitializeComponent();
            listOrder = Helper.DB.Order.ToList();
            listOrderInfo = Helper.DB.Product.ToList();
            listOrderExtended = new List<OrderExtended>();
            foreach(var order in listOrder)
            {
                Classes.OrderExtended orderExtended = new Classes.OrderExtended();
                orderExtended.order = order;
                // Получаем список компонентов для данного заказа
                listOrderExtended.Add(orderExtended);
            }
        }

    }
}
