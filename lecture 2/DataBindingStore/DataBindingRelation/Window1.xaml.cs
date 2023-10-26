using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
    
// Добавили пространства имен типизированных классов
using DataBindingRelation.Data;
using DataBindingRelation.Data.NorthwindDataSetTableAdapters;
using DS2 = DataBindingRelation.Data.NorthDataSetTableAdapters;// Псевдоним
    
// Подключение пространств имен инфраструктуры ADO.NET
using System.Data;
using System.Data.OleDb;
    
namespace DataBindingRelation
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
    
            Page1();
            Page2();
            Page3();
            Page5();
        }
    
        #region Вкладка Page1
        void Page1()
        {
            // Создание экземпляров сгененированных классов
            NorthwindDataSet dataSet = new NorthwindDataSet();
            CustomersTableAdapter custAdap = new CustomersTableAdapter();
            OrdersTableAdapter ordAdap = new OrdersTableAdapter();
            Order_Details_ExtendedTableAdapter ord_det_extAdap =
                new Order_Details_ExtendedTableAdapter();
    
            // Потабличное наполнение экземпляра типизированного набора данных
            custAdap.Fill(dataSet.Customers);
            ordAdap.Fill(dataSet.Orders);
            ord_det_extAdap.Fill(dataSet.Order_Details_Extended);
    
            // Подключение набора данных к интерфейсу
            grid1.DataContext = dataSet.Customers;
        }
        #endregion
    
        #region Вкладка Page2
        void Page2()
        {
            // Создание экземпляров сгененированных классов
            NorthDataSet dataSet = new NorthDataSet();
            DS2.CustomersTableAdapter custAdap = new DS2.CustomersTableAdapter();
            DS2.OrdersTableAdapter ordAdap = new DS2.OrdersTableAdapter();
            DS2.Order_Details_ExtendedTableAdapter ord_det_extAdap =
                new DS2.Order_Details_ExtendedTableAdapter();
    
            // Потабличное наполнение экземпляра типизированного набора данных
            custAdap.Fill(dataSet.Customers);
            ordAdap.Fill(dataSet.Orders);
            ord_det_extAdap.Fill(dataSet.Order_Details_Extended);
    
            // Подключение набора данных к интерфейсу
            grid2.DataContext = dataSet.Customers;
        }
        #endregion
    
        #region Вкладка Page3
        void Page3()
        {
            // Извлекаем в поле строку соединения из файла App.config
            String connectionString = System.Configuration.
                ConfigurationManager.ConnectionStrings["MyNorthwind"].ConnectionString;
    
            DataSet dataSet = new DataSet();// Создаем множественный набор данных
    
            // Заполняем множественный набор данных из БД
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbCommand selectCommand = conn.CreateCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
    
                // Загружает всю таблицу Customers
                selectCommand.CommandText = "SELECT * FROM Customers";
                adapter.Fill(dataSet, "Customers");
    
                // Загружает всю таблицу Orders
                selectCommand.CommandText = "SELECT * FROM Orders";
                adapter.Fill(dataSet, "Orders");
    
                // Загружаем представление по SQL-запросу
                selectCommand.CommandText = @"SELECT DISTINCTROW [Order Details].OrderID, [Order Details].ProductID, Products.ProductName,
                    [Order Details].UnitPrice, [Order Details].Quantity, [Order Details].Discount,
                    CCur([Order Details].[UnitPrice]*[Quantity]*(1-[Discount])/100)*100 AS ExtendedPrice 
                    FROM Products INNER JOIN [Order Details] ON Products.ProductID = [Order Details].ProductID 
                    ORDER BY [Order Details].OrderID";
                adapter.Fill(dataSet, "Order_Details_Extended");
            }
    
            // Создание отношения между таблицами Customers и Orders
            dataSet.Relations.Add("CustomersOrders",
                dataSet.Tables["Customers"].Columns["CustomerID"],
                dataSet.Tables["Orders"].Columns["CustomerID"]);
    
            // Создание отношения между таблицами Orders и Order_Details_Extended
            // Немного другим способом
            DataColumn parentColumn = dataSet.Tables["Orders"].Columns["OrderID"];
            DataColumn childColumn = dataSet.Tables["Order_Details_Extended"].Columns["OrderID"];
            DataRelation relation = new DataRelation(
                "Orders_Order_Details_Extended",
                parentColumn, childColumn);
            dataSet.Relations.Add(relation);
    
            // Подключение набора данных к интерфейсу
            grid3.DataContext = dataSet.Tables["Customers"];
        }
        #endregion
    
        #region Вкладка Page5
        void Page5()
        {
            // Готовим путь к сохранению XML-документа
            String path;
            //path = System.Environment.CurrentDirectory + "\\Data";//Вариант 1
            path = AppDomain.CurrentDomain.BaseDirectory + "Data";  //Вариант 2
            string fileName = System.IO.Path.Combine(path, "XmlDocument.xml");
    
            // Создаем только раз
            if (System.IO.File.Exists(fileName))
                return;
    
            // Создаем заполненный реляционный набор данных 
            MyObject myObject = new MyObject();
            DataSet dataSet = myObject.GetCollection();
    
            // Создаем XML-документ и загружаем его данными набора
            System.Xml.XmlDataDocument document =
                new System.Xml.XmlDataDocument(dataSet);
    
            // Сохраняем документ в файле
            document.Save(fileName);
    
            // Бросаем набор и документ (для очевидности!) 
            dataSet.Dispose();
            document = null;
    
            // Вызываем сборщик мусора (для разнообразия!)
            GC.WaitForFullGCComplete(); // Ждать завершения сборки мусора
            GC.Collect();               // Начать сборку мусора
        }
        #endregion
    }
    
    // Вкладка Page4. Объект с методом, возвращающим коллекцию привязки
    class MyObject
    {
        public DataSet GetCollection()
        {
            // Извлекаем в поле строку соединения из файла App.config
            String connectionString = System.Configuration.
                ConfigurationManager.ConnectionStrings["MyNorthwind"].ConnectionString;
    
            DataSet dataSet = new DataSet();// Создаем множественный набор данных
    
            // Заполняем множественный набор данных из БД
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbCommand selectCommand = conn.CreateCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
    
                // Загружает всю таблицу Customers
                selectCommand.CommandText = "SELECT * FROM Customers";
                adapter.Fill(dataSet, "Customers");
    
                // Загружает всю таблицу Orders
                selectCommand.CommandText = "SELECT * FROM Orders";
                adapter.Fill(dataSet, "Orders");
    
                // Загружаем представление по SQL-запросу
                selectCommand.CommandText = @"SELECT DISTINCTROW [Order Details].OrderID, [Order Details].ProductID, Products.ProductName,
                    [Order Details].UnitPrice, [Order Details].Quantity, [Order Details].Discount,
                    CCur([Order Details].[UnitPrice]*[Quantity]*(1-[Discount])/100)*100 AS ExtendedPrice 
                    FROM Products INNER JOIN [Order Details] ON Products.ProductID = [Order Details].ProductID 
                    ORDER BY [Order Details].OrderID";
                adapter.Fill(dataSet, "Order_Details_Extended");
            }
    
            // Создание отношения между таблицами Customers и Orders
            dataSet.Relations.Add("CustomersOrders",
                dataSet.Tables["Customers"].Columns["CustomerID"],
                dataSet.Tables["Orders"].Columns["CustomerID"]);
    
            // Создание отношения между таблицами Orders и Order_Details_Extended
            // Немного другим способом
            DataColumn parentColumn = dataSet.Tables["Orders"].Columns["OrderID"];
            DataColumn childColumn = dataSet.Tables["Order_Details_Extended"].Columns["OrderID"];
            DataRelation relation = new DataRelation(
                "Orders_Order_Details_Extended",
                parentColumn, childColumn);
            dataSet.Relations.Add(relation);
    
            return dataSet;
        }
    }
}