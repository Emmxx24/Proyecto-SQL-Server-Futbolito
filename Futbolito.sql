CREATE DATABASE Futbolito;
GO

USE Futbolito;
GO

/*Esquema para Participante, Jugador y Arbitro*/
CREATE SCHEMA Persona;
GO

/*Esquema para Torneo, Jornada, Lugar y DetalleTorneo*/
CREATE SCHEMA Juego;
GO

/*Esquema para Partido, ResultadoPartido, Gol y Tarjeta*/
CREATE SCHEMA Evento;
GO

/*Esquema para Equipo y DetalleEquipo*/
CREATE SCHEMA Club;
GO

insert into club.equipo

CREATE TABLE Persona.Participante 
(
	IdParticipante BIGINT IDENTITY(1,1) NOT NULL,
	NombreParticipante VARCHAR(50) NOT NULL,
	Genero VARCHAR(10) NOT NULL,
	Telefono VARCHAR(10) NOT NULL,
	CorreoElectronico VARCHAR(50) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	Edad INT,

	CONSTRAINT PK_PARTICIPANTE PRIMARY KEY (IdParticipante),
	CONSTRAINT UQ_PARTICIPANTE_TELEFONO UNIQUE(Telefono),
	CONSTRAINT UQ_PARTICIPANTE_CORREO UNIQUE(CorreoElectronico)
);
GO

/*Disparador para calcular la edad del Participante*/
CREATE TRIGGER Persona.TR_PARTICIPANTE_CALCULAR_EDAD
ON Persona.Participante
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p
    SET p.Edad = 
        DATEDIFF(YEAR, i.FechaNacimiento, GETDATE()) - 
        CASE 
            WHEN MONTH(i.FechaNacimiento) > MONTH(GETDATE()) THEN 1
            WHEN MONTH(i.FechaNacimiento) = MONTH(GETDATE()) 
             AND DAY(i.FechaNacimiento)  > DAY(GETDATE()) THEN 1
            ELSE 0
        END
    FROM Persona.Participante p
    INNER JOIN inserted i ON p.IdParticipante = i.IdParticipante;
END;

CREATE TABLE Juego.Lugar
(
	IdLugar BIGINT IDENTITY(1,1) NOT NULL,
	Nombre VARCHAR(50) NOT NULL,
	Ubicacion VARCHAR(50) NOT NULL,
	Capacidad INT NOT NULL,
	
	CONSTRAINT PK_LUGAR PRIMARY KEY (IdLugar),
	CONSTRAINT UQ_LUGAR_UBICACION UNIQUE (Ubicacion),
	CONSTRAINT UQ_LUGAR_NOMBRE UNIQUE (Nombre)
);
GO

CREATE TABLE Juego.Torneo
(
	IdTorneo BIGINT IDENTITY(1,1) NOT NULL,
	NombreTorneo VARCHAR(50) NOT NULL,
	EdadMin INT NOT NULL,
	EdadMax INT NOT NULL,
	Genero VARCHAR(50) NOT NULL, /*Agregar regla O ENUM*/
	FechaInicio DATE NOT NULL,
	FechaFin DATE NOT NULL,
	/*Se calculan con los disparadores*/
	CantEquipos INT,
	NumJornadas INT,

	CONSTRAINT PK_TORNEO PRIMARY KEY (IdTorneo),
	CONSTRAINT UQ_TORNEO_NOMBRE UNIQUE (NombreTorneo)
);
GO

CREATE TABLE Club.Equipo
(
	IdEquipo BIGINT IDENTITY(1,1) NOT NULL,
	NombreEquipo VARCHAR(50) NOT NULL,
	Logo VARCHAR(500) NOT NULL, 
	CantJugadores INT, 

	CONSTRAINT PK_EQUIPO PRIMARY KEY (IdEquipo),
	CONSTRAINT UQ_EQUIPO_NOMBRE UNIQUE (NombreEquipo)
);
GO

CREATE TABLE Persona.Arbitro
(
	IdArbitro BIGINT IDENTITY(1,1) NOT NULL,
	IdParticipante BIGINT NOT NULL,
	CedulaArbitro VARCHAR(15) NOT NULL,

	CONSTRAINT PK_ARBITRO PRIMARY KEY (IdArbitro),
	CONSTRAINT FK_ARBITRO_PARTICIPANTE FOREIGN KEY (IdParticipante) 
	REFERENCES Persona.Participante (IdParticipante),
	CONSTRAINT UQ_ARBITRO_CEDULA UNIQUE (CedulaArbitro),
	CONSTRAINT UQ_ARBITRO_PARTICIPANTE UNIQUE (IdParticipante)
);
GO

CREATE TABLE Persona.Jugador
(
	IdJugador BIGINT IDENTITY(1,1) NOT NULL,
	IdParticipante BIGINT NOT NULL,
	Posicion VARCHAR(50) NOT NULL, /*Podriamos agregar una regla*/
	Numero INT NOT NULL,
	TipoSangre VARCHAR(10) NOT NULL, /*PODRIAMOS AGREGAR UNA REGLA O ENUM*/
	AcumuladorAmarillas INT DEFAULT 0,
	Estado VARCHAR(15) DEFAULT 'Activo',

	CONSTRAINT PK_JUGADOR PRIMARY KEY (IdJugador),
	CONSTRAINT FK_JUGADOR_PARTICIPANTE FOREIGN KEY (IdParticipante) 
	REFERENCES Persona.Participante (IdParticipante),
	CONSTRAINT UQ_JUGADOR_PARTICIPANTE UNIQUE (IdParticipante)
);
GO

CREATE TABLE Juego.Jornada
(
	IdJornada BIGINT IDENTITY(1,1) NOT NULL,
	IdTorneo BIGINT NOT NULL,
	NumeroJornada INT NOT NULL,

	CONSTRAINT PK_JORNADA PRIMARY KEY (IdJornada),
	CONSTRAINT FK_JORNADA_TORNEO FOREIGN KEY (IdTorneo)
	REFERENCES Juego.Torneo (IdTorneo)
);
GO

CREATE TABLE Club.DetalleEquipo
(
	IdEquipo BIGINT NOT NULL,
	IdJugador BIGINT NOT NULL,

	CONSTRAINT FK_DETALLEEQUIPO_EQUIPO FOREIGN KEY (IdEquipo) 
	REFERENCES Club.Equipo (IdEquipo),
	CONSTRAINT FK_DETALLEEQUIPO_JUGADOR FOREIGN KEY (IdJugador) 
	REFERENCES Persona.Jugador (IdJugador),
	CONSTRAINT UQ_EQUIPO_JUGADOR UNIQUE (IdEquipo, IdJugador)
);
GO

/*Disparador para actualizar el numero de jugadores de algun equipo*/
CREATE TRIGGER Club.TR_DETALLEEQUIPO_CANTIDAD
ON Club.DetalleEquipo
AFTER INSERT, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE e
    SET e.CantJugadores = (
        SELECT COUNT(*) 
        FROM Club.DetalleEquipo d 
        WHERE d.IdEquipo = e.IdEquipo
    )
    FROM Club.Equipo e
    WHERE e.IdEquipo IN (SELECT IdEquipo FROM inserted);
    UPDATE e
    SET e.CantJugadores = (
        SELECT COUNT(*) 
        FROM Club.DetalleEquipo d 
        WHERE d.IdEquipo = e.IdEquipo
    )
    FROM Club.Equipo e
    WHERE e.IdEquipo IN (SELECT IdEquipo FROM deleted);
END;

CREATE TABLE Juego.DetalleTorneo
(
	IdTorneo BIGINT NOT NULL,
	IdEquipo BIGINT NOT NULL,

	CONSTRAINT FK_DETALLETORNEO_EQUIPO FOREIGN KEY (IdEquipo)
	REFERENCES Club.Equipo (IdEquipo),
	CONSTRAINT FK_DETALLETORNEO_TORNEO FOREIGN KEY (IdTorneo)
	REFERENCES Juego.Torneo (IdTorneo),
	CONSTRAINT UQ_TORNEO_EQUIPO UNIQUE (IdTorneo, IdEquipo)
);
GO

/*Disparador para validad el genero, edad y equipo de un jugador al registrarse en DetalleEquipo*/

CREATE TRIGGER Club.TR_DETALLEEQUIPO_VALIDAR_JUGADOR
ON Club.DetalleEquipo
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE NOT EXISTS (
            SELECT 1
            FROM Juego.DetalleTorneo dt
            WHERE dt.IdEquipo = i.IdEquipo
        )
    )
    BEGIN
        RAISERROR('Error: El equipo no está inscrito en ningún torneo.',16,1);
        RETURN;
    END
    IF EXISTS (
        SELECT 1
        FROM inserted i
        INNER JOIN Club.DetalleEquipo de
            ON de.IdEquipo = i.IdEquipo
           AND de.IdJugador = i.IdJugador
    )
    BEGIN
        RAISERROR('Error: El jugador ya está inscrito en ese equipo.',16,1);
        RETURN;
    END

    INSERT INTO Club.DetalleEquipo(IdEquipo, IdJugador)
    SELECT IdEquipo, IdJugador
    FROM inserted;
END;
GO

CREATE TABLE Evento.Partido
(
	IdPartido BIGINT IDENTITY(1,1) NOT NULL,
	IdArbitro BIGINT NOT NULL,
	IdJornada BIGINT NOT NULL,
	IdLugar BIGINT NOT NULL,
	IdLocal BIGINT NOT NULL,
	IdVisitante BIGINT NOT NULL,
	Fecha DATE NOT NULL,
	HoraInicio TIME NOT NULL,
	Estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',

	CONSTRAINT PK_PARTIDO PRIMARY KEY (IdPartido),
	CONSTRAINT FK_PARTIDO_ARBITRO FOREIGN KEY (IdArbitro)
	REFERENCES Persona.Arbitro (IdArbitro),
	CONSTRAINT FK_PARTIDO_JORNADA FOREIGN KEY (IdJornada)
	REFERENCES Juego.Jornada (IdJornada),
	CONSTRAINT FK_PARTIDO_LUGAR FOREIGN KEY (IdLugar)
	REFERENCES Juego.Lugar (IdLugar),
	CONSTRAINT FK_PARTIDO_EQLOCAL FOREIGN KEY (IdLocal)
	REFERENCES Club.Equipo (IdEquipo),
	CONSTRAINT FK_PARTIDO_EQVISIT FOREIGN KEY (IdVisitante)
	REFERENCES Club.Equipo (IdEquipo)
);
GO

CREATE TABLE Evento.ResultadoPartido
(
	IdResultado BIGINT IDENTITY(1,1) NOT NULL,
	IdPartido BIGINT NOT NULL,
	GolesLocal TINYINT NOT NULL,
	GolesVisitante TINYINT NOT NULL,
	HoraFin TIME NOT NULL,

	CONSTRAINT PK_RESULTADO PRIMARY KEY (IdResultado),
	CONSTRAINT FK_RESULTADO_PARTIDO FOREIGN KEY (IdPartido)
	REFERENCES Evento.Partido (IdPartido),
	CONSTRAINT UQ_RESULTADO_PARTIDO UNIQUE (IdPartido)
);
GO

CREATE TABLE Evento.Gol
(
	IdGol BIGINT IDENTITY(1,1) NOT NULL,
	IdJugador BIGINT NOT NULL,
	IdPartido BIGINT NOT NULL,
	Minuto TINYINT NOT NULL,

	CONSTRAINT PK_GOL PRIMARY KEY (IdGol),
	CONSTRAINT FK_GOL_JUGADOR FOREIGN KEY (IdJugador)
	REFERENCES Persona.Jugador (IdJugador),
	CONSTRAINT FK_GOLD_PARTIDO FOREIGN KEY (IdPartido)
	REFERENCES Evento.Partido (IdPartido)
);
GO

CREATE TABLE Evento.Tarjeta
(
	IdTarjeta BIGINT IDENTITY(1,1) NOT NULL,
	IdJugador BIGINT NOT NULL,
	IdPartido BIGINT NOT NULL,
	TipoTarjeta VARCHAR(10) NOT NULL, /*agregar regla*/
	Minuto TINYINT NOT NULL,

	CONSTRAINT PK_TARJETA PRIMARY KEY (IdTarjeta),
	CONSTRAINT FK_TARJETA_JUGADOR FOREIGN KEY (IdJugador)
	REFERENCES Persona.Jugador (IdJugador),
	CONSTRAINT FK_TARJETA_PARTIDO FOREIGN KEY (IdPartido)
	REFERENCES Evento.Partido (IdPartido)
);
GO

/*Reglas*/

/*Regla con resitricción de cadenas*/
CREATE RULE RL_POSICION AS @POSICION IN 
('Portero', 'Defensa', 'Medio', 'Delantero');
GO

EXEC sp_bindrule 'RL_POSICION', 'Persona.Jugador.Posicion';
GO

/*Regla con resitricción de numero*/
CREATE RULE RL_CAPACIDAD AS @CAPACIDAD > 1000;
GO

EXEC sp_bindrule 'RL_CAPACIDAD', 'Juego.Lugar.Capacidad';
GO

/*Disparadores*/
/*1.- Disparador para calcular la edad del Participante*/
CREATE TRIGGER Persona.TR_PARTICIPANTE_CALCULAR_EDAD
ON Persona.Participante
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p
    SET p.Edad = 
        DATEDIFF(YEAR, i.FechaNacimiento, GETDATE()) - 
        CASE 
            WHEN MONTH(i.FechaNacimiento) > MONTH(GETDATE()) THEN 1
            WHEN MONTH(i.FechaNacimiento) = MONTH(GETDATE()) 
             AND DAY(i.FechaNacimiento)  > DAY(GETDATE()) THEN 1
            ELSE 0
        END
    FROM Persona.Participante p
    INNER JOIN inserted i ON p.IdParticipante = i.IdParticipante;
END;


/*5.- Disparador para actualizar el estado de los jugadores despues de las inserciones de tarjetas, 
asi como para reiniciar su acumulador de tarjetas amarillas*/
CREATE TRIGGER Evento.TR_TARJETA_ESTADO_JUGADOR
ON Evento.Tarjeta
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    -- 1. CASO INSERT
    IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE j
        SET j.AcumuladorAmarillas = j.AcumuladorAmarillas + 1
        FROM Persona.Jugador j
        INNER JOIN inserted i ON j.IdJugador = i.IdJugador
        WHERE i.TipoTarjeta = 'Amarilla';

        UPDATE j
        SET j.Estado = 'Suspendido',
            j.AcumuladorAmarillas = 0 
        FROM Persona.Jugador j
        INNER JOIN inserted i ON j.IdJugador = i.IdJugador
        WHERE i.TipoTarjeta = 'Roja' OR j.AcumuladorAmarillas >= 2;
    END

    -- 2. CASO DELETE
    IF EXISTS (SELECT * FROM deleted) AND NOT EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE j
        SET j.AcumuladorAmarillas = CASE WHEN j.AcumuladorAmarillas > 0 THEN j.AcumuladorAmarillas - 1 ELSE 0 END
        FROM Persona.Jugador j
        INNER JOIN deleted d ON j.IdJugador = d.IdJugador
        WHERE d.TipoTarjeta = 'Amarilla' AND j.Estado = 'Activo';

        UPDATE j
        SET j.Estado = 'Activo',
            j.AcumuladorAmarillas = CASE WHEN d.TipoTarjeta = 'Amarilla' THEN 1 ELSE 0 END
        FROM Persona.Jugador j
        INNER JOIN deleted d ON j.IdJugador = d.IdJugador
        WHERE j.Estado = 'Suspendido'
        AND NOT EXISTS (
            SELECT 1 FROM Evento.Tarjeta t 
            WHERE t.IdJugador = j.IdJugador AND t.IdPartido = d.IdPartido 
			AND t.TipoTarjeta = 'Roja' AND t.IdTarjeta != d.IdTarjeta
        );
    END

    -- 3. CASO UPDATE: 
    IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
    BEGIN
        IF UPDATE(TipoTarjeta) OR UPDATE(IdJugador)
        BEGIN
            UPDATE j
            SET j.Estado = 'Activo',
                j.AcumuladorAmarillas = CASE 
                    WHEN d.TipoTarjeta = 'Amarilla' 
					AND j.Estado = 'Activo' 
					AND j.AcumuladorAmarillas > 0 THEN j.AcumuladorAmarillas - 1 
                    ELSE j.AcumuladorAmarillas END 
            FROM Persona.Jugador j
            INNER JOIN deleted d ON j.IdJugador = d.IdJugador;

            UPDATE j
            SET j.AcumuladorAmarillas = j.AcumuladorAmarillas + 1
            FROM Persona.Jugador j
            INNER JOIN inserted i ON j.IdJugador = i.IdJugador
            WHERE i.TipoTarjeta = 'Amarilla';

            UPDATE j
            SET j.Estado = 'Suspendido',
                j.AcumuladorAmarillas = 0
            FROM Persona.Jugador j
            INNER JOIN inserted i ON j.IdJugador = i.IdJugador
            WHERE i.TipoTarjeta = 'Roja' OR j.AcumuladorAmarillas >= 2;
        END
    END
END;
GO

/*6.- Disparador para cambiar estado de partido (pendiente a jugado) y estado de jugador (suspendido a activo)*/
CREATE TRIGGER Evento.TR_RESULTADO_ACTUALIZAR_ESTADOS
ON Evento.ResultadoPartido
AFTER INSERT
/*Trigger Actualizar CantEquipos y NumJornadas en la tabla Torneo al inscribir un equipo.*/
USE Futbolito
GO

DROP TRIGGER IF EXISTS Juego.TR_ActualizarTorneo
GO

CREATE TRIGGER Juego.TR_ActualizarTorneo
ON Juego.DetalleTorneo
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE p
    SET p.Estado = 'Jugado'
    FROM Evento.Partido p
    INNER JOIN inserted i ON p.IdPartido = i.IdPartido;

    UPDATE j
    SET j.Estado = 'Activo'
    FROM Persona.Jugador j
    INNER JOIN Club.DetalleEquipo de ON j.IdJugador = de.IdJugador
    INNER JOIN Evento.Partido p ON (de.IdEquipo = p.IdLocal OR de.IdEquipo = p.IdVisitante)
    INNER JOIN inserted i ON p.IdPartido = i.IdPartido
    WHERE j.Estado = 'Suspendido';
END;


/* =========================================================
   1. PARTICIPANTES (Forzando IDs 1 al 14)
INSERT INTO Persona.Participante (NombreParticipante, Genero, Telefono, CorreoElectronico, FechaNacimiento)
VALUES 
('Marco Antonio Ortiz', 'Masculino', '5551234567', 'marco.ortiz@arbitros.mx', '1988-03-14'),
('Cesar Arturo Ramos', 'Masculino', '5557654321', 'cesar.ramos@arbitros.mx', '1983-12-15'),
('Guillermo Ochoa', 'Masculino', '5551112222', 'memo.ochoa@mail.com', '1985-07-13'),
('Rafael Marquez', 'Masculino', '5553334444', 'rafa.marquez@mail.com', '1979-02-13'),
('Edson Alvarez', 'Masculino', '5555556666', 'edson.alvarez@mail.com', '1997-10-24'),
('Andres Guardado', 'Masculino', '5557778888', 'andres.guardado@mail.com', '1986-09-28'),
('Hirving Lozano', 'Masculino', '5559990000', 'chucky.lozano@mail.com', '1995-07-30'),
('Raul Jimenez', 'Masculino', '5550001111', 'raul.jimenez@mail.com', '1991-05-05'),
('Luis Malagon', 'Masculino', '5552223333', 'luis.malagon@mail.com', '1997-03-02'),
('Johan Vasquez', 'Masculino', '5554445555', 'johan.vasquez@mail.com', '1998-10-22'),
('Cesar Montes', 'Masculino', '5556667777', 'cesar.montes@mail.com', '1997-02-24'),
('Luis Chavez', 'Masculino', '5558889999', 'luis.chavez@mail.com', '1996-01-15'),
('Orbelin Pineda', 'Masculino', '5551010101', 'orbelin.pineda@mail.com', '1996-03-24'),
('Santiago Gimenez', 'Masculino', '5552020202', 'santi.gimenez@mail.com', '2001-04-18');
GO

/* =========================================================
   2. LUGARES (Forzando IDs 1 al 3)
INSERT INTO Juego.Lugar (Nombre, Ubicacion, Capacidad)
VALUES 
('Estadio Azteca', 'Ciudad de Mexico', 83264),
('Estadio Akron', 'Jalisco', 46232),
('Estadio BBVA', 'Nuevo Leon', 51000);
GO

/* =========================================================
   3. TORNEOS (Forzando IDs 1 y 2)
INSERT INTO Juego.Torneo (NombreTorneo, EdadMin, EdadMax, Genero, FechaInicio, FechaFin)
VALUES 
('Liga Clausura 2026', 16, 45, 'Masculino', '2026-01-10', '2026-05-30'),
('Copa MX 2026', 18, 40, 'Mixto', '2026-08-01', '2026-12-15');
GO

/* =========================================================
   4. EQUIPOS (Forzando IDs 1 al 3)
INSERT INTO Club.Equipo (NombreEquipo, Logo)
VALUES 
('Aguilas del Centro', 'url_aguilas.png'),
('Chivas del Norte', 'url_chivas.png'),
('Tigres del Sur', 'url_tigres.png'),
('Equipo 4', 'Equipo_4.png'),
('Equipo 5', 'Equipo_5.png'),
('Equipo 6', 'Equipo_6.png'),
('Equipo 7', 'Equipo_7.png'),
('Equipo 8', 'Equipo_8.png'),
('Equipo 9', 'Equipo_9.png'),
('Equipo 10', 'Equipo_10.png');
GO

/* =========================================================
   5. ÁRBITROS (Forzando IDs 1 y 2)
INSERT INTO Persona.Arbitro (IdParticipante, CedulaArbitro)
VALUES 
(1, 'ARB-2026-001'),
(2, 'ARB-2026-002');
GO

/* =========================================================
   6. JUGADORES (Forzando IDs 1 al 12)
INSERT INTO Persona.Jugador (IdParticipante, Posicion, Numero, TipoSangre)
VALUES 
(3, 'Portero', 13, 'O+'),
(4, 'Defensa', 4, 'O-'),
(5, 'Defensa', 19, 'A+'),
(6, 'Medio', 18, 'O+'),
(7, 'Delantero', 22, 'B+'),
(8, 'Delantero', 9, 'O+'),
(9, 'Portero', 1, 'O+'),
(10, 'Defensa', 5, 'AB+'),
(11, 'Defensa', 3, 'O-'),
(12, 'Medio', 24, 'A+'),
(13, 'Medio', 17, 'O+'),
(14, 'Delantero', 11, 'O+');
GO

/* =========================================================
   7. JORNADAS (Forzando IDs 1 al 3)
INSERT INTO Juego.Jornada (IdTorneo, NumeroJornada)
VALUES 
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(2, 1),
(2, 2),
(2, 3);
GO

/* =========================================================
   8. DETALLE DE EQUIPOS (No usa Identity)
INSERT INTO Club.DetalleEquipo (IdEquipo, IdJugador)
VALUES 
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6),
(2, 7), (2, 8), (2, 9), (2, 10), (2, 11), (2, 12);
GO

/* =========================================================
   9. DETALLE DE TORNEO (No usa Identity)
INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo)
VALUES 
(1, 1),
(1, 2),
(1, 3),
(1, 4),
(1, 5),
(1, 6),
(2, 7),
(2, 8),
(2, 9),
(2, 10);
GO

/* =========================================================
   10. PARTIDOS (Forzando IDs 1 al 3)
/*INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio, Estado)
VALUES 
(1, 1, 1, 1, 2, '2026-01-15', '19:00:00', 'Finalizado'),
(2, 2, 2, 2, 1, '2026-01-22', '20:30:00', 'Finalizado'),
(1, 3, 3, 1, 3, '2026-01-29', '17:00:00', 'Pendiente');
GO*/

/* =========================================================
   11. RESULTADOS (Forzando IDs 1 y 2)
INSERT INTO Evento.ResultadoPartido (IdPartido, GolesLocal, GolesVisitante, HoraFin)
VALUES 
(1, 2, 1, '20:55:00'),
(2, 2, 2, '22:25:00');
GO

/* =========================================================
   12. GOLES (Forzando IDs 1 al 7)
INSERT INTO Evento.Gol (IdJugador, IdPartido, Minuto)
VALUES 
(5, 1, 12), 
(12, 1, 45), 
(6, 1, 89),  
(8, 2, 5),   
(10, 2, 33), 
(3, 2, 60),  
(4, 2, 75);  
GO

/* =========================================================
   13. TARJETAS (Forzando IDs 1 al 5)
INSERT INTO Evento.Tarjeta (IdJugador, IdPartido, TipoTarjeta, Minuto)
VALUES 
(2, 1, 'Amarilla', 25),
(11, 1, 'Amarilla', 55),
(11, 1, 'Roja', 80),  
(5, 2, 'Amarilla', 15),
(7, 2, 'Amarilla', 40);
GO



/*
/* =========================================================
   1. PRUEBA DE DISPARADOR 1 (Calculo de Edad)
-- Metemos participantes con diferentes fechas de nacimiento para ver si tu trigger calcula bien la edad.
INSERT INTO Persona.Participante (NombreParticipante, Genero, Telefono, CorreoElectronico, FechaNacimiento)
VALUES 
('Arbitro Chido', 'Masculino', '5551112222', 'arbi@mail.com', '1980-05-10'),
('Jugador Castigado', 'Masculino', '5553334444', 'castigo@mail.com', '1995-01-01'),
('Jugador Normal 1', 'Masculino', '5555556666', 'norm1@mail.com', '2000-12-31'),
('Jugador Normal 2', 'Masculino', '5557778888', 'norm2@mail.com', '1998-06-15');
GO

-- REVISIÓN DEL TRIGGER 1: Corre esto y fíjate si la columna "Edad" tiene datos lógicos en lugar de NULL.
SELECT IdParticipante, NombreParticipante, FechaNacimiento, Edad FROM Persona.Participante;
GO

/* =========================================================
   2. INFRAESTRUCTURA BÁSICA (Torneos, Lugares, Equipos)
-- Regla probada: Capacidad > 1000
INSERT INTO Juego.Lugar (Nombre, Ubicacion, Capacidad) VALUES ('Estadio Central', 'CDMX', 50000);
INSERT INTO Juego.Torneo (NombreTorneo, EdadMin, EdadMax, Genero, FechaInicio, FechaFin) VALUES ('Torneo Verano', 18, 40, 'Masculino', '2026-06-01', '2026-12-01');
INSERT INTO Juego.Jornada (IdTorneo, NumeroJornada) VALUES (1, 1);
INSERT INTO Club.Equipo (NombreEquipo, Logo) VALUES ('Los Terribles', 'logo1.png'), ('Los Troncos', 'logo2.png');
INSERT INTO Persona.Arbitro (IdParticipante, CedulaArbitro) VALUES (1, 'ARB-001');
GO

/* =========================================================
   3. PREPARACIÓN PARA EL DISPARADOR 6 (Estados)
-- Vamos a meter a los jugadores. OJO: Al jugador 1 lo vamos a meter como 'Suspendido' a propósito.
INSERT INTO Persona.Jugador (IdParticipante, Posicion, Numero, TipoSangre, Estado)
VALUES 
(2, 'Defensa', 4, 'O+', 'Suspendido'), -- ¡Este es el que debe cambiar a Activo!
(3, 'Delantero', 9, 'A+', 'Activo'),
(4, 'Portero', 1, 'B-', 'Activo');
GO

-- Los asignamos a los equipos. 
-- Equipo 1 (Los Terribles) tiene al suspendido y al delantero. Equipo 2 (Los Troncos) al portero.
INSERT INTO Club.DetalleEquipo (IdEquipo, IdJugador) VALUES (1, 1), (1, 2), (2, 3);
GO

/* =========================================================
   4. CREACIÓN DEL PARTIDO 
-- El partido entra por defecto como 'Pendiente' (IdLocal=1, IdVisitante=2)
INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio)
VALUES (1, 1, 1, 1, 2, GETDATE(), '16:00:00');
GO

-- REVISIÓN ANTES DEL RESULTADO: 
-- Verifica que el partido dice 'Pendiente' y el Jugador 1 dice 'Suspendido'.
SELECT IdPartido, Estado AS EstadoPartido FROM Evento.Partido;
SELECT IdJugador, Estado AS EstadoJugador FROM Persona.Jugador WHERE IdJugador = 1;
GO

/* =========================================================
   5. ¡EL MOMENTO DE LA VERDAD! (Prueba del Trigger 6)
-- Al insertar el resultado, el Trigger debe dispararse solo.
INSERT INTO Evento.ResultadoPartido (IdPartido, GolesLocal, GolesVisitante, HoraFin)
VALUES (1, 2, 1, '18:00:00'); -- Partido 1 termina 2 a 1.
GO

-- REVISIÓN FINAL: Si hiciste bien las cosas, esta consulta debe arrojar 'Jugado' y 'Activo'.
SELECT 'Estado del Partido (Debería ser Jugado):' AS Prueba, Estado FROM Evento.Partido WHERE IdPartido = 1
UNION ALL
SELECT 'Estado del Jugador (Debería ser Activo):' AS Prueba, Estado FROM Persona.Jugador WHERE IdJugador = 1;
GO

/* =========================================================
   6. PRUEBA DE TARJETAS: PRIMERA AMARILLA (INSERT)
-- Al Jugador 2 (el delantero del equipo 1) le sacan su primera amarilla.
INSERT INTO Evento.Tarjeta (IdJugador, IdPartido, TipoTarjeta, Minuto)
VALUES (2, 1, 'Amarilla', 15);
GO

-- REVISIÓN: El Acumulador de Jugador 2 debe subir a 1, Estado 'Activo'.
SELECT IdJugador, Estado, AcumuladorAmarillas 
FROM Persona.Jugador WHERE IdJugador = 2;
GO

/* =========================================================
   7. PRUEBA DE TARJETAS: LA SEGUNDA AMARILLA (INSERT EXTREMO)
-- Al mismo Jugador 2 le sacan otra amarilla en el mismo partido.
INSERT INTO Evento.Tarjeta (IdJugador, IdPartido, TipoTarjeta, Minuto)
VALUES (2, 1, 'Amarilla', 40);
GO

-- REVISIÓN: El trigger 5 debió actuar. Estado 'Suspendido', Acumulador 0.
SELECT IdJugador, Estado, AcumuladorAmarillas 
FROM Persona.Jugador WHERE IdJugador = 2;
GO

/* =========================================================
   8. PRUEBA DE TARJETAS: ROJA DIRECTA (INSERT)
-- Al Jugador 3 (el portero del equipo 2) le sacan roja directa.
INSERT INTO Evento.Tarjeta (IdJugador, IdPartido, TipoTarjeta, Minuto)
VALUES (3, 1, 'Roja', 60);
GO

-- REVISIÓN: Estado 'Suspendido', Acumulador 0.
SELECT IdJugador, Estado, AcumuladorAmarillas 
FROM Persona.Jugador WHERE IdJugador = 3;
GO

/* =========================================================
   9. PRUEBA DEL "CTRL+Z" (DELETE - ELIMINACIÓN DE ERROR)
-- Imagina que el árbitro se equivocó. Borramos la SEGUNDA amarilla del Jugador 2.
-- En esta base limpia, esa tarjeta tiene el IdTarjeta = 2.
DELETE FROM Evento.Tarjeta WHERE IdTarjeta = 2;
GO

-- REVISIÓN CRÍTICA: El Jugador 2 debe resucitar a 'Activo' y recuperar 1 en su acumulador.
SELECT IdJugador, Estado, AcumuladorAmarillas 
FROM Persona.Jugador WHERE IdJugador = 2;
GO

/* =========================================================
   10. PRUEBA DEL ESCUDO (UPDATE - CAMBIO DE OPINIÓN)
-- Al Jugador 2 le modifican su única amarilla (IdTarjeta = 1) y se la cambian por Roja.
UPDATE Evento.Tarjeta 
SET TipoTarjeta = 'Roja' 
WHERE IdTarjeta = 1;
GO

-- REVISIÓN: El Jugador 2 vuelve a estar 'Suspendido' y su acumulador se va a 0 de nuevo.
SELECT IdJugador, Estado, AcumuladorAmarillas 
FROM Persona.Jugador WHERE IdJugador = 2;
GO

/* =========================================================
   11. LA PRUEBA FINAL: RESURRECCIÓN BLOQUEADA (DELETE)
-- El Jugador 3 está suspendido por la Roja del paso 8.
-- Vamos a meterle una Amarilla a la fuerza (IdTarjeta = 4).
INSERT INTO Evento.Tarjeta (IdJugador, IdPartido, TipoTarjeta, Minuto)
VALUES (3, 1, 'Amarilla', 80);
GO
-- Ahora, borramos ESA amarilla. 
DELETE FROM Evento.Tarjeta WHERE IdTarjeta = 4;
GO

-- REVISIÓN: Si tu trigger está bien blindado, el Jugador 3 DEBE SEGUIR 'Suspendido'. 
-- No se puede resucitar porque la Roja del paso 8 sigue viva.
SELECT 'Prueba de Blindaje' AS Test, IdJugador, Estado, AcumuladorAmarillas 
FROM Persona.Jugador WHERE IdJugador = 3;
GO*/
    DECLARE @idTorneo BIGINT
    DECLARE @total INT
    DECLARE @jornadas INT
    DECLARE @jornadasActuales INT

    SELECT TOP 1 @idTorneo = IdTorneo FROM inserted
    IF @idTorneo IS NULL
        SELECT TOP 1 @idTorneo = IdTorneo FROM deleted

    -- Contar equipos actuales
    SELECT @total = COUNT(*)
    FROM Juego.DetalleTorneo
    WHERE IdTorneo = @idTorneo

    -- Calcular jornadas
    IF @total % 2 = 0
        SET @jornadas = @total - 1
    ELSE
        SET @jornadas = @total

    -- Actualizar Torneo
    UPDATE Juego.Torneo
    SET CantEquipos = @total,
        NumJornadas = @jornadas
    WHERE IdTorneo = @idTorneo

    -- Contar jornadas actuales en Juego.Jornada
    SELECT @jornadasActuales = COUNT(*)
    FROM Juego.Jornada
    WHERE IdTorneo = @idTorneo

    -- Insertar jornadas faltantes
    IF @jornadas > @jornadasActuales
    BEGIN
        DECLARE @i INT = @jornadasActuales + 1
        WHILE @i <= @jornadas
        BEGIN
            INSERT INTO Juego.Jornada(IdTorneo, NumeroJornada)
            VALUES (@idTorneo, @i)
            SET @i = @i + 1
        END
    END

    -- Eliminar jornadas sobrantes
    IF @jornadas < @jornadasActuales
    BEGIN
        DELETE FROM Juego.Jornada
        WHERE IdTorneo = @idTorneo
          AND NumeroJornada > @jornadas
    END

END
GO

UPDATE Juego.Torneo SET NumJornadas = 7 WHERE IdTorneo = 1

UPDATE Juego.Torneo
SET NumJornadas = 7
WHERE IdTorneo = 1

INSERT INTO Club.Equipo(NombreEquipo, Logo)
VALUES
('Real Madrid', 'a'),
('Barcelona', 'b'),
('AC Millan', 'c'),
('Liverpool', 'd');

USE Futbolito
GO

SELECT * FROM Juego.Jornada
SELECT * FROM Juego.DetalleTorneo


-- Corrección manual
UPDATE Juego.Torneo
SET NumJornadas = 5
WHERE IdTorneo = 1
SELECT IdTorneo, CantEquipos, NumJornadas FROM Juego.Torneo
