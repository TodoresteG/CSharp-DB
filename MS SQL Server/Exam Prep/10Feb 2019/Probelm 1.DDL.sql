CREATE TABLE Planets (
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(30) NOT NULL
)

CREATE TABLE Spaceports (
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	PlanetId INT FOREIGN KEY REFERENCES Planets(Id)
)

CREATE TABLE Spaceships (
	Id INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	Manufacturer VARCHAR(50) NOT NULL,
	LightSpeedRate INT DEFAULT 0
)

CREATE TABLE Colonists (
	Id INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	Ucn VARCHAR(10) NOT NULL UNIQUE,
	BirthDate DATE NOT NULL
)

CREATE TABLE Journeys (
	Id INT PRIMARY KEY IDENTITY,
	JourneyStart DATETIME NOT NULL,
	JourneyEnd DATETIME NOT NULL,
	Purpose VARCHAR(11),
	DestinationSpaceportId INT FOREIGN KEY REFERENCES Spaceports(Id),
	SpaceshipId INT FOREIGN KEY REFERENCES Spaceships(Id),

	CONSTRAINT chk_Purpose CHECK (Purpose IN ('Medical', 'Technical', 'Educational', 'Military'))
)

CREATE TABLE TravelCards (
	Id INT PRIMARY KEY IDENTITY,
	CardNumber CHAR(10) NOT NULL UNIQUE,
	JobDuringJourney VARCHAR(8),
	ColonistId INT FOREIGN KEY REFERENCES Colonists(Id),
	JourneyId INT FOREIGN KEY REFERENCES Journeys(Id),

	CONSTRAINT chk_JobDuringJourney CHECK (JobDuringJourney IN ('Pilot', 'Engineer', 'Trooper', 'Cleaner', 'Cook'))
)