USE [Prestamos]
GO
/****** Object:  User [alumno]    Script Date: 7/10/2021 10:38:37 ******/
CREATE USER [alumno] FOR LOGIN [alumno] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [Prestamos]    Script Date: 7/10/2021 10:38:37 ******/
CREATE USER [Prestamos] FOR LOGIN [Prestamos] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Prestamos]
GO
/****** Object:  StoredProcedure [dbo].[BorrarDetalle]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BorrarDetalle]
@idDetalle INT
AS
	
BEGIN
 DELETE FROM DetallePrestamos WHERE DetallePrestamos.Id = @idDetalle;
END

GO
/****** Object:  StoredProcedure [dbo].[BorrarPrestamo]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BorrarPrestamo]
	@idPrestamo INT
AS
BEGIN

DELETE FROM Prestamos WHERE Prestamos.id = @idPrestamo;

END

GO
/****** Object:  StoredProcedure [dbo].[BorrarUsuario]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BorrarUsuario] 
	@idUsuario INT
AS
BEGIN

DELETE FROM Usuarios WHERE Usuarios.Id = @idUsuario;

END

GO
/****** Object:  StoredProcedure [dbo].[Cuotas_borrar]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[Cuotas_borrar]
	@idDetalle int, @nroCuota int = NULL
AS
BEGIN
	DELETE FROM Cuotas WHERE IdDetalle = @idDetalle AND (NroCouta= @nroCuota OR NroCouta IS NULL)
END
GO
/****** Object:  StoredProcedure [dbo].[Cuotas_insertar]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cuotas_insertar]
@idDetalle int, @nroCuota int, @fechaVencimiento Date, @idEstado int, @monto float
	
AS
BEGIN
	INSERT INTO Cuotas(IdDetalle, NroCouta, FechaVencimiento, IdEstadoCouta, Monto)
	VALUES (@idDetalle,@nroCuota,@fechaVencimiento,@idEstado,@monto)
	SELECT @idDetalle;
END
GO
/****** Object:  StoredProcedure [dbo].[Cuotas_obtenerPorId]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cuotas_obtenerPorId]
	@idDetalle int, @nroCuota int 
AS
BEGIN
	
	SELECT * FROM Cuotas WHERE IdDetalle = @idDetalle AND NroCouta = @nroCuota;
END
GO
/****** Object:  StoredProcedure [dbo].[Cuotas_obtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cuotas_obtenerTodos]
@idDetalle int
	
AS
BEGIN
	SELECT * FROM Cuotas
	INNER JOIN DetallePrestamos ON DetallePrestamos.Id = Cuotas.IdDetalle
	WHERE IdDetalle = @idDetalle
END
GO
/****** Object:  StoredProcedure [dbo].[Cuotas_Update]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Cuotas_Update]
	@idDetalle int, @nroCuota int, @fechaVencimiento Date, @idEstado int, @monto float
AS
BEGIN
	UPDATE Cuotas
	SET NroCouta = @nroCuota,
	FechaVencimiento = @fechaVencimiento,
	IdEstadoCouta = @idEstado,
	Monto = @monto
	WHERE IdDetalle = @idDetalle
	SELECT @idDetalle;
END
GO
/****** Object:  StoredProcedure [dbo].[Departamentos_ObtenerPorIdProvincia]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Departamentos_ObtenerPorIdProvincia]
		@intIdProvincia INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Id],
		[Nombre]
  FROM [Departamentos]
  WHERE Departamentos.IdProvincia = @intIdProvincia 
  ORDER BY 
	Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[DetallePrestamo_ObtenerPorId]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DetallePrestamo_ObtenerPorId]
@IdDetalle INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
  FROM [DetallePrestamos]
  WHERE DetallePrestamos.Id = @IdDetalle
END
GO
/****** Object:  StoredProcedure [dbo].[DetallePrestamo_Update]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DetallePrestamo_Update]
	@idDetalle INT,@Monto INT,@idEstadoPrestamo INT,@CantCuotas INT,@InteresXCuota INT,@DiasEntreCuotas INT,@DiasTolereancia INT,@FechaAcuerdo DATETIME
AS
BEGIN
	

		UPDATE DetallePrestamos
		SET Monto = @Monto,
		IdEstadoDePrestamo = @idEstadoPrestamo,
		CantidadCuotas = @CantCuotas,
		InteresXCuota = @InteresXCuota,
		DiasEntreCuotas = @DiasEntreCuotas,
		DiasTolerancia = @DiasTolereancia,
		FechaDeAcuerdo = @FechaAcuerdo
		WHERE Id = @idDetalle
		RETURN @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[Detalles_obtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Detalles_obtenerTodos]
	
AS
BEGIN
	SELECT * FROM DetallePrestamos
	
	
END

GO
/****** Object:  StoredProcedure [dbo].[EstadosDePrestamo_obtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EstadosDePrestamo_obtenerTodos]
	
AS
BEGIN
		SELECT*FROM EstadosDePrestamos
   ORDER BY Id
END
GO
/****** Object:  StoredProcedure [dbo].[Generos_ObtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Generos_ObtenerTodos]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Id],
		[Nombre],
		[Orden]
  FROM [Generos]
  ORDER BY 
	Orden
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarDepartamento]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarDepartamento]
@IdProvincia INT, @Nombre VARCHAR(250)
	
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO Departamentos (IdProvincia,Nombre)
	VALUES(@IdProvincia,@Nombre);
    
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarDepartamento_scalar]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarDepartamento_scalar]
@IdProvincia INT, @Nombre VARCHAR(250)
	
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO Departamentos (IdProvincia,Nombre)
	VALUES(@IdProvincia,@Nombre);
	SELECT @@IDENTITY;
    
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarDetalle]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarDetalle]
@Monto INT,@idEstadoPrestamo INT,@CantCuotas INT,@InteresXCuota INT,@DiasEntreCuotas INT,@DiasTolereancia INT,@FechaAcuerdo DATETIME
	
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO DetallePrestamos(Monto,FechaDeAcuerdo,CantidadCuotas,InteresXCuota,DiasEntreCuotas,DiasTolerancia,IdEstadoDePrestamo)
	VALUES(@Monto,@FechaAcuerdo,@CantCuotas,@InteresXCuota,@DiasEntreCuotas,@DiasTolereancia,@idEstadoPrestamo);
    
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarDetalle_scalar]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarDetalle_scalar]
@Monto INT,@idEstadoPrestamo INT,@CantCuotas INT,@InteresXCuota INT,@DiasEntreCuotas INT,@DiasTolereancia INT,@FechaAcuerdo DATETIME = NULL
	
AS
BEGIN

	
	INSERT INTO DetallePrestamos(Monto,FechaDeAcuerdo,CantidadCuotas,InteresXCuota,DiasEntreCuotas,DiasTolerancia,IdEstadoDePrestamo)
	VALUES(@Monto,@FechaAcuerdo,@CantCuotas,@InteresXCuota,@DiasEntreCuotas,@DiasTolereancia,@idEstadoPrestamo);
    SELECT @@IDENTITY;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarEstadoPrestamo]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarEstadoPrestamo]
@Comentarios VARCHAR(512), @Nombre VARCHAR(50)
	
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO EstadosDePrestamos(Nombre,Comentarios)
	VALUES(@Nombre,@Comentarios);
    
END
GO
/****** Object:  StoredProcedure [dbo].[Insertarlocalidad]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertarlocalidad]
@IdDpto INT, @Nombre VARCHAR(250)
	
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO Localidades (IdDepartamento,Nombre)
	VALUES(@IdDpto,@Nombre);
    
END
GO
/****** Object:  StoredProcedure [dbo].[Insertarlocalidad_scalar]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertarlocalidad_scalar]
@IdDpto INT, @Nombre VARCHAR(250)
	
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO Localidades (IdDepartamento,Nombre)
	VALUES(@IdDpto,@Nombre);
	SELECT @@IDENTITY;
    
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarPrestamo]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarPrestamo]
@idDetalle INT,@idPestamista INT,@idPrestador INT
	
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO Prestamos(IdDetallePrestamo,IdUsuarioPrestamista,IdUsuarioPrestador)
	VALUES(@idDetalle,@idPestamista,@idPrestador);
    
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarPrestamo_scalar]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarPrestamo_scalar]
@idDetalle INT,@idPestamista INT,@idPrestador INT = NULL
	
AS
BEGIN

	
	INSERT INTO Prestamos(IdDetallePrestamo,IdUsuarioPrestamista,IdUsuarioPrestador)
	VALUES(@idDetalle,@idPestamista,@idPrestador);
    SELECT @@IDENTITY;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarUsuario]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarUsuario]
	-- Add the parameters for the stored procedure here
	@Username VARCHAR(50), @Password VARCHAR(20),@Nombre VARCHAR(50),@Apellido VARCHAR(50),@Mail VARCHAR(320),
	@Telefono VARCHAR(50),@Direccion VARCHAR(320),@Dni VARCHAR(20),@CBU VARCHAR(20),@CBUAlias VARCHAR(20),@Cuit VARCHAR(20),
	@Puntos INT,@Ocupacion VARCHAR(50),@Descripcion VARCHAR(512),@UrlFoto VARCHAR(512),@IdGenero INT,@IdLocalidad INT
	,@FechaCreacion DATETIME,@FechaNacimiento DATETIME,@CantPrestamosExitosos INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO Usuarios(UserName,Password,Nombre,Apellido,Mail,Telefono,Direccion,DNI,CBU,CBUAlias,CUIT,Puntos
	,Ocupacion,FechaCreación,Descripcion,IdGenero,IdLocalidad,URLFoto,CantidadPrestamosExitosos,FechaNacimiento)
	VALUES(@Username, @Password,@Nombre,@Apellido,@Mail,@Telefono,@Direccion,@Dni,@CBU,@CBUAlias,@Cuit,@Puntos,@Ocupacion,@FechaCreacion,
	@Descripcion,@IdGenero,@IdLocalidad,@UrlFoto,@CantPrestamosExitosos	,@FechaNacimiento);

END
GO
/****** Object:  StoredProcedure [dbo].[InsertarUsuario_scalar]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertarUsuario_scalar]
	-- Add the parameters for the stored procedure here
	@Username VARCHAR(50), @Password VARCHAR(20),@Nombre VARCHAR(50),@Apellido VARCHAR(50),@Mail VARCHAR(320),
	@Telefono VARCHAR(50),@Direccion VARCHAR(320),@Dni VARCHAR(20),@CBU VARCHAR(20),@CBUAlias VARCHAR(20),@Cuit VARCHAR(20),
	@Puntos INT,@Ocupacion VARCHAR(50),@Descripcion VARCHAR(512) = NULL,@UrlFoto VARCHAR(512) = NULL,@IdGenero INT,@IdLocalidad INT
	,@FechaCreacion DATETIME,@FechaNacimiento DATETIME,@CantPrestamosExitosos INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO Usuarios(UserName,Password,Nombre,Apellido,Mail,Telefono,Direccion,DNI,CBU,CBUAlias,CUIT,Puntos
	,Ocupacion,FechaCreación,Descripcion,IdGenero,IdLocalidad,URLFoto,CantidadPrestamosExitosos,FechaNacimiento,ApiKey)
	VALUES(@Username, @Password,@Nombre,@Apellido,@Mail,@Telefono,@Direccion,@Dni,@CBU,@CBUAlias,@Cuit,@Puntos,@Ocupacion,@FechaCreacion,
	@Descripcion,@IdGenero,@IdLocalidad,@UrlFoto,@CantPrestamosExitosos	,@FechaNacimiento,NEWID());

	SELECT @@IDENTITY;

END
GO
/****** Object:  StoredProcedure [dbo].[ListarPrestamosDelQuePide]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListarPrestamosDelQuePide]
	@IdUsuario INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT Prestamos.IdDetallePrestamo,Usuarios.UserName AS prestamista, DetallePrestamos.Monto ,EstadosDePrestamos.Nombre AS estado FROM Prestamos
	INNER JOIN DetallePrestamos ON Prestamos.IdDetallePrestamo = DetallePrestamos.Id
	INNER JOIN EstadosdePrestamos ON DetallePrestamos.IdEstadoDePrestamo = EstadosDePrestamos.id
	INNER JOIN Usuarios ON Usuarios.Id = Prestamos.IdUsuarioPrestamista
	WHERE Prestamos.IdUsuarioPrestador = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[ListarPrestamosDePrestamista]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ListarPrestamosDePrestamista]
	@IdUsuario INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Prestamos.IdDetallePrestamo, Usuarios.UserName AS prestamista, DetallePrestamos.Monto ,EstadosDePrestamos.Nombre AS estado FROM Prestamos
	INNER JOIN DetallePrestamos ON Prestamos.IdDetallePrestamo = DetallePrestamos.Id
	INNER JOIN EstadosdePrestamos ON DetallePrestamos.IdEstadoDePrestamo = EstadosDePrestamos.id
	INNER JOIN Usuarios ON Usuarios.Id = Prestamos.IdUsuarioPrestador
	WHERE Prestamos.IdUsuarioPrestamista = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[Localidades_ObtenerPorIdDepto]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Localidades_ObtenerPorIdDepto]
		@intIdDepto INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Id],
		[Nombre]
  FROM [Localidades]
  WHERE Localidades.Id = @intIdDepto
  ORDER BY 
	Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[PreguntasFrecuentes_obtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PreguntasFrecuentes_obtenerTodos]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM PreguntasFrecuentes 
	ORDER BY Id
END
GO
/****** Object:  StoredProcedure [dbo].[Prestamo_ObtenerActores]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Prestamo_ObtenerActores]
@IdPrestamo INT
AS	
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Usuarios.UserName AS Nombre_prestamista
  FROM Usuarios  INNER JOIN Prestamos ON Usuarios.id = Prestamos.IdUsuarioPrestamista
  WHERE Prestamos.Id = @IdPrestamo

  	SELECT Usuarios.UserName AS Nombre_recibidor
  FROM Usuarios  INNER JOIN Prestamos ON Usuarios.id = Prestamos.IdUsuarioPrestador
  WHERE Prestamos.Id = @IdPrestamo

  
END
GO
/****** Object:  StoredProcedure [dbo].[Prestamo_ObtenerPorId]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Prestamo_ObtenerPorId]
@IdPrestamo INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * 
  FROM Prestamos
  WHERE Prestamos.IdDetallePrestamo = @IdPrestamo
END
GO
/****** Object:  StoredProcedure [dbo].[Prestamo_obtenerPorMonto]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Prestamo_obtenerPorMonto] 
	@MaxMonto INT, @IdUsuario INT
AS
BEGIN
	SELECT
		DetallePrestamos.Id, 
		DetallePrestamos.Monto, 
		Usuarios.UserName,
		Usuarios.URLFoto
	FROM DetallePrestamos 
	INNER JOIN Prestamos ON DetallePrestamos.Id = Prestamos.IdDetallePrestamo 
	INNER JOIN Usuarios ON Prestamos.IdUsuarioPrestamista = Usuarios.Id
	WHERE DetallePrestamos.Monto <= @MaxMonto AND Prestamos.IdUsuarioPrestamista != @IdUsuario AND Prestamos.IdUsuarioPrestador IS NULL
END

GO
/****** Object:  StoredProcedure [dbo].[Prestamos_busquedaFiltrada]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Prestamos_busquedaFiltrada]
	@MontoMax INT  = NULL,
	@MaxInteres  INT = NULL,
	@MinDiasTolerancia INT = NULL,
	@MinDiasEntreCuotas INT  = NULL, 
	@MinCantCutoas INT  = NULL,
	@IdUsuario INT
AS
BEGIN
	SELECT Prestamos.Id, Usuarios.UserName, 
	DetallePrestamos.Monto,
	DetallePrestamos.FechaDeAcuerdo,
	DetallePrestamos.InteresXCuota,
	DetallePrestamos.CantidadCuotas,
	DetallePrestamos.DiasEntreCuotas,
	DetallePrestamos.DiasTolerancia,
	DetallePrestamos.IdEstadoDePrestamo

	FROM Prestamos
	INNER JOIN DetallePrestamos ON DetallePrestamos.Id = Prestamos.IdDetallePrestamo
	INNER JOIN Usuarios ON Usuarios.id = Prestamos.IdUsuarioPrestamista
	WHERE (DetallePrestamos.Monto <= @MontoMax OR @MontoMax IS NULL)
	AND (DetallePrestamos.CantidadCuotas >= @MinCantCutoas OR @MinCantCutoas IS NULL)
	AND(DetallePrestamos.DiasEntreCuotas >= @MinDiasEntreCuotas OR @MinDiasEntreCuotas IS NULL) 
	AND (DetallePrestamos.InteresXCuota <= @MaxInteres OR @MaxInteres IS NULL) 
	AND (DetallePrestamos.DiasTolerancia >= @MinDiasTolerancia OR @MinDiasTolerancia IS NULL)  
	AND Prestamos.IdUsuarioPrestamista != @IdUsuario AND Prestamos.IdUsuarioPrestador IS NULL
END
GO
/****** Object:  StoredProcedure [dbo].[Prestamos_obtenerPorNombreEstado]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Prestamos_obtenerPorNombreEstado] 
	@nombreEstado VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT DetallePrestamos.*, EstadosDePrestamos.Nombre FROM DetallePrestamos
   INNER JOIN EstadosDePrestamos ON DetallePrestamos.IdEstadoDePrestamo = EstadosDePrestamos.Id
   WHERE @nombreEstado = EstadosDePrestamos.Nombre
   ORDER BY DetallePrestamos.Monto
END
GO
/****** Object:  StoredProcedure [dbo].[Prestamos_obtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Prestamos_obtenerTodos]
	
AS
BEGIN
	SELECT * FROM Prestamos
	
	
END

GO
/****** Object:  StoredProcedure [dbo].[Prestamos_update]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Prestamos_update]
@idDetalle INT,@idPestamista INT,@idPrestador INT , @Id INT
	
AS
BEGIN

	
	UPDATE Prestamos
	SET IdDetallePrestamo = @idDetalle,
	IdUsuarioPrestamista = @idPestamista,
	IdUsuarioPrestador = @idPrestador
	WHERE Prestamos.Id = @Id
	
    
END
GO
/****** Object:  StoredProcedure [dbo].[Provincias_ObtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Provincias_ObtenerTodos]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Id],
		[Nombre]
  FROM [Provincias]
  ORDER BY 
	Orden
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_cambiarContraseña]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================

CREATE PROCEDURE [dbo].[Usuario_cambiarContraseña]
	@idUsuario INT , @password VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	

    UPDATE Usuarios
	SET Password = @password
	WHERE Id = @idUsuario
	RETURN @@ROWCOUNT;
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorId]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorId]
		@intId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Usuarios].[Id]								AS [Usuarios_Id]						,
		[Usuarios].[UserName]						AS [Usuarios_UserName]					,
		[Usuarios].[Password]						AS [Usuarios_Password]					,
		[Usuarios].[Nombre]							AS [Usuarios_Nombre]					,
		[Usuarios].[Apellido]						AS [Usuarios_Apellido]					,
		[Usuarios].[Mail]							AS [Usuarios_Mail]						,
		[Usuarios].[Telefono]						AS [Usuarios_Telefono]					,
		[Usuarios].[Direccion]						AS [Usuarios_Direccion]				,
		[Usuarios].[DNI]							AS [Usuarios_DNI]						,
		[Usuarios].[CBU]							AS [Usuarios_CBU]						,
		[Usuarios].[CBUAlias]						AS [Usuarios_CBUAlias]					,
		[Usuarios].[CUIT]							AS [Usuarios_CUIT]						,
		[Usuarios].[Puntos]							AS [Usuarios_Puntos]					,
		[Usuarios].[Ocupacion]						AS [Usuarios_Ocupacion]				,
		[Usuarios].[FechaCreación]					AS [Usuarios_FechaCreación]			,
		[Usuarios].[Descripcion]					AS [Usuarios_Descripcion]				,
		[Usuarios].[IdGenero]						AS [Usuarios_IdGenero]					,
		[Usuarios].[IdLocalidad]					AS [Usuarios_IdLocalidad]				,
		[Usuarios].[URLFoto]						AS [Usuarios_URLFoto]					,
		[Usuarios].[CantidadPrestamosExitosos]		AS [Usuarios_CantidadPrestamosExitosos],
		[Usuarios].[FechaNacimiento]				AS [Usuarios_FechaNacimiento]			,
		[Usuarios].[ApiKey]				            AS [Usuarios_Apikey]			,
		Generos.Nombre								AS Generos_Nombre						,
		Localidades.Nombre							AS Localidades_Nombre 
  FROM [Usuarios]
  INNER JOIN Generos ON Usuarios.IdGenero = Generos.Id
  INNER JOIN Localidades ON Usuarios.IdLocalidad = Localidades.Id
  WHERE Usuarios.Id = @intId
  ORDER BY 
	Usuarios.Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorIdGenero]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorIdGenero]
@Idgenero INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Usuarios].[Id]								AS [Usuarios_Id]						,
		[Usuarios].[UserName]						AS [Usuarios_UserName]					,
		[Usuarios].[Password]						AS [Usuarios_Password]					,
		[Usuarios].[Nombre]							AS [Usuarios_Nombre]					,
		[Usuarios].[Apellido]						AS [Usuarios_Apellido]					,
		[Usuarios].[Mail]							AS [Usuarios_Mail]						,
		[Usuarios].[Telefono]						AS [Usuarios_Telefono]					,
		[Usuarios].[Direccion]						AS [Usuarios_Direccion]				,
		[Usuarios].[DNI]							AS [Usuarios_DNI]						,
		[Usuarios].[CBU]							AS [Usuarios_CBU]						,
		[Usuarios].[CBUAlias]						AS [Usuarios_CBUAlias]					,
		[Usuarios].[CUIT]							AS [Usuarios_CUIT]						,
		[Usuarios].[Puntos]							AS [Usuarios_Puntos]					,
		[Usuarios].[Ocupacion]						AS [Usuarios_Ocupacion]				,
		[Usuarios].[FechaCreación]					AS [Usuarios_FechaCreación]			,
		[Usuarios].[Descripcion]					AS [Usuarios_Descripcion]				,
		[Usuarios].[IdGenero]						AS [Usuarios_IdGenero]					,
		[Usuarios].[IdLocalidad]					AS [Usuarios_IdLocalidad]				,
		[Usuarios].[URLFoto]						AS [Usuarios_URLFoto]					,
		[Usuarios].[CantidadPrestamosExitosos]		AS [Usuarios_CantidadPrestamosExitosos],
		[Usuarios].[FechaNacimiento]				AS [Usuarios_FechaNacimiento]			,
		[Usuarios].[ApiKey]				            AS [Usuarios_Apikey]					,
		Generos.Nombre								AS Generos_Nombre						,
		Localidades.Nombre							AS Localidades_Nombre 
  FROM [Usuarios]
  INNER JOIN Generos ON Usuarios.IdGenero = Generos.Id
  INNER JOIN Localidades ON Usuarios.IdLocalidad = Localidades.Id
  WHERE Usuarios.IdGenero = @Idgenero
  ORDER BY 
	Usuarios.Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorIdLocalidad]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorIdLocalidad]
		@intIdLocalidad INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Usuarios].[Id]								AS [Usuarios_Id]						,
		[Usuarios].[UserName]						AS [Usuarios_UserName]					,
		[Usuarios].[Password]						AS [Usuarios_Password]					,
		[Usuarios].[Nombre]							AS [Usuarios_Nombre]					,
		[Usuarios].[Apellido]						AS [Usuarios_Apellido]					,
		[Usuarios].[Mail]							AS [Usuarios_Mail]						,
		[Usuarios].[Telefono]						AS [Usuarios_Telefono]					,
		[Usuarios].[Direccion]						AS [Usuarios_Direccion]				,
		[Usuarios].[DNI]							AS [Usuarios_DNI]						,
		[Usuarios].[CBU]							AS [Usuarios_CBU]						,
		[Usuarios].[CBUAlias]						AS [Usuarios_CBUAlias]					,
		[Usuarios].[CUIT]							AS [Usuarios_CUIT]						,
		[Usuarios].[Puntos]							AS [Usuarios_Puntos]					,
		[Usuarios].[Ocupacion]						AS [Usuarios_Ocupacion]				,
		[Usuarios].[FechaCreación]					AS [Usuarios_FechaCreación]			,
		[Usuarios].[Descripcion]					AS [Usuarios_Descripcion]				,
		[Usuarios].[IdGenero]						AS [Usuarios_IdGenero]					,
		[Usuarios].[IdLocalidad]					AS [Usuarios_IdLocalidad]				,
		[Usuarios].[URLFoto]						AS [Usuarios_URLFoto]					,
		[Usuarios].[CantidadPrestamosExitosos]		AS [Usuarios_CantidadPrestamosExitosos],
		[Usuarios].[FechaNacimiento]				AS [Usuarios_FechaNacimiento]			,
		[Usuarios].[ApiKey]				            AS [Usuarios_Apikey]			,
		Generos.Nombre								AS Generos_Nombre						,
		Localidades.Nombre							AS Localidades_Nombre 
  FROM [Usuarios]
  INNER JOIN Generos ON Usuarios.IdGenero = Generos.Id
  INNER JOIN Localidades ON Usuarios.IdLocalidad = Localidades.Id
  WHERE Usuarios.IdLocalidad = @intIdLocalidad
  ORDER BY 
	Usuarios.Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorNombreUsuario]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorNombreUsuario]
		@UserName VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT 
		[Usuarios].[Id]								AS [Usuarios_Id]						,
		[Usuarios].[UserName]						AS [Usuarios_UserName]					,
		[Usuarios].[Password]						AS [Usuarios_Password]					,
		[Usuarios].[Nombre]							AS [Usuarios_Nombre]					,
		[Usuarios].[Apellido]						AS [Usuarios_Apellido]					,
		[Usuarios].[Mail]							AS [Usuarios_Mail]						,
		[Usuarios].[Telefono]						AS [Usuarios_Telefono]					,
		[Usuarios].[Direccion]						AS [Usuarios_Direccion]				,
		[Usuarios].[DNI]							AS [Usuarios_DNI]						,
		[Usuarios].[CBU]							AS [Usuarios_CBU]						,
		[Usuarios].[CBUAlias]						AS [Usuarios_CBUAlias]					,
		[Usuarios].[CUIT]							AS [Usuarios_CUIT]						,
		[Usuarios].[Puntos]							AS [Usuarios_Puntos]					,
		[Usuarios].[Ocupacion]						AS [Usuarios_Ocupacion]				,
		[Usuarios].[FechaCreación]					AS [Usuarios_FechaCreación]			,
		[Usuarios].[Descripcion]					AS [Usuarios_Descripcion]				,
		[Usuarios].[IdGenero]						AS [Usuarios_IdGenero]					,
		[Usuarios].[IdLocalidad]					AS [Usuarios_IdLocalidad]				,
		[Usuarios].[URLFoto]						AS [Usuarios_URLFoto]					,
		[Usuarios].[CantidadPrestamosExitosos]		AS [Usuarios_CantidadPrestamosExitosos],
		[Usuarios].[FechaNacimiento]				AS [Usuarios_FechaNacimiento]			,
		[Usuarios].[ApiKey]				            AS [Usuarios_Apikey]			,
		Generos.Nombre								AS Generos_Nombre						,
		Localidades.Nombre							AS Localidades_Nombre 
  FROM [Usuarios]
  INNER JOIN Generos ON Usuarios.IdGenero = Generos.Id
  INNER JOIN Localidades ON Usuarios.IdLocalidad = Localidades.Id
  WHERE Usuarios.UserName = @UserName
  ORDER BY 
	Usuarios.Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_ObtenerPorPuntos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Usuario_ObtenerPorPuntos]
		@puntos INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		[Usuarios].[Id]								AS [Usuarios_Id]						,
		[Usuarios].[UserName]						AS [Usuarios_UserName]					,
		[Usuarios].[Password]						AS [Usuarios_Password]					,
		[Usuarios].[Nombre]							AS [Usuarios_Nombre]					,
		[Usuarios].[Apellido]						AS [Usuarios_Apellido]					,
		[Usuarios].[Mail]							AS [Usuarios_Mail]						,
		[Usuarios].[Telefono]						AS [Usuarios_Telefono]					,
		[Usuarios].[Direccion]						AS [Usuarios_Direccion]				,
		[Usuarios].[DNI]							AS [Usuarios_DNI]						,
		[Usuarios].[CBU]							AS [Usuarios_CBU]						,
		[Usuarios].[CBUAlias]						AS [Usuarios_CBUAlias]					,
		[Usuarios].[CUIT]							AS [Usuarios_CUIT]						,
		[Usuarios].[Puntos]							AS [Usuarios_Puntos]					,
		[Usuarios].[Ocupacion]						AS [Usuarios_Ocupacion]				,
		[Usuarios].[FechaCreación]					AS [Usuarios_FechaCreación]			,
		[Usuarios].[Descripcion]					AS [Usuarios_Descripcion]				,
		[Usuarios].[IdGenero]						AS [Usuarios_IdGenero]					,
		[Usuarios].[IdLocalidad]					AS [Usuarios_IdLocalidad]				,
		[Usuarios].[URLFoto]						AS [Usuarios_URLFoto]					,
		[Usuarios].[CantidadPrestamosExitosos]		AS [Usuarios_CantidadPrestamosExitosos],
		[Usuarios].[FechaNacimiento]				AS [Usuarios_FechaNacimiento]			,
		[Usuarios].[ApiKey]				            AS [Usuarios_Apikey]			,
		Generos.Nombre								AS Generos_Nombre						,
		Localidades.Nombre							AS Localidades_Nombre 
  FROM [Usuarios]
  INNER JOIN Generos ON Usuarios.IdGenero = Generos.Id
  INNER JOIN Localidades ON Usuarios.IdLocalidad = Localidades.Id
  WHERE Usuarios.Puntos >= @puntos
  ORDER BY 
	Usuarios.Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[Usuarios_login]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuarios_login]
@mail VARCHAR(320), @contra	VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.


    SELECT 
	[Usuarios].[Id]								AS [Usuarios_Id]						,
		[Usuarios].[UserName]						AS [Usuarios_UserName]					,
		[Usuarios].[Password]						AS [Usuarios_Password]					,
		[Usuarios].[Nombre]							AS [Usuarios_Nombre]					,
		[Usuarios].[Apellido]						AS [Usuarios_Apellido]					,
		[Usuarios].[Mail]							AS [Usuarios_Mail]						,
		[Usuarios].[Telefono]						AS [Usuarios_Telefono]					,
		[Usuarios].[Direccion]						AS [Usuarios_Direccion]				,
		[Usuarios].[DNI]							AS [Usuarios_DNI]						,
		[Usuarios].[CBU]							AS [Usuarios_CBU]						,
		[Usuarios].[CBUAlias]						AS [Usuarios_CBUAlias]					,
		[Usuarios].[CUIT]							AS [Usuarios_CUIT]						,
		[Usuarios].[Puntos]							AS [Usuarios_Puntos]					,
		[Usuarios].[Ocupacion]						AS [Usuarios_Ocupacion]				,
		[Usuarios].[FechaCreación]					AS [Usuarios_FechaCreación]			,
		[Usuarios].[Descripcion]					AS [Usuarios_Descripcion]				,
		[Usuarios].[IdGenero]						AS [Usuarios_IdGenero]					,
		[Usuarios].[IdLocalidad]					AS [Usuarios_IdLocalidad]				,
		[Usuarios].[URLFoto]						AS [Usuarios_URLFoto]					,
		[Usuarios].[CantidadPrestamosExitosos]		AS [Usuarios_CantidadPrestamosExitosos],
		[Usuarios].[FechaNacimiento]				AS [Usuarios_FechaNacimiento]			,
		[Usuarios].[ApiKey]				            AS [Usuarios_Apikey]			,
		Generos.Nombre								AS Generos_Nombre						,
		Localidades.Nombre							AS Localidades_Nombre 
	FROM Usuarios
	INNER JOIN Generos ON Usuarios.IdGenero = Generos.Id
  INNER JOIN Localidades ON Usuarios.IdLocalidad = Localidades.Id
	WHERE Mail = @mail AND Password = @contra

	END
GO
/****** Object:  StoredProcedure [dbo].[Usuarios_obtenerIdPorApiKey]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Usuarios_obtenerIdPorApiKey]
	@ApiKey VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	SELECT Id from Usuarios WHERE ApiKey = @ApiKey
END
GO
/****** Object:  StoredProcedure [dbo].[Usuarios_obtenerTodos]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Usuarios_obtenerTodos]
	
AS
BEGIN
		SELECT 
		[Usuarios].[Id]								AS [Usuarios_Id]						,
		[Usuarios].[UserName]						AS [Usuarios_UserName]					,
		[Usuarios].[Password]						AS [Usuarios_Password]					,
		[Usuarios].[Nombre]							AS [Usuarios_Nombre]					,
		[Usuarios].[Apellido]						AS [Usuarios_Apellido]					,
		[Usuarios].[Mail]							AS [Usuarios_Mail]						,
		[Usuarios].[Telefono]						AS [Usuarios_Telefono]					,
		[Usuarios].[Direccion]						AS [Usuarios_Direccion]				,
		[Usuarios].[DNI]							AS [Usuarios_DNI]						,
		[Usuarios].[CBU]							AS [Usuarios_CBU]						,
		[Usuarios].[CBUAlias]						AS [Usuarios_CBUAlias]					,
		[Usuarios].[CUIT]							AS [Usuarios_CUIT]						,
		[Usuarios].[Puntos]							AS [Usuarios_Puntos]					,
		[Usuarios].[Ocupacion]						AS [Usuarios_Ocupacion]				,
		[Usuarios].[FechaCreación]					AS [Usuarios_FechaCreación]			,
		[Usuarios].[Descripcion]					AS [Usuarios_Descripcion]				,
		[Usuarios].[IdGenero]						AS [Usuarios_IdGenero]					,
		[Usuarios].[IdLocalidad]					AS [Usuarios_IdLocalidad]				,
		[Usuarios].[URLFoto]						AS [Usuarios_URLFoto]					,
		[Usuarios].[CantidadPrestamosExitosos]		AS [Usuarios_CantidadPrestamosExitosos],
		[Usuarios].[FechaNacimiento]				AS [Usuarios_FechaNacimiento]			,
		[Usuarios].[ApiKey]				            AS [Usuarios_Apikey]			,
		Generos.Nombre								AS Generos_Nombre						,
		Localidades.Nombre							AS Localidades_Nombre 
  FROM [Usuarios]
  INNER JOIN Generos ON Usuarios.IdGenero = Generos.Id
  INNER JOIN Localidades ON Usuarios.IdLocalidad = Localidades.Id
  ORDER BY Usuarios.UserName
END

GO
/****** Object:  StoredProcedure [dbo].[Usuarios_Update]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Usuarios_Update] 
	 @idUsuario INT,  @Username VARCHAR(50), @Password VARCHAR(20),@Nombre VARCHAR(50),@Apellido VARCHAR(50),@Mail VARCHAR(320),
	@Telefono VARCHAR(50),@Direccion VARCHAR(320),@Dni VARCHAR(20),@CBU VARCHAR(20),@CBUAlias VARCHAR(20),@Cuit VARCHAR(20),
	@Puntos INT,@Ocupacion VARCHAR(50),@Descripcion VARCHAR(512),@UrlFoto VARCHAR(512),@IdGenero INT,@IdLocalidad INT
	,@FechaCreacion DATETIME,@FechaNacimiento DATETIME,@CantPrestamosExitosos INT
AS
BEGIN

 UPDATE Usuarios SET Usuarios.Password = @Password
,Nombre = @Nombre,
 Apellido = @Apellido 
 , Mail = @Mail 
 , Telefono = @Telefono
 , Direccion = @Direccion 
 , DNI = @Dni 
 , CBU = @CBU 
 , CBUAlias = @CBUAlias 
 , CUIT = @Cuit 
 , Puntos = @Puntos 
 , Ocupacion = @Ocupacion 
 , Descripcion = @Descripcion 
 , URLFoto = @UrlFoto
 , IdGenero = @IdGenero 
 , IdLocalidad = @IdLocalidad
 , FechaCreación = @FechaCreacion
 , FechaNacimiento = @FechaNacimiento 
 , CantidadPrestamosExitosos = @CantPrestamosExitosos
  WHERE Id = @idUsuario




END
GO
/****** Object:  StoredProcedure [dbo].[Usuarios_verificarApiKey]    Script Date: 7/10/2021 10:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Usuarios_verificarApiKey]
	@ApiKey VARCHAR(50)
AS
BEGIN
	SELECT COUNT(Usuarios.ApiKey) AS CantidadAfectadas FROM Usuarios WHERE ApiKey = @ApiKey

END

GO
