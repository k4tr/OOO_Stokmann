using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OOO_Sport_Products.Classes
{
    public class ProductExtended
    {
        public Model.Product Product { get; set; }
       

        public string ProductPathPhoto
        {
            get
            {

                string temp;
                if (!String.IsNullOrEmpty(this.Product.ProductPhoto))
                {
                    temp = @"\Resources\" + this.Product.ProductPhoto;
                }
                else
                {
                    temp = @"\Resources\заглушка.jpg";
                }
                return temp;

            }
        }
        public int ProductCostWithDiscount
        {
            get
            {
                int discountAmount = (int)(ProductCost * (Product.ProductDiscount / 100.0));
                return (int)(ProductCost - discountAmount);
            }
        }
        public int ProductDiscountCost
        {
            get
            {

                int discountAmount = (int)(ProductCost * (Product.ProductDiscount / 100.0));
                return (int)(ProductCost - discountAmount);
            }
            set { this.ProductDiscountCost = value; }
        }

        public Visibility ProductCostDiscountVisibility
        {
            get
            {
                Visibility result = Visibility.Collapsed;
                if (Product.ProductDiscount > 1)
                {
                    result = Visibility.Visible;
                }
                return result;
            }
        }
        public SolidColorBrush ColorFocused
        {
            get
            {
                SolidColorBrush sb = new SolidColorBrush();
                if (Product.ProductDiscount >= 5)
                {
                    sb.Color = Colors.LightPink;
                }
                else
                {
                    sb.Color = Colors.White;
                }
                return sb;
            }
        }
        public string ProductName
        {
            get { string temp; temp = this.Product.ProductName; return temp; }

            set { this.Product.ProductName = value; }
        }
        public string ProductDescription
        {
            get { return this.Product.ProductDescription;}
        }
        public double ProductCost
        {
            get { return (double)this.Product.ProductCost; }
        }
        public double ProductDiscount
        {
            get { return (double)this.Product.ProductDiscount; }
        }
        public int ProductManufactureId
        {
            get { return (int)this.Product.ProductManufactureId; }
        }
    }
}
