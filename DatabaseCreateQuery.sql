CREATE TABLE [UserLocator] (
	[ID] int NOT NULL IDENTITY PRIMARY KEY,
	[UserLoginEmail] nvarchar(450) NOT NULL,
	[UserTitle] nvarchar(50)
);
GO
CREATE TABLE [CourseStatus] (
	[StatusID] int NOT NULL IDENTITY PRIMARY KEY,
	[Status] nvarchar(200)
);
GO
CREATE TABLE [Departments] (
	[Name] nvarchar(400),
	[Code] nvarchar(5) PRIMARY KEY
);
GO
CREATE TABLE [CourseInstance] (
	[CourseInstanceID] int NOT NULL IDENTITY PRIMARY KEY,
	[Name] nvarchar(max) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[Department] nvarchar(5) NOT NULL,
	[Number] int NOT NULL,
	[Semester] nvarchar(6) NOT NULL,
	[Year] int NOT NULL,
	[StatusID] int,
	[DueDate] DATE NOT NULL,
	UNIQUE ([Department], [Number], [Semester], [Year]),
	FOREIGN KEY ([StatusID]) REFERENCES [CourseStatus] ([StatusID]) ON DELETE SET NULL,
	FOREIGN KEY ([Department]) REFERENCES [Departments] ([Code])
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
CREATE TABLE [Instructors] (
	[IKey] int NOT NULL IDENTITY PRIMARY KEY,
	[CourseInstanceID] int NOT NULL,
	[UserID] int NOT NULL,
	UNIQUE([CourseInstanceID], [UserID]),
	FOREIGN KEY ([CourseInstanceID]) REFERENCES [CourseInstance] ([CourseInstanceID]) ON DELETE CASCADE,
	FOREIGN KEY ([UserID]) REFERENCES [UserLocator] ([ID])
);
GO
CREATE TABLE [CourseNotes] (
	[NoteId] int NOT NULL IDENTITY PRIMARY KEY,
	[Note] nvarchar(max),
	[NoteModified] DATE,
	[CourseInstanceID] int NOT NULL,
	FOREIGN KEY ([CourseInstanceID]) REFERENCES [CourseInstance] ([CourseInstanceID]) ON DELETE CASCADE
);
GO
CREATE TABLE [LONotes] (
	[NoteId] int NOT NULL IDENTITY PRIMARY KEY,
	[Note] nvarchar(max),
	[NoteModified] DATE,
	[NoteUserModified] nvarchar(450),
	[LOID] int NOT NULL,
	FOREIGN KEY ([LOID]) REFERENCES [LearningOutcomes] ([LOID]) ON DELETE CASCADE
);
GO
CREATE TABLE [Notifications] (
	[UserID] int NOT NULL PRIMARY KEY,
	[Text] nvarchar(max) NOT NULL,
	[DateNotified] DATE NOT NULL,
	FOREIGN KEY ([UserID]) REFERENCES [UserLocator] ([ID]) ON DELETE CASCADE
);
GO
CREATE TABLE [Messages] (
	[ID] int NOT NULL IDENTITY PRIMARY KEY,
	[Text] nvarchar(max),
	[Date] DATE,
	[Sender] int NOT NULL,
	[Receiver] int NOT NULL,
	FOREIGN KEY ([Sender]) REFERENCES [UserLocator] ([ID]),
	FOREIGN KEY ([Receiver]) REFERENCES [UserLocator] ([ID])
);
GO