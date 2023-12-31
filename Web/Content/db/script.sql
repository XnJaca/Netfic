USE [master]
GO
/****** Object:  Database [netfic]    Script Date: 6/19/2023 2:33:56 PM ******/
CREATE DATABASE [netfic]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'netfic', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\netfic.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'netfic_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\netfic_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [netfic] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [netfic].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [netfic] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [netfic] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [netfic] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [netfic] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [netfic] SET ARITHABORT OFF 
GO
ALTER DATABASE [netfic] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [netfic] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [netfic] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [netfic] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [netfic] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [netfic] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [netfic] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [netfic] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [netfic] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [netfic] SET  DISABLE_BROKER 
GO
ALTER DATABASE [netfic] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [netfic] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [netfic] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [netfic] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [netfic] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [netfic] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [netfic] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [netfic] SET RECOVERY FULL 
GO
ALTER DATABASE [netfic] SET  MULTI_USER 
GO
ALTER DATABASE [netfic] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [netfic] SET DB_CHAINING OFF 
GO
ALTER DATABASE [netfic] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [netfic] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [netfic] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [netfic] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'netfic', N'ON'
GO
ALTER DATABASE [netfic] SET QUERY_STORE = ON
GO
ALTER DATABASE [netfic] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [netfic]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[emisorId] [int] NOT NULL,
	[receptorId] [int] NOT NULL,
	[mensaje] [varchar](200) NOT NULL,
	[fechaEnvio] [datetime] NOT NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direccion](
	[id] [int] NOT NULL,
	[idUsuario] [int] NOT NULL,
	[provincia] [varchar](50) NOT NULL,
	[canton] [varchar](50) NOT NULL,
	[distrito] [varchar](50) NOT NULL,
	[direccion] [varchar](50) NOT NULL,
	[codigoPostal] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Direccion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoPedido]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoPedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EstadoPedido] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoProducto]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoProducto](
	[id] [int] NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EstadoProducto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EvaluacionProducto]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EvaluacionProducto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[productoId] [int] NOT NULL,
	[calificacion] [int] NOT NULL,
 CONSTRAINT [PK_EvaluacionProducto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EvaluacionVendedor]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EvaluacionVendedor](
	[id] [int] NOT NULL,
	[usuarioId] [int] NOT NULL,
	[calificacion] [int] NOT NULL,
 CONSTRAINT [PK_EvaluacionVendedor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Foto]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Foto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[productoId] [int] NOT NULL,
	[foto] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Foto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MetodoPago]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetodoPago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuarioId] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[numero] [int] NOT NULL,
 CONSTRAINT [PK_MetodoPago] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuarioId] [int] NOT NULL,
	[direccionId] [int] NOT NULL,
	[estadoPedidoId] [int] NOT NULL,
	[metodoPagoId] [int] NOT NULL,
	[total] [float] NOT NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidoProducto]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoProducto](
	[productoId] [int] NOT NULL,
	[pedidoId] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[estadoPedidoId] [int] NOT NULL,
	[subtotal] [float] NOT NULL,
 CONSTRAINT [PK_PedidoProducto] PRIMARY KEY CLUSTERED 
(
	[productoId] ASC,
	[pedidoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[precio] [float] NOT NULL,
	[cantidad] [int] NOT NULL,
	[categoriaId] [int] NOT NULL,
	[estadoId] [int] NOT NULL,
	[vendedorId] [int] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefono]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefono](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuarioId] [int] NOT NULL,
	[numero] [int] NOT NULL,
	[tipoTelefono] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Telefono] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoUsuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuarioxUsuario]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuarioxUsuario](
	[usuarioId] [int] NOT NULL,
	[tipoUsuarioId] [int] NOT NULL,
 CONSTRAINT [PK_TipoUsuarioxUsuario] PRIMARY KEY CLUSTERED 
(
	[usuarioId] ASC,
	[tipoUsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/19/2023 2:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Usuario] FOREIGN KEY([emisorId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Usuario]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Usuario1] FOREIGN KEY([receptorId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Usuario1]
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD  CONSTRAINT [FK_Direccion_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Direccion] CHECK CONSTRAINT [FK_Direccion_Usuario]
GO
ALTER TABLE [dbo].[EvaluacionProducto]  WITH CHECK ADD  CONSTRAINT [FK_EvaluacionProducto_Producto] FOREIGN KEY([productoId])
REFERENCES [dbo].[Producto] ([id])
GO
ALTER TABLE [dbo].[EvaluacionProducto] CHECK CONSTRAINT [FK_EvaluacionProducto_Producto]
GO
ALTER TABLE [dbo].[EvaluacionVendedor]  WITH CHECK ADD  CONSTRAINT [FK_EvaluacionVendedor_Usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[EvaluacionVendedor] CHECK CONSTRAINT [FK_EvaluacionVendedor_Usuario]
GO
ALTER TABLE [dbo].[Foto]  WITH CHECK ADD  CONSTRAINT [FK_Foto_Producto] FOREIGN KEY([productoId])
REFERENCES [dbo].[Producto] ([id])
GO
ALTER TABLE [dbo].[Foto] CHECK CONSTRAINT [FK_Foto_Producto]
GO
ALTER TABLE [dbo].[MetodoPago]  WITH CHECK ADD  CONSTRAINT [FK_MetodoPago_Usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[MetodoPago] CHECK CONSTRAINT [FK_MetodoPago_Usuario]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Direccion] FOREIGN KEY([direccionId])
REFERENCES [dbo].[Direccion] ([id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Direccion]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_EstadoPedido] FOREIGN KEY([estadoPedidoId])
REFERENCES [dbo].[EstadoPedido] ([id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_EstadoPedido]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_MetodoPago] FOREIGN KEY([metodoPagoId])
REFERENCES [dbo].[MetodoPago] ([id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_MetodoPago]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Usuario]
GO
ALTER TABLE [dbo].[PedidoProducto]  WITH CHECK ADD  CONSTRAINT [FK_PedidoProducto_EstadoPedido] FOREIGN KEY([estadoPedidoId])
REFERENCES [dbo].[EstadoPedido] ([id])
GO
ALTER TABLE [dbo].[PedidoProducto] CHECK CONSTRAINT [FK_PedidoProducto_EstadoPedido]
GO
ALTER TABLE [dbo].[PedidoProducto]  WITH CHECK ADD  CONSTRAINT [FK_PedidoProducto_Pedido] FOREIGN KEY([pedidoId])
REFERENCES [dbo].[Pedido] ([id])
GO
ALTER TABLE [dbo].[PedidoProducto] CHECK CONSTRAINT [FK_PedidoProducto_Pedido]
GO
ALTER TABLE [dbo].[PedidoProducto]  WITH CHECK ADD  CONSTRAINT [FK_PedidoProducto_Producto] FOREIGN KEY([productoId])
REFERENCES [dbo].[Producto] ([id])
GO
ALTER TABLE [dbo].[PedidoProducto] CHECK CONSTRAINT [FK_PedidoProducto_Producto]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([categoriaId])
REFERENCES [dbo].[Categoria] ([id])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_EstadoProducto] FOREIGN KEY([estadoId])
REFERENCES [dbo].[EstadoProducto] ([id])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_EstadoProducto]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Usuario] FOREIGN KEY([vendedorId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Usuario]
GO
ALTER TABLE [dbo].[Telefono]  WITH CHECK ADD  CONSTRAINT [FK_Telefono_Usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Telefono] CHECK CONSTRAINT [FK_Telefono_Usuario]
GO
ALTER TABLE [dbo].[TipoUsuarioxUsuario]  WITH CHECK ADD  CONSTRAINT [FK_TipoUsuarioxUsuario_TipoUsuario] FOREIGN KEY([tipoUsuarioId])
REFERENCES [dbo].[TipoUsuario] ([id])
GO
ALTER TABLE [dbo].[TipoUsuarioxUsuario] CHECK CONSTRAINT [FK_TipoUsuarioxUsuario_TipoUsuario]
GO
ALTER TABLE [dbo].[TipoUsuarioxUsuario]  WITH CHECK ADD  CONSTRAINT [FK_TipoUsuarioxUsuario_Usuario] FOREIGN KEY([usuarioId])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[TipoUsuarioxUsuario] CHECK CONSTRAINT [FK_TipoUsuarioxUsuario_Usuario]
GO
USE [master]
GO
ALTER DATABASE [netfic] SET  READ_WRITE 
GO
