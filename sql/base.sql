/****** Object:  Table [dbo].[clientes]    Script Date: 08/22/2023 01:01:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[clientes](
	[clienteId] [int] IDENTITY(1,1) NOT NULL,
	[personaId] [int] NOT NULL,
	[contrasenia] [varchar](50) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_clientes] PRIMARY KEY CLUSTERED 
(
	[clienteId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[cuentas](
	[cuentaId] [int] IDENTITY(1,1) NOT NULL,
	[clienteId] [int] NOT NULL,
	[nroCuenta] [varchar](50) NOT NULL,
	[tipoCuentaId] [int] NOT NULL,
	[saldoInicial] [decimal](18, 5) NOT NULL,
	[saldo] [decimal](18, 5) NOT NULL,
	[limiteDiario] [decimal](18, 5) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_cuentas] PRIMARY KEY CLUSTERED 
(
	[cuentaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[movimientos](
	[movimientoId] [int] IDENTITY(1,1) NOT NULL,
	[cuentaId] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[tipoId] [int] NOT NULL,
	[valor] [decimal](18, 5) NOT NULL,
	[saldo] [decimal](18, 5) NOT NULL,
 CONSTRAINT [PK_movimientos] PRIMARY KEY CLUSTERED 
(
	[movimientoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[personas](
	[personaId] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NOT NULL,
	[genero] [char](1) NOT NULL,
	[edad] [int] NOT NULL,
	[identificacion] [varchar](50) NOT NULL,
	[direccion] [varchar](250) NOT NULL,
	[telefono] [varchar](50) NOT NULL,
 CONSTRAINT [PK_personas] PRIMARY KEY CLUSTERED 
(
	[personaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[tipos_cuenta](
	[tipoCuentaId] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](150) NOT NULL,
 CONSTRAINT [PK_tipos_cuenta] PRIMARY KEY CLUSTERED 
(
	[tipoCuentaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[tipos_movimiento](
	[TipoMovimientoId] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](150) NULL,
 CONSTRAINT [PK_tipos_movimientos] PRIMARY KEY CLUSTERED 
(
	[TipoMovimientoId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO tipos_cuenta(descripcion) VALUES('Ahorro');
INSERT INTO tipos_cuenta(descripcion) VALUES('Corriente');
INSERT INTO tipos_movimiento(descripcion) VALUES('Debito');
INSERT INTO tipos_movimiento(descripcion) VALUES('Credito');