USE [Industry_Incident]
GO
/****** Object:  User [bouabdallah]    Script Date: 14/03/2023 00:44:37 ******/
CREATE USER [bouabdallah] FOR LOGIN [haythem] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [TestLogin]    Script Date: 14/03/2023 00:44:37 ******/
CREATE USER [TestLogin] FOR LOGIN [TestLogin] WITH DEFAULT_SCHEMA=[db_owner]
GO
ALTER ROLE [db_owner] ADD MEMBER [TestLogin]
GO
/****** Object:  Table [db_owner].[indicator]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [db_owner].[indicator](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_indicator] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [db_owner].[UserAcces]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [db_owner].[UserAcces](
	[IDZone] [int] NOT NULL,
	[IDUser] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_UserAcces] PRIMARY KEY CLUSTERED 
(
	[IDZone] ASC,
	[IDUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [db_owner].[Zone]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [db_owner].[Zone](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Zone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incident]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incident](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDUser] [nvarchar](250) NOT NULL,
	[Type] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Zone] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Indicator] [int] NOT NULL,
 CONSTRAINT [PK_Incident] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncidentType]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncidentType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_IncidentType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [nvarchar](250) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[FamillyName] [nvarchar](250) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 14/03/2023 00:44:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[IDUser] [nvarchar](250) NOT NULL,
	[IDRole] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[IDUser] ASC,
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [db_owner].[indicator] ON 
GO
INSERT [db_owner].[indicator] ([ID], [Name]) VALUES (1, N'Accident avec arrêt')
GO
SET IDENTITY_INSERT [db_owner].[indicator] OFF
GO
INSERT [db_owner].[UserAcces] ([IDZone], [IDUser]) VALUES (2, N'aaaa')
GO
INSERT [db_owner].[UserAcces] ([IDZone], [IDUser]) VALUES (2, N'h')
GO
INSERT [db_owner].[UserAcces] ([IDZone], [IDUser]) VALUES (2, N'LAPTOP-RMHSDIOF\Bouabdallah')
GO
INSERT [db_owner].[UserAcces] ([IDZone], [IDUser]) VALUES (2, N'x')
GO
SET IDENTITY_INSERT [db_owner].[Zone] ON 
GO
INSERT [db_owner].[Zone] ([ID], [Name]) VALUES (1, N'A')
GO
INSERT [db_owner].[Zone] ([ID], [Name]) VALUES (2, N'B')
GO
SET IDENTITY_INSERT [db_owner].[Zone] OFF
GO
SET IDENTITY_INSERT [dbo].[Incident] ON 
GO
INSERT [dbo].[Incident] ([ID], [IDUser], [Type], [Description], [Zone], [Date], [Indicator]) VALUES (5, N'LAPTOP-RMHSDIOF\Bouabdallah', 2, N'101010', 2, CAST(N'2023-03-17T22:17:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Incident] ([ID], [IDUser], [Type], [Description], [Zone], [Date], [Indicator]) VALUES (6, N'LAPTOP-RMHSDIOF\Bouabdallah', 1, N'65532', 1, CAST(N'2023-03-11T22:25:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Incident] ([ID], [IDUser], [Type], [Description], [Zone], [Date], [Indicator]) VALUES (1006, N'LAPTOP-RMHSDIOF\Bouabdallah', 2, N'33330', 1, CAST(N'2023-03-17T17:17:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Incident] ([ID], [IDUser], [Type], [Description], [Zone], [Date], [Indicator]) VALUES (1007, N'LAPTOP-RMHSDIOF\Bouabdallah', 1, N'99', 1, CAST(N'2023-03-25T17:56:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Incident] ([ID], [IDUser], [Type], [Description], [Zone], [Date], [Indicator]) VALUES (1009, N'LAPTOP-RMHSDIOF\Bouabdallah', 1, N'101010', 2, CAST(N'2023-03-17T22:17:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Incident] ([ID], [IDUser], [Type], [Description], [Zone], [Date], [Indicator]) VALUES (1010, N'LAPTOP-RMHSDIOF\Bouabdallah', 1, N'65532', 2, CAST(N'2023-03-11T22:25:00.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Incident] OFF
GO
SET IDENTITY_INSERT [dbo].[IncidentType] ON 
GO
INSERT [dbo].[IncidentType] ([ID], [Type]) VALUES (1, N'S')
GO
INSERT [dbo].[IncidentType] ([ID], [Type]) VALUES (2, N'A')
GO
SET IDENTITY_INSERT [dbo].[IncidentType] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([ID], [Role]) VALUES (1, N'User')
GO
INSERT [dbo].[Role] ([ID], [Role]) VALUES (2, N'Admin')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
INSERT [dbo].[User] ([ID], [Name], [FamillyName], [Title]) VALUES (N'h', N'h', N'h', N'h')
GO
INSERT [dbo].[User] ([ID], [Name], [FamillyName], [Title]) VALUES (N'LAPTOP-RMHSDIOF\Bouabdallah', N'tes', N'tes', N'tes')
GO
INSERT [dbo].[UserRole] ([IDUser], [IDRole]) VALUES (N'aaaa', 2)
GO
INSERT [dbo].[UserRole] ([IDUser], [IDRole]) VALUES (N'h', 2)
GO
INSERT [dbo].[UserRole] ([IDUser], [IDRole]) VALUES (N'LAPTOP-RMHSDIOF\Bouabdallah', 2)
GO
INSERT [dbo].[UserRole] ([IDUser], [IDRole]) VALUES (N'x', 2)
GO
