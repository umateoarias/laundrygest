USE [master]
GO

/****** Object:  Database [Laundrygest]    Script Date: 15/05/2025 15:08:57 ******/
CREATE DATABASE [Laundrygest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Laundrygest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Laundrygest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Laundrygest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Laundrygest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Laundrygest] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Laundrygest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Laundrygest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Laundrygest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Laundrygest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Laundrygest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Laundrygest] SET ARITHABORT OFF 
GO
ALTER DATABASE [Laundrygest] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Laundrygest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Laundrygest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Laundrygest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Laundrygest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Laundrygest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Laundrygest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Laundrygest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Laundrygest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Laundrygest] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Laundrygest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Laundrygest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Laundrygest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Laundrygest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Laundrygest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Laundrygest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Laundrygest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Laundrygest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Laundrygest] SET  MULTI_USER 
GO
ALTER DATABASE [Laundrygest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Laundrygest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Laundrygest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Laundrygest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Laundrygest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Laundrygest] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Laundrygest] SET QUERY_STORE = ON
GO
ALTER DATABASE [Laundrygest] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Laundrygest]
GO
/****** Object:  Table [dbo].[client]    Script Date: 15/05/2025 15:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[client](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](255) NOT NULL,
	[last_name] [varchar](255) NOT NULL,
	[email] [varchar](255) NULL,
	[telephone] [varchar](255) NOT NULL,
	[nif] [varchar](255) NULL,
	[username_app] [varchar](255) NULL,
	[password_app] [varchar](255) NULL,
	[address] [varchar](255) NULL,
	[postal_code] [varchar](255) NULL,
	[locality] [varchar](255) NULL,
	[last_login_mobile] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[collection]    Script Date: 15/05/2025 15:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[collection](
	[number] [int] IDENTITY(1,1) NOT NULL,
	[created_at] [datetime] NOT NULL,
	[due_date] [datetime] NULL,
	[tax_base] [decimal](18, 4) NULL,
	[tax_amount] [decimal](18, 4) NULL,
	[total] [decimal](18, 4) NULL,
	[client_code] [int] NULL,
	[collection_type_code] [int] NOT NULL,
	[invoice_id] [int] NULL,
	[due_total] [decimal](18, 4) NULL,
	[payment_mode] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[collection_item]    Script Date: 15/05/2025 15:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[collection_item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[collected_at] [datetime] NULL,
	[num_pieces] [int] NOT NULL,
	[observation] [varchar](500) NULL,
	[store_location] [varchar](255) NULL,
	[collection_number] [int] NOT NULL,
	[pricelist_code] [int] NOT NULL,
	[delivery_number] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[collection_type]    Script Date: 15/05/2025 15:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[collection_type](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delivery]    Script Date: 15/05/2025 15:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delivery](
	[number] [int] IDENTITY(1,1) NOT NULL,
	[delivery_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice]    Script Date: 15/05/2025 15:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[number] [int] NOT NULL,
	[total_base] [decimal](18, 2) NOT NULL,
	[tax_amount] [decimal](18, 2) NOT NULL,
	[tax_base] [decimal](18, 2) NOT NULL,
	[client_code] [int] NOT NULL,
	[invoice_date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pricelist]    Script Date: 15/05/2025 15:08:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pricelist](
	[code] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [varchar](255) NOT NULL,
	[unit_price] [decimal](18, 2) NOT NULL,
	[collection_type_code] [int] NOT NULL,
	[num_pieces] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[collection] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[collection]  WITH CHECK ADD FOREIGN KEY([client_code])
REFERENCES [dbo].[client] ([code])
GO
ALTER TABLE [dbo].[collection]  WITH CHECK ADD FOREIGN KEY([collection_type_code])
REFERENCES [dbo].[collection_type] ([code])
GO
ALTER TABLE [dbo].[collection]  WITH CHECK ADD FOREIGN KEY([invoice_id])
REFERENCES [dbo].[invoice] ([id])
GO
ALTER TABLE [dbo].[collection_item]  WITH CHECK ADD FOREIGN KEY([collection_number])
REFERENCES [dbo].[collection] ([number])
GO
ALTER TABLE [dbo].[collection_item]  WITH CHECK ADD FOREIGN KEY([pricelist_code])
REFERENCES [dbo].[pricelist] ([code])
GO
ALTER TABLE [dbo].[collection_item]  WITH CHECK ADD  CONSTRAINT [FK_deliveryItems] FOREIGN KEY([delivery_number])
REFERENCES [dbo].[delivery] ([number])
GO
ALTER TABLE [dbo].[collection_item] CHECK CONSTRAINT [FK_deliveryItems]
GO
ALTER TABLE [dbo].[invoice]  WITH CHECK ADD FOREIGN KEY([client_code])
REFERENCES [dbo].[client] ([code])
GO
ALTER TABLE [dbo].[pricelist]  WITH CHECK ADD FOREIGN KEY([collection_type_code])
REFERENCES [dbo].[collection_type] ([code])
GO
USE [master]
GO
ALTER DATABASE [Laundrygest] SET  READ_WRITE 
GO
