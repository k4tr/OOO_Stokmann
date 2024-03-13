using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Sport_Products.Classes
{
    public class OrderExtended
    {
        public Model.Order order { get; set; }
        public Model.Product product { get; set; }

        //сумма всего заказа
        public double SumOrder { get; set; }

        //сумма скидки всего заказа
        public double SumOrderWithDiscount { get; set; }

        //общая скидка в %
        public double SumDiscountPercent { get; set; }
        public decimal CalcSummaOrder(List<Model.OrderProduct> listProductsInOrder)
        {
            SumOrder = 0;

            foreach (var orderProduct in listProductsInOrder)
            {
                SumOrder += Convert.ToDouble(orderProduct.Product.ProductCost * orderProduct.OrderProductQuantity);
            }

            return (decimal)SumOrder;
        }

        public decimal CalcSummaDiscountPercent(List<Model.OrderProduct> listProductsInOrder)
        {
            SumOrderWithDiscount = 0;

            foreach (var orderProduct in listProductsInOrder)
            {
                decimal productSaleDecimal = (decimal)orderProduct.Product.ProductDiscount;
                
            }

            return (decimal)SumOrderWithDiscount;
        }
    }
}
