-- 1

CREATE DATABASE Minions;

-- 2

CREATE TABLE Minions (
	Id INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	[Age] INT
)

CREATE TABLE Towns (
	Id INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
)

-- 3

ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

-- 4

ALTER TABLE Minions
DROP COLUMN Age

INSERT INTO Minions (Id, [Name], Age, TownId) VALUES
(1, 'Kevin', 22, 1),
(2, 'Bob', 15, 3),
(3, 'Stwerad', NULL, 2)

INSERT INTO Towns (Id, [Name]) VALUES
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')

-- 5

DROP TABLE Minions
DROP TABLE Towns

-- 7 

CREATE TABLE People (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(2),
	Height DECIMAL(15, 2),
	[Weight] DECIMAL(15, 2),
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(200),
)

INSERT INTO People ([Name], Height, [Weight], Gender, Birthdate) VALUES
('TESTER', 1.67, 67, 'm', '1998-02-14'),
('JENA', 1.57, 47, 'f', '1998-02-19'),
('TEJUK', 1.78, 100, 'm', '1988-04-29'),
('VISOK', 2.12, 87, 'm', '1999-12-24'),
('SLAB', 1.67, 37, 'f', '1995-04-09')

-- 8

CREATE TABLE Users (
	Id BIGINT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) NOT NULL UNIQUE,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(900),
	LastLoginTime TIME,
	IsDeleted INT
)

INSERT INTO Users(Username, [Password], IsDeleted) VALUES
('SADSAD', 'WQWQDSA', 0),
('WDWDDSA', 'DASDASDSA', 1),
('CBCVBCXF', 'DFSFDSFDSFDS', 0),
('LKJLKJMNMBN', 'EWREGFDG', 0),
('EST', 'TRI', 1)

-- 9

ALTER TABLE Users
DROP CONSTRAINT PK_Users

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (Id, Username)

-- 10

ALTER TABLE Users
ADD CONSTRAINT CHK_UserPassword CHECK (DATALENGTH([Password]) > 4);

-- 11

ALTER TABLE Users
ADD CONSTRAINT df_Time
DEFAULT CURRENT_TIMESTAMP FOR LastLoginTime

-- 12

ALTER TABLE Users
DROP CONSTRAINT PK_Users

ALTER TABLE Users
ADD CONSTRAINT PK_Users PRIMARY KEY (Id)

ALTER TABLE Users
ADD CONSTRAINT CHK_Username CHECK (DATALENGTH(Username) > 2);

-- 13

CREATE DATABASE Movies

CREATE TABLE Directors (
	Id INT PRIMARY KEY IDENTITY,
	DirectorName VARCHAR(30) NOT NULL,
	Notes VARCHAR(100)
)

INSERT INTO Directors(DirectorName, Notes) VALUES
('Pesho', NULL),
('Ivan', NULL),
('Gosho', 'NQMA KOMENTAR'),
('Strahil', NULL),
('Tosho', 'PULEN S BELEJKI')

CREATE TABLE Genres (
	Id INT PRIMARY KEY IDENTITY,
	GenreName VARCHAR(30) NOT NULL,
	Notes VARCHAR(100)
)

INSERT INTO Genres(GenreName, Notes) VALUES
('Comedy', 'MNOGO SMQH'),
('Horror', NULL),
('Action', 'QKO ACTION'),
('Sci-Fi', NULL),
('NEZNAM', 'NE ZNAM BRAT')

CREATE TABLE Categories (
	Id INT PRIMARY KEY IDENTITY,
	CategoryName VARCHAR(30) NOT NULL,
	Notes VARCHAR(100)
)

INSERT INTO Categories(CategoryName, Notes) VALUES
('IZCHERPAH', NULL),
('SE', NULL),
('BE', 'NQMA KOMENTAR'),
('BRAT', NULL),
('ME', 'PULEN S BELEJKI')

CREATE TABLE Movies (
	Id INT PRIMARY KEY IDENTITY,
	Title VARCHAR(40) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear INT NOT NULL,
	[Length] TIME,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Rating INT NOT NULL,
	Notes VARCHAR(200)
)

INSERT INTO Movies(Title, CopyrightYear, [Length], Rating, Notes) VALUES
('FILM', 1999, NULL, 5, NULL),
('STAR WARS', 1979, CURRENT_TIMESTAMP, 9, NULL),
('LORD OF THE RINGS', 1993, NULL, 8, 'MNOGO QK FILM'),
('MOVIE', 2014, NULL, 1, NULL),
('LUCIFER', 2016, NULL, 10, 'I''m THE DEVIL')

-- 14

CREATE DATABASE CarRental

CREATE TABLE Categories (
	Id INT PRIMARY KEY IDENTITY,
	CategoryName VARCHAR(40) NOT NULL,
	DailyRate DECIMAL(10, 2) NOT NULL,
	WeeklyRate DECIMAL(10, 2),
	MonthlyRate DECIMAL(10, 2),
	WeekendRate DECIMAL(10, 2) NOT NULL
)

INSERT INTO Categories(CategoryName, DailyRate, WeekendRate) VALUES
('Small Vehicle', 40.99, 60.99),
('Truck', 100.00, 150.00),
('Motor', 300.00, 450.00)

CREATE TABLE Cars (
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber BIGINT NOT NULL,
	Manufacturer VARCHAR(50),
	Model VARCHAR(50) NOT NULL,
	CarYear INT,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Doors INT,
	Picture VARBINARY(500),
	Condition VARCHAR(100),
	Available CHAR(1) NOT NULL
)

INSERT INTO Cars(PlateNumber, Model, CategoryId, Available) VALUES
(12344512, 'Corsa', 1, 'T'),
(64354543, 'Honda', 3, 'F'),
(23434633, 'Man', 2, 'T')

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	Title VARCHAR(50) NOT NULL,
	Notes VARCHAR(100)
)

INSERT INTO Employees(FirstName, LastName, Title) VALUES
('Pesho', 'Peshev', 'Obshtak'),
('Gosho', 'Geshev', 'Shefa'),
('Stamat', 'Grozniq', 'Chisti Kenefi')

CREATE TABLE Customers (
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenceNumber VARCHAR(MAX) NOT NULL,
	FullName VARCHAR(100) NOT NULL,
	[Address] VARCHAR(MAX) NOT NULL,
	City VARCHAR(50),
	ZIPCode INT NOT NULL,
	Notes VARCHAR(100)
)

INSERT INTO Customers(DriverLicenceNumber, FullName, [Address], ZIPCode) VALUES
('KJFSAGASKDBKS', 'PISNA MI OT EDNO I SUHTO', 'BULGARIQ BRAT', 2344),
('ADASASDASFF', 'NE ZNAM', 'SOFUQ', 1232),
('DASDASDSADADD', 'PISNA MI OT EDNO I SUHTO', 'BRAT', 2222)

CREATE TABLE RentalOrders (
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	CarId INT FOREIGN KEY REFERENCES Cars(Id),
	TankLevel INT NOT NULL,
    KilometrageStart INT NOT NULL,
    KilometrageEnd INT NOT NULL,
    TotalKilometrage AS KilometrageEnd - KilometrageStart,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    TotalDays AS DATEDIFF(DAY, StartDate, EndDate),
    RateApplied DECIMAL(9, 2),
    TaxRate DECIMAL(9, 2),
    OrderStatus NVARCHAR(50),
    Notes NVARCHAR(MAX),
)

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, StartDate, EndDate) 
       VALUES (1, 1, 1, 54, 2189, 2456, '2017-11-05', '2017-11-08'),
              (2, 2, 2, 22, 13565, 14258, '2017-11-06', '2017-11-11'),
              (3, 3, 3, 180, 1202, 1964, '2017-11-09', '2017-11-12')

-- 15

CREATE DATABASE Hotel

   USE Hotel

CREATE TABLE Employees (
	   Id INT IDENTITY (1, 1),
	   FirstName NVARCHAR(50),
	   LastName NVARCHAR(50),
	   Title NVARCHAR(50),
	   Notes NVARCHAR(MAX)

	   CONSTRAINT PK_Employees
	   PRIMARY KEY (Id)
)

CREATE TABLE Customers (
       AccountNumber INT IDENTITY(1, 1),
       FirstName NVARCHAR(50) NOT NULL,
       LastName NVARCHAR(50) NOT NULL,
       PhoneNumber NVARCHAR(20) NOT NULL,
       EmergencyName NVARCHAR(50),
       EmergencyNumber NVARCHAR(20) NOT NULL,
       Notes NVARCHAR(MAX),

       CONSTRAINT PK_Customers
       PRIMARY KEY (AccountNumber)
)

CREATE TABLE RoomStatus (
       RoomStatus NVARCHAR(50) NOT NULL,
       Notes NVARCHAR(MAX),

       CONSTRAINT PK_RoomStatus
       PRIMARY KEY (RoomStatus)
)

CREATE TABLE RoomTypes (
       RoomType NVARCHAR(50) NOT NULL,
       Notes NVARCHAR(MAX),

       CONSTRAINT PK_RoomTypes
       PRIMARY KEY (RoomType)
)

CREATE TABLE BedTypes (
       BedType NVARCHAR(50)NOT NULL,
       Notes NVARCHAR(MAX),

       CONSTRAINT PK_BedTypes
       PRIMARY KEY (BedType)
)

CREATE TABLE Rooms (
       RoomNumber INT IDENTITY(1, 1),
       RoomType NVARCHAR(50),
       BedType NVARCHAR(50),
       Rate DECIMAL(9, 2),
       RoomStatus NVARCHAR(50),
       Notes NVARCHAR(MAX),

       CONSTRAINT PK_Rooms
       PRIMARY KEY (RoomNumber),

       CONSTRAINT FK_Rooms_RoomTypes
       FOREIGN KEY (RoomType)
       REFERENCES RoomTypes(RoomType),

       CONSTRAINT FK_Rooms_BedTypes
       FOREIGN KEY (BedType)
       REFERENCES BedTypes(BedType),

       CONSTRAINT FK_Rooms_RoomStatus
       FOREIGN KEY (RoomStatus)
       REFERENCES RoomStatus(RoomStatus)
)

CREATE TABLE Payments (
       Id INT IDENTITY(1, 1),
       EmployeeId INT,
       PaymentDate DATE NOT NULL,
       AccountNumber INT,
       FirstDateOccupied DATE NOT NULL,
       LastDateOccupied DATE NOT NULL,
       TotalDays AS DATEDIFF(Day, FirstDateOccupied, LastDateOccupied),
       AmountCharged DECIMAL(9, 2) NOT NULL,
       TaxRate DECIMAL(9, 2),
       TaxAmount DECIMAL(9, 2),
       PaymentTotal DECIMAL(9, 2),
       Notes NVARCHAR(MAX),

       CONSTRAINT PK_Payments
       PRIMARY KEY (Id),

       CONSTRAINT FK_Payments_Employees
       FOREIGN KEY (EmployeeId)
       REFERENCES Employees(Id),

       CONSTRAINT FK_Payments_Customers
       FOREIGN KEY (AccountNumber)
       REFERENCES Customers(AccountNumber)
)

CREATE TABLE Occupancies (
       Id INT IDENTITY(1, 1),
       EmployeeId INT,
       DateOccupied DATE,
       AccountNumber INT,
       RoomNumber INT,
       RateApplied DECIMAL(9, 2) NOT NULL,
       PhoneCharge DECIMAL(9, 2),
       Notes NVARCHAR(MAX),

       CONSTRAINT PK_Occupancies
       PRIMARY KEY (Id),

       CONSTRAINT FK_Occupancies_Employees
       FOREIGN KEY (EmployeeId)
       REFERENCES Employees(Id),
   
       CONSTRAINT FK_Occupancies_Customers
       FOREIGN KEY (AccountNumber)
       REFERENCES Customers(AccountNumber),

       CONSTRAINT FK_Occupancies_Rooms
       FOREIGN KEY (RoomNumber)
       REFERENCES Rooms(RoomNumber)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes) 
       VALUES ('Ivan', 'Ivanov', 'Receptionist', 'I am Ivan'),
              ('Martin', 'Martinov', 'Technical support', 'I am Martin'),
              ('Mara', 'Mareva', 'Cleaner', 'I am Marcheto')

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyNumber)
       VALUES ('Pesho', 'Peshov', '+395883333333', '123'),
              ('Gosho', 'Goshov', '+395882222222', '123'),
              ('Kosio', 'Kosiov', '+395888888888', '123')

INSERT INTO RoomStatus (RoomStatus, Notes) 
       VALUES ('Clean', 'The room is clean.'),
              ('Dirty', 'The room is dirty.'),
              ('Repair', 'The room is for repair.')

INSERT INTO RoomTypes (RoomType, Notes)
       VALUES ('Small', 'Room with one bed'),
              ('Medium', 'Room with two beds'),
              ('Large', 'Room with three beds')

INSERT INTO BedTypes (BedType)
       VALUES ('Normal'),
              ('Comfort'),
              ('VIP')

INSERT INTO Rooms (RoomType, BedType, Rate, RoomStatus)
       VALUES ('Small', 'Normal', 50, 'Dirty'),
              ('Medium', 'Comfort', 70, 'Clean'),
              ('Large', 'VIP', 100, 'Repair')

INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, AmountCharged, TaxRate)
       VALUES (1, '2015-05-06', 1, '2015-06-18', '2015-07-03', 1256.33, 166.23),
              (2, '2017-10-11', 2, '2017-10-12', '2017-10-22', 556, 125.95),
              (3, '2017-10-26', 3, '2017-11-09', '2017-11-11', 146.74, 100)

INSERT INTO Occupancies (EmployeeId, AccountNumber, RoomNumber, RateApplied)
       VALUES (1, 1, 1, 55.55),
              (2, 2, 2, 44.44),
              (3, 3, 3, 33.33)

-- 16

CREATE DATABASE SoftUni
    GO
   USE SoftUni

CREATE TABLE Towns (
       Id INT IDENTITY(1, 1),
       Name NVARCHAR(50) NOT NULL,
    
       CONSTRAINT PK_Towns
       PRIMARY KEY (Id)
)

CREATE TABLE Addresses (
       Id INT IDENTITY(1, 1),
       AddressText NVARCHAR(50) NOT NULL,
       TownId INT,
    
       CONSTRAINT PK_Addresses
       PRIMARY KEY (Id),
    
       CONSTRAINT FK_Addresses_Towns
       FOREIGN KEY (TownId)
       REFERENCES Towns(Id)
)

CREATE TABLE Departments (
       Id INT IDENTITY(1, 1),
       Name NVARCHAR(50) NOT NULL,
    
       CONSTRAINT PK_Departments
       PRIMARY KEY (Id)
)

CREATE TABLE Employees (
       Id INT IDENTITY(1, 1),
       FirstName NVARCHAR(20) NOT NULL,
       MiddleName NVARCHAR(20) NOT NULL,
       LastName NVARCHAR(20) NOT NULL,
       JobTitle NVARCHAR(20) NOT NULL,
       DepartmentId INT,
       HireDate DATE,
       Salary DECIMAL(9, 2) NOT NULL,
       AddressId INT FOREIGN KEY REFERENCES Addresses(Id),
    
       CONSTRAINT PK_Employees
       PRIMARY KEY (Id),
    
       CONSTRAINT FK_Employees_Departments
       FOREIGN KEY (DepartmentId)
       REFERENCES Departments(Id),
    
       CONSTRAINT FK_Employees_Addresses
       FOREIGN KEY (AddressId)
       REFERENCES Addresses(Id)
)

-- 17

BACKUP DATABASE SoftUni TO DISK='C:\Users\Todor\Desktop\SoftUni\C# DB\MS SQL Server\1.Introduction to Databases\Exercise\softuni-backup.bak'

   DROP DATABASE SoftUni

RESTORE DATABASE SoftUni FROM DISK='C:\Users\Todor\Desktop\SoftUni\C# DB\MS SQL Server\1.Introduction to Databases\Exercise\softuni-backup.bak'

-- 18

INSERT INTO Towns (Name)
       VALUES ('Sofia'),
              ('Plovdiv'),
              ('Varna'),
              ('Burgas')

INSERT INTO Departments (Name)
       VALUES ('Engineering'),
              ('Sales'),
              ('Marketing'),
              ('Software Development'),
              ('Quality Assurance')

INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
       VALUES ('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500),
              ('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000),
              ('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25),
              ('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 5000),
              ('Peter', 'Dimitrov', 'Georgiev', 'Intern', 3, '2016-08-28', 599.88)

-- 19

SELECT * FROM Towns

SELECT * FROM Departments

SELECT * FROM Employees

-- 20

  SELECT * FROM Towns
ORDER BY Name

  SELECT * FROM Departments
ORDER BY Name

  SELECT * FROM Employees
ORDER BY Salary DESC

-- 21

  SELECT Name
    FROM Towns
ORDER BY Name

  SELECT Name
    FROM Departments
ORDER BY Name

  SELECT FirstName, LastName, JobTitle, Salary
    FROM Employees
ORDER BY Salary DESC

-- 22

UPDATE Employees
   SET Salary *= 1.1

SELECT Salary
  FROM Employees

-- 23

UPDATE Payments
   SET TaxRate *= 0.97

SELECT TaxRate
  FROM Payments

-- 24

TRUNCATE TABLE Occupancies