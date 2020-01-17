CREATE TABLE Manufacturers (
	ManufacturerID INT NOT NULL IDENTITY(1, 1),
	[Name] VARCHAR(20) NOT NULL,
	EstablishedOn DATE NOT NULL,

	CONSTRAINT PK_Manufacturers PRIMARY KEY (ManufacturerID)
)

INSERT INTO Manufacturers ([Name], EstablishedOn) VALUES
('BMW', '07/03/1916'),
('Tesla', '01/01/2003'),
('Lada', '01/05/1966')

CREATE TABLE Models (
	ModelID INT NOT NULL IDENTITY(101, 1),
	[Name] VARCHAR(30) NOT NULL,
	ManufacturerID INT NOT NULL,

	CONSTRAINT PK_ModelId PRIMARY KEY (ModelID),
	CONSTRAINT FK_Model_Manufacturer FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Models ([Name], ManufacturerID) VALUES
('X1', 1),
('i6', 1),
('Model S', 2),
('Model X', 2),
('Model 3', 2),
('Nova', 3)