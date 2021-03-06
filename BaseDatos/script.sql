USE [master]
GO
/****** Object:  Database [Base]    Script Date: 5/30/2022 2:26:13 AM ******/
CREATE DATABASE [Base]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Base', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Base.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Base_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Base_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Base] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Base].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Base] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Base] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Base] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Base] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Base] SET ARITHABORT OFF 
GO
ALTER DATABASE [Base] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Base] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Base] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Base] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Base] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Base] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Base] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Base] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Base] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Base] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Base] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Base] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Base] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Base] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Base] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Base] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Base] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Base] SET RECOVERY FULL 
GO
ALTER DATABASE [Base] SET  MULTI_USER 
GO
ALTER DATABASE [Base] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Base] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Base] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Base] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Base] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Base] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Base] SET QUERY_STORE = OFF
GO
USE [Base]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 5/30/2022 2:26:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[cl_id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[cl_contrasenia] [varchar](30) NOT NULL,
	[cl_estado] [bit] NOT NULL,
	[cl_nombre] [varchar](30) NOT NULL,
	[cl_genero] [varchar](30) NOT NULL,
	[cl_edad] [int] NOT NULL,
	[cl_identificacion] [varchar](30) NOT NULL,
	[cl_direccion] [varchar](50) NULL,
	[cl_telefono] [varchar](20) NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[cl_id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cuentas]    Script Date: 5/30/2022 2:26:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuentas](
	[cu_numero_cuenta] [varchar](30) NOT NULL,
	[cu_id_cliente] [int] NOT NULL,
	[cu_tipo_cuenta] [varchar](30) NOT NULL,
	[cu_saldo_inicial] [money] NOT NULL,
	[cu_estado] [bit] NOT NULL,
 CONSTRAINT [PK__cuentas__5138EEC71FAE4CF6] PRIMARY KEY CLUSTERED 
(
	[cu_numero_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movimientos]    Script Date: 5/30/2022 2:26:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movimientos](
	[mo_id_movimiento] [int] IDENTITY(1,1) NOT NULL,
	[mo_numero_cuenta] [varchar](30) NOT NULL,
	[mo_fecha] [datetime] NOT NULL,
	[mo_tipo_movimiento] [varchar](30) NOT NULL,
	[mo_valor] [money] NOT NULL,
	[mo_saldo] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[mo_id_movimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cliente] ON 

INSERT [dbo].[cliente] ([cl_id_cliente], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_identificacion], [cl_direccion], [cl_telefono]) VALUES (1, N'1234', 1, N'Jose Lema', N'Masculino', 33, N'1003094594', N'Amazonas y Otavalo sn y principal', N'098254785')
INSERT [dbo].[cliente] ([cl_id_cliente], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_identificacion], [cl_direccion], [cl_telefono]) VALUES (2, N'5678', 1, N'Marianela Montalvo', N'Masculino', 44, N'1003862024', N'Amazonas y NNUU', N'097548965')
INSERT [dbo].[cliente] ([cl_id_cliente], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_identificacion], [cl_direccion], [cl_telefono]) VALUES (3, N'1245', 1, N'Juan Osorio', N'Masculino', 46, N'1738174332', N'13 junio y Equinoccial', N'098874587')
INSERT [dbo].[cliente] ([cl_id_cliente], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_identificacion], [cl_direccion], [cl_telefono]) VALUES (5, N'2222', 1, N'Juan Males', N'Masculino', 30, N'1567789002', N'9 de Octubre', N'098273783')
SET IDENTITY_INSERT [dbo].[cliente] OFF
GO
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo_cuenta], [cu_saldo_inicial], [cu_estado]) VALUES (N'1112222', 5, N'Ahorros', 0.0000, 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo_cuenta], [cu_saldo_inicial], [cu_estado]) VALUES (N'225487', 2, N'Corriente', 100.0000, 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo_cuenta], [cu_saldo_inicial], [cu_estado]) VALUES (N'478758', 1, N'Ahorro', 2000.0000, 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo_cuenta], [cu_saldo_inicial], [cu_estado]) VALUES (N'495878', 3, N'Ahorros', 0.0000, 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo_cuenta], [cu_saldo_inicial], [cu_estado]) VALUES (N'496825', 2, N'Ahorros', 540.0000, 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_tipo_cuenta], [cu_saldo_inicial], [cu_estado]) VALUES (N'585545', 1, N'Corriente', 1000.0000, 1)
GO
SET IDENTITY_INSERT [dbo].[movimientos] ON 

INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (22, N'225487', CAST(N'2022-05-30T01:59:06.710' AS DateTime), N'DEPOSITO', 100.0000, 100.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (23, N'478758', CAST(N'2022-05-30T01:59:06.710' AS DateTime), N'DEPOSITO', 2000.0000, 2000.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (24, N'495878', CAST(N'2022-05-30T01:59:06.713' AS DateTime), N'DEPOSITO', 0.0000, 0.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (25, N'496825', CAST(N'2022-05-30T01:59:06.713' AS DateTime), N'DEPOSITO', 540.0000, 540.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (26, N'585545', CAST(N'2022-05-30T01:59:06.713' AS DateTime), N'DEPOSITO', 1000.0000, 1000.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (29, N'478758', CAST(N'2022-05-30T02:08:16.947' AS DateTime), N'Retiro', -20.0000, 1980.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (30, N'478758', CAST(N'2022-05-30T02:10:14.943' AS DateTime), N'Deposito', 80.0000, 2060.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (31, N'478758', CAST(N'2022-05-30T02:23:26.237' AS DateTime), N'Deposito', 80.0000, 2140.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_valor], [mo_saldo]) VALUES (32, N'478758', CAST(N'2022-05-30T02:24:19.047' AS DateTime), N'Retiro', -200.0000, 1940.0000)
SET IDENTITY_INSERT [dbo].[movimientos] OFF
GO
ALTER TABLE [dbo].[cuentas]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_cuentas] FOREIGN KEY([cu_id_cliente])
REFERENCES [dbo].[cliente] ([cl_id_cliente])
GO
ALTER TABLE [dbo].[cuentas] CHECK CONSTRAINT [FK_cuentas_cuentas]
GO
ALTER TABLE [dbo].[movimientos]  WITH CHECK ADD  CONSTRAINT [FK_movimientos_cuentas] FOREIGN KEY([mo_numero_cuenta])
REFERENCES [dbo].[cuentas] ([cu_numero_cuenta])
GO
ALTER TABLE [dbo].[movimientos] CHECK CONSTRAINT [FK_movimientos_cuentas]
GO
USE [master]
GO
ALTER DATABASE [Base] SET  READ_WRITE 
GO
