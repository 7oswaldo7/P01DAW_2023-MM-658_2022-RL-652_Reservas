create database Reservas
go
use Reservas
CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(255) UNIQUE NOT NULL,
    Telefono NVARCHAR(15) NOT NULL,
    Contrasena NVARCHAR(255) NOT NULL,
    Rol NVARCHAR(15) CHECK (Rol IN ('Cliente', 'Empleado', 'Administrador')) NOT NULL
);

CREATE TABLE Sucursal (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Direccion NVARCHAR(255) NOT NULL,
    Telefono NVARCHAR(15) NOT NULL,
    AdministradorId INT NOT NULL,
    EspaciosTotales INT NOT NULL CHECK (EspaciosTotales >= 0),
    CONSTRAINT FK_Sucursal_Administrador FOREIGN KEY (AdministradorId) REFERENCES Usuario(Id) 
        ON DELETE NO ACTION ON UPDATE CASCADE
);
CREATE TABLE EspacioParqueo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SucursalId INT NOT NULL,
    Numero INT NOT NULL,
    Ubicacion NVARCHAR(100) NOT NULL,
    CostoPorHora DECIMAL(10,2) NOT NULL CHECK (CostoPorHora >= 0),
    Estado NVARCHAR(10) CHECK (Estado IN ('Disponible', 'Ocupado', 'Reservado')) NOT NULL,
    CONSTRAINT FK_EspacioParqueo_Sucursal FOREIGN KEY (SucursalId) REFERENCES Sucursal(Id) 
        ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE TABLE Reserva (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    EspacioParqueoId INT NOT NULL,
    FechaHoraInicio DATETIME NOT NULL,
    HorasReservadas INT NOT NULL CHECK (HorasReservadas > 0),
    Estado NVARCHAR(10) CHECK (Estado IN ('Activa', 'Cancelada', 'Finalizada')) NOT NULL,
    CONSTRAINT FK_Reserva_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id) 
        ON DELETE NO ACTION ON UPDATE CASCADE,
    CONSTRAINT FK_Reserva_EspacioParqueo FOREIGN KEY (EspacioParqueoId) REFERENCES EspacioParqueo(Id) 
        ON DELETE NO ACTION ON UPDATE NO ACTION
);
