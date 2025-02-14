-- Script SQL para crear las tablas Dispositivos y Eventos
CREATE SCHEMA IoT;
CREATE TABLE IoT.Devices (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    [Type] NVARCHAR(50)
);

CREATE TABLE IoT.Events (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DeviceId INT,
    CreationDATE DATETIME,
    Description NVARCHAR(255),
    FOREIGN KEY (DeviceId) REFERENCES Devices(Id)
);

-- Consulta para obtener la cantidad de eventos por dispositivo
SELECT 
    d.Id, 
    d.Name, 
    COUNT(e.Id) AS QtyEvent
FROM IoT.Devices d WITH(NoLock)
LEFT JOIN IoT.Events e WITH(NoLock) 
	ON d.DeviceId = e.Id
GROUP BY d.Id, d.Name;