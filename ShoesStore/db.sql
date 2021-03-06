USE [ShoesStore]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/3/2021 9:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 12/3/2021 9:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ShoeId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/3/2021 9:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[OrderAmount] [float] NOT NULL,
	[OrderAddress] [nvarchar](max) NOT NULL,
	[OrderPhone] [nvarchar](max) NOT NULL,
	[OrderEmail] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 12/3/2021 9:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publishers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Publishers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/3/2021 9:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shoes]    Script Date: 12/3/2021 9:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shoes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ShortDes] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[ShoeLive] [bit] NOT NULL,
	[PublisherId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Shoes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/3/2021 9:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Email] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Jogging Shoe')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailId], [OrderId], [ShoeId], [Quantity]) VALUES (1, 1, 5, 2)
INSERT [dbo].[OrderDetails] ([OrderDetailId], [OrderId], [ShoeId], [Quantity]) VALUES (2, 1, 4, 2)
INSERT [dbo].[OrderDetails] ([OrderDetailId], [OrderId], [ShoeId], [Quantity]) VALUES (3, 1, 3, 3)
INSERT [dbo].[OrderDetails] ([OrderDetailId], [OrderId], [ShoeId], [Quantity]) VALUES (4, 2, 5, 2)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderAmount], [OrderAddress], [OrderPhone], [OrderEmail]) VALUES (1, CAST(N'2021-12-03 21:00:07.7464246' AS DateTime2), 22000, N'12/11/9', N'0932060143', N'antruong3007@gmail.com')
INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderAmount], [OrderAddress], [OrderPhone], [OrderEmail]) VALUES (2, CAST(N'2021-12-03 21:01:45.6834050' AS DateTime2), 8800, N'12/11/9', N'0932060143', N'antruong3007@gmail.com')
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Publishers] ON 

INSERT [dbo].[Publishers] ([Id], [Name]) VALUES (1, N'Nike')
SET IDENTITY_INSERT [dbo].[Publishers] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Shoes] ON 

INSERT [dbo].[Shoes] ([Id], [Name], [Price], [Quantity], [ShortDes], [Image], [ShoeLive], [PublisherId], [CategoryId]) VALUES (3, N'Nike', 2000, 2000, N'This shoe is so deep', N'shoes-img9.png', 1, 1, 1)
INSERT [dbo].[Shoes] ([Id], [Name], [Price], [Quantity], [ShortDes], [Image], [ShoeLive], [PublisherId], [CategoryId]) VALUES (4, N'Addidas', 3000, 100, N'This shoe is so deep', N'shoes-img1.png', 1, 1, 1)
INSERT [dbo].[Shoes] ([Id], [Name], [Price], [Quantity], [ShortDes], [Image], [ShoeLive], [PublisherId], [CategoryId]) VALUES (5, N'Nike-Air', 4000, 12, N'This shoe is so deep', N'shoes-img5.png', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Shoes] OFF
INSERT [dbo].[Users] ([Email], [Name], [Address], [Password], [RegistrationDate], [RoleId], [Phone], [Status]) VALUES (N'antruong3007@gmail.com', N'Tom''s Cruise', N'12/11/9', N'$2b$10$qIHi8wyo11x/bQ0Sj5hS.OMwIv8tVb502x6VM4PzztzasaPo3ev1W', CAST(N'2021-12-03 21:07:23.5488228' AS DateTime2), 2, N'0932060143', 1)
INSERT [dbo].[Users] ([Email], [Name], [Address], [Password], [RegistrationDate], [RoleId], [Phone], [Status]) VALUES (N'antruong30072000@gmail.com', N'AT', N'12/11/9', N'$2b$10$OP15QswJNXhYJraV9C9WvuTO0hk15CsmDN8h.3oC8HlpZKPXcoCAa', CAST(N'2021-12-03 21:24:59.7891573' AS DateTime2), 1, N'0123456789', 1)
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
ALTER TABLE [dbo].[Shoes]  WITH CHECK ADD  CONSTRAINT [FK_Shoes_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shoes] CHECK CONSTRAINT [FK_Shoes_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Shoes]  WITH CHECK ADD  CONSTRAINT [FK_Shoes_Publishers_PublisherId] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Publishers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Shoes] CHECK CONSTRAINT [FK_Shoes_Publishers_PublisherId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_RoleId]
GO
