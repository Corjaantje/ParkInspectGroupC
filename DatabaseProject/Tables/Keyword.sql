CREATE TABLE [dbo].[Keyword]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [CategoryId] INT NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_Keyword_KeywordCategory] FOREIGN KEY ([CategoryId]) REFERENCES [KeywordCategory]([Id])
)
