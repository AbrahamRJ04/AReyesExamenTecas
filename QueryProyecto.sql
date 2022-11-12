CREATE DATABASE AReyesTecasExamen
USE AReyesTecasExamen


CREATE TABLE Rol
(
IdRo INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(25)
)

CREATE TABLE Cliente
(
IdCliente INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
ApellidoPaterno VARCHAR(50) NOT NULL,
ApellidoMaterno VARCHAR(50)NOT NULL,
NumeroCliente VARCHAR(50) UNIQUE,
Email VARCHAR(250) NULL,
Password VARCHAR(50),
FechaNacimiento DATE NOT NULL,
Telefono VARCHAR(50) NULL,
CURP VARCHAR(50) NOT NULL,
Imagen VARCHAR (MAX) NULL,
IdRol INT REFERENCES Rol(IdRol)
)

CREATE TABLE Cuenta
(
IdNumeroCuenta INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
Saldo DECIMAL NOT NULL,
FechaCreacion DATETIME NOT NULL,
IdCliente INT REFERENCES Cliente(IdCliente)
)

CREATE TABLE TipoTransaccion
(
IdTipoTransaccion INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50)
)

CREATE TABLE Transaccion
(
IdTransaccion INT PRIMARY KEY IDENTITY(1,1),
IdTipoTransaccion INT REFERENCES TipoTransaccion(IdTipoTransaccion)NOT NULL,
Detalle	Varchar(MAX),
FechaTransaccion DATETIME NOT NULL,
IdCuenta INT REFERENCES Cuenta(IdNumeroCuenta),
MontoTransaccion Decimal NOT NULL
)
---STORED PROCEDURE GETALL
USE [AReyesTecasExamen]
GO
CREATE PROCEDURE ClienteGetAll
AS
BEGIN
SELECT Cliente.[IdCliente]
      ,Cliente.[Nombre]
      ,Cliente.[ApellidoPaterno]
      ,Cliente.[ApellidoMaterno]
      ,Cliente.[NumeroCliente]
      ,Cliente.[Email]
      ,Cliente.[Password]
      ,Cliente.[FechaNacimiento]
      ,Cliente.[Telefono]
      ,Cliente.[CURP]
      ,Cliente.[Imagen]
      ,Cliente.[IdRol]
	  ,Rol.Nombre as TipoRol
  FROM [dbo].[Cliente] INNER JOIN Rol on Cliente.IdRol = Rol.IdRol
  END
GO
--STORED PROCEDURE GETBYID-------------------------------------------------------------------
USE [AReyesTecasExamen]
GO
CREATE PROCEDURE ClienteById 
@IdCliente INT
AS
BEGIN
SELECT Cliente.[IdCliente]
      ,Cliente.[Nombre]
      ,Cliente.[ApellidoPaterno]
      ,Cliente.[ApellidoMaterno]
      ,Cliente.[NumeroCliente]
      ,Cliente.[Email]
      ,Cliente.[Password]
      ,Cliente.[FechaNacimiento]
      ,Cliente.[Telefono]
      ,Cliente.[CURP]
      ,Cliente.[Imagen]
      ,Cliente.[IdRol]
	  ,Rol.Nombre as TipoRol
  FROM [dbo].[Cliente] INNER JOIN Rol on Cliente.IdRol = Rol.IdRol
  WHERE IdCliente =@IdCliente
  END
GO
---------------------------------------------------------------------
--STORED PROCEDURE DELETE Cliente
USE [AReyesTecasExamen]
GO
CREATE PROCEDURE ClienteDelete
@IdCliente INT
AS
BEGIN

DELETE FROM [dbo].[Cliente]
      WHERE IdCliente =@IdCliente

DELETE FROM [dbo].Cuenta
	  WHERE IdCliente = @IdCliente
END

GO
--------------------------------------------------------------------------------
--CLIENTE ADD
USE [AReyesTecasExamen]
GO
CREATE PROCEDURE ClienteAdd
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Email VARCHAR(250),
@Password VARCHAR(50),
@FechaNacimiento VARCHAR(50),
@Telefono VARCHAR(50),
@CURP VARCHAR(50),
@Imagen VARCHAR(MAX),
@IdRol INT
AS
BEGIN
INSERT INTO [dbo].[Cliente]
           ([Nombre]
           ,[ApellidoPaterno]
           ,[ApellidoMaterno]
           ,[Email]
           ,[Password]
           ,[FechaNacimiento]
           ,[Telefono]
           ,[CURP]
           ,[Imagen]
           ,[IdRol])
     VALUES
           (@Nombre
           ,@ApellidoPaterno
           ,@ApellidoMaterno
           ,@Email
           ,@Password
           ,CONVERT(DATE,@FechaNacimiento,105)
           ,@Telefono
           ,@CURP
           ,@Imagen
           ,@IdRol)
END


GO


--------------------------------------------------------------------------------------------------


EXEC ClienteAdd 'Aline','Ortis','Aguilar','aline@gmail.com','alix456','12/02/2000','5574857698','cfxxx',NULL,1
EXEC ClienteUpdate 17,'Aline','Ortiz','Aguilar','aline@gmail.com','alix456','12/02/2000','5574857698','cfxxx',NULL,1
EXEC ClienteGetAll
---------------------------------------------------------------------------------------------------

--TRIGGER ACCION DE CLIENTE
CREATE TRIGGER ClienteNumeroClinete
ON [dbo].[Cliente]
AFTER INSERT
AS
	BEGIN
		DECLARE @NumeroCliente VARCHAR(50)
		DECLARE @IdCliente INT
		DECLARE @FechaNacimiento DATE

		SET @NumeroCliente = 
		(
			SELECT CONVERT(VARCHAR,IdCliente)+CONVERT(VARCHAR,FechaNacimiento) FROM inserted
		)
		SET @IdCliente = 
		(
			SELECT IdCliente FROM inserted
		)

		UPDATE Cliente
		SET NumeroCliente = @NumeroCliente
		WHERE IdCliente = @IdCliente
	END
	---------------------------------------------------------------------------------------
	--STORED PROCEDURE CLIENTE UPDATE
	USE [AReyesTecasExamen]
GO
CREATE PROCEDURE ClienteUpdate
@IdCliente INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Email VARCHAR(250),
@Password VARCHAR(50),
@FechaNacimiento VARCHAR(50),
@Telefono VARCHAR(50),
@CURP VARCHAR(50),
@Imagen VARCHAR(MAX),
@IdRol INT
AS
BEGIN
UPDATE [dbo].[Cliente]
   SET [Nombre] = @Nombre
      ,[ApellidoPaterno] = @ApellidoPaterno
      ,[ApellidoMaterno] = @ApellidoMaterno
      ,[Email] = @Email
      ,[Password] = @Password
      ,[FechaNacimiento] = CONVERT(date,@FechaNacimiento,105)
      ,[Telefono] = @Telefono
      ,[CURP] = @CURP
      ,[Imagen] = @Imagen
      ,[IdRol] = @IdRol
 WHERE IdCliente = @IdCliente
END
	----------------------------------------------------------------------------------------

	--HASTA ESTE PUNTO TENEMOS YA EL CRUD DE CLIENTE
	--GETALL ROL
	USE [AReyesTecasExamen]
GO
CREATE PROCEDURE RolGetAll
AS
BEGIN
SELECT [IdRol]
      ,[Nombre]
  FROM [dbo].[Rol]
  END
GO
EXEC RolGetAll
--------------------------------------------------------------------------------------------------
---STORED PROCEDURE PARA VISTA DE CUENTAS DEL CLIENTE
USE [AReyesTecasExamen]
GO
CREATE PROCEDURE CuentaGetByIdCliente
@IdCliente INT
AS
BEGIN
SELECT [IdNumeroCuenta]
      ,[Nombre]
      ,[Saldo]
      ,[FechaCreacion]
      ,[IdCliente]
  FROM [dbo].[Cuenta]
  WHERE IdCliente = @IdCliente
  END
GO
--------------------------------------------------------------------------------------
EXEC CuentaGetByIdCliente 1
------------------------------------------------------------------------------------

--CRUD DE CUENTAS
CREATE PROCEDURE CuentaGetById 1
@IdNumeroCuenta INT
AS
BEGIN
SELECT [IdNumeroCuenta]
      ,[Nombre]
      ,[Saldo]
      ,[FechaCreacion]
      ,[IdCliente]
  FROM [dbo].[Cuenta]
  WHERE IdNumeroCuenta = @IdNumeroCuenta
  END
GO
--DELETE

USE [AReyesTecasExamen]
GO
CREATE PROCEDURE CuentaDelete
@IdNumeroCuenta INT
AS
BEGIN
DELETE FROM [dbo].[Cuenta]
      WHERE IdNumeroCuenta = @IdNumeroCuenta
	  END
GO
-- ADD
USE [AReyesTecasExamen]
GO
CREATE PROCEDURE CuentaAdd
@Nombre VARCHAR(50),
@Saldo DECIMAL,
@IdCliente INT
AS
BEGIN
INSERT INTO [dbo].[Cuenta]
           ([Nombre]
           ,[Saldo]
           ,[FechaCreacion]
           ,[IdCliente])
     VALUES
           (@Nombre
           ,@Saldo
           ,GETDATE()
           ,@IdCliente)
END

GO

EXEC CuentaAdd 'Ahorro Futuro',400,1
--UPDATE

USE [AReyesTecasExamen]
GO
CREATE PROCEDURE CuentaUpdate
@IdNumeroCuenta INT,
@Nombre VARCHAR(50),
@IdCliente INT
AS
BEGIN

UPDATE [dbo].[Cuenta]
   SET [Nombre] = @Nombre
      ,[IdCliente] = @IdCliente
 WHERE IdNumeroCuenta = @IdNumeroCuenta
END

GO



