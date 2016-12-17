CREATE TABLE [dbo].[Diagram]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [QuestionId] INT NOT NULL, 
    [ReportSectionId] INT NOT NULL, 
    CONSTRAINT [FK_Diagram_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id]), 
    CONSTRAINT [FK_Diagram_ReportSection] FOREIGN KEY ([ReportSectionId]) REFERENCES [ReportSection]([Id])
)
