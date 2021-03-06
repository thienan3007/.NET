USE [master]
GO
/****** Object:  Database [BookStore]    Script Date: 11/2/2021 10:29:51 PM ******/
CREATE DATABASE [BookStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BookStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\BookStore.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BookStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\BookStore_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BookStore] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BookStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BookStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BookStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BookStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BookStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BookStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BookStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BookStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BookStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BookStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BookStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BookStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BookStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BookStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BookStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BookStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BookStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BookStore] SET  MULTI_USER 
GO
ALTER DATABASE [BookStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BookStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BookStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BookStore] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BookStore] SET DELAYED_DURABILITY = DISABLED 
GO
USE [BookStore]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Author](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[phone] [varchar](20) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Book]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[language] [nvarchar](50) NOT NULL,
	[noOfPages] [int] NOT NULL,
	[categoryID] [int] NOT NULL,
	[authorID] [int] NOT NULL,
	[description] [nvarchar](1000) NULL,
	[quantityLeft] [int] NOT NULL,
	[statusID] [int] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookBorrow]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookBorrow](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[borrowDate] [datetime] NULL,
	[dueDate] [datetime] NULL,
	[totalPrice] [float] NULL,
	[studentId] [int] NULL,
	[librarianID] [int] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_BookBorrow] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookBorrowDetail]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookBorrowDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BookBorrowID] [int] NULL,
	[BookID] [int] NULL,
	[price] [float] NULL,
	[quantity] [int] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_BookBorrowDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookOrder]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderDate] [datetime] NULL,
	[totalPrice] [float] NULL,
	[studentID] [int] NULL,
	[librarianID] [int] NULL,
 CONSTRAINT [PK_BookOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookOrderDetail]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookOrderDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BookOrderID] [int] NULL,
	[BookID] [int] NULL,
	[price] [float] NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_BookOrderDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookReturn]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookReturn](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[returnDate] [datetime] NULL,
	[dueDate] [datetime] NULL,
	[studentID] [int] NULL,
	[librarianID] [int] NULL,
 CONSTRAINT [PK_BookReturn] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookReturnDetail]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookReturnDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BookReturnID] [int] NULL,
	[BookID] [int] NULL,
	[quantity] [int] NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_BookReturnDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookStatus]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookStatus](
	[bookStatusID] [int] IDENTITY(1,1) NOT NULL,
	[bookStatusName] [nvarchar](100) NULL,
	[description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_BookStatus] PRIMARY KEY CLUSTERED 
(
	[bookStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[categoryID] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[categoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Librarian]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Librarian](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[password] [varchar](50) NULL,
	[name] [nvarchar](100) NULL,
	[address] [nvarchar](200) NULL,
	[email] [varchar](100) NULL,
	[phone] [varchar](20) NULL,
	[statusID] [int] NULL,
 CONSTRAINT [PK_Librarian] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LibrarianStatus]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibrarianStatus](
	[librarianStatusID] [int] IDENTITY(1,1) NOT NULL,
	[librarianStatusName] [nvarchar](100) NULL,
	[description] [nvarchar](200) NULL,
 CONSTRAINT [PK_LibrarianStatus] PRIMARY KEY CLUSTERED 
(
	[librarianStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[phone] [varchar](20) NULL,
	[address] [nvarchar](200) NULL,
	[statusID] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentStatus]    Script Date: 11/2/2021 10:29:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentStatus](
	[studentStatusID] [int] NOT NULL,
	[studentStatusName] [nvarchar](100) NULL,
	[description] [nvarchar](200) NULL,
 CONSTRAINT [PK_StudentStatus] PRIMARY KEY CLUSTERED 
(
	[studentStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (1, N'Mahatma Gandhi', N'mahatma@gmail.com', N'123456789')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (2, N'Mark Twain', N'mark@gmail.com', N'987654321')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (3, N'J.K Rowling', N'jk@gmail.com', N'123456666')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (4, N'thien an', N'antruong3007@gmail.com', N'123123123')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (5, N'Thien Phuc', N'thienphuc@gmail.com', N'1231312313')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (6, N'Nhu Quynh', N'nhuquynh@gmail.com', N'2313123123')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (9, N'Nhat Quang', N'nhatquang@gmail.com', N'1231233')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (12, N'Quang Thanh', N'quangthanh123@gmail.com', N'123123123213')
INSERT [dbo].[Author] ([id], [name], [email], [phone]) VALUES (13, N'John', N'anturong@gmail.com', N'321283718923')
SET IDENTITY_INSERT [dbo].[Author] OFF
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([id], [title], [language], [noOfPages], [categoryID], [authorID], [description], [quantityLeft], [statusID]) VALUES (1, N'Shinichi', N'Japanese', 200, 2, 3, N'really good', 15, 2)
INSERT [dbo].[Book] ([id], [title], [language], [noOfPages], [categoryID], [authorID], [description], [quantityLeft], [statusID]) VALUES (4, N'Doraemon', N'Japanese', 200, 3, 2, N'really good', 8, 1)
INSERT [dbo].[Book] ([id], [title], [language], [noOfPages], [categoryID], [authorID], [description], [quantityLeft], [statusID]) VALUES (5, N'Avengers', N'English', 1000, 1, 1, N'Really good', 100, 1)
INSERT [dbo].[Book] ([id], [title], [language], [noOfPages], [categoryID], [authorID], [description], [quantityLeft], [statusID]) VALUES (6, N'Bat Man Life', N'English', 1000, 3, 9, N'Really Good', 100, 3)
INSERT [dbo].[Book] ([id], [title], [language], [noOfPages], [categoryID], [authorID], [description], [quantityLeft], [statusID]) VALUES (7, N'Iron Man process', N'English', 1000, 1, 1, N'', 12, 1)
SET IDENTITY_INSERT [dbo].[Book] OFF
SET IDENTITY_INSERT [dbo].[BookBorrow] ON 

INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (10, CAST(N'2021-10-22 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 400, 2, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (11, CAST(N'2021-10-22 00:00:00.000' AS DateTime), CAST(N'2021-10-31 00:00:00.000' AS DateTime), 600, 10, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (12, CAST(N'2021-10-27 00:00:00.000' AS DateTime), CAST(N'2021-10-28 00:00:00.000' AS DateTime), 300, 15, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (13, CAST(N'2021-10-25 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 700, 15, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (14, CAST(N'2021-10-25 00:00:00.000' AS DateTime), CAST(N'2021-10-27 00:00:00.000' AS DateTime), 200, 15, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (15, CAST(N'2021-10-28 00:00:00.000' AS DateTime), CAST(N'2021-10-30 00:00:00.000' AS DateTime), 100, 2, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (16, CAST(N'2021-10-27 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 200, 2, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (17, CAST(N'2021-10-27 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 200, 2, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (18, CAST(N'2021-10-27 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 200, 2, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (1017, CAST(N'2021-11-30 00:00:00.000' AS DateTime), CAST(N'2021-12-02 00:00:00.000' AS DateTime), 200, 2, 1, 1)
INSERT [dbo].[BookBorrow] ([id], [borrowDate], [dueDate], [totalPrice], [studentId], [librarianID], [status]) VALUES (1018, CAST(N'2021-11-02 00:00:00.000' AS DateTime), CAST(N'2021-11-03 00:00:00.000' AS DateTime), 200, 2, 1, 1)
SET IDENTITY_INSERT [dbo].[BookBorrow] OFF
SET IDENTITY_INSERT [dbo].[BookBorrowDetail] ON 

INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (6, 10, 4, 200, 2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (7, 10, 1, 200, 2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (8, 11, 4, 200, 2, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (9, 11, 1, 400, 4, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (10, 12, 1, 300, 3, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (11, 10, 4, 200, -2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (12, 10, 1, 200, 2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (13, 10, 4, 200, -2, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (14, 10, 1, 200, 0, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (15, 13, 4, 100, 2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (16, 13, 1, 600, 6, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (17, 13, 4, 100, 1, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (18, 13, 1, 600, 0, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (19, 14, 4, 100, 2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (20, 14, 1, 100, 2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (21, 14, 4, 100, 1, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (22, 14, 1, 100, 0, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (23, 15, 4, 100, 10, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (24, 16, 4, 200, 2, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (25, 17, 4, 200, 2, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (26, 18, 4, 200, 2, 0)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (27, 18, 4, 200, 0, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (28, 1017, 4, 200, 2, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (29, 1018, 4, 200, 2, 1)
INSERT [dbo].[BookBorrowDetail] ([id], [BookBorrowID], [BookID], [price], [quantity], [status]) VALUES (30, 12, 1, 300, 0, 1)
SET IDENTITY_INSERT [dbo].[BookBorrowDetail] OFF
SET IDENTITY_INSERT [dbo].[BookReturn] ON 

INSERT [dbo].[BookReturn] ([id], [returnDate], [dueDate], [studentID], [librarianID]) VALUES (1, CAST(N'2021-10-29 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 2, 1)
INSERT [dbo].[BookReturn] ([id], [returnDate], [dueDate], [studentID], [librarianID]) VALUES (2, CAST(N'2021-10-29 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 2, 1)
INSERT [dbo].[BookReturn] ([id], [returnDate], [dueDate], [studentID], [librarianID]) VALUES (3, CAST(N'2021-10-30 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 15, 1)
INSERT [dbo].[BookReturn] ([id], [returnDate], [dueDate], [studentID], [librarianID]) VALUES (4, CAST(N'2021-10-29 00:00:00.000' AS DateTime), CAST(N'2021-10-27 00:00:00.000' AS DateTime), 15, 1)
INSERT [dbo].[BookReturn] ([id], [returnDate], [dueDate], [studentID], [librarianID]) VALUES (5, CAST(N'2021-10-30 00:00:00.000' AS DateTime), CAST(N'2021-10-29 00:00:00.000' AS DateTime), 2, 1)
INSERT [dbo].[BookReturn] ([id], [returnDate], [dueDate], [studentID], [librarianID]) VALUES (1005, CAST(N'2021-11-02 00:00:00.000' AS DateTime), CAST(N'2021-10-28 00:00:00.000' AS DateTime), 2, 1)
SET IDENTITY_INSERT [dbo].[BookReturn] OFF
SET IDENTITY_INSERT [dbo].[BookReturnDetail] ON 

INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (1, 1, 4, 2, 200)
INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (2, 2, 1, 2, 200)
INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (3, 3, 4, 1, 100)
INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (4, 3, 1, 3, 600)
INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (5, 4, 4, 1, 100)
INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (6, 4, 1, 2, 100)
INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (7, 5, 4, 2, 200)
INSERT [dbo].[BookReturnDetail] ([id], [BookReturnID], [BookID], [quantity], [price]) VALUES (8, 1005, 1, 3, 300)
SET IDENTITY_INSERT [dbo].[BookReturnDetail] OFF
SET IDENTITY_INSERT [dbo].[BookStatus] ON 

INSERT [dbo].[BookStatus] ([bookStatusID], [bookStatusName], [description]) VALUES (1, N'Mới', N'còn mới')
INSERT [dbo].[BookStatus] ([bookStatusID], [bookStatusName], [description]) VALUES (2, N'Cũ', N'cũ rồi')
INSERT [dbo].[BookStatus] ([bookStatusID], [bookStatusName], [description]) VALUES (3, N'Mới nhập về', N'hàng vừa nhập')
INSERT [dbo].[BookStatus] ([bookStatusID], [bookStatusName], [description]) VALUES (4, N'Đang cho mượn', N'đang cho mượn')
SET IDENTITY_INSERT [dbo].[BookStatus] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (1, N'Tiểu thuyết')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (2, N'Trinh thám')
INSERT [dbo].[Category] ([categoryID], [categoryName]) VALUES (3, N'Truyện tranh')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Librarian] ON 

INSERT [dbo].[Librarian] ([id], [password], [name], [address], [email], [phone], [statusID]) VALUES (1, N'123', N'thienan', N'district 1', N'thienan@gmail.com', N'12313123', 1)
SET IDENTITY_INSERT [dbo].[Librarian] OFF
SET IDENTITY_INSERT [dbo].[LibrarianStatus] ON 

INSERT [dbo].[LibrarianStatus] ([librarianStatusID], [librarianStatusName], [description]) VALUES (1, N'new', N'new')
INSERT [dbo].[LibrarianStatus] ([librarianStatusID], [librarianStatusName], [description]) VALUES (2, N'old', N'old')
SET IDENTITY_INSERT [dbo].[LibrarianStatus] OFF
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (2, N'Truong Duc Thien An', N'antruong@gmail.com', N'1234567890', N'Ho Chi Minh District', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (3, N'Truong Thien Phuc ', N'thienphuc@gmail.com', N'1234567890', N'Da Nang district', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (4, N'Nguyen Van A', N'a@gmail.com', N'1234567980', N'District 2', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (5, N'Nguyen Van B', N'b@gmail.com', N'12312313', N'District 4', 2)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (8, N'Truong Duc Thien An', N'antruong@gmail.com', N'123123123', N'asdadasd', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (9, N'Nguyen van c ', N'c@gmail.com', N'123123', N'asdasd', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (10, N'Nguyen Van D', N'D@gmail.com', N'12313123', N'sadasdasd', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (11, N'Nguyen Van E', N'e@gmail.com', N'312313132', N'district 2', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (12, N'Nguyen Van F', N'f@gmail.com', N'123123', N'adad', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (13, N'Nguyen Van F', N'f@gmail.com', N'123123', N'adad', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (14, N'Nguyen Van Z', N'z@gmail.com', N'123123123', N'district 1', 1)
INSERT [dbo].[Student] ([id], [name], [email], [phone], [address], [statusID]) VALUES (15, N'Nguyen Van Z', N'z@gmail.com', N'123123123', N'district 1', 1)
SET IDENTITY_INSERT [dbo].[Student] OFF
INSERT [dbo].[StudentStatus] ([studentStatusID], [studentStatusName], [description]) VALUES (1, N'New', N'new')
INSERT [dbo].[StudentStatus] ([studentStatusID], [studentStatusName], [description]) VALUES (2, N'Old', N'old')
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author] FOREIGN KEY([authorID])
REFERENCES [dbo].[Author] ([id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_BookStatus] FOREIGN KEY([statusID])
REFERENCES [dbo].[BookStatus] ([bookStatusID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_BookStatus]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Category] FOREIGN KEY([categoryID])
REFERENCES [dbo].[Category] ([categoryID])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Category]
GO
ALTER TABLE [dbo].[BookBorrow]  WITH CHECK ADD  CONSTRAINT [FK_BookBorrow_Librarian] FOREIGN KEY([librarianID])
REFERENCES [dbo].[Librarian] ([id])
GO
ALTER TABLE [dbo].[BookBorrow] CHECK CONSTRAINT [FK_BookBorrow_Librarian]
GO
ALTER TABLE [dbo].[BookBorrow]  WITH CHECK ADD  CONSTRAINT [FK_BookBorrow_Student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[BookBorrow] CHECK CONSTRAINT [FK_BookBorrow_Student]
GO
ALTER TABLE [dbo].[BookBorrowDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookBorrowDetail_Book] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[BookBorrowDetail] CHECK CONSTRAINT [FK_BookBorrowDetail_Book]
GO
ALTER TABLE [dbo].[BookBorrowDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookBorrowDetail_BookBorrow] FOREIGN KEY([BookBorrowID])
REFERENCES [dbo].[BookBorrow] ([id])
GO
ALTER TABLE [dbo].[BookBorrowDetail] CHECK CONSTRAINT [FK_BookBorrowDetail_BookBorrow]
GO
ALTER TABLE [dbo].[BookOrder]  WITH CHECK ADD  CONSTRAINT [FK_BookOrder_Librarian] FOREIGN KEY([librarianID])
REFERENCES [dbo].[Librarian] ([id])
GO
ALTER TABLE [dbo].[BookOrder] CHECK CONSTRAINT [FK_BookOrder_Librarian]
GO
ALTER TABLE [dbo].[BookOrder]  WITH CHECK ADD  CONSTRAINT [FK_BookOrder_Student] FOREIGN KEY([studentID])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[BookOrder] CHECK CONSTRAINT [FK_BookOrder_Student]
GO
ALTER TABLE [dbo].[BookOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookOrderDetail_Book] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[BookOrderDetail] CHECK CONSTRAINT [FK_BookOrderDetail_Book]
GO
ALTER TABLE [dbo].[BookOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookOrderDetail_BookOrder] FOREIGN KEY([BookOrderID])
REFERENCES [dbo].[BookOrder] ([id])
GO
ALTER TABLE [dbo].[BookOrderDetail] CHECK CONSTRAINT [FK_BookOrderDetail_BookOrder]
GO
ALTER TABLE [dbo].[BookReturn]  WITH CHECK ADD  CONSTRAINT [FK_BookReturn_Librarian] FOREIGN KEY([librarianID])
REFERENCES [dbo].[Librarian] ([id])
GO
ALTER TABLE [dbo].[BookReturn] CHECK CONSTRAINT [FK_BookReturn_Librarian]
GO
ALTER TABLE [dbo].[BookReturn]  WITH CHECK ADD  CONSTRAINT [FK_BookReturn_Student] FOREIGN KEY([studentID])
REFERENCES [dbo].[Student] ([id])
GO
ALTER TABLE [dbo].[BookReturn] CHECK CONSTRAINT [FK_BookReturn_Student]
GO
ALTER TABLE [dbo].[BookReturnDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookReturnDetail_Book] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[BookReturnDetail] CHECK CONSTRAINT [FK_BookReturnDetail_Book]
GO
ALTER TABLE [dbo].[BookReturnDetail]  WITH CHECK ADD  CONSTRAINT [FK_BookReturnDetail_BookReturn] FOREIGN KEY([BookReturnID])
REFERENCES [dbo].[BookReturn] ([id])
GO
ALTER TABLE [dbo].[BookReturnDetail] CHECK CONSTRAINT [FK_BookReturnDetail_BookReturn]
GO
ALTER TABLE [dbo].[Librarian]  WITH CHECK ADD  CONSTRAINT [FK_Librarian_LibrarianStatus] FOREIGN KEY([statusID])
REFERENCES [dbo].[LibrarianStatus] ([librarianStatusID])
GO
ALTER TABLE [dbo].[Librarian] CHECK CONSTRAINT [FK_Librarian_LibrarianStatus]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_StudentStatus] FOREIGN KEY([statusID])
REFERENCES [dbo].[StudentStatus] ([studentStatusID])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_StudentStatus]
GO
USE [master]
GO
ALTER DATABASE [BookStore] SET  READ_WRITE 
GO
