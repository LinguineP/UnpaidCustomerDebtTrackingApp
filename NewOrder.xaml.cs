using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace dest
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Page
    { 

        private Shoppingcart currentCart;
        private DbConnector connector;
        List<Customer> CustomerList;
       

        public NewOrder()
        {
            InitializeComponent();
            currentCart = new Shoppingcart();

            connector = new DbConnector();
            CustomerList = connector.GetAllCustomers();
            CustomersComboBox.ItemsSource = CustomerList.Select(c => c.NameOfCustomer).ToList();

        }




        //inputs
        private int getCustomerID()
        {
            CustomerList=connector.GetAllCustomers();
            string currentCustomer = CustomersComboBox.Text.ToString();

            Customer currentC=CustomerList.FirstOrDefault(c => c.NameOfCustomer.Contains(currentCustomer, StringComparison.OrdinalIgnoreCase));
            return currentC.Id;
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("save");

            connector.OrderInsert(getCustomerID(), getOrderText(), currentCart.GetSumPrice());

            this.NavigationService.Navigate(new Home());


        }
        private void ButtonAbort(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("abort");
            this.NavigationService.Navigate(new Home());


        }


        private void placeHold(object sender, RoutedEventArgs e)
        {


            cart.Text = "lorem ipsum";
            Console.WriteLine("tmp placehold");



        }

        public string getOrderText() {

            string output = "";

            if (currentCart.GetNumberOfItem1() != 0)
            {
                output += ($"{currentCart.GetNumberOfItem1()}" + "x " + currentCart.itemList[0]);
            }

            if (currentCart.GetNumberOfItem2() != 0)
            {
                output += ("    " + $"{currentCart.GetNumberOfItem2()}" + "x " + currentCart.itemList[1]);
            }

            if (currentCart.GetNumberOfItem3() != 0)
            {
                output += ("    " + $"{currentCart.GetNumberOfItem3()}" + "x " + currentCart.itemList[2]);
            }

            if (currentCart.GetNumberOfItem4() != 0)
            {
                output += ("    " + $"{currentCart.GetNumberOfItem4()}" + "x " + currentCart.itemList[3]);
            }
            return output;

        }



        private void updateText()
        {

            string output = "Cart: ";



            if (currentCart.isItEmpty())
                return;
            
            
            cart.Text = output + getOrderText();


        }



        //output handling



        private void minus0(object sender, RoutedEventArgs e)
        {
            currentCart.DecrementNumberOfItem1();
            updateText();

        }


        private void plus0(object sender, RoutedEventArgs e)
        {
            currentCart.IncrementNumberOfItem1();
            updateText();
        }


        private void minus1(object sender, RoutedEventArgs e)
        {
            currentCart.DecrementNumberOfItem2();
            updateText();
        }


        private void plus1(object sender, RoutedEventArgs e)
        {
            currentCart.IncrementNumberOfItem2();
            updateText();
        }

        private void minus2(object sender, RoutedEventArgs e)
        {
            currentCart.DecrementNumberOfItem3();
            updateText();
        }


        private void plus2(object sender, RoutedEventArgs e)
        {
            currentCart.IncrementNumberOfItem3();
            updateText();
        }


        private void minus3(object sender, RoutedEventArgs e)
        {
            currentCart.DecrementNumberOfItem4();
            updateText();
        }


        private void plus3(object sender, RoutedEventArgs e)
        {
            currentCart.IncrementNumberOfItem4();
            updateText();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            string inputText = CustomersComboBox.Text.Trim();
            CustomerList = connector.GetAllCustomers();
            foreach (Customer c in CustomerList)
                {

                    if (string.Equals(c.NameOfCustomer, inputText, StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }

                }
           


            if (!string.IsNullOrEmpty(inputText))
            {
                connector.customerInsert(inputText);
                //CustomersComboBox.Items.Add(inputText);
                CustomerList = connector.GetAllCustomers();

                CustomersComboBox.ItemsSource = CustomerList.Select(c => c.NameOfCustomer).ToList();

            }
        }


    }

   


}