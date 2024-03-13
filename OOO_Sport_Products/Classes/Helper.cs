using System; 
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace OOO_Sport_Products.Classes
{
    public class Helper
    {
        //Объявдление объект связи с БД
        public static Model.OOOStokmanEntities DB { get; set; }
        //
        public static Model.User User { get; set; }
       
    }
}
