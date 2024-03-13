using Microsoft.Win32;
using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //Получение данных        
            string strName = tbName.Text; //Название игры
            string strManufacture = cbManufacture.Text;
            string strPrice = tbPrice.Text;
            string strDiscount = tbDiscount.Text;
            string strDescription = tbDescription.Text;
            


            // Далее можно использовать selectedContent по своему усмотрению

            int selectedManufacture = 1 + cbManufacture.SelectedIndex;

            //Изображение 
            // Получение текущего изображения из элемента Image
            string fileName;
            string fileName1 = null;

            //Исключение если пустые поля
            if (strName != "" && strPrice != "")
            {
                var image = SelectedPhoto.Source as BitmapSource;


                fileName = null;
                // Проверка, что изображение не равно null
                if (image != null)
                {
                    // Генерация уникального имени файла
                    string uniqName = strName.Replace(" ", "");
                    fileName = $"{uniqName.ToString()}.jpg"; //название файла с фото

                    //Путь файла
                    string folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Resources");

                    // Полный путь к сохраняемому файлу
                    string savePath = System.IO.Path.Combine(folderPath, fileName);

                    // Создание кодировщика JPEG для сохранения изображения
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));


                    // Сохранение файла на диск
                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }
                    if (fileName != null)
                    {
                        fileName1 = fileName;
                    }
                }
                else
                {
                    fileName1 = "NULL";
                }
                //Наш товар
                //System.Windows.MessageBox.Show("Название игры:" + strNameGame.ToString() + "\n" +
                //                "Жанр: " + strGenre.ToString() + "\n" +
                //                "Цена: " + strPrice.ToString() + "\n" +
                //                "Скидка: " + strDiscount.ToString() + "\n" +
                //                "Разработчик: " + strDeveloper.ToString() + "\n" +
                //                "Дата релиза: " + strReleaseDate.ToString() + "\n" +
                //                "Описание товара: " + strDescription.ToString() + "\n" +
                //                "Изображение: " + fileName1.ToString() + "\n"
                //    );

                //Сохранение в базу данных
                //Создаём объект товара
                Model.Product game = new Model.Product()
                {
                    //Запполняем поля
                    ProductName = strName,
                    ProductCost = int.Parse(strPrice),
                    ProductDiscount = int.Parse(strDiscount),
                    ProductManufactureId = selectedManufacture,
                    ProductPhoto = fileName,
                    ProductDescription = strDescription
                };

                // Добавление нового продукта в таблицу product
                Helper.DB.Product.Add(game);
                try
                {
                    //Сохранение в базу
                    Helper.DB.SaveChanges();
                    System.Windows.MessageBox.Show("Товар добавлен");
                    this.Close();
                }
                catch
                {
                    System.Windows.MessageBox.Show("Произошёл сбой при сохранении");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Вы не все заполнили");
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Manufacture> brands = new List<Manufacture>();
            brands = Helper.DB.Manufacture.ToList();
            cbManufacture.ItemsSource = brands;
            cbManufacture.DisplayMemberPath = "ManufactureName";
            cbManufacture.SelectedValuePath = "ManufactureId";
            cbManufacture.SelectedIndex = 0;
            cbManufacture.SelectedValue = 0;
        }
        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg) | *.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                SelectedPhoto.Source = bitmap;
            }
        }
        private void tbPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$"); // Регулярное выражение для проверки числа
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Если ввод не является числом, то обработка события формируется и значит ввод числа недопустим
            }
        }
        private void tbDiscount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$"); // Регулярное выражение для проверки числа
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; // Если ввод не является числом, то обработка события формируется и значит ввод числа недопустим
            }
        }
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
