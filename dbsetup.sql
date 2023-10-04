CREATE DATABASE DestAppDB;

USE DestAppDB;

CREATE TABLE Customers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nameOfCustomer VARCHAR(255) NOT NULL,
    amountOwed DECIMAL(10, 2) NOT NULL
);


CREATE TABLE Orders (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    OrderContents TEXT,
    OrderPrice INT,
    IsOrderDone BOOL, 
    FOREIGN KEY (CustomerID) REFERENCES Customers(id)
);
