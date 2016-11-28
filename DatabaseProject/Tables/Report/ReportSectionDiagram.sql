CREATE TABLE [dbo].[ReportSectionDiagram]
(
	[ReportSectionId] INT NOT NULL , 
    [DiagramId] INT NOT NULL, 
    PRIMARY KEY ([ReportSectionId], [DiagramId]), 
    CONSTRAINT [FK_ReportSectionDiagram_ReportSectionId] FOREIGN KEY ([ReportSectionId]) REFERENCES [ReportSection]([Id]), 
    CONSTRAINT [FK_ReportSectionDiagram_Diagram] FOREIGN KEY ([DiagramId]) REFERENCES [Diagram]([Id])
)
