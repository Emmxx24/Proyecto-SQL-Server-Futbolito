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
	Logo VARCHAR(50) NOT NULL, /*Guardaría la URL de la imagen*/
	CantJugadores INT, /*La calculan los disparadores*/

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

USE Futbolito;
GO

/* =========================================================
   1. PRUEBA DE DISPARADOR 1 (Calculo de Edad)
========================================================= */
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
========================================================= */
-- Regla probada: Capacidad > 1000
INSERT INTO Juego.Lugar (Nombre, Ubicacion, Capacidad) VALUES ('Estadio Central', 'CDMX', 50000);
INSERT INTO Juego.Torneo (NombreTorneo, EdadMin, EdadMax, Genero, FechaInicio, FechaFin) VALUES ('Torneo Verano', 18, 40, 'Masculino', '2026-06-01', '2026-12-01');
INSERT INTO Juego.Jornada (IdTorneo, NumeroJornada) VALUES (1, 1);
INSERT INTO Club.Equipo (NombreEquipo, Logo) VALUES ('Los Terribles', 'logo1.png'), ('Los Troncos', 'logo2.png');
INSERT INTO Persona.Arbitro (IdParticipante, CedulaArbitro) VALUES (1, 'ARB-001');
GO

/* =========================================================
   3. PREPARACIÓN PARA EL DISPARADOR 6 (Estados)
========================================================= */
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
========================================================= */
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
========================================================= */
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
========================================================= */
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
========================================================= */
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
========================================================= */
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
========================================================= */
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
========================================================= */
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
========================================================= */
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
GO