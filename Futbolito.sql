/*Creación de la base de datos*/
CREATE DATABASE Futbolito;
GO

/*Usar la base de datos recién creada*/
USE Futbolito;
GO



/*Creación de esquemas*/

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



/*Creación de las tablas*/

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
	Genero VARCHAR(50) NOT NULL, 
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
	Posicion VARCHAR(50) NOT NULL, 
	Numero INT NOT NULL,
	TipoSangre VARCHAR(10) NOT NULL, 
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
	CONSTRAINT FK_GOL_PARTIDO FOREIGN KEY (IdPartido)
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
GO

/*2.- Disparador para actualizar CantEquipos y NumJornadas en la tabla Torneo al inscribir un equipo.*/
CREATE TRIGGER Juego.TR_ActualizarTorneo
ON Juego.DetalleTorneo
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

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

/*3.- Disparador para actualizar el numero de jugadores de algun equipo*/
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
GO

/*4.- Disparador para validad el genero, edad y equipo de un jugador al registrarse en DetalleEquipo*/
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
GO

USE Futbolito;
GO

/* 1. Participantes (18 en total: 16 Jugadores y 2 Árbitros) */
INSERT INTO Persona.Participante (NombreParticipante, Genero, Telefono, CorreoElectronico, FechaNacimiento) 
VALUES
('Jugador 1',  'M', '5550000001', 'j1@mail.com',  '2000-01-01'),
('Jugador 2',  'M', '5550000002', 'j2@mail.com',  '2000-01-02'),
('Jugador 3',  'M', '5550000003', 'j3@mail.com',  '2000-01-03'),
('Jugador 4',  'M', '5550000004', 'j4@mail.com',  '2000-01-04'),
('Jugador 5',  'M', '5550000005', 'j5@mail.com',  '2000-01-05'),
('Jugador 6',  'M', '5550000006', 'j6@mail.com',  '2000-01-06'),
('Jugador 7',  'M', '5550000007', 'j7@mail.com',  '2000-01-07'),
('Jugador 8',  'M', '5550000008', 'j8@mail.com',  '2000-01-08'),
('Jugador 9',  'M', '5550000009', 'j9@mail.com',  '2000-01-09'),
('Jugador 10', 'M', '5550000010', 'j10@mail.com', '2000-01-10'),
('Jugador 11', 'M', '5550000011', 'j11@mail.com', '2000-01-11'),
('Jugador 12', 'M', '5550000012', 'j12@mail.com', '2000-01-12'),
('Jugador 13', 'M', '5550000013', 'j13@mail.com', '2000-01-13'),
('Jugador 14', 'M', '5550000014', 'j14@mail.com', '2000-01-14'),
('Jugador 15', 'M', '5550000015', 'j15@mail.com', '2000-01-15'),
('Jugador 16', 'M', '5550000016', 'j16@mail.com', '2000-01-16'),
('Arbitro 1',  'M', '5550000017', 'a1@mail.com',  '1990-01-01'),
('Arbitro 2',  'M', '5550000018', 'a2@mail.com',  '1990-01-02');
GO

/* 2. Lugares (Regla: Capacidad > 1000) */
INSERT INTO Juego.Lugar (Nombre, Ubicacion, Capacidad) 
VALUES
('Estadio 1', 'Ubicacion 1', 5000),
('Estadio 2', 'Ubicacion 2', 6000),
('Estadio 3', 'Ubicacion 3', 7000);
GO

/* 3. Torneo */
INSERT INTO Juego.Torneo (NombreTorneo, EdadMin, EdadMax, Genero, FechaInicio, FechaFin) 
VALUES
('Torneo 1', 18, 40, 'M', '2026-05-01', '2026-06-30');
GO

/* 4. Equipos */
INSERT INTO Club.Equipo (NombreEquipo, Logo) 
VALUES
('Equipo 1', 'logo1.png'),
('Equipo 2', 'logo2.png'),
('Equipo 3', 'logo3.png'),
('Equipo 4', 'logo4.png');
GO

/* 5. Inscribir Equipos al Torneo (DetalleTorneo) 
   NOTA: Esto disparará TR_ActualizarTorneo y creará las jornadas 1, 2 y 3 automáticamente. */
INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) 
VALUES
(1, 1), (1, 2), (1, 3), (1, 4);
GO

/* Completar las jornadas para que sean 6 (torneo ida y vuelta) */
INSERT INTO Juego.Jornada (IdTorneo, NumeroJornada) 
VALUES
(1, 4), (1, 5), (1, 6);

UPDATE Juego.Torneo 
SET NumJornadas = 6 
WHERE IdTorneo = 1;
GO

/* 6. Árbitros (Asociamos los ID de Participante 17 y 18) */
INSERT INTO Persona.Arbitro (IdParticipante, CedulaArbitro) 
VALUES
(17, 'ARB-001'), 
(18, 'ARB-002');
GO

/* 7. Jugadores (Asociamos los ID de Participante del 1 al 16) 
   Regla RL_POSICION: 'Portero', 'Defensa', 'Medio', 'Delantero' */
INSERT INTO Persona.Jugador (IdParticipante, Posicion, Numero, TipoSangre) 
VALUES
(1, 'Portero',   1, 'O+'), (2, 'Defensa',   2, 'O+'), (3, 'Medio',     3, 'O+'), (4, 'Delantero', 4, 'O+'),
(5, 'Portero',   1, 'A+'), (6, 'Defensa',   2, 'A+'), (7, 'Medio',     3, 'A+'), (8, 'Delantero', 4, 'A+'),
(9, 'Portero',   1, 'B+'), (10,'Defensa',   2, 'B+'), (11,'Medio',     3, 'B+'), (12,'Delantero', 4, 'B+'),
(13,'Portero',   1, 'AB+'),(14,'Defensa',   2, 'AB+'),(15,'Medio',     3, 'AB+'),(16,'Delantero', 4, 'AB+');
GO

/* 8. Asignar Jugadores a Equipos (DetalleEquipo) 
   Ya es posible hacerlo porque los equipos ya están en DetalleTorneo */
INSERT INTO Club.DetalleEquipo (IdEquipo, IdJugador) 
VALUES
(1, 1), (1, 2),  (1, 3),  (1, 4),  -- Jugadores del Equipo 1
(2, 5), (2, 6),  (2, 7),  (2, 8),  -- Jugadores del Equipo 2
(3, 9), (3, 10), (3, 11), (3, 12), -- Jugadores del Equipo 3
(4, 13),(4, 14), (4, 15), (4, 16); -- Jugadores del Equipo 4
GO

/* 9. Partidos (12 Partidos para que todos jueguen contra todos 2 veces) */
-- Jornada 1
INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio) VALUES
(1, 1, 1, 1, 2, '2026-05-02', '10:00:00'),
(2, 1, 2, 3, 4, '2026-05-02', '12:00:00');

-- Jornada 2
INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio) VALUES
(1, 2, 3, 1, 3, '2026-05-09', '10:00:00'),
(2, 2, 1, 4, 2, '2026-05-09', '12:00:00');

-- Jornada 3
INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio) VALUES
(1, 3, 2, 4, 1, '2026-05-16', '10:00:00'),
(2, 3, 3, 2, 3, '2026-05-16', '12:00:00');

-- Jornada 4 (Vuelta de la J1, se invierten local y visitante)
INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio) VALUES
(1, 4, 2, 2, 1, '2026-05-23', '10:00:00'),
(2, 4, 3, 4, 3, '2026-05-23', '12:00:00');

-- Jornada 5 (Vuelta de la J2, se invierten local y visitante)
INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio) VALUES
(1, 5, 1, 3, 1, '2026-05-30', '10:00:00'),
(2, 5, 2, 2, 4, '2026-05-30', '12:00:00');

-- Jornada 6 (Vuelta de la J3, se invierten local y visitante)
INSERT INTO Evento.Partido (IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio) VALUES
(1, 6, 3, 1, 4, '2026-06-06', '10:00:00'),
(2, 6, 1, 3, 2, '2026-06-06', '12:00:00');
GO

/* 10. Resultados (1 por cada partido insertado. Al insertar, el Trigger 
       TR_RESULTADO_ACTUALIZAR_ESTADOS pasará los partidos a estado "Jugado") */
INSERT INTO Evento.ResultadoPartido (IdPartido, GolesLocal, GolesVisitante, HoraFin) 
VALUES
(1,  2, 1, '12:00:00'),
(2,  0, 0, '14:00:00'),
(3,  1, 3, '12:00:00'),
(4,  2, 2, '14:00:00'),
(5,  0, 1, '12:00:00'),
(6,  1, 0, '14:00:00'),
(7,  3, 1, '12:00:00'),
(8,  0, 2, '14:00:00'),
(9,  1, 1, '12:00:00'),
(10, 4, 0, '14:00:00'),
(11, 2, 0, '12:00:00'),
(12, 1, 1, '14:00:00');
GO

USE Futbolito;
GO

UPDATE Persona.Jugador
SET AcumuladorAmarillas = 0
WHERE AcumuladorAmarillas = 1

UPDATE Evento.Partido
SET Estado = 'Pendiente'
WHERE Estado = 'Jugado'