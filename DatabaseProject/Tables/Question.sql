CREATE TABLE [dbo].[Question]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Description] VARCHAR(MAX) NOT NULL, 
	[SortId] INT NOT NULL, 
    [ModuleId] INT NULL, 
    CONSTRAINT [FK_Question_Module] FOREIGN KEY ([ModuleId]) REFERENCES [Module]([Id]), 
    CONSTRAINT [FK_Question_QuestionSort] FOREIGN KEY ([SortId]) REFERENCES [QuestionSort]([Id])
)
