USE master
GO

Create database [BusManagementSystem]
go 

USE [BusManagementSystem]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 11/6/2024 3:48:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[BookingDate] [datetime2](7) NULL,
	[Status] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[ModifiedAt] [datetime2](7) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[TicketId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bus]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bus](
	[BusId] [int] IDENTITY(1,1) NOT NULL,
	[BusNumber] [int] NULL,
	[DriverId] [int] NULL,
	[Status] [int] NULL,
	[AssignedRouteId] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[ModifiedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[BusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusStop]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusStop](
	[StopId] [int] IDENTITY(1,1) NOT NULL,
	[StopName] [nvarchar](100) NULL,
	[Location] [nvarchar](100) NULL,
	[RouteId] [int] NULL,
	[StopOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Driver]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[DriverId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Status] [int] NULL,
	[Shift] [datetime2](7) NULL,
	[Email] [nvarchar](50) NULL,
	[RoleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FreeTicket]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreeTicket](
	[FreeTicketId] [int] IDENTITY(1,1) NOT NULL,
	--[TicketId] [int] NOT NULL,
	[RecipientName] [varchar](100) NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[IDNumber] [varchar](20) NOT NULL,
	[IDFrontImage] [varbinary](max) NULL,
	[IDBackImage] [varbinary](max) NULL,
	[District] [varchar](100) NOT NULL,
	[Ward] [varchar](100) NOT NULL,
	[RecipientType] [varchar](50) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](100) NULL,
	[Portrait3x4Image] [varbinary](max) NULL,
	[ProofFrontImage] [varbinary](max) NULL,
	[ProofBackImage] [varbinary](max) NULL,
	[TicketDeliveryAddress] [varchar](255) NOT NULL,
	[IssueDate] [date] NOT NULL,
	[ValidUntil] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[FreeTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FreeTicketVerification]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreeTicketVerification](
	[VerificationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FreeTicketId] [int] NULL,
	[VerificationImage] [nvarchar](255) NULL,
	[VerificationDate] [datetime2](7) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VerificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NULL,
	[UserId] [int] NULL,
	[Amount] [decimal](10, 2) NULL,
	[PaymentDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentDetail]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDetail](
	[PaymentDetailId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentId] [int] NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Route]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[RouteId] [int] IDENTITY(1,1) NOT NULL,
	[RouteName] [nvarchar](100) NULL,
	[StartLocation] [nvarchar](100) NULL,
	[EndLocation] [nvarchar](100) NULL,
	[Distance] [decimal](10, 2) NULL,
	[Duration] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[RouteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Price] [decimal](10, 2) NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[Status] [int] NULL,
	[IsFreeTicket] [bit] NULL,
	[RouteId] [int] NULL,
	[TicketType] [nvarchar](50) NULL,
	[PriorityType] [nvarchar](50) NULL,
	[IsPriority] [bit] NULL,
	[Photo3x4] [varbinary](max) NULL,
	[IDCardFront] [varbinary](max) NULL,
	[IDCardBack] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/6/2024 3:48:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[RoleId] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
	[Password] [nvarchar](11) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Booking] ADD  DEFAULT (getdate()) FOR [BookingDate]
GO
ALTER TABLE [dbo].[Booking] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Bus] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FreeTicketVerification] ADD  DEFAULT (getdate()) FOR [VerificationDate]
GO
ALTER TABLE [dbo].[Payment] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[Ticket] ADD  DEFAULT ((0)) FOR [IsFreeTicket]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([TicketId])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Bus]  WITH CHECK ADD FOREIGN KEY([AssignedRouteId])
REFERENCES [dbo].[Route] ([RouteId])
ALTER TABLE [dbo].[Bus]  WITH CHECK ADD FOREIGN KEY([DriverId])
REFERENCES [dbo].[Driver] ([DriverId])
GO
ALTER TABLE [dbo].[BusStop]  WITH CHECK ADD FOREIGN KEY([RouteId])
REFERENCES [dbo].[Route] ([RouteId])
GO
ALTER TABLE [dbo].[Driver]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
--ALTER TABLE [dbo].[FreeTicket]  WITH CHECK ADD FOREIGN KEY([TicketId])
--REFERENCES [dbo].[Ticket] ([TicketId])
--GO
ALTER TABLE [dbo].[FreeTicketVerification]  WITH CHECK ADD FOREIGN KEY([FreeTicketId])
REFERENCES [dbo].[FreeTicket] ([FreeTicketId])
GO
ALTER TABLE [dbo].[FreeTicketVerification]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([BookingId])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[PaymentDetail]  WITH CHECK ADD FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([PaymentId])
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD FOREIGN KEY([RouteId])
REFERENCES [dbo].[Route] ([RouteId])
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
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
