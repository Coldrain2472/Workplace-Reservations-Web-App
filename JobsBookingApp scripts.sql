USE [master]
GO
/****** Object:  Database [JobsBookingAppDb]    Script Date: 6/4/2025 5:51:18 PM ******/
CREATE DATABASE [JobsBookingAppDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JobsBookingAppDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\JobsBookingAppDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JobsBookingAppDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\JobsBookingAppDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [JobsBookingAppDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JobsBookingAppDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JobsBookingAppDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JobsBookingAppDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JobsBookingAppDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [JobsBookingAppDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JobsBookingAppDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET RECOVERY FULL 
GO
ALTER DATABASE [JobsBookingAppDb] SET  MULTI_USER 
GO
ALTER DATABASE [JobsBookingAppDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JobsBookingAppDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JobsBookingAppDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JobsBookingAppDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JobsBookingAppDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JobsBookingAppDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'JobsBookingAppDb', N'ON'
GO
ALTER DATABASE [JobsBookingAppDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [JobsBookingAppDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [JobsBookingAppDb]
GO
/****** Object:  Table [dbo].[EmployeeFavorites]    Script Date: 6/4/2025 5:51:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeFavorites](
	[EmployeeId] [int] NOT NULL,
	[WorkplaceId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[WorkplaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 6/4/2025 5:51:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](120) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Username] [varchar](40) NOT NULL,
	[Password] [varchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 6/4/2025 5:51:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[ReservationId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StartTime] [date] NOT NULL,
	[EndTime] [date] NOT NULL,
	[WorkplaceId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workplaces]    Script Date: 6/4/2025 5:51:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workplaces](
	[WorkplaceId] [int] IDENTITY(1,1) NOT NULL,
	[Floor] [int] NOT NULL,
	[Zone] [varchar](50) NOT NULL,
	[HasMonitor] [bit] NOT NULL,
	[HasDock] [bit] NOT NULL,
	[IsNearWindow] [bit] NOT NULL,
	[IsNearPrinter] [bit] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkplaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (2, 1)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (2, 14)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (2, 15)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (5, 3)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (5, 11)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (5, 13)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (6, 1)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (6, 2)
INSERT [dbo].[EmployeeFavorites] ([EmployeeId], [WorkplaceId]) VALUES (6, 13)
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (1, N'Alice Dimitrova', N'alice.dimitrova@example.com', N'alice.dimitrova', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (2, N'Boris Ivanov', N'boris.ivanov@example.com', N'boris.ivanov', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (3, N'Clara Stoyanova', N'clara.stoyanova@example.com', N'clara.stoyanova', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (4, N'Dimitar Georgiev', N'dimitar.georgiev@example.com', N'dimitar.georgiev', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (5, N'Elena Petrova', N'elena.petrova@example.com', N'elena.petrova', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (6, N'John Smith', N'john.smith@example.com', N'john.smith', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (7, N'Emily Johnson', N'emily.johnson@example.com', N'emily.johnson', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (8, N'Michael Brown', N'michael.brown@example.com', N'michael.brown', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (9, N'Sarah Davis', N'sarah.davis@example.com', N'sarah.davis', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (10, N'David Wilson', N'david.wilson@example.com', N'david.wilson', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (11, N'Laura Moore', N'laura.moore@example.com', N'laura.moore', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (12, N'James Taylor', N'james.taylor@example.com', N'james.taylor', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (13, N'Olivia Anderson', N'olivia.anderson@example.com', N'olivia.anderson', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (14, N'Daniel Thomas', N'daniel.thomas@example.com', N'daniel.thomas', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (15, N'Grace Jackson', N'grace.jackson@example.com', N'grace.jackson', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (16, N'Matthew White', N'matthew.white@example.com', N'matthew.white', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (17, N'Sophie Harris', N'sophie.harris@example.com', N'sophie.harris', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (18, N'Andrew Martin', N'andrew.martin@example.com', N'andrew.martin', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (19, N'Chloe Thompson', N'chloe.thompson@example.com', N'chloe.thompson', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
INSERT [dbo].[Employees] ([EmployeeId], [Name], [Email], [Username], [Password]) VALUES (20, N'Benjamin Lee', N'benjamin.lee@example.com', N'benjamin.lee', N'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Reservations] ON 

INSERT [dbo].[Reservations] ([ReservationId], [EmployeeId], [StartTime], [EndTime], [WorkplaceId], [CreatedAt]) VALUES (1, 6, CAST(N'2025-06-06' AS Date), CAST(N'2025-06-07' AS Date), 1, CAST(N'2025-06-04T16:18:37.677' AS DateTime))
INSERT [dbo].[Reservations] ([ReservationId], [EmployeeId], [StartTime], [EndTime], [WorkplaceId], [CreatedAt]) VALUES (2, 2, CAST(N'2025-06-11' AS Date), CAST(N'2025-06-12' AS Date), 9, CAST(N'2025-06-04T16:22:33.427' AS DateTime))
INSERT [dbo].[Reservations] ([ReservationId], [EmployeeId], [StartTime], [EndTime], [WorkplaceId], [CreatedAt]) VALUES (3, 5, CAST(N'2025-06-09' AS Date), CAST(N'2025-06-10' AS Date), 8, CAST(N'2025-06-04T16:59:02.460' AS DateTime))
INSERT [dbo].[Reservations] ([ReservationId], [EmployeeId], [StartTime], [EndTime], [WorkplaceId], [CreatedAt]) VALUES (4, 5, CAST(N'2025-06-16' AS Date), CAST(N'2025-06-17' AS Date), 8, CAST(N'2025-06-04T17:00:34.017' AS DateTime))
INSERT [dbo].[Reservations] ([ReservationId], [EmployeeId], [StartTime], [EndTime], [WorkplaceId], [CreatedAt]) VALUES (5, 5, CAST(N'2025-06-05' AS Date), CAST(N'2025-06-06' AS Date), 13, CAST(N'2025-06-04T17:08:52.180' AS DateTime))
INSERT [dbo].[Reservations] ([ReservationId], [EmployeeId], [StartTime], [EndTime], [WorkplaceId], [CreatedAt]) VALUES (6, 2, CAST(N'2025-06-05' AS Date), CAST(N'2025-06-06' AS Date), 14, CAST(N'2025-06-04T17:13:39.370' AS DateTime))
SET IDENTITY_INSERT [dbo].[Reservations] OFF
GO
SET IDENTITY_INSERT [dbo].[Workplaces] ON 

INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (1, 1, N'A', 1, 1, 1, 0, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (2, 1, N'A', 1, 0, 0, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (3, 1, N'B', 0, 1, 1, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (4, 2, N'B', 1, 1, 0, 0, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (5, 2, N'C', 0, 0, 1, 0, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (6, 2, N'C', 1, 0, 1, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (7, 3, N'D', 1, 1, 1, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (8, 3, N'D', 0, 1, 0, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (9, 3, N'E', 1, 0, 0, 0, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (10, 4, N'E', 1, 1, 1, 0, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (11, 4, N'F', 0, 0, 0, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (12, 4, N'F', 1, 1, 0, 0, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (13, 5, N'G', 1, 0, 1, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (14, 5, N'G', 0, 1, 0, 1, 1)
INSERT [dbo].[Workplaces] ([WorkplaceId], [Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES (15, 5, N'H', 1, 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Workplaces] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Employee__536C85E4EC8A01CD]    Script Date: 6/4/2025 5:51:18 PM ******/
ALTER TABLE [dbo].[Employees] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Employee__A9D105347D76305F]    Script Date: 6/4/2025 5:51:18 PM ******/
ALTER TABLE [dbo].[Employees] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmployeeFavorites]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeFavorites]  WITH CHECK ADD FOREIGN KEY([WorkplaceId])
REFERENCES [dbo].[Workplaces] ([WorkplaceId])
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD FOREIGN KEY([WorkplaceId])
REFERENCES [dbo].[Workplaces] ([WorkplaceId])
GO
USE [master]
GO
ALTER DATABASE [JobsBookingAppDb] SET  READ_WRITE 
GO
