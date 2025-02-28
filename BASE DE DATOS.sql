create database PARQUEOSDB 
go
use PARQUEOSDB 
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre varchar(100) NOT NULL,
    Correo VARCHAR(255) UNIQUE NOT NULL,
    Telefono VARCHAR(15) NOT NULL,
    Contrasena VARCHAR(255) NOT NULL,
    Rol VARCHAR(15) NOT NULL
);

CREATE TABLE Sucursales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Direccion VARCHAR(255) NOT NULL,
    Telefono VARCHAR(15) NOT NULL,
    AdministradorId INT NOT NULL,
    EspaciosTotales INT NOT NULL,
    CONSTRAINT FK_Sucursal_Administrador FOREIGN KEY (AdministradorId) REFERENCES Usuarios(Id) 
        ON DELETE NO ACTION ON UPDATE CASCADE
);
CREATE TABLE EspacioParqueos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SucursalId INT NOT NULL,
    Numero INT NOT NULL,
    Ubicacion VARCHAR(100) NOT NULL,
    CostoPorHora DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(10) NOT NULL,
    CONSTRAINT FK_EspacioParqueo_Sucursal FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id) 
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE Reservas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    EspacioParqueoId INT NOT NULL,
    FechaHoraInicio DATETIME NOT NULL,
    HorasReservadas INT NOT NULL ,
    Estado VARCHAR(10) NOT NULL,
    CONSTRAINT FK_Reserva_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) 
        ON DELETE NO ACTION ON UPDATE CASCADE,
    CONSTRAINT FK_Reserva_EspacioParqueo FOREIGN KEY (EspacioParqueoId) REFERENCES EspacioParqueos(Id) 
        ON DELETE NO ACTION ON UPDATE NO ACTION
);


INSERT INTO [dbo].[Usuarios] ([Nombre], [Correo], [Telefono], [Contrasena], [Rol])
VALUES 
('Juan Perez', 'juan.perez@example.com', '1234567890', 'contrasenaSegura123', 'Cliente'),
('Maria Gomez', 'maria.gomez@example.com', '0987654321', 'contrasenaSegura456', 'Empleado')
GO