-- Create the BusManagementSystem database
CREATE DATABASE BusManagementSystem;
GO

-- Use the new database
USE BusManagementSystem;
GO

-- Create Role table
CREATE TABLE Role (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50)
);

-- Create User table
CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    DateOfBirth DATETIME2,
    RoleId INT FOREIGN KEY REFERENCES Role(RoleId),
    Email NVARCHAR(50),
    PhoneNumber NVARCHAR(11),
    Status INT
);

-- Create Driver table
CREATE TABLE Driver (
    DriverId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    PhoneNumber NVARCHAR(15),
    Status INT,
    Shift DATETIME2,
    Email NVARCHAR(50)
);

-- Create Route table
CREATE TABLE Route (
    RouteId INT PRIMARY KEY IDENTITY(1,1),
    RouteName NVARCHAR(100),
    StartLocation NVARCHAR(100),
    EndLocation NVARCHAR(100),
    Distance DECIMAL(10, 2),
    Duration TIME
);

-- Create Bus table (now referencing Driver and Route correctly)
CREATE TABLE Bus (
    BusId INT PRIMARY KEY IDENTITY(1,1),
    BusNumber INT,
    DriverId INT FOREIGN KEY REFERENCES Driver(DriverId),
    Status INT,
    AssignedRouteId INT FOREIGN KEY REFERENCES Route(RouteId),
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    ModifiedAt DATETIME2
);

-- Create BusStop table
CREATE TABLE BusStop (
    StopId INT PRIMARY KEY IDENTITY(1,1),
    StopName NVARCHAR(100),
    Location NVARCHAR(100),
    RouteId INT FOREIGN KEY REFERENCES Route(RouteId),
    StopOrder INT
);

-- Create Booking table
CREATE TABLE Booking (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT FOREIGN KEY REFERENCES [User](UserId),
    BusId INT FOREIGN KEY REFERENCES Bus(BusId),
    BookingDate DATETIME2 DEFAULT GETDATE(),
    Status INT,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    ModifiedAt DATETIME2,
    CreatedBy INT,
    ModifiedBy INT
);

-- Create Ticket table
CREATE TABLE Ticket (
    TicketId INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT FOREIGN KEY REFERENCES Booking(BookingId),
    UserId INT FOREIGN KEY REFERENCES [User](UserId),
    Price FLOAT,
    DateTime DATETIME2,
    Status INT
);

-- Create Payment table
CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY IDENTITY(1,1),
    BookingId INT FOREIGN KEY REFERENCES Booking(BookingId),
    UserId INT FOREIGN KEY REFERENCES [User](UserId),
    Amount DECIMAL(10, 2),
    PaymentDate DATETIME2 DEFAULT GETDATE()
);

-- Create PaymentDetail table
CREATE TABLE PaymentDetail (
    PaymentDetailId INT PRIMARY KEY IDENTITY(1,1),
    PaymentId INT FOREIGN KEY REFERENCES Payment(PaymentId),
    Description NVARCHAR(255),
    Status INT
);

-- Adding sample data for Roles (optional)
INSERT INTO Role (RoleName) VALUES ('Admin'), ('Staff'), ('Member');
