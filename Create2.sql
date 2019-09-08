
USE [LearningOutcomeDB];
GO

DROP TABLE [LearningOutcomes];
DROP TABLE [CourseInstance];

CREATE TABLE [CourseInstance] (
    [CourseInstanceID] int NOT NULL IDENTITY PRIMARY KEY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
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

CREATE TABLE [EvaluationMetrics] (
	[EMID] int NOT NULL IDENTITY PRIMARY KEY,
	[Name] nvarchar(max) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[LOID] int NOT NULL,
	FOREIGN KEY ([LOID]) REFERENCES [LearningOutcomes] ([LOID]) ON DELETE CASCADE
);
GO

CREATE TABLE [SampleFiles] (
	[SID] int NOT NULL IDENTITY PRIMARY KEY,
	[Score] int NOT NULL,
	[FileName] nvarchar(max),
	[EMID] int NOT NULL,
	FOREIGN KEY ([EMID]) REFERENCES [EvaluationMetrics] ([EMID]) ON DELETE CASCADE
);
GO