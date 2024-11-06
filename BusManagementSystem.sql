USE [master]
GO
/****** Object:  Database [BusManagementSystem]    Script Date: 06/11/2024 3:31:35 CH ******/
CREATE DATABASE [BusManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BusManagementSystem', FILENAME = N'D:\New folder\MSSQL16.MSSQLSERVER\MSSQL\DATA\BusManagementSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BusManagementSystem_log', FILENAME = N'D:\New folder\MSSQL16.MSSQLSERVER\MSSQL\DATA\BusManagementSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BusManagementSystem] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BusManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BusManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BusManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BusManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BusManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BusManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [BusManagementSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BusManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BusManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BusManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BusManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BusManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BusManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BusManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BusManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BusManagementSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BusManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BusManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BusManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BusManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BusManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BusManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BusManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BusManagementSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [BusManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [BusManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BusManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BusManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BusManagementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BusManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BusManagementSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BusManagementSystem', N'ON'
GO
ALTER DATABASE [BusManagementSystem] SET QUERY_STORE = ON
GO
ALTER DATABASE [BusManagementSystem] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BusManagementSystem]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 06/11/2024 3:31:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[BusId] [int] NULL,
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
/****** Object:  Table [dbo].[Bus]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[BusStop]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[Driver]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[FreeTicket]    Script Date: 06/11/2024 3:31:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreeTicket](
	[FreeTicketId] [int] IDENTITY(1,1) NOT NULL,
	[TicketId] [int] NOT NULL,
	[RecipientName] [varchar](100) NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[IDNumber] [varchar](20) NOT NULL,
	[IDCardFront] [varbinary](max) NULL,
	[IDCardBack] [varbinary](max) NULL,
	[District] [varchar](100) NOT NULL,
	[Ward] [varchar](100) NOT NULL,
	[RecipientType] [varchar](50) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](100) NULL,
	[Portrait3x4Image] [varbinary](max) NULL,
	[ProofFrontImage] [varbinary](max) NOT NULL,
	[ProofBackImage] [varbinary](max) NOT NULL,
	[TicketDeliveryAddress] [varchar](255) NOT NULL,
	[IssueDate] [date] NOT NULL,
	[ValidUntil] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[FreeTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FreeTicketVerification]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[Payment]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[PaymentDetail]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[Route]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[Ticket]    Script Date: 06/11/2024 3:31:35 CH ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 06/11/2024 3:31:35 CH ******/
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
SET IDENTITY_INSERT [dbo].[Driver] ON 

INSERT [dbo].[Driver] ([DriverId], [Name], [PhoneNumber], [Status], [Shift], [Email], [RoleId]) VALUES (1, N'John Doe', N'1234567890', 1, CAST(N'2024-10-22T08:00:00.0000000' AS DateTime2), N'johndoe@BusManagement.org', 4)
SET IDENTITY_INSERT [dbo].[Driver] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Staff')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'Member')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (4, N'Driver')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Route] ON 

INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (1, N'10', N'District 1', N'District 7', CAST(10.50 AS Decimal(10, 2)), CAST(N'00:25:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (2, N'55', N'Tan Binh District', N'Phu Nhuan District', CAST(8.30 AS Decimal(10, 2)), CAST(N'00:20:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (3, N'52', N'District 2', N'Thu Duc City', CAST(15.20 AS Decimal(10, 2)), CAST(N'00:35:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (4, N'01', N'Binh Thanh District', N'District 9', CAST(12.00 AS Decimal(10, 2)), CAST(N'00:30:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (5, N'30', N'District 3', N'Go Vap District', CAST(7.80 AS Decimal(10, 2)), CAST(N'00:18:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (6, N'09', N'District 5', N'District 10', CAST(6.40 AS Decimal(10, 2)), CAST(N'00:15:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (7, N'13', N'District 4', N'District 8', CAST(9.10 AS Decimal(10, 2)), CAST(N'00:22:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (8, N'D4', N'Tan Phu District', N'Binh Tan District', CAST(13.70 AS Decimal(10, 2)), CAST(N'00:32:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (9, N'67-1', N'District 1', N'District 3', CAST(5.60 AS Decimal(10, 2)), CAST(N'00:12:00' AS Time))
INSERT [dbo].[Route] ([RouteId], [RouteName], [StartLocation], [EndLocation], [Distance], [Duration]) VALUES (10, N'70-1', N'District 11', N'District 6', CAST(8.90 AS Decimal(10, 2)), CAST(N'00:20:00' AS Time))
SET IDENTITY_INSERT [dbo].[Route] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Name], [DateOfBirth], [RoleId], [Email], [PhoneNumber], [Password], [Status]) VALUES (1, N'SystemAdmin', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 1, N'SystemAdmin@BusManagement.org', N'1234567890', N'@1', 1)
INSERT [dbo].[User] ([UserId], [Name], [DateOfBirth], [RoleId], [Email], [PhoneNumber], [Password], [Status]) VALUES (2, N'Isabella David', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 2, N'staff1@BusManagement.org', N'2345678901', N'@1', 1)
INSERT [dbo].[User] ([UserId], [Name], [DateOfBirth], [RoleId], [Email], [PhoneNumber], [Password], [Status]) VALUES (3, N'Michael Charlotte', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 2, N'staff2@BusManagement.org', N'3456789012', N'@1', 1)
INSERT [dbo].[User] ([UserId], [Name], [DateOfBirth], [RoleId], [Email], [PhoneNumber], [Password], [Status]) VALUES (4, N'Steve Paris', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 2, N'staff3@BusManagement.org', N'4567890123', N'@1', 1)
INSERT [dbo].[User] ([UserId], [Name], [DateOfBirth], [RoleId], [Email], [PhoneNumber], [Password], [Status]) VALUES (5, N'NguyenLe', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 3, N'nguyenbr23@gmail.com', N'5678901234', N'@1', 1)
INSERT [dbo].[User] ([UserId], [Name], [DateOfBirth], [RoleId], [Email], [PhoneNumber], [Password], [Status]) VALUES (6, N'Member User 2', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 3, N'member2@gmail.com', N'6789012345', N'@1', 1)
INSERT [dbo].[User] ([UserId], [Name], [DateOfBirth], [RoleId], [Email], [PhoneNumber], [Password], [Status]) VALUES (7, N'Member User 3', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), 3, N'member3@gmail.com', N'7890123456', N'@1', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
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
GO
ALTER TABLE [dbo].[BusStop]  WITH CHECK ADD FOREIGN KEY([RouteId])
REFERENCES [dbo].[Route] ([RouteId])
GO
ALTER TABLE [dbo].[Driver]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[FreeTicket]  WITH CHECK ADD FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([TicketId])
GO
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
USE [master]
GO
ALTER DATABASE [BusManagementSystem] SET  READ_WRITE 
GO
