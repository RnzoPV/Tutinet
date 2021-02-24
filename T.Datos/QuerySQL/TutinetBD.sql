
IF DB_ID('TutinetDB') is not null
	BEGIN 
		USE master
		DROP DATABASE TutinetDB
	END
GO

CREATE DATABASE TutinetDB
GO
USE TutinetDB
GO

/*
CREATE TABLE MYTABLE(
mydecimal_col DECIMAL(5,2)
)
GO

INSERT INTO MYTABLE VALUES  (123.123)
GO

INSERT INTO MYTABLE VALUES  (123.125)
GO
SELECT* FROM MYTABLE
GO
*/

CREATE TABLE TipoDoc(
tipodoc_id INT NOT NULL IDENTITY(100,1),
tipodoc_nombre VARCHAR(40) NOT NULL,
tipodoc_abreviatura VARCHAR(15) NOT NULL
)
GO
--CONSTRAINT
ALTER TABLE Tipodoc
	ADD PRIMARY KEY (tipodoc_id)
GO
--DATOS
INSERT INTO TipoDoc values ('Libreta Electoral o DNI','L.E/DNI')
INSERT INTO TipoDoc values ('Carnet de Extranjeria','Carnet Ext.')
INSERT INTO TipoDoc values ('Registro Unico de Contribuyente','RUC')
INSERT INTO TipoDoc values ('Pasaporte','Pasaporte')
INSERT INTO TipoDoc values ('Partida de Nacimiento Identidad','P.Nac.')
GO

SELECT * FROM TipoDoc
GO
/*----------------------------
CREATE TABLE Persona(
persona_id INT not null IDENTITY(100,1),
persona_nombre VARCHAR(20) NOT NULL,
persona_apellido VARCHAR(40)  NULL,
persona_fecnac DATE  NULL,
persona_tipodoc_id INT NULL,
persona_doc VARCHAR(15)  NULL,
persona_feccre DATE NOT NULL,
persona_celular char(9)  NULL
)
GO
--Llaves y constraints
ALTER TABLE Persona
	ADD PRIMARY KEY (persona_id),
	FOREIGN KEY (persona_tipodoc_id) REFERENCES TipoDoc(tipodoc_id),
	CONSTRAINT uq_persona_doc UNIQUE(persona_doc)
GO
CREATE TABLE Empleado(
persona_id INT NOT NULL IDENTITY(100,1),
empleado_usuario VARCHAR(40) NOT NULL,
empleado_contrasena CHAR(64)NOT NULL,
empleado_perfil INT NULL,
empleado_estado INT NOT NULL,
)

INSERT INTO Persona VALUES ('Renzo','Peralta Villaizan','1996-02-16',100,80135144,GETDATE(),896642214)
GO*/

CREATE TABLE Empleado(
empleado_id INT NOT NULL IDENTITY(100,1),
empleado_nombre VARCHAR(20) NOT NULL,
empleado_apellido VARCHAR(40)  NULL,
empleado_fecnac DATE  NULL,
empleado_tipodoc_id INT NULL,
empleado_doc VARCHAR(15)  NULL,
empleado_feccre DATE NOT NULL,
empleado_celular char(9)  NULL,
empleado_usuario VARCHAR(40) NOT NULL,
empleado_contrasena CHAR(64)NOT NULL,
empleado_estado INT NOT NULL,
)

--CONSTRAINT
ALTER TABLE Empleado
	ADD PRIMARY KEY (empleado_id),
	CONSTRAINT uq_empleado_usuario UNIQUE (empleado_usuario),
	CONSTRAINT df_empleado_estado DEFAULT 1 FOR empleado_estado,
	FOREIGN KEY (empleado_tipodoc_id) REFERENCES TipoDoc(tipodoc_id),
	CONSTRAINT uq_empleado_doc UNIQUE(empleado_doc),
	CONSTRAINT uq_empleado_celular UNIQUE(empleado_celular)
GO
--DATOS
INSERT INTO Empleado VALUES('Renzo','Peralta Villaizan','1996-02-16',100,80135144,GETDATE(),896642214,'Renzo.Peralta','408c81d0bdb66ee4e5543576d8914e5cc7a7a885a4b624873b979c084a37c8ea',DEFAULT)
INSERT INTO Empleado VALUES('Jose','Padilla','1996-02-16',100,70127829,GETDATE(),953734233,'jose.padilla','5c0a1311fe6a27b4bae6cc7cb17f3478545d9ae306f2137b1970e88a99ad953d',DEFAULT)
GO

SELECT * FROM Empleado
GO


CREATE TABLE Perfil(
perfil_id INT NOT NULL IDENTITY(100,1),
perfil_descripcion VARCHAR (100) NOT NULL,
perfil_feccre DATE NOT NULL,
perfil_estado VARCHAR(1)
)
GO
--CONSTRAINT
ALTER TABLE Perfil
	ADD PRIMARY KEY (perfil_id),
	DEFAULT 1 FOR perfil_estado
GO
--DATOS
INSERT INTO Perfil VALUES ('TI',GETDATE(),DEFAULT)
INSERT INTO Perfil VALUES ('Compras',GETDATE(),DEFAULT)
GO

SELECT * FROM Perfil
GO

CREATE TABLE Empleado_Perfil_Detalle(
empleado_id INT NOT NULL,
perfil_id INT NOT NULL,
empleado_perfil_detalle_feccre DATE NOT NULL,
empleado_perfil_detalle_estado INT NOT NULL,
)
GO
--CONSTRAINT
ALTER TABLE Empleado_Perfil_Detalle
	ADD FOREIGN KEY (empleado_id) REFERENCES Empleado(empleado_id),
	FOREIGN KEY (perfil_id) REFERENCES Perfil(perfil_id),
	 PRIMARY KEY (perfil_id,empleado_id),
	 DEFAULT 1 FOR empleado_perfil_detalle_estado
GO
INSERT INTO  Empleado_Perfil_Detalle VALUES (100,100,GETDATE(),DEFAULT)
INSERT INTO  Empleado_Perfil_Detalle VALUES (101,101,GETDATE(),DEFAULT)
GO

SELECT * FROM Empleado_Perfil_Detalle
GO

CREATE TABLE Modulo(
	modulo_id INT NOT NULL IDENTITY(100,1),
	modulo_nombre VARCHAR (100) NOT NULL,
	modulo_descripcion VARCHAR(200) NULL,
	modulo_fecre DATE NOT NULL,
	modulo_estado int NOT NULL
)
GO
--CONSTRAINT
ALTER TABLE Modulo
	ADD PRIMARY KEY (modulo_id),
	DEFAULT 1 FOR modulo_estado
GO

INSERT INTO Modulo VALUES ('INGRESAR PRODUCTO','Modulo para ingresar productos al sistema',GETDATE(),DEFAULT)
GO

SELECT * FROM Modulo
GO

CREATE TABLE Permisos(
modulo_id INT NOT NULL,
perfil_id INT NOT NULL,
permisos_feccre DATE NOT NULL,
permisos_estado INT NOT NULL
)
GO
--CONSTRAINT
ALTER TABLE Permisos
	ADD FOREIGN KEY (modulo_id) REFERENCES Modulo(modulo_id),
	FOREIGN KEY(perfil_id) REFERENCES Perfil(perfil_id),
	PRIMARY KEY(modulo_id,perfil_id),
	DEFAULT 1 FOR permisos_estado
GO
INSERT INTO Permisos VALUES (100,100,GETDATE(),DEFAULT)
GO
INSERT INTO Permisos VALUES (100,101,GETDATE(),DEFAULT)
GO

SELECT * FROM Permisos
GO

CREATE TABLE Categoria(
categoria_id INT NOT NULL IDENTITY(100,1),
categoria_padre_id INT NULL,
categoria_nombre varchar(20) NOT NULL
)
GO
--CONSTRAINT
ALTER TABLE Categoria
	ADD PRIMARY KEY (categoria_id),
	FOREIGN KEY (categoria_padre_id) REFERENCES Categoria(categoria_id)
GO
--DATOS
INSERT INTO Categoria VALUES (null,'Confiteria')
INSERT INTO Categoria VALUES(100,'Galletas')
INSERT INTO Categoria VALUES(100,'Chicles')
INSERT INTO Categoria VALUES(100,'Chocolate')
INSERT INTO Categoria VALUES(100,'Chupetes')
GO

SELECT * FROM Categoria
GO

CREATE TABLE Producto(
producto_id INT NOT NULL IDENTITY(100,1),
producto_sku VARCHAR(13),
producto_nombre VARCHAR(60) NOT NULL,
producto_descripcion VARCHAR(40) ,
producto_stock DECIMAL(5,2),
producto_categoria INT NOT NULL,
producto_precio DECIMAL(7,2),
producto_marca INT NOT NULL,
producto_umedidad CHAR(2) NOT NULL,
producto_estado INT NOT NULL
)
GO

--CONSTRAINT PRODUCTO
ALTER TABLE Producto 
	ADD PRIMARY KEY (producto_id),
	CONSTRAINT fk_producto_categoria_id FOREIGN KEY(producto_categoria) REFERENCES Categoria(categoria_id),
	CONSTRAINT ch_producto_umedidad CHECK (producto_umedidad IN ('KG','UN')),
	DEFAULT 1 FOR producto_estado
GO
--DATOS
INSERT INTO Producto VALUES ('9293203','FOUR PACK GALLETA MENTA BLACK OUTX240GR','ENVASE 240 G',10,101,3.5,1,'UN',DEFAULT)
INSERT INTO Producto VALUES ('4234234','GALLETA GLACITAS CON COBERTURA SABOR TOFFEE','PAQUETE 6 UN',10,101,3.4,1,'UN',DEFAULT)
INSERT INTO Producto VALUES ('5234234','GOMA DE MASCAR SABOR FRUTAS COOL BUBBLE 18S','UNIDAD X1 30.6GR',20,102,3.5,1,'UN',DEFAULT)
INSERT INTO Producto VALUES ('5233435','BOMBON CHOCOLATE BITTER MANI','DOYPACK 136 GR',36,103,10.7,1,'UN',DEFAULT)
GO

SELECT * FROM Producto
GO



/*
CREATE TABLE Cliente(
persona_id INT not null,
cliente_email VARCHAR(40),
cliente_contrasena VARCHAR(64),
cliente_feccre DATE,
)
GO
CREATE TABLE Persona_Direccion(
dircli_id INT NOT NULL,
dircli_distrito INT NOT NULL,
dircli_direccion varchar (200) NOT NULL,
dircli_numero varchar(6) NOT NULL,
dircli_dptcsa varchar(10),
--idpersona fk
persona_id INT NOT NULL
)
GO
*/
SELECT * FROM Categoria
SELECT * FROM Empleado
SELECT * FROM Empleado_Perfil_Detalle
SELECT * FROM Modulo
SELECT * FROM Perfil
SELECT * FROM Permisos
SELECT * FROM Producto
SELECT * FROM TipoDoc
GO

CREATE PROCEDURE USP_Validacion_Login
	@emp_usuario varchar(40),
	@emp_contrasena char(64)
AS
	SELECT * FROM Empleado 
		WHERE empleado_usuario = @emp_usuario 
			and empleado_contrasena = @emp_contrasena
GO
/*Para ejemplo inserto un usuario desactivado*/
SELECT * FROM Empleado
GO
INSERT INTO Empleado VALUES('Elie','Stark','1998-02-14',100,20127829,GETDATE(),983734233,'elie.stark','cf2992442d49a98927daa284a8a1b8d8f314eba132a1d3726ed0e965fd430d42',0)
GO
EXEC USP_Validacion_Login 'Renzo.Peralta','408c81d0bdb66ee4e5543576d8914e5cc7a7a885a4b624873b979c084a37c8ea0'
GO
EXEC USP_Validacion_Login 'elie.stark','cf2992442d49a98927daa284a8a1b8d8f314eba132a1d3726ed0e965fd430d42'
GO

SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_TYPE = 'PROCEDURE'
	ORDER BY ROUTINE_NAME
GO
SELECT * FROM INFORMATION_SCHEMA.ROUTINES
GO

select * from sys.procedures
GO



