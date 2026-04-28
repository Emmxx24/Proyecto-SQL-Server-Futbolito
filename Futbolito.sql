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
	REFERENCES Persona.Jugador (IdJugador)
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
	REFERENCES Juego.Torneo (IdTorneo)
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
	REFERENCES Evento.Partido (IdPartido)
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


/*Prueba para verificar que todo este bien*/
SELECT IdTorneo, NombreTorneo FROM Juego.Torneo;
SELECT IdEquipo, NombreEquipo FROM Club.Equipo;

INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (1, 18); -- America
INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (1, 19); -- Chivas

INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (3, 18)
INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (3, 19)

INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (4, 18)
INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (4, 19)

INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (1, 20)
INSERT INTO Juego.DetalleTorneo (IdTorneo, IdEquipo) VALUES (1, 21)

INSERT INTO 