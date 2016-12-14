CREATE TABLE [dbo].[QuestionAnswer]
(
	[QuestionnaireId] INT NOT NULL , 
    [QuestionId] INT NOT NULL, 
    [Result] NVARCHAR(MAX) NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY ([QuestionId], [QuestionnaireId]), 
    CONSTRAINT [FK_QuestionAnswer_Questionnaire] FOREIGN KEY ([QuestionnaireId]) REFERENCES [Questionnaire]([Id]), 
    CONSTRAINT [FK_QuestionAnswer_Question] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id])
)
