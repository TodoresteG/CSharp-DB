CREATE DATABASE OnlineStore

USE OnlineStore

CREATE TABLE Cities (
	CityID INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Customers (
	CustomerID INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL,
	Birthday DATE NOT NULL,
	CityID INT FOREIGN KEY (CityID) REFERENCES Cities(CityID)
)

CREATE TABLE Orders (
	OrderID INT PRIMARY KEY IDENTITY(1, 1),
	CustomerID INT FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes (
	ItemTypeID INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Items (
	ItemID INT PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(50) NOT NULL,
	ItemTypeID INT FOREIGN KEY (ItemTypeID) REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems (
	OrderID INT NOT NULL,
	ItemID INT NOT NULL,

	CONSTRAINT PK_OrderItems PRIMARY KEY (OrderID, ItemID),
	CONSTRAINT FK_OrderItems_Order FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
	CONSTRAINT FK_OrderItems_Items FOREIGN KEY (ItemID) REFERENCES Items(ItemID)
)