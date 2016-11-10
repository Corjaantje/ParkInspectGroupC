CREATE TABLE [dbo].[QuestionKeyword]
(
	[QuestionId] INT NOT NULL, 
    [KeywordId] INT NOT NULL, 
    PRIMARY KEY ([KeywordId], [QuestionId]), 
    CONSTRAINT [FK_QuestionKeyword_Question] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id]), 
    CONSTRAINT [FK_QuestionKeyword_Keyword] FOREIGN KEY ([KeywordId]) REFERENCES [Keyword]([Id]),
)
