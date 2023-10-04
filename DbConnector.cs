using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace dest
{

    

    internal class DbConnector
    {

        private string connectionString = "Server=localhost;Port=3306;Database=DestAppDB;User=root;Password=pavle2001;";



        public void customerInsert(string nameOfCustomer, decimal amountOwed=0)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO customers (nameOfCustomer, amountOwed) VALUES (@nameOfCustomer, @amountOwed)";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@nameOfCustomer", nameOfCustomer);
                command.Parameters.AddWithValue("@amountOwed", amountOwed);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateAmountOwed(int customerId, int newAmountOwed)
        {

            Customer c = ReadCustomer(customerId);
            if (c != null) { 
                int previouslyOwed = c.AmountOwed;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Customers SET amountOwed = @NewAmountOwed WHERE id = @CustomerId";
                    MySqlCommand command = new MySqlCommand(sql, connection);

                    // Parameters
                    command.Parameters.AddWithValue("@NewAmountOwed", previouslyOwed + newAmountOwed);
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Amount owed updated successfully for customer ID {customerId}.");
                    }
                    else
                    {
                        Console.WriteLine($"Customer with ID {customerId} not found.");
                    }
                }
            }
        }

        public Customer ReadCustomer(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT id, nameOfCustomer, amountOwed FROM customers WHERE id = @id";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer( reader.GetInt32("id"),reader.GetString("nameOfCustomer"),(int)reader.GetDecimal("amountOwed"));
                    }
                    else
                    {
                        return null; // No record found
                    }
                }
            }
        }

        public Customer ReadCustomerName(string cname)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT id, nameOfCustomer, amountOwed FROM customers WHERE nameOfCustomer = @cname";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@cname", cname);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer(reader.GetInt32("id"), reader.GetString("nameOfCustomer"), (int)reader.GetDecimal("amountOwed"));
                    }
                    else
                    {
                        return null; // No record found
                    }
                }
            }
        }

       

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT id, nameOfCustomer, amountOwed FROM customers";
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer(reader.GetInt32("id"), reader.GetString("nameOfCustomer"), (int)reader.GetDecimal("amountOwed"));
                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }


        public void OrderInsert(int CustomerID,string OrderContents ,int OrderPrice,bool IsOrderDone=false)
        {

            DateTime currentDate = DateTime.Today;
            string OrderDate = currentDate.ToString("yyyy-MM-dd");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
               
                connection.Open();
               

                string sql = "INSERT INTO orders (CustomerID, OrderDate, OrderContents, OrderPrice, IsOrderDone) " +
                "VALUES (@CustomerID, @OrderDate, @OrderContents, @OrderPrice, @IsOrderDone)";
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                command.Parameters.AddWithValue("@OrderDate", OrderDate);
                command.Parameters.AddWithValue("@OrderContents", OrderContents);
                command.Parameters.AddWithValue("@OrderPrice", OrderPrice);
                command.Parameters.AddWithValue("@IsOrderDone", IsOrderDone);
                command.ExecuteNonQuery();
            }


            UpdateAmountOwed(CustomerID, OrderPrice);
        }



        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Orders";
                MySqlCommand command = new MySqlCommand(sql, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {   


                        Order order = new Order( reader.GetInt32("OrderID"),
                            reader.GetString("OrderContents"),
                            this.ReadCustomer((int)reader.GetInt32("CustomerID")).NameOfCustomer,
                            reader.GetDateTime("OrderDate"),
                             reader.GetInt32("OrderPrice"),
                            reader.GetBoolean("IsOrderDone"));
                        orders.Add(order);
                    }

                     
                 }
            }

            return orders;
        }


        public void UpdateOrder(int orderId, bool isOrderDone)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE Orders SET IsOrderDone = @IsOrderDone WHERE OrderID = @OrderID";
                MySqlCommand command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@IsOrderDone", isOrderDone);
                command.Parameters.AddWithValue("@OrderID", orderId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Order updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update the order.");
                }
            }
        }
    }
}
