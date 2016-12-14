CREATE TABLE [dbo].[Question]
(
	[Id] INT IDENTITY(1,1) NOT NULL,  
	[SortId] INT NOT NULL, 
	[Description] VARCHAR(MAX) NOT NULL, 
    [ModuleId] INT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_Question] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Question_Module] FOREIGN KEY ([ModuleId]) REFERENCES [Module]([Id]), 
    CONSTRAINT [FK_Question_QuestionSort] FOREIGN KEY ([SortId]) REFERENCES [QuestionSort]([Id])
)
