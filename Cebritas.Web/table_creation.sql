USE [w1740485_CebritasDb]
GO

CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EdmMetadata](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModelHash] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Usuario](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[AuthenticationCode] [nvarchar](max) NULL,
	[ActivationCode] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[RoleId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AccessToken](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[UserId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SolicitudAlerta](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Fecha] [datetime] NOT NULL,
	[Latitud] [float] NOT NULL,
	[Longitud] [float] NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Tipo] [nvarchar](max) NULL,
	[TiempoEstimado] [nvarchar](max) NULL,
	[Activo] [bit] NOT NULL,
	[UserId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AlertaUrbana](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SolicitudId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Precio](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FourSquareFirstCategoryId] [nvarchar](max) NULL,
	[FourSquareVenueId] [nvarchar](max) NULL,
	[MinPrice] [int] NOT NULL,
	[MaxPrice] [int] NOT NULL,
	[Capacity] [int] NOT NULL,
	[Parking] [bit] NOT NULL,
	[Holidays] [bit] NOT NULL,
	[SmokingArea] [bit] NOT NULL,
	[KidsArea] [bit] NOT NULL,
	[Delivery] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  ForeignKey [AccessToken_User]    Script Date: 04/07/2013 19:01:40 ******/
ALTER TABLE [dbo].[AccessToken]  WITH CHECK ADD  CONSTRAINT [AccessToken_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Usuario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccessToken] CHECK CONSTRAINT [AccessToken_User]
GO
/****** Object:  ForeignKey [AlertaUrbana_SolicitudAlerta]    Script Date: 04/07/2013 19:01:40 ******/
ALTER TABLE [dbo].[AlertaUrbana]  WITH CHECK ADD  CONSTRAINT [AlertaUrbana_SolicitudAlerta] FOREIGN KEY([SolicitudId])
REFERENCES [dbo].[SolicitudAlerta] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlertaUrbana] CHECK CONSTRAINT [AlertaUrbana_SolicitudAlerta]
GO
/****** Object:  ForeignKey [SolicitudAlerta_Usuario]    Script Date: 04/07/2013 19:01:40 ******/
ALTER TABLE [dbo].[SolicitudAlerta]  WITH CHECK ADD  CONSTRAINT [SolicitudAlerta_Usuario] FOREIGN KEY([UserId])
REFERENCES [dbo].[Usuario] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SolicitudAlerta] CHECK CONSTRAINT [SolicitudAlerta_Usuario]
GO
/****** Object:  ForeignKey [Role_Users]    Script Date: 04/07/2013 19:01:40 ******/
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [Role_Users] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [Role_Users]
GO
