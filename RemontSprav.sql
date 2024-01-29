USE [master]
GO
/****** Object:  Database [RemontSprav]    Script Date: 30.01.2024 1:13:46 ******/
CREATE DATABASE [RemontSprav]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RemontSprav', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RemontSprav.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RemontSprav_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RemontSprav_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RemontSprav] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RemontSprav].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RemontSprav] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RemontSprav] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RemontSprav] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RemontSprav] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RemontSprav] SET ARITHABORT OFF 
GO
ALTER DATABASE [RemontSprav] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [RemontSprav] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RemontSprav] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RemontSprav] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RemontSprav] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RemontSprav] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RemontSprav] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RemontSprav] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RemontSprav] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RemontSprav] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RemontSprav] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RemontSprav] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RemontSprav] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RemontSprav] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RemontSprav] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RemontSprav] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RemontSprav] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RemontSprav] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RemontSprav] SET  MULTI_USER 
GO
ALTER DATABASE [RemontSprav] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RemontSprav] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RemontSprav] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RemontSprav] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RemontSprav] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RemontSprav] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RemontSprav] SET QUERY_STORE = OFF
GO
USE [RemontSprav]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 30.01.2024 1:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreviewPages]    Script Date: 30.01.2024 1:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreviewPages](
	[PreviewPageID] [int] IDENTITY(1,1) NOT NULL,
	[ShortText] [nvarchar](max) NOT NULL,
	[RepairMessageID] [int] NOT NULL,
 CONSTRAINT [PK_RepairMessages] PRIMARY KEY CLUSTERED 
(
	[PreviewPageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RepairMessages]    Script Date: 30.01.2024 1:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RepairMessages](
	[RepairMessageID] [int] IDENTITY(1,1) NOT NULL,
	[MessageText] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RepairMessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 30.01.2024 1:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Role]) VALUES (1, N'andrei', N'12345', 1)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Role]) VALUES (2, N'serger', N'12345', 2)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Role]) VALUES (3, N'test', N'test', 1)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[PreviewPages] ON 

INSERT [dbo].[PreviewPages] ([PreviewPageID], [ShortText], [RepairMessageID]) VALUES (2, N'Зависание компьютера', 1)
INSERT [dbo].[PreviewPages] ([PreviewPageID], [ShortText], [RepairMessageID]) VALUES (3, N'удаление программы', 2)
INSERT [dbo].[PreviewPages] ([PreviewPageID], [ShortText], [RepairMessageID]) VALUES (6, N'пропала сеть', 9)
INSERT [dbo].[PreviewPages] ([PreviewPageID], [ShortText], [RepairMessageID]) VALUES (7, N'ошибка 0х00001', 10)
SET IDENTITY_INSERT [dbo].[PreviewPages] OFF
GO
SET IDENTITY_INSERT [dbo].[RepairMessages] ON 

INSERT [dbo].[RepairMessages] ([RepairMessageID], [MessageText]) VALUES (1, N'для того чтобы починить нужно перезапустить компьютер')
INSERT [dbo].[RepairMessages] ([RepairMessageID], [MessageText]) VALUES (2, N'Найдите в поиске раздел установка и удаление программ')
INSERT [dbo].[RepairMessages] ([RepairMessageID], [MessageText]) VALUES (9, N'Запустите средство диагности сетей из меню поиск')
INSERT [dbo].[RepairMessages] ([RepairMessageID], [MessageText]) VALUES (10, N'Отключите всё из USB-портов кроме клавиатуры и мыши')
SET IDENTITY_INSERT [dbo].[RepairMessages] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'user')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles]
GO
ALTER TABLE [dbo].[PreviewPages]  WITH CHECK ADD  CONSTRAINT [FK_PreviewPages_RepairMessages] FOREIGN KEY([RepairMessageID])
REFERENCES [dbo].[RepairMessages] ([RepairMessageID])
GO
ALTER TABLE [dbo].[PreviewPages] CHECK CONSTRAINT [FK_PreviewPages_RepairMessages]
GO
USE [master]
GO
ALTER DATABASE [RemontSprav] SET  READ_WRITE 
GO
