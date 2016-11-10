CREATE TABLE [dbo].[Keyword]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CategoryId] INT NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Keyword_KeywordCategory] FOREIGN KEY ([CategoryId]) REFERENCES [KeywordCategory]([Id])
)
