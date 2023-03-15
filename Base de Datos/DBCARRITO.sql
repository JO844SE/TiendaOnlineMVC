create database DBCARRITO
GO

USE DBCARRITO
GO

CREATE TABLE CATEGORIA(
IdCategoria int primary key identity,
Descripcion varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)
GO

CREATE TABLE MARCA(
IdMarca int primary key identity,
Descripcion varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)
GO

CREATE TABLE PRODUCTO(
IdProducto int primary key identity,
Nombre varchar (500),
Descripcion varchar (500),
IdMarca int references Marca(IdMarca),
IdCategoria int references Categoria(IdCategoria),
Precio decimal (10,2) default 0,
Stock int,
RutaImagen varchar(100),
NombreImagen varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)
go

CREATE TABLE CLIENTE(
IdCliente int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(150),
Reestablecer bit default 0,
FechaRegistro datetime default getdate()
)


CREATE TABLE CARRITO(
IdCarrito int primary key identity,
IdCliente int references Cliente(IdCliente),
IdProducto int references Producto(IdProducto),
Cantidad int
)
go


CREATE TABLE VENTA(
IdVenta int primary key identity,
IdCliente int references Cliente(IdCliente),
TotalProducto int,
MontoTotal decimal(10,2),
Contacto varchar(50),
IdDistrito varchar(10),
Telefono varchar(50),
Direccion varchar(500),
IdTransaccion varchar(50),
FechaVenta datetime default getdate()
)
go

CREATE TABLE DETALLE_VENTA(
IdDetalleVenta int primary key identity,
IdVenta int references Venta(IdVenta),
IdProducto int references Producto(IdProducto),
Cantidad int,
Total decimal (10,2)
)
go


CREATE TABLE USUARIO(
IdUsuario int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(150),
Reestablecer bit default 1,
Activo bit default 1,
FechaRegistro  datetime default getdate()
)
go

CREATE TABLE DEPARTAMENTO(
IdDepartamento varchar(2) not null,
Descripcion varchar(50) not null
)
go

CREATE TABLE PROVINCIA(
IdProvincia varchar(4) not null,
Descripcion varchar(45) not null,
IdDepartamento varchar(2) not null
)
go

CREATE TABLE DISTRITO(
IdDistrito varchar(6) not null,
Descripcion varchar(50) not null,
IdProvincia varchar(4) not null,
IdDepartamento varchar (2) not null
)



CREATE PROC SP_RegistrarUsuario(
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	set @Resultado = 0
	if not exists(select * from USUARIO where Correo = @Correo)
	begin
		insert into USUARIO(Nombres, Apellidos, Correo, Clave, Activo) VALUES
		(@Nombres, @Apellidos, @Correo, @Clave, @Activo)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end
go




CREATE PROC SP_EditarUsuario(
@IdUsuario int,
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	set @Resultado = 0
	
	if not exists(select * from USUARIO where Correo = @Correo and Idusuario != @IdUsuario)
	begin
		update top (1) USUARIO set
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Activo = @Activo
		where IdUsuario = @IdUsuario 

		set @Resultado = 1
	end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end
go





/* INSERT USUARIO */
select * from usuario
insert into usuario(Nombres,Apellidos,Correo,Clave) values ('Erik José','Pérez López','example@gmail.com','ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae')

/* INSERT CATEGORIA */
select * from categoria
insert into categoria(Descripcion) values
('Tecnologia'),
('Muebles'),
('Dormitorio'),
('Deportes')

/* INSERT MARCA */
select * from marca
insert into marca (Descripcion) values
('SONYTE'),
('LGTE'),
('CANON'),
('SAMSUNG')

/* INSERT DEPARTAMENTO */
select * from departamento
insert into departamento (IdDepartamento, Descripcion) values
('01','San Marcos'),
('02','Quetzaltenago')

/* INSERT PROVINCIA */
select * from provincia 
insert into provincia (IdProvincia, Descripcion, IdDepartamento) values
('1201','San Marcos','01')

/* INSERT DISTRITO */
select * from distrito
insert into distrito(IdDistrito, Descripcion, IdProvincia, IdDepartamento) values
('031452','Las Manzanas','1201','01')

