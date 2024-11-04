USE master
GO

CREATE DATABASE [BusManagementSystem]
GO

USE [BusManagementSystem]
GO

-- Create tables
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Table: Role
CREATE TABLE [dbo].[Role](
    RoleId INT IDENTITY(1,1) NOT NULL,
    RoleName NVARCHAR(50) NULL,
    PRIMARY KEY CLUSTERED (RoleId ASC)
) ON [PRIMARY]
GO

-- Table: Route
CREATE TABLE [dbo].[Route](
    RouteId INT IDENTITY(1,1) NOT NULL,
    RouteName NVARCHAR(100) NULL,
    StartLocation NVARCHAR(100) NULL,
    EndLocation NVARCHAR(100) NULL,
    Distance DECIMAL(10, 2) NULL,
    Duration TIME(7) NULL,
    PRIMARY KEY CLUSTERED (RouteId ASC)
) ON [PRIMARY]
GO

-- Table: User
CREATE TABLE [dbo].[User](
    UserId INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(50) NULL,
    DateOfBirth DATETIME2(7) NULL,
    RoleId INT NULL,
    Email NVARCHAR(50) NULL,
    PhoneNumber NVARCHAR(11) NULL,
    Password NVARCHAR(11) NULL,
    Status INT NULL,
    PRIMARY KEY CLUSTERED (UserId ASC),
    FOREIGN KEY (RoleId) REFERENCES [dbo].[Role] (RoleId)
) ON [PRIMARY]
GO

-- Table: Ticket
CREATE TABLE Ticket (
    TicketId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    Price DECIMAL(10, 2),
    StartDate DATETIME2(7),
    EndDate DATETIME2(7),
    Status INT,
    IsFreeTicket BIT DEFAULT 0,
    RouteId INT,
    TicketType NVARCHAR(50),
    PriorityType NVARCHAR(50),
    IsPriority BIT,
    Photo3x4 VARBINARY(MAX),
    IDCardFront VARBINARY(MAX),
    IDCardBack VARBINARY(MAX),
    FOREIGN KEY (UserId) REFERENCES [dbo].[User] (UserId),
    FOREIGN KEY (RouteId) REFERENCES [dbo].[Route] (RouteId)
) ON [PRIMARY]
GO

-- Table: Booking
CREATE TABLE Booking (
    BookingId INT IDENTITY(1,1) NOT NULL,
    UserId INT,
    BusId INT,
    BookingDate DATETIME2 DEFAULT GETDATE(),
    Status INT,
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    ModifiedAt DATETIME2,
    CreatedBy INT,
    ModifiedBy INT,
    TicketId INT,
    PRIMARY KEY CLUSTERED (BookingId ASC),
    FOREIGN KEY (UserId) REFERENCES [dbo].[User] (UserId),
    FOREIGN KEY (TicketId) REFERENCES Ticket (TicketId)
) ON [PRIMARY]
GO

-- Table: Bus
CREATE TABLE [dbo].[Bus](
    BusId INT IDENTITY(1,1) NOT NULL,
    BusNumber INT NULL,
    DriverId INT NULL,
    Status INT NULL,
    AssignedRouteId INT NULL,
    CreatedAt DATETIME2(7) DEFAULT GETDATE(),
    ModifiedAt DATETIME2(7),
    PRIMARY KEY CLUSTERED (BusId ASC),
    FOREIGN KEY (AssignedRouteId) REFERENCES [dbo].[Route] (RouteId)
) ON [PRIMARY]
GO

-- Table: BusStop
CREATE TABLE [dbo].[BusStop](
    StopId INT IDENTITY(1,1) NOT NULL,
    StopName NVARCHAR(100) NULL,
    Location NVARCHAR(100) NULL,
    RouteId INT NULL,
    StopOrder INT NULL,
    PRIMARY KEY CLUSTERED (StopId ASC),
    FOREIGN KEY (RouteId) REFERENCES [dbo].[Route] (RouteId)
) ON [PRIMARY]
GO

-- Table: Driver
CREATE TABLE [dbo].[Driver](
    DriverId INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(50) NULL,
    PhoneNumber NVARCHAR(15) NULL,
    Status INT NULL,
    Shift DATETIME2(7) NULL,
    Email NVARCHAR(50) NULL,
    RoleId INT NULL,
    PRIMARY KEY CLUSTERED (DriverId ASC),
    FOREIGN KEY (RoleId) REFERENCES [dbo].[Role] (RoleId)
) ON [PRIMARY]
GO

-- Table: FreeTicket
CREATE TABLE [dbo].[FreeTicket](
    FreeTicketId INT IDENTITY(1,1) PRIMARY KEY,
    TicketId INT NOT NULL,
    RecipientName VARCHAR(100) NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    DateOfBirth DATE NOT NULL,
    IDNumber VARCHAR(20) NOT NULL,
    IDFrontImage VARCHAR(255),
    IDBackImage VARCHAR(255),
    District VARCHAR(100) NOT NULL,
    Ward VARCHAR(100) NOT NULL,
    RecipientType VARCHAR(50) NOT NULL,
    Phone VARCHAR(15) NOT NULL,
    Email VARCHAR(100),
    Portrait3x4Image VARCHAR(255),
    ProofFrontImage VARCHAR(255) NOT NULL,
    ProofBackImage VARCHAR(255) NOT NULL,
    TicketDeliveryAddress VARCHAR(255) NOT NULL,
    IssueDate DATE NOT NULL,
    ValidUntil DATE,
    FOREIGN KEY (TicketId) REFERENCES Ticket (TicketId)
) ON [PRIMARY]
GO

-- Table: FreeTicketVerification
CREATE TABLE [dbo].[FreeTicketVerification](
    VerificationId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    FreeTicketId INT,
    VerificationImage NVARCHAR(255),
    VerificationDate DATETIME2(7) DEFAULT GETDATE(),
    Status INT,
    FOREIGN KEY (UserId) REFERENCES [dbo].[User] (UserId),
    FOREIGN KEY (FreeTicketId) REFERENCES [dbo].[FreeTicket] (FreeTicketId)
) ON [PRIMARY]
GO

-- Table: Payment
CREATE TABLE [dbo].[Payment](
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT,
    UserId INT,
    Amount DECIMAL(10, 2),
    PaymentDate DATETIME2(7) DEFAULT GETDATE(),
    FOREIGN KEY (BookingId) REFERENCES [dbo].[Booking] (BookingId),
    FOREIGN KEY (UserId) REFERENCES [dbo].[User] (UserId)
) ON [PRIMARY]
GO

-- Table: PaymentDetail
CREATE TABLE [dbo].[PaymentDetail](
    PaymentDetailId INT IDENTITY(1,1) PRIMARY KEY,
    PaymentId INT,
    Description NVARCHAR(255),
    Status INT,
    FOREIGN KEY (PaymentId) REFERENCES [dbo].[Payment] (PaymentId)
) ON [PRIMARY]
GO

-- Adding sample data for Roles (optional)
INSERT INTO [BusManagementSystem].[dbo].[Role] (RoleName) VALUES ('Admin'), ('Staff'), ('Member'), ('Driver');

-- Inserting sample accounts into User table
-- Insert admin account
INSERT INTO [BusManagementSystem].[dbo].[User] (Name, DateOfBirth, RoleId, Email, PhoneNumber, Password, Status)
VALUES ('SystemAdmin', '2024-01-01', 1, 'SystemAdmin@BusManagement.org', '1234567890', '@1', 1);

-- Insert staff accounts
INSERT INTO [BusManagementSystem].[dbo].[User] (Name, DateOfBirth, RoleId, Email, PhoneNumber, Password, Status)
VALUES 
    ('Isabella David', '2024-01-01', 2, 'staff1@BusManagement.org', '2345678901', '@1', 1),
    ('Michael Charlotte', '2024-01-01', 2, 'staff2@BusManagement.org', '3456789012', '@1', 1),
    ('Steve Paris', '2024-01-01', 2, 'staff3@BusManagement.org', '4567890123', '@1', 1);

-- Insert random member accounts
INSERT INTO [BusManagementSystem].[dbo].[User] (Name, DateOfBirth, RoleId, Email, PhoneNumber, Password, Status)
VALUES 
    ('NguyenLe', '2024-01-01', 3, 'nguyenbr23@gmail.com', '5678901234', '@1', 1),
    ('Member User 2', '2024-01-01', 3, 'member2@gmail.com', '6789012345', '@1', 1),
    ('Member User 3', '2024-01-01', 3, 'member3@gmail.com', '7890123456', '@1', 1);

-- Insert a new driver
INSERT INTO [BusManagementSystem].[dbo].[Driver] (Name, PhoneNumber, Status, Shift, Email, RoleId)
VALUES ('John Doe', '1234567890', 1, '2024-10-22 08:00:00', 'johndoe@BusManagement.org', 4);

-- Insert Route data
INSERT INTO [BusManagementSystem].[dbo].[Route] ([RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) 
VALUES
('10', 'District 1', 'District 7', 10.5, '00:25:00'),
('55', 'Tan Binh District', 'Phu Nhuan District', 8.3, '00:20:00'),
('52', 'District 2', 'Thu Duc City', 15.2, '00:35:00'),
('01', 'Binh Thanh District', 'District 9', 12.0, '00:30:00'),
('30', 'District 3', 'Go Vap District', 7.8, '00:18:00'),
('09', 'District 5', 'District 10', 6.4, '00:15:00'),
('13', 'District 4', 'District 8', 9.1, '00:22:00'),
('D4', 'Tan Phu District', 'Binh Tan District', 13.7, '00:32:00'),
('67-1', 'District 1', 'District 3', 5.6, '00:12:00'),
('70-1', 'District 11', 'District 6', 8.9, '00:20:00');
