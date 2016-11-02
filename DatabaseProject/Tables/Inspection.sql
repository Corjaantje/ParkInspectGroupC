CREATE TABLE [dbo].[Inspection]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[AssignmentId] INT NOT NULL, 
    [RegionId] INT NOT NULL, 
    [Location] VARCHAR(50) NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [StatusId] INT NOT NULL, 
    [InspectorId] INT NULL, 
    CONSTRAINT [FK_Inspection_Assignment] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignment]([Id]), 
    CONSTRAINT [FK_Inspection_InspectionStatus] FOREIGN KEY ([StatusId]) REFERENCES [InspectionStatus]([Id]), 
    CONSTRAINT [FK_Inspection_Region] FOREIGN KEY ([RegionId]) REFERENCES [Region]([Id]), 
    CONSTRAINT [FK_Inspection_Employee] FOREIGN KEY ([InspectorId]) REFERENCES [Employee]([Id]) 
)
