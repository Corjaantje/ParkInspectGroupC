CREATE TABLE [dbo].[ReportSection]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(MAX) NOT NULL, 
    [Summary] VARCHAR(MAX) NULL, 
    [ReportId] INT NOT NULL, 
    CONSTRAINT [FK_ReportSection_Report] FOREIGN KEY ([ReportId]) REFERENCES [Report]([Id]), 
)
