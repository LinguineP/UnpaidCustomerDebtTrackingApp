using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace dest
{
    /// <summary>
    /// Interaction logic for Vieworders.xaml
    /// </summary>
    public partial class ViewOrders : Page
    {
        private List<Customer> customers;
        private static List<Order> orders;
        private static List<OrderItem> items;
        static DbConnector  connector;
        private static ListView m_ItemListView;

      


        public ViewOrders()
        {
            InitializeComponent();

            m_ItemListView = itemListView;

            connector = new DbConnector();
            
            customers=connector.GetAllCustomers();

            orders=connector.GetOrders();

            items=new List<OrderItem>();

         
            

            ItemiseOrders();

            refreshPage();


          


        }

        static void ItemiseOrders( ) 
        {

            if (items != null)
            {
                items.Clear();
            }
            foreach (Order o in orders) 
            {
                items.Add(new OrderItem
                {
                    IsChecked = o.IsOrderIsPayed(),
                    Date=o.GetOrderDate(),
                    OrderContents=o.GetOrderContent(),
                    OrderOwner=o.GetOrderOwner(),
                    Price=o.GetOrderPrice(),
                    orderID=o.GetOrderID(),
                    orderOwnerID=connector.ReadCustomerName(o.GetOrderOwner()).Id
                });
            
            }
                
        
        }


        public void HandleEvent(object sender, EventArgs e)
        {
            refreshPage();
        }



        public static ListView GetM_ItemListView()
        {
            return m_ItemListView;
        }

        private static void refreshPage()
        {
            orders.Clear();
            orders = connector.GetOrders();

            items = new List<OrderItem>();



            ItemiseOrders();

            items.Sort(new CustomItemComparer());
            var categorySummaries = items
                                        .Where(item => !item.IsChecked) // Filter unchecked items
                                        .GroupBy(item => item.OrderOwner) // Group items by category
                                        .Select(group => new
                                        {
                                            Category = group.Key,

                                        })
                                        .ToList();


            foreach (var c in categorySummaries)
            {
                foreach (var i in items)
                {
                    if (c.Category == i.OrderOwner)
                    {
                        i.SetAltOrderOwner();
                    }

                }







                ICollectionView view = CollectionViewSource.GetDefaultView(items);
                view.GroupDescriptions.Add(new PropertyGroupDescription("OrderOwner"));

                items.Sort(new CustomItemComparer());
                m_ItemListView.ItemsSource = view;
               

            }

        }

        private void ButtonHome(object sender, RoutedEventArgs e)
        {
         
            this.NavigationService.Navigate(new Home());


        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            refreshPage();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Code to execute when the checkbox is unchecked
            refreshPage();
        }


    }

    //view object

    public class OrderItem : INotifyPropertyChanged
    {

        private bool isChecked;

        public int orderID { get; set; }
        public string OrderContents { get; set; }

        public int orderOwnerID{get; set;}



    public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;

                    DbConnector connector= new DbConnector();

                    connector.UpdateOrder(orderID, value);


                    if (IsChecked)
                        connector.UpdateAmountOwed(this.orderOwnerID, -(int)Price);
                    else 
                        connector.UpdateAmountOwed(this.orderOwnerID, (int)Price);

                    

                    OnPropertyChanged();
                }
            }
        }
        public string OrderOwner { get; set; }

        public DateTime? Date { get; set; }
        public decimal Price { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void SetAltOrderOwner(){

            DbConnector connector = new DbConnector();
            Customer c=connector.ReadCustomerName(OrderOwner);
            OrderOwner = OrderOwner + "   is owed: " + c.AmountOwed ;
            
        }

        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {


           
          
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }



   

    public class CustomItemComparer : IComparer<OrderItem>
    {
        public int Compare(OrderItem x, OrderItem y)
        {
            // First, compare by IsChecked (Unchecked items first)
            int isCheckedComparison = x.IsChecked.CompareTo(y.IsChecked);

            if (isCheckedComparison != 0)
            {
                return isCheckedComparison;
            }

            // If both items have the same IsChecked value, compare by Date
            if (x.Date is null || y.Date is null)
            {
                return 0;
            }
            else if (x.Date < y.Date)
            {
                return -1;
            }
            else  
            {
                return 1;
            }
                
        }
    }




}
