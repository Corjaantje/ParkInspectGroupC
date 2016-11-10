print 'InspectionStatus script aan het uitvoeren :)';

SET IDENTITY_INSERT [InspectionStatus] ON;

insert InspectionStatus(Id, Description) select 1, 'Reviewing'
where not exists (select 1 from InspectionStatus where Id = 1);

insert InspectionStatus(Id, Description) select 2, 'Closed'
where not exists (select 1 from InspectionStatus where Id = 2);

insert InspectionStatus(Id, Description) select 3, 'Inspecting'
where not exists (select 1 from InspectionStatus where Id = 3);

insert InspectionStatus(Id, Description) select 4, 'Open'
where not exists (select 1 from InspectionStatus where Id = 4);

SET IDENTITY_INSERT [InspectionStatus] OFF;