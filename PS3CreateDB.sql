CREATE DATABASE [LearningOutcomeDB];
GO

USE [LearningOutcomeDB];
GO

CREATE TABLE [CourseInstance] (
    [CourseInstanceID] int NOT NULL IDENTITY PRIMARY KEY,
    [Name] nvarchar(max) NOT NULL,
    [Descripton] nvarchar(max) NOT NULL,
    [Department] nvarchar(5) NOT NULL,
    [Number] int NOT NULL,
	[Semester] nvarchar(6) NOT NULL,
	[Year] int NOT NULL,
	UNIQUE ([Department], [Number], [Semester], [Year])
);
GO

CREATE TABLE [LearningOutcomes] (
    [LOID] int NOT NULL IDENTITY PRIMARY KEY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max),
    [CourseInstanceID] int NOT NULL,
    FOREIGN KEY ([CourseInstanceID]) REFERENCES [CourseInstance] ([CourseInstanceID]) ON DELETE CASCADE
);
GO