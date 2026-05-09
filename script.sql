IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Alumnos] (
    [AlumnoId] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NOT NULL,
    [Apellido] nvarchar(max) NOT NULL,
    [FechaNacimiento] datetime2 NOT NULL,
    [Grado] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Alumnos] PRIMARY KEY ([AlumnoId])
);
GO

CREATE TABLE [Materias] (
    [MateriaId] int NOT NULL IDENTITY,
    [NombreMateria] nvarchar(max) NOT NULL,
    [Docente] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Materias] PRIMARY KEY ([MateriaId])
);
GO

CREATE TABLE [Expedientes] (
    [ExpedienteId] int NOT NULL IDENTITY,
    [AlumnoId] int NOT NULL,
    [MateriaId] int NOT NULL,
    [NotaFinal] real NOT NULL,
    [Observaciones] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Expedientes] PRIMARY KEY ([ExpedienteId]),
    CONSTRAINT [FK_Expedientes_Alumnos_AlumnoId] FOREIGN KEY ([AlumnoId]) REFERENCES [Alumnos] ([AlumnoId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Expedientes_Materias_MateriaId] FOREIGN KEY ([MateriaId]) REFERENCES [Materias] ([MateriaId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Expedientes_AlumnoId] ON [Expedientes] ([AlumnoId]);
GO

CREATE INDEX [IX_Expedientes_MateriaId] ON [Expedientes] ([MateriaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260509014817_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

