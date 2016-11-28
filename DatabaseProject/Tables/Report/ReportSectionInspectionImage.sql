CREATE TABLE [dbo].[ReportSectionInspectionImage]
(
	[ReportSectionId] INT NOT NULL , 
    [InspectionImageId] INT NOT NULL, 
    PRIMARY KEY ([ReportSectionId], [InspectionImageId]), 
    CONSTRAINT [FK_ReportSectionInspectionImage_ReportSection] FOREIGN KEY ([ReportSectionId]) REFERENCES [ReportSection]([Id]), 
    CONSTRAINT [FK_ReportSectionInspectionImage_InspectionImage] FOREIGN KEY ([InspectionImageId]) REFERENCES [InspectionImage]([Id])
)
