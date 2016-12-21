CREATE TABLE [dbo].[Question]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[SortId] INT NOT NULL, 
	[Description] VARCHAR(MAX) NOT NULL, 
    [ModuleId] INT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Question_Module] FOREIGN KEY ([ModuleId]) REFERENCES [Module]([Id]), 
    CONSTRAINT [FK_Question_QuestionSort] FOREIGN KEY ([SortId]) REFERENCES [QuestionSort]([Id])
)
