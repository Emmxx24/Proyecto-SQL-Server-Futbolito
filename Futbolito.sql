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
	REFERENCES Juego.Torneo (IdTorneo)
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
/*/Disparador para calcular la edad del Participante*/
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


/* =========================================================
   1. PARTICIPANTES (Forzando IDs 1 al 14)
========================================================= */
SET IDENTITY_INSERT Persona.Participante ON;
INSERT INTO Persona.Participante (IdParticipante, NombreParticipante, Genero, Telefono, CorreoElectronico, FechaNacimiento)
VALUES 
(1, 'Marco Antonio Ortiz', 'Masculino', '5551234567', 'marco.ortiz@arbitros.mx', '1988-03-14'),
(2, 'Cesar Arturo Ramos', 'Masculino', '5557654321', 'cesar.ramos@arbitros.mx', '1983-12-15'),
(3, 'Guillermo Ochoa', 'Masculino', '5551112222', 'memo.ochoa@mail.com', '1985-07-13'),
(4, 'Rafael Marquez', 'Masculino', '5553334444', 'rafa.marquez@mail.com', '1979-02-13'),
(5, 'Edson Alvarez', 'Masculino', '5555556666', 'edson.alvarez@mail.com', '1997-10-24'),
(6, 'Andres Guardado', 'Masculino', '5557778888', 'andres.guardado@mail.com', '1986-09-28'),
(7, 'Hirving Lozano', 'Masculino', '5559990000', 'chucky.lozano@mail.com', '1995-07-30'),
(8, 'Raul Jimenez', 'Masculino', '5550001111', 'raul.jimenez@mail.com', '1991-05-05'),
(9, 'Luis Malagon', 'Masculino', '5552223333', 'luis.malagon@mail.com', '1997-03-02'),
(10, 'Johan Vasquez', 'Masculino', '5554445555', 'johan.vasquez@mail.com', '1998-10-22'),
(11, 'Cesar Montes', 'Masculino', '5556667777', 'cesar.montes@mail.com', '1997-02-24'),
(12, 'Luis Chavez', 'Masculino', '5558889999', 'luis.chavez@mail.com', '1996-01-15'),
(13, 'Orbelin Pineda', 'Masculino', '5551010101', 'orbelin.pineda@mail.com', '1996-03-24'),
(14, 'Santiago Gimenez', 'Masculino', '5552020202', 'santi.gimenez@mail.com', '2001-04-18');
SET IDENTITY_INSERT Persona.Participante OFF;
GO

/* =========================================================
   2. LUGARES (Forzando IDs 1 al 3)
========================================================= */
SET IDENTITY_INSERT Juego.Lugar ON;
INSERT INTO Juego.Lugar (IdLugar, Nombre, Ubicacion, Capacidad)
VALUES 
(1, 'Estadio Azteca', 'Ciudad de Mexico', 83264),
(2, 'Estadio Akron', 'Jalisco', 46232),
(3, 'Estadio BBVA', 'Nuevo Leon', 51000);
SET IDENTITY_INSERT Juego.Lugar OFF;
GO

/* =========================================================
   3. TORNEOS (Forzando IDs 1 y 2)
========================================================= */
SET IDENTITY_INSERT Juego.Torneo ON;
INSERT INTO Juego.Torneo (IdTorneo, NombreTorneo, EdadMin, EdadMax, Genero, FechaInicio, FechaFin)
VALUES 
(1, 'Liga Clausura 2026', 16, 45, 'Masculino', '2026-01-10', '2026-05-30'),
(2, 'Copa MX 2026', 18, 40, 'Mixto', '2026-08-01', '2026-12-15');
SET IDENTITY_INSERT Juego.Torneo OFF;
GO

/* =========================================================
   4. EQUIPOS (Forzando IDs 1 al 3)
========================================================= */
SET IDENTITY_INSERT Club.Equipo ON;
INSERT INTO Club.Equipo (IdEquipo, NombreEquipo, Logo)
VALUES 
(1, 'Aguilas del Centro', 'url_aguilas.png'),
(2, 'Chivas del Norte', 'url_chivas.png'),
(3, 'Tigres del Sur', 'url_tigres.png');
SET IDENTITY_INSERT Club.Equipo OFF;
GO

/* =========================================================
   5. ÁRBITROS (Forzando IDs 1 y 2)
========================================================= */
SET IDENTITY_INSERT Persona.Arbitro ON;
INSERT INTO Persona.Arbitro (IdArbitro, IdParticipante, CedulaArbitro)
VALUES 
(1, 1, 'ARB-2026-001'),
(2, 2, 'ARB-2026-002');
SET IDENTITY_INSERT Persona.Arbitro OFF;
GO

/* =========================================================
   6. JUGADORES (Forzando IDs 1 al 12)
========================================================= */
SET IDENTITY_INSERT Persona.Jugador ON;
INSERT INTO Persona.Jugador (IdJugador, IdParticipante, Posicion, Numero, TipoSangre)
VALUES 
(1, 3, 'Portero', 13, 'O+'),
(2, 4, 'Defensa', 4, 'O-'),
(3, 5, 'Defensa', 19, 'A+'),
(4, 6, 'Medio', 18, 'O+'),
(5, 7, 'Delantero', 22, 'B+'),
(6, 8, 'Delantero', 9, 'O+'),
(7, 9, 'Portero', 1, 'O+'),
(8, 10, 'Defensa', 5, 'AB+'),
(9, 11, 'Defensa', 3, 'O-'),
(10, 12, 'Medio', 24, 'A+'),
(11, 13, 'Medio', 17, 'O+'),
(12, 14, 'Delantero', 11, 'O+');
SET IDENTITY_INSERT Persona.Jugador OFF;
GO

/* =========================================================
   7. JORNADAS (Forzando IDs 1 al 3)
========================================================= */
SET IDENTITY_INSERT Juego.Jornada ON;
INSERT INTO Juego.Jornada (IdJornada, IdTorneo, NumeroJornada)
VALUES 
(1, 1, 1),
(2, 1, 2),
(3, 1, 3);
SET IDENTITY_INSERT Juego.Jornada OFF;
GO

/* =========================================================
   8. DETALLE DE EQUIPOS (No usa Identity)
========================================================= */
INSERT INTO Club.DetalleEquipo (IdEquipo, IdJugador)
VALUES 
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), (1, 6),
(2, 7), (2, 8), (2, 9), (2, 10), (2, 11), (2, 12);
GO

/* =========================================================
   9. DETALLE DE TORNEO (No usa Identity)
========================================================= */
INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo)
VALUES 
(1, 1),
(1, 2),
(1, 3);
GO

/* =========================================================
   10. PARTIDOS (Forzando IDs 1 al 3)
========================================================= */
SET IDENTITY_INSERT Evento.Partido ON;
INSERT INTO Evento.Partido (IdPartido, IdArbitro, IdJornada, IdLugar, IdLocal, IdVisitante, Fecha, HoraInicio, Estado)
VALUES 
(1, 1, 1, 1, 1, 2, '2026-01-15', '19:00:00', 'Finalizado'),
(2, 2, 2, 2, 2, 1, '2026-01-22', '20:30:00', 'Finalizado'),
(3, 1, 3, 3, 1, 3, '2026-01-29', '17:00:00', 'Pendiente');
SET IDENTITY_INSERT Evento.Partido OFF;
GO

/* =========================================================
   11. RESULTADOS (Forzando IDs 1 y 2)
========================================================= */
SET IDENTITY_INSERT Evento.ResultadoPartido ON;
INSERT INTO Evento.ResultadoPartido (IdResultado, IdPartido, GolesLocal, GolesVisitante, HoraFin)
VALUES 
(1, 1, 2, 1, '20:55:00'),
(2, 2, 2, 2, '22:25:00');
SET IDENTITY_INSERT Evento.ResultadoPartido OFF;
GO

/* =========================================================
   12. GOLES (Forzando IDs 1 al 7)
========================================================= */
SET IDENTITY_INSERT Evento.Gol ON;
INSERT INTO Evento.Gol (IdGol, IdJugador, IdPartido, Minuto)
VALUES 
(1, 5, 1, 12), 
(2, 12, 1, 45), 
(3, 6, 1, 89),  
(4, 8, 2, 5),   
(5, 10, 2, 33), 
(6, 3, 2, 60),  
(7, 4, 2, 75);  
SET IDENTITY_INSERT Evento.Gol OFF;
GO

/* =========================================================
   13. TARJETAS (Forzando IDs 1 al 5)
========================================================= */
SET IDENTITY_INSERT Evento.Tarjeta ON;
INSERT INTO Evento.Tarjeta (IdTarjeta, IdJugador, IdPartido, TipoTarjeta, Minuto)
VALUES 
(1, 2, 1, 'Amarilla', 25),
(2, 11, 1, 'Amarilla', 55),
(3, 11, 1, 'Roja', 80),  
(4, 5, 2, 'Amarilla', 15),
(5, 7, 2, 'Amarilla', 40);
SET IDENTITY_INSERT Evento.Tarjeta OFF;
GO

USE Futbolito;
GO

SELECT
j.IdJugador, CONCAT(p.NombreParticipante, ' [', j.Posicion, ' - ', j.Numero, ']')
FROM Persona.Jugador j
INNER JOIN Persona.Participante p
ON p.IdParticipante = j.IdParticipante
INNER JOIN Club.DetalleEquipo de
ON j.IdJugador = de.IdJugador
INNER JOIN Club.Equipo e
ON e.IdEquipo = de.IdEquipo
WHERE e.IdEquipo = 1
AND j.Estado = 'Activo';
GO