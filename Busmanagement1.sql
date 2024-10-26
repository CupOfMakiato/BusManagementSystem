USE [master]
GO
/****** Object:  Database [BusManagementSystem1]    Script Date: 26/10/2024 10:03:49 PM ******/
CREATE DATABASE [BusManagementSystem1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BusManagementSystem1', FILENAME = N'/var/opt/mssql/data/BusManagementSystem1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BusManagementSystem1_log', FILENAME = N'/var/opt/mssql/data/BusManagementSystem1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BusManagementSystem1] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BusManagementSystem1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BusManagementSystem1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET ARITHABORT OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BusManagementSystem1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BusManagementSystem1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BusManagementSystem1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BusManagementSystem1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET RECOVERY FULL 
GO
ALTER DATABASE [BusManagementSystem1] SET  MULTI_USER 
GO
ALTER DATABASE [BusManagementSystem1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BusManagementSystem1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BusManagementSystem1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BusManagementSystem1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BusManagementSystem1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BusManagementSystem1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BusManagementSystem1', N'ON'
GO
ALTER DATABASE [BusManagementSystem1] SET QUERY_STORE = OFF
GO
USE [BusManagementSystem1]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 26/10/2024 10:03:49 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bus]    Script Date: 26/10/2024 10:03:49 PM ******/
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
/****** Object:  Table [dbo].[BusStop]    Script Date: 26/10/2024 10:03:49 PM ******/
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
/****** Object:  Table [dbo].[Driver]    Script Date: 26/10/2024 10:03:49 PM ******/
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
	[Password] [nvarchar](11) NULL,
	[RoleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FreeTicket]    Script Date: 26/10/2024 10:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreeTicket](
	[FreeTicketID] [int] IDENTITY(1,1) NOT NULL,
	[TicketID] [int] NOT NULL,
	[RecipientName] [varchar](100) NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[IDNumber] [varchar](20) NOT NULL,
	[IDFrontImage] [varchar](255) NULL,
	[IDBackImage] [varchar](255) NULL,
	[District] [varchar](100) NOT NULL,
	[Ward] [varchar](100) NOT NULL,
	[RecipientType] [varchar](50) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](100) NULL,
	[Portrait3x4Image] [varchar](255) NULL,
	[ProofFrontImage] [varchar](255) NOT NULL,
	[ProofBackImage] [varchar](255) NOT NULL,
	[TicketDeliveryAddress] [varchar](255) NOT NULL,
	[IssueDate] [date] NOT NULL,
	[ValidUntil] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[FreeTicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FreeTicketVerification]    Script Date: 26/10/2024 10:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FreeTicketVerification](
	[VerificationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[VerificationImage] [nvarchar](255) NULL,
	[VerificationDate] [datetime2](7) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VerificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 26/10/2024 10:03:49 PM ******/
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
/****** Object:  Table [dbo].[PaymentDetail]    Script Date: 26/10/2024 10:03:49 PM ******/
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
/****** Object:  Table [dbo].[RegistrationType]    Script Date: 26/10/2024 10:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistrationType](
	[RegistrationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RegistrationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 26/10/2024 10:03:49 PM ******/
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
/****** Object:  Table [dbo].[Route]    Script Date: 26/10/2024 10:03:49 PM ******/
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
/****** Object:  Table [dbo].[RouteTicket]    Script Date: 26/10/2024 10:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RouteTicket](
	[RouteTicketId] [int] IDENTITY(1,1) NOT NULL,
	[RouteId] [int] NOT NULL,
	[TicketId] [int] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RouteTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 26/10/2024 10:03:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NULL,
	[UserId] [int] NULL,
	[Price] [decimal](10, 2) NULL,
	[DateTime] [datetime2](7) NULL,
	[Status] [int] NULL,
	[RegistrationTypeId] [int] NULL,
	[IsFreeTicket] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26/10/2024 10:03:49 PM ******/
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
ALTER TABLE [dbo].[RouteTicket] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[RouteTicket] ADD  DEFAULT (getdate()) FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Ticket] ADD  DEFAULT ((0)) FOR [IsFreeTicket]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([BusId])
REFERENCES [dbo].[Bus] ([BusId])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Bus]  WITH CHECK ADD FOREIGN KEY([AssignedRouteId])
REFERENCES [dbo].[Route] ([RouteId])
GO
ALTER TABLE [dbo].[Bus]  WITH CHECK ADD FOREIGN KEY([DriverId])
REFERENCES [dbo].[Driver] ([DriverId])
GO
ALTER TABLE [dbo].[BusStop]  WITH CHECK ADD FOREIGN KEY([RouteId])
REFERENCES [dbo].[Route] ([RouteId])
GO
ALTER TABLE [dbo].[Driver]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[FreeTicket]  WITH CHECK ADD FOREIGN KEY([TicketID])
REFERENCES [dbo].[Ticket] ([TicketId])
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
ALTER TABLE [dbo].[RouteTicket]  WITH CHECK ADD FOREIGN KEY([RouteId])
REFERENCES [dbo].[Route] ([RouteId])
GO
ALTER TABLE [dbo].[RouteTicket]  WITH CHECK ADD FOREIGN KEY([TicketId])
REFERENCES [dbo].[Ticket] ([TicketId])
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([BookingId])
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD FOREIGN KEY([RegistrationTypeId])
REFERENCES [dbo].[RegistrationType] ([RegistrationTypeId])
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
USE [master]
GO
ALTER DATABASE [BusManagementSystem1] SET  READ_WRITE 
GO
