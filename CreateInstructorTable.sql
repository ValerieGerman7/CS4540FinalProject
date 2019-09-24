CREATE TABLE [Instructors] (
	[CourseInstanceID] int NOT NULL,
	[InstructorID] nvarchar(450) NOT NULL,
	[InstructorTitle] varchar(50),
	UNIQUE([CourseInstanceID], [InstructorID]),
	FOREIGN KEY ([CourseInstanceID]) REFERENCES [CourseInstance] ([CourseInstanceID]) ON DELETE CASCADE,
	FOREIGN KEY ([InstructorID]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);