using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Crypto.Engines;
using System;

namespace dest
{





    class Customer {
        private int id;
        private string nameOfCustomer;
        private int amountOwed;

        public Customer(int id, string nameOfCustomer, int amountOwed)
        {
            Id = id;
            NameOfCustomer = nameOfCustomer;
            AmountOwed = amountOwed;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NameOfCustomer
        {
            get { return nameOfCustomer; }
            set { nameOfCustomer = value; }
        }

        public int AmountOwed
        {
            get { return amountOwed; }
            set { amountOwed = value; }
        }



    }


    public class Order
    {

        private int orderID;
        private string orderContent;
        private string orderOwner;
        private DateTime? orderDate;
        private int orderPrice;
        private bool orderIsPayed;



        public Order(int orderID = -1, string orderContent = "NA", string orderOwner = "NA", DateTime? orderDate = null, int orderPrice = 0, bool orderIsPayed = false)
        {

            this.orderID = orderID;
            this.orderContent = orderContent;
            this.orderOwner = orderOwner;
            this.orderDate = orderDate;
            this.orderPrice = orderPrice;
            this.orderIsPayed = orderIsPayed;
        }

        public string GetOrderContent()
        {
            return this.orderContent;
        }


        public string GetOrderOwner()
        {
            return (string)this.orderOwner;
        }

        public DateTime? GetOrderDate()
        {
            return this.orderDate;
        }
        public int GetOrderPrice()
        {

            return orderPrice;
        }

        public int GetOrderID()
        {

            return orderID;
        }

        public bool IsOrderIsPayed()
        {
            return orderIsPayed;
        }



        public void SetOrderContent(string orderContent) { this.orderContent = orderContent; }

        public void SetOrderOwner(string orderOwner) { this.orderOwner = orderOwner; }

        public void SetOrderDate(DateTime orderDate) { this.orderDate = orderDate; }

        public void SetOrderPrice(int price) { this.orderPrice = price; }

    

        public void SetOrderIsPayed(bool payed) { this.orderIsPayed = payed; }

    }




    public class Shoppingcart
    {
        private int numberofItem1;
        private int numberofItem2;
        private int numberofItem3;
        private int numberofItem4;

        private const int priceItem1 = 1800;
        private const int priceItem2 = 2000;
        private const int priceItem3 = 1800;
        private const int priceItem4 = 2000;


        private bool isEmpty;

        public string[] itemList = { "item1", "item2", "item3", "item4" };

        public Shoppingcart()
        {
            isEmpty=true;
            numberofItem1 = 0;
            numberofItem2 = 0;
            numberofItem3 = 0;
            numberofItem4 = 0;
        }

        public bool isItEmpty() { 
           int s = numberofItem1 +numberofItem2+numberofItem3+numberofItem4;
            if (s == 0)
                return true;
            return false;
        }
        public int GetNumberOfItem1()
        {
            return numberofItem1;
        }

        public void IncrementNumberOfItem1()
        {
            numberofItem1 += 1;
        }

        public void DecrementNumberOfItem1()
        {
            if (numberofItem1 > 0)
                numberofItem1 -= 1;
        }

        public int GetNumberOfItem2()
        {
            return numberofItem2;
        }

        public void IncrementNumberOfItem2()
        {
            numberofItem2 += 1;
        }

        public void DecrementNumberOfItem2()
        {
            if (numberofItem2 > 0)
                numberofItem2 -= 1;
        }

        public int GetNumberOfItem3()
        {
            return numberofItem3;
        }

        public void IncrementNumberOfItem3()
        {
            numberofItem3 += 1;
        }

        public void DecrementNumberOfItem3()
        {
            if (numberofItem3 > 0)
                numberofItem3 -= 1;
        }

        public int GetNumberOfItem4()
        {
            return numberofItem4;
        }

        public void IncrementNumberOfItem4()
        {
            numberofItem4 += 1;
        }

        public void DecrementNumberOfItem4()
        {
            if (numberofItem4 > 0)
                numberofItem4 -= 1;
        }



        public int GetSumPrice()
        {
            int price=numberofItem1*priceItem1 + numberofItem2*priceItem2+numberofItem3*priceItem3+numberofItem4*priceItem4;
            return price;
        }

    }














}