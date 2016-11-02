CREATE TABLE [dbo].[Question]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[SortId] INT NOT NULL, 
	[Description] VARCHAR(MAX) NOT NULL, 
    [ModuleId] INT NULL, 
    CONSTRAINT [FK_Question_Module] FOREIGN KEY ([ModuleId]) REFERENCES [Module]([Id]), 
    CONSTRAINT [FK_Question_QuestionSort] FOREIGN KEY ([SortId]) REFERENCES [QuestionSort]([Id])
)
