CREATE TABLE [dbo].[AssignmentReport]
(
	[AssignmentId] INT NOT NULL , 
    [ReportId] INT NOT NULL, 
    PRIMARY KEY ([ReportId], [AssignmentId]), 
    CONSTRAINT [FK_AssignmentReport_Assignment] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignment]([Id]), 
    CONSTRAINT [FK_AssignmentReport_Report] FOREIGN KEY ([ReportId]) REFERENCES [Report]([Id])
)
