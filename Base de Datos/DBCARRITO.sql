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

CREATE TABLE UNIDAD(
IdUnidad int primary key identity,
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
IdUnidad int references Unidad(IdUnidad),
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
)go



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



/* ---------- PROCEDIMIENTOS PARA CATEGORIA -----------------*/

create PROC SP_RegistrarCategoria(
@Descripcion varchar(100),
@Activo bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
	begin
		insert into CATEGORIA(Descripcion,Activo) values (@Descripcion,@Activo)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'La categoría ya existe'

end
go




Create procedure SP_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(100),
@Activo bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion =@Descripcion and IdCategoria != @IdCategoria)
		update top (1) CATEGORIA set
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdCategoria = @IdCategoria
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La categoría ya existe'
	end
end
go


create procedure SP_EliminarCategoria(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from CATEGORIA c
	 inner join PRODUCTO p on p.IdCategoria = c.IdCategoria
	 where c.IdCategoria = @IdCategoria
	)
	begin
	 delete top (1) from CATEGORIA where IdCategoria = @IdCategoria
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La categoria se encuentara relacionada a un producto'
	end
end
GO


/* ---------- PROCEDIMIENTOS PARA MARCA -----------------*/

create PROC SP_RegistrarMarca(
@Descripcion varchar(100),
@Activo bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM MARCA WHERE Descripcion = @Descripcion)
	begin
		insert into MARCA(Descripcion,Activo) values (@Descripcion,@Activo)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'La marca ya existe'

end
go




Create procedure SP_EditarMarca(
@IdMarca int,
@Descripcion varchar(100),
@Activo bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM MARCA WHERE Descripcion =@Descripcion and IdMarca != @IdMarca)
		update top (1) MARCA set
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdMarca = @IdMarca
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La marca ya existe'
	end
end
go


create procedure SP_EliminarMarca(
@IdMarca int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from MARCA m
	 inner join PRODUCTO p on p.IdCategoria = m.IdMarca
	 where m.IdMarca = @IdMarca
	)
	begin
	 delete top (1) from MARCA where IdMarca = @IdMarca
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La marca se encuentara relacionada a un producto'
	end
end
GO



/* ---------- PROCEDIMIENTOS PARA UNIDAD DE MEDIDA -----------------*/

create PROC SP_RegistrarUnidad(
@Descripcion varchar(100),
@Activo bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM UNIDAD WHERE Descripcion = @Descripcion)
	begin
		insert into UNIDAD(Descripcion,Activo) values (@Descripcion,@Activo)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'La unidad de medida ya existe'

end
go




Create procedure SP_EditarUnidad(
@IdUnidad int,
@Descripcion varchar(100),
@Activo bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM UNIDAD WHERE Descripcion =@Descripcion and IdUnidad != @IdUnidad)
		update top (1) UNIDAD set
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdUnidad = @IdUnidad
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La unidad de medida ya existe'
	end
end
go


create procedure SP_EliminarUnidad(
@IdUnidad int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from UNIDAD u
	 inner join PRODUCTO p on p.IdUnidad = u.IdUnidad
	 where u.IdUnidad = @IdUnidad
	)
	begin
	 delete top (1) from UNIDAD where IdUnidad = @IdUnidad
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La unidad de medida se encuentara relacionada a un producto'
	end
end
GO







/* REGISTRAR PRODUCTO*/
create proc SP_RegistrarProducto(
@Nombre varchar(500),
@Descripcion varchar(500),
@IdMarca int,
@IdCategoria int,
@IdUnidad int,
@Precio decimal(10,2),
@Stock int,
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre = @Nombre)
	begin
		insert into PRODUCTO(Nombre,Descripcion,IdMarca,IdCategoria,IdUnidad,Precio,Stock,Activo) values (
		@Nombre,@Descripcion,@IdMarca,@IdCategoria,@IdUnidad, @Precio,@Stock,@Activo)

		SET @Resultado = scope_identity()
	end
	else
	set @Mensaje = 'El producto ya existe'
end
go


/* EDITAR PRODUCTO */
create proc sp_EditarProducto(
@IdProducto int,
@Nombre varchar(500),
@Descripcion varchar(500),
@IdMarca varchar(100),
@IdCategoria varchar(100),
@IdUnidad varchar(100),
@Precio decimal(10,2),
@Stock int,
@Activo bit,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre = @Nombre and IdProducto != @IdProducto)
	begin
		update PRODUCTO set 
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdMarca = @IdMarca,
		IdCategoria = @IdCategoria,
		IdUnidad = @IdUnidad,
		Precio =@Precio ,
		Stock =@Stock ,
		Activo = @Activo 
		where IdProducto = @IdProducto

		SET @Resultado = 1
	end
	else
	set @Mensaje = 'El producto ya existe'
end
go


/* ELIMINAR PRODUCTO */
create PROC SP_EliminarProducto(
@IdProducto int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	set @Resultado = 0

	IF EXISTS (SELECT * FROM DETALLE_VENTA dv
	INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
	WHERE p.IdProducto = @IdProducto)
	set @Mensaje = 'El producto se encuentra relacionado a una venta'
	else
	begin

		delete top(1) from PRODUCTO where IdProducto = @IdProducto
		set @Resultado = 1 
	end
end
go


/* LISTAR PRODUCTO*/
create proc SP_ListarProducto
as
begin
select p.IdProducto, p.Nombre, p.Descripcion,
m.IdMarca, m.Descripcion[DesMarca],
c.IdCategoria, c.Descripcion[DesCategoria],
u.IdUnidad, u.Descripcion[DesUnidad],
p.Precio, p.Stock,p.RutaImagen,p.NombreImagen,p.Activo
from PRODUCTO p
inner join MARCA m on m.IdMarca = p.IdMarca
inner join CATEGORIA c on c.IdCategoria = p.IdCategoria
inner join UNIDAD u on u.IdUnidad = p.IdUnidad


end

go



/* Dashboard*/
create PROC SP_ReporteDashboard
as
begin
select 
(select count(*) from CLIENTE) [TotalCliente],
(select isnull(sum(cantidad),0) from DETALLE_VENTA) [TotalVenta],
(select count(*) from PRODUCTO) [TotalProducto]
end
go


/* Reporte de Ventas */
create PROC SP_ReporteVentas(
@fechainicio varchar(10),
@fechafin varchar(10),
@idtransaccion varchar(50)
)
as
begin
set dateformat dmy;
select CONVERT( char(10), v.FechaVenta,103)[FechaVenta], CONCAT(c.Nombres,' ', c.Apellidos)[Cliente], p.Nombre[Producto], p.Precio, dv.Cantidad, dv.Total, v.IdTransaccion from DETALLE_VENTA dv
inner join PRODUCTO p on p.IdProducto = dv.IdProducto
inner join VENTA v on v.IdVenta = dv.IdVenta
inner join CLIENTE c on c.IdCliente = v.IdCliente
where convert(date, v.FechaVenta) between @fechainicio and @fechafin
and v.IdTransaccion = iif(@idtransaccion = '',v.IdTransaccion,@idtransaccion)
end
go



/* Registro de clientes */
create proc SP_RegistrarCliente(
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Mensaje varchar (500) output,
@Resultado int output
)
as 
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Correo = @Correo)
	begin
		insert into CLIENTE(Nombres,Apellidos,Correo,Clave,Reestablecer) values
		(@Nombres,@Apellidos,@Correo,@Clave,0)

		SET @Resultado = SCOPE_IDENTITY()
		end
		else
		 set @Mensaje = 'El correo del usuario ya existe'
end
go



/* Existe Carrito */
create proc SP_ExisteCarrito(
@IdCliente int,
@IdProducto int,
@Resultado bit output
)
as
begin
	if exists(select * from CARRITO where IdCliente = @IdCliente and IdProducto = @IdProducto)
		set @Resultado = 1
	else
		set @Resultado = 0
end
go


/* Operación  Carrito */
create proc SP_OperacionCarrito(
@IdCliente int,
@IdProducto int,
@Sumar bit,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	set @Resultado = 1
	set @Mensaje = ''


	declare @existecarrito bit = iif(exists(select * from CARRITO where IdCliente = @IdCliente and IdProducto = @IdProducto),1,0)
	declare @strockproducto int = (select stock from PRODUCTO where IdProducto  = @IdProducto)

	BEGIN TRY
		
		BEGIN TRANSACTION OPERACION

		if(@Sumar = 1)
		begin

			if(@strockproducto > 0)
			begin

				if(@existecarrito = 1)
					update CARRITO set Cantidad = Cantidad + 1 where IdCliente = @IdCliente and IdProducto = @IdProducto
				else
					insert into CARRITO(IdCliente, IdProducto, Cantidad) Values (@IdCliente,@IdProducto,1)


				update PRODUCTO set Stock = Stock - 1 where IdProducto = @IdProducto
			end
			else
			begin
				set @Resultado = 0
				set @Mensaje = 'El producto no cuenta con stock disponible'
			end


		end
		else
		begin
			update CARRITO set Cantidad = Cantidad - 1 where IdCliente = @IdCliente and IdProducto = @IdProducto 
			update PRODUCTO set Stock = Stock + 1 where IdProducto = @IdProducto
			
		end
		
		COMMIT TRANSACTION OPERACION
		
	END TRY
	BEGIN CATCH
		set @Resultado = 0
		set @Mensaje =  ERROR_MESSAGE()
		ROLLBACK TRANSACTION OPERACION
	END CATCH 
end
go

/* Obtener Lista de producto Cliente en carrito*/
create proc fn_obtenerCarritoCliente(
@idcliente int
)
returns table 
as
return(
	select p.IdProducto, m.Descripcion[DesMarca],p.Nombre,p.Precio,c.Cantidad,p.RutaImagen,p.NombreImagen
	from CARRITO	c
	inner join PRODUCTO p on p.IdProducto = c.IdProducto
	inner join MARCA m on m.IdMarca = p.IdMarca
	where c.IdCliente = @idcliente
)
go

select * from fn_obtenerCarritoCliente(2)


/* Eliminar producto del carrito carrito*/
create proc SP_EliminarCarrito(
@IdCliente int,
@IdProducto int,
@Resultado bit output
)
as
begin

	set @Resultado = 1
	declare @cantidadproducto int = (select Cantidad from CARRITO where IdCliente = @IdCliente and IdProducto = @IdProducto)

	BEGIN TRY

		BEGIN TRANSACTION OPERACION

		update PRODUCTO set Stock = stock + @cantidadproducto where IdProducto = @IdProducto
		delete top(1) from CARRITO where IdCliente = @IdCliente and IdProducto = @IdProducto

		COMMIT TRANSACTION OPERACION

	END TRY
	BEGIN CATCH
		set @Resultado = 0
		ROLLBACK TRANSACTION OPERACION
	END CATCH
end
go
 

/* Estructura para almacenar data de producto*/
 CREATE TYPE [dbo].[EDetalle_Venta] AS TABLE (
	[IdProducto] int NULL,
	[Cantidad] int NULL,
	[Total] decimal(18,2) NULL
 )
 go


 /* Procedimiento para crear una venta */
create procedure usp_RegistrarVenta(
@IdCliente int,
@TotalProducto int,
@MontoTotal decimal(18,2),
@Contacto varchar(100),
@IdDistrito varchar(6),
@Telefono varchar(10),
@Direccion varchar(100),
@IdTransaccion varchar(50),
@DetalleVenta [EDetalle_Venta] READONLY, 
@Resultado bit output,
@Mensaje varchar(500) output                             
)
as
begin
	
	begin try

		declare @idventa int = 0
		set @Resultado = 1
		set @Mensaje = ''

		begin  transaction registro

		insert into VENTA(IdCliente,TotalProducto,MontoTotal,Contacto,IdDistrito,Telefono,Direccion,IdTransaccion)
		values(@IdCliente,@TotalProducto,@MontoTotal,@Contacto,@IdDistrito,@Telefono,@Direccion,@IdTransaccion)

		set @idventa = SCOPE_IDENTITY()

		insert into DETALLE_VENTA(IdVenta,IdProducto,Cantidad,Total)
		select @idventa,IdProducto,Cantidad,Total from @DetalleVenta

		DELETE FROM CARRITO where IdCliente = @IdCliente

		commit transaction registro

	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction registro
	end catch

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


/* INSERT UNIDAD */
select * from UNIDAD
insert into UNIDAD(Descripcion) values
('Unidad'),
('Libra'),
('Quintal'),
('Litro')

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
insert into DISTRITO(IdDistrito, Descripcion, IdProvincia, IdDepartamento) values
('1452','El Durazno','1201','01')


select * from departamento

select * from PROVINCIA where IdDepartamento = @iddepartamento

select * from DISTRITO where IdProvincia = 1201 and IdDepartamento = @iddepartamento