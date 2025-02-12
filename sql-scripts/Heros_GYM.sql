USE [master]
GO
/****** Object:  Database [Heros_GYM]    Script Date: 1/23/2024 7:32:02 PM ******/
CREATE DATABASE [Heros_GYM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Heros_GYM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Heros_GYM.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Heros_GYM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Heros_GYM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Heros_GYM] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Heros_GYM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Heros_GYM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Heros_GYM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Heros_GYM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Heros_GYM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Heros_GYM] SET ARITHABORT OFF 
GO
ALTER DATABASE [Heros_GYM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Heros_GYM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Heros_GYM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Heros_GYM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Heros_GYM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Heros_GYM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Heros_GYM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Heros_GYM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Heros_GYM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Heros_GYM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Heros_GYM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Heros_GYM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Heros_GYM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Heros_GYM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Heros_GYM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Heros_GYM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Heros_GYM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Heros_GYM] SET RECOVERY FULL 
GO
ALTER DATABASE [Heros_GYM] SET  MULTI_USER 
GO
ALTER DATABASE [Heros_GYM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Heros_GYM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Heros_GYM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Heros_GYM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Heros_GYM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Heros_GYM] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Heros_GYM', N'ON'
GO
ALTER DATABASE [Heros_GYM] SET QUERY_STORE = ON
GO
ALTER DATABASE [Heros_GYM] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Heros_GYM]
GO
/****** Object:  Table [dbo].[Memberships]    Script Date: 1/23/2024 7:32:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Memberships](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Amount] [int] NULL,
	[Period] [int] NULL,
	[User_Id] [int] NULL,
	[IsDeleted] [char](1) NULL,
	[UserDeleted] [int] NULL,
 CONSTRAINT [PK_Memberships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IsDeleted] [char](1) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetAllDeletedMemberShipsData]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[GetAllDeletedMemberShipsData]
AS
SELECT ms.Name [MemberShip], ms.Amount[Max Amount], ms.Period[Period(Months)], u.Name[User Deleted]
FROM Memberships ms, Users u
where u.Id = ms.User_Id and ms.IsDeleted = 't'
GO
/****** Object:  Table [dbo].[Members]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](max) NULL,
	[Age] [int] NULL,
	[Gender] [char](1) NULL,
	[User_Id] [int] NULL,
	[Membership_Id] [int] NULL,
	[IsDeleted] [char](1) NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetAllMemberShipsData]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[GetAllMemberShipsData]
as
select ms.Name [Membership], COUNT(m.Id) [Count Of Members], ms.Amount [Max Amount], ms.Period[Period(in Months)], u.Name[User Added]
from Memberships ms left join Members m on ms.Id = m.Membership_Id
left join Users u on u.Id = ms.User_Id
where ms.IsDeleted = 'f'
group by ms.Name, ms.Amount, ms.Period, u.Name
GO
/****** Object:  Table [dbo].[Member_Programs]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member_Programs](
	[Member_Id] [int] NOT NULL,
	[Program_Id] [int] NOT NULL,
	[EndDate] [date] NULL,
	[StartDate] [date] NOT NULL,
 CONSTRAINT [PK_Member_Programs] PRIMARY KEY CLUSTERED 
(
	[Member_Id] ASC,
	[Program_Id] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Programs]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Programs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[Salary] [money] NULL,
	[UserId] [int] NULL,
	[UserDeleted] [int] NULL,
	[IsDeleted] [char](1) NULL,
 CONSTRAINT [PK_Programs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetAllProgramsData]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[GetAllProgramsData]
as
select p.Name [Program], COUNT(mp.Member_Id) [Count Of Members], p.Description [Description], p.Salary [Salary], u.Name[User Added]
from Programs p left join Member_Programs mp on p.Id = mp.Program_Id
left join Users u on u.Id = p.UserId
where P.IsDeleted = 'f'
group by p.Name, p.Description, p.Salary, u.Name
GO
/****** Object:  View [dbo].[GetAllDeletedProgramsData]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[GetAllDeletedProgramsData]
AS
SELECT p.Name [Program], p.[Description], p.Salary [Salary], u.Name[User Deleted]
FROM Programs p, Users u
where u.Id = p.UserId and p.IsDeleted = 't'
GO
/****** Object:  Table [dbo].[Coach_Addresses]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coach_Addresses](
	[Id] [int] NOT NULL,
	[Address] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Coach_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coach_Phones]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coach_Phones](
	[Id] [int] NOT NULL,
	[Phone] [int] NOT NULL,
 CONSTRAINT [PK_Coach_Phones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coaches]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coaches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](max) NULL,
	[Program_Id] [int] NULL,
	[User_Id] [int] NULL,
	[IsDeleted] [char](1) NULL,
 CONSTRAINT [PK_Coaches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetectMembershipsUpdated]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetectMembershipsUpdated](
	[Name] [varchar](50) NULL,
	[Amount] [int] NULL,
	[Period] [int] NULL,
	[UserUpdated] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member_Phones]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member_Phones](
	[Id] [int] NOT NULL,
	[Phone] [int] NOT NULL,
 CONSTRAINT [PK_Member_Phones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 1/23/2024 7:32:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 1) NULL,
	[Date] [date] NULL,
	[Time] [time](0) NULL,
	[User_Id] [int] NULL,
	[Member_Id] [int] NULL,
	[IsDeleted] [char](1) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [Password], [IsDeleted]) VALUES (1, N'mostafa', N'591998', N'f')
INSERT [dbo].[Users] ([Id], [Name], [Password], [IsDeleted]) VALUES (2, N'sara', N'1592001', N'f')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Coaches] ADD  CONSTRAINT [DF_Coaches_IsDeleted]  DEFAULT ('f') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF_Members_IsDeleted]  DEFAULT ('f') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Memberships] ADD  CONSTRAINT [DF_Memberships_IsDeleted]  DEFAULT ('f') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Payments] ADD  CONSTRAINT [DF_Payments_IsDeleted]  DEFAULT ('f') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Programs] ADD  CONSTRAINT [DF_Programs_IsDeleted]  DEFAULT ('f') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Coach_Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Coach_Addresses_Coaches] FOREIGN KEY([Id])
REFERENCES [dbo].[Coaches] ([Id])
GO
ALTER TABLE [dbo].[Coach_Addresses] CHECK CONSTRAINT [FK_Coach_Addresses_Coaches]
GO
ALTER TABLE [dbo].[Coach_Phones]  WITH CHECK ADD  CONSTRAINT [FK_Coach_Phones_Coaches] FOREIGN KEY([Id])
REFERENCES [dbo].[Coaches] ([Id])
GO
ALTER TABLE [dbo].[Coach_Phones] CHECK CONSTRAINT [FK_Coach_Phones_Coaches]
GO
ALTER TABLE [dbo].[Coaches]  WITH CHECK ADD  CONSTRAINT [FK_Coaches_Programs] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Programs] ([Id])
GO
ALTER TABLE [dbo].[Coaches] CHECK CONSTRAINT [FK_Coaches_Programs]
GO
ALTER TABLE [dbo].[Coaches]  WITH CHECK ADD  CONSTRAINT [FK_Coaches_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Coaches] CHECK CONSTRAINT [FK_Coaches_Users]
GO
ALTER TABLE [dbo].[Member_Phones]  WITH CHECK ADD  CONSTRAINT [FK_Member_Phones_Members] FOREIGN KEY([Id])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Member_Phones] CHECK CONSTRAINT [FK_Member_Phones_Members]
GO
ALTER TABLE [dbo].[Member_Programs]  WITH CHECK ADD  CONSTRAINT [FK_Member_Programs_Members] FOREIGN KEY([Member_Id])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Member_Programs] CHECK CONSTRAINT [FK_Member_Programs_Members]
GO
ALTER TABLE [dbo].[Member_Programs]  WITH CHECK ADD  CONSTRAINT [FK_Member_Programs_Programs] FOREIGN KEY([Program_Id])
REFERENCES [dbo].[Programs] ([Id])
GO
ALTER TABLE [dbo].[Member_Programs] CHECK CONSTRAINT [FK_Member_Programs_Programs]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Memberships] FOREIGN KEY([Membership_Id])
REFERENCES [dbo].[Memberships] ([Id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Memberships]
GO
ALTER TABLE [dbo].[Members]  WITH CHECK ADD  CONSTRAINT [FK_Members_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Members] CHECK CONSTRAINT [FK_Members_Users]
GO
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [FK_Memberships_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [FK_Memberships_Users]
GO
ALTER TABLE [dbo].[Memberships]  WITH CHECK ADD  CONSTRAINT [FK_Memberships_Users1] FOREIGN KEY([UserDeleted])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Memberships] CHECK CONSTRAINT [FK_Memberships_Users1]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Members] FOREIGN KEY([Member_Id])
REFERENCES [dbo].[Members] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Members]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Users] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Users]
GO
ALTER TABLE [dbo].[Programs]  WITH CHECK ADD  CONSTRAINT [FK_Programs_Users] FOREIGN KEY([UserDeleted])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Programs] CHECK CONSTRAINT [FK_Programs_Users]
GO
ALTER TABLE [dbo].[Programs]  WITH CHECK ADD  CONSTRAINT [FK_Programs_Users1] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Programs] CHECK CONSTRAINT [FK_Programs_Users1]
GO
USE [master]
GO
ALTER DATABASE [Heros_GYM] SET  READ_WRITE 
GO
