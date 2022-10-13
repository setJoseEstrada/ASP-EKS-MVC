create database EKS

create table usuarios(
id int identity (1,1) primary key, 
correo varchar (100),
contra varchar (100),
nombre varchar (100))

create table productos (
id int identity(1,1) primary key,
nombre varchar (50), 
costo int ,
idMarca int foreign key references marca)

create table marca(
id int identity (1,1) primary key, 
nombre varchar (50))

select * from marca