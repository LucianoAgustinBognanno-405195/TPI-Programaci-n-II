create database CecchettoLogica
go
use CecchettoLogica
go

create table Tipos_Vehiculos(
Id_Tipo_Vehiculo int identity (1,1),
Vehiculo varchar (50),
constraint PK_Vehiculo primary key (Id_Tipo_Vehiculo)
)
create table Tipos_Baterias (
Id_Tipo_Bateria int identity (1,1),
Tipos varchar (50),
Constraint PK_Tipo_Bateria primary key (Id_Tipo_Bateria)
)
create table Ciudades (
Id_Ciudad int identity (1,1),
Ciudad varchar (50),
Constraint PK_Ciudad Primary key (Id_Ciudad)
)
Create table Tipos_Contactos(
Id_Tipo_Contacto int identity (1,1),
Tipo_Contacto varchar (50),
constraint Pk_Tipos_contacto primary key (Id_Tipo_Contacto)
)
Create table Tamanios_Bateria(
Id_Tamanio int identity (1,1),
Dimensiones varchar (50),
Grupo varchar (50),
constraint Pk_Tamanio primary key (Id_Tamanio)
)
Create table Tipos_Empleados(
Id_Tipo_Empleado int identity (1,1),
Tipo_Empleado varchar (50),
constraint Pk_Tipos_Empleados primary key (Id_Tipo_Empleado)
)
create table Tipos_Facturas (
Id_Tipo_Factura int identity (1,1),
Tipo_Factura varchar (50),
Constraint PK_Tipo_Factura primary key (Id_Tipo_Factura)
)
create table Voltajes (
Id_Voltaje int identity (1,1),
Voltaje Varchar (50),
constraint PK_Voltaje primary key (Id_Voltaje)
)
Create table Marcas (
Id_Marca int identity(1,1),
Descripcion varchar (50), 
constraint Pk_marca primary key (id_marca)
)
Create table Barrios(
Id_Barrio int identity (1,1),
Barrio varchar (50),
Id_Ciudad int,
constraint Pk_Barrio primary key (Id_Barrio),
constraint Fk_Ciudad Foreign Key (Id_Ciudad) references Ciudades(Id_Ciudad)
)
create table Direcciones (
Id_Direccion int identity (1,1),
Calle varchar (50),
Altura int,
Id_Barrio int,
constraint Pk_Direccion primary key (Id_Direccion),
constraint Fk_Barrio foreign key (Id_Barrio) references Barrios (Id_Barrio)
)
create table Tipos_Clientes(
Id_Tipo_Cliente int identity(1,1),
Tipo varchar(50),
constraint PK_Tipo_Cliente primary key (Id_Tipo_Cliente)
)
create table Vehiculos (
Id_Vehiculo int identity (1,1),
Descripcion varchar (50),
Id_Tipo_Vehiculo int,
constraint PK_Id_Vehiculo primary key (Id_Vehiculo),
constraint Fk_Id_Vehiculo foreign key (Id_Tipo_Vehiculo) references Tipos_Vehiculos (Id_Tipo_Vehiculo)
)
Create table Estados_Servicio (
Id_Estado int identity (1,1),
Estado varchar (50) not null,
constraint Pk_Estado_Servicio primary key (Id_Estado)
)
create table Cuotas(
Id_Cuota int identity (1,1),
Cuota int,
Recargo int,
constraint PK_Cuota primary Key (Id_Cuota) 
)
create table Tipos_Servicios(
Id_Tipo_Servicio int Identity,
Descripcion Varchar(50),
constraint PK_Tipos_Servicios primary key(Id_Tipo_Servicio)
)
create table Tipos_Documentos(
Id_Tipo_Documento int identity (1,1),
Tipos varchar (50),
constraint PK_Tipo_Documento primary key (Id_Tipo_Documento)
)

create table Documentos(
Id_Documento int identity (1,1),
Id_Tipo_Documento int,
Documento int not null,
constraint PK_Id_Documento primary key (Id_Documento),
constraint FK_Id_Tipo_Documento foreign key (Id_Tipo_Documento) references Tipos_Documentos (Id_Tipo_Documento)
)
create table Clientes(
Id_Cliente int identity (1,1),
Nombre varchar (50),
Apellido varchar (50),
Id_Documento int,
Id_Direccion int,
Id_Tipo_Cliente int,
constraint Pk_Cliente primary key (Id_Cliente),
constraint Fk_Id_Direccion foreign key (Id_Direccion) references Direcciones (Id_Direccion),
constraint Fk_Tipo_Cliente foreign key (Id_Tipo_Cliente) references Tipos_Clientes (Id_Tipo_Cliente),
constraint FK_Id_Documento foreign key (Id_Documento) references Documentos (Id_Documento)
)
Create table Sucursales(
Id_Sucursal int identity (1,1),
Sucursal varchar (50),
Id_Direccion int,
Id_Vehiculo int,
constraint Pk_Sucursales primary key (Id_Sucursal),
constraint FK_Direccion Foreign Key (Id_Direccion) references Direcciones(Id_Direccion),
constraint FK_vehiculo_Sucursal Foreign Key (Id_vehiculo) references Vehiculos(Id_Vehiculo)
)
Create table Empleados(
Id_Empleado int identity (1,1),
Nombre varchar (50) not null,
Apellido varchar (50) not null, 
Id_Documento int,
Id_Direccion int,
Id_Sucursal int,
Id_tipo_Empleado int,
usuario varchar (30),
contrasenia varchar (50),
Activo bit not null,
Constraint Pk_Empleados primary key (Id_Empleado),
Constraint Fk_Empleados_Direcciones foreign key (Id_Direccion) references Direcciones (Id_Direccion),
Constraint Fk_Empleados_Sucursales foreign key (Id_Sucursal) references Sucursales(Id_Sucursal),
Constraint Fk_Tipo_Empleado foreign key (Id_Tipo_Empleado) references Tipos_Empleados (Id_Tipo_Empleado),
Constraint Fk_Dni foreign key (Id_Documento) references Documentos (Id_Documento)
)
create table Servicios(
Id_Servicio int Identity (1,1),
Fecha_Servicio date,
Id_Cliente int,
Id_Empleado int,
Id_Sucursal int,
Descripcion Varchar(50),
Id_Tipo_Servicio int,
Estado_Servicio int,
constraint PK_Servicios primary key (Id_Servicio),
constraint FK_Servicios_Tipo foreign key (Id_Tipo_Servicio) references Tipos_Servicios(Id_Tipo_Servicio),
constraint FK_Estado_Servicio foreign key (Estado_Servicio) references Estados_Servicio(Id_Estado),
Constraint Fk_Servicios_Clientes foreign key (Id_Cliente) references Clientes (Id_Cliente),
Constraint Fk_Servicio_Empleados foreign key (Id_Empleado) references Empleados (Id_Empleado),
Constraint Fk_Servicio_Sucursales foreign key (Id_Sucursal) references Sucursales (Id_Sucursal)
)
create table Contactos(
Id_Contacto int identity (1,1),
Id_Tipo_Contacto int,
Id_Cliente int,
Id_Empleado int,
constraint PK_Contacto primary key (Id_Contacto),
constraint Fk_Tipo_Contacto foreign key (Id_Tipo_Contacto) references Tipos_Contactos (Id_Tipo_Contacto),
constraint Fk_Cliente foreign key (Id_Cliente) references Clientes (Id_Cliente),
constraint Fk_empleado foreign key (Id_Empleado) references Empleados (Id_Empleado)
)
Create table Medios_pagos(
Id_Medio_Pago int identity (1,1),
Medio Varchar(50),
Constraint PK_Medio_Pago primary key (Id_Medio_Pago)
)
Create table Movimientos(
Id_Movimiento int identity (1,1),
Movimiento Varchar (50),
constraint Pk_Moviemieto primary key (Id_Movimiento)
)
Create table Facturas(
Id_Factura int identity (1,1),
Id_Cliente int, 
Id_Tipo_Factura int,
Fecha date,
Constraint Pk_Facturas primary key (Id_Factura),
Constraint Fk_Facturas_Clientes foreign key (Id_Cliente) references Clientes(Id_Cliente),
Constraint Fk_Facturas_TiposFactura foreign key (Id_Tipo_Factura) references Tipos_Facturas(Id_Tipo_Factura)
)
create table Baterias(
Id_Bateria int identity,
Id_Tipo_Bateria int,
Id_Voltaje int,
Id_Marca int,
Id_Tamanio_Bateria int,
constraint PK_Baterias primary key (Id_Bateria),
constraint FK_Baterias_Tipo foreign key (Id_Tipo_Bateria) references Tipos_Baterias(Id_Tipo_Bateria),
constraint FK_Baterias_Voltaje foreign key (Id_Voltaje) references Voltajes(Id_Voltaje),
constraint FK_Baterias_Marca foreign key (Id_Marca) references Marcas(Id_Marca),
constraint FK_Baterias_Tamanio foreign key (Id_Tamanio_Bateria) references Tamanios_Bateria(Id_Tamanio)
)
create table Stocks(
Id_Stock int identity,
Id_Bateria int,
Id_Movimiento int,
Id_Sucursal int,
Cantidad int,
constraint PK_Stocks primary key (Id_Stock),
constraint FK_Stocks_Bateria foreign key (Id_Bateria) references Baterias(Id_Bateria),
constraint FK_Stocks_Movimiento foreign key (Id_Movimiento) references Movimientos(Id_Movimiento),
constraint FK_Stocks_Sucursal foreign key (Id_Sucursal) references Sucursales(Id_Sucursal)
)
Create table Detalles_Servicio (
Id_Detalle_Servicio int identity (1,1),
Id_Bateria int,
Id_Servicio int,
Id_Vehiculo int,
Constraint Pk_Detalles_Servicio primary key (Id_Detalle_Servicio),
Constraint Fk_Detalles_Pedidos_Baterias foreign key (Id_Bateria) references Baterias (Id_Bateria),
constraint FK_Servicios_Vehiculo foreign key (Id_Vehiculo) references Vehiculos(Id_Vehiculo),
Constraint Fk_Detalles_Pedidos_Servicios foreign key (Id_Servicio) references Servicios (Id_Servicio)
)
Create table Detalles_Facturas(
Id_Detalle_Factura int identity (1,1),
Id_Servicio int,
Id_Factura int,
Pre_Unitario int,
Cantidad decimal,
Subtotal decimal,
constraint Pk_Detalle_Factura primary key (Id_Detalle_Factura),
constraint FK_Factura Foreign Key (Id_Factura) references Facturas(Id_Factura),
constraint FK_Servicio Foreign Key (Id_Servicio) references Servicios(Id_Servicio)
)
Create table Facturas_X_Medios(
Id_Factura_Medio int identity (1,1),
Id_Medio_Pago int,
Id_Cuota int,
Id_Detalle_Factura int,
Importe decimal(10,2),
constraint Pk_Facturas_X_Medios primary key (Id_Factura_Medio),
constraint Fk_Medio_Pago Foreign Key (Id_Medio_Pago)
references Medios_Pagos(Id_Medio_Pago),
constraint Fk_Cuota Foreign Key (Id_Cuota)
references Cuotas(Id_Cuota),
constraint Fk_Detalle_Factura Foreign Key (Id_Detalle_Factura)
references Detalles_Facturas(Id_Detalle_Factura)
)
create table vehiculos_sucursales(
Id_vehiculo_sucursal int identity (1,1),
id_vehiculo int,
id_sucursal int,
constraint PK_vehiculo_sucursal primary key (Id_vehiculo_sucursal),
constraint fk_id_vehiculo_sucursal foreign key (id_vehiculo) references Vehiculos (id_vehiculo),
constraint fk_id_sucursal foreign key (id_sucursal) references Sucursales (id_sucursal)
)
go