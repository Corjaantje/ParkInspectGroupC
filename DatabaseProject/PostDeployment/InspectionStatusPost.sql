print 'InspectionStatus script aan het uitvoeren :)';

insert InspectionStatus select 1, 'Reviewing'
where not exists (select 1 from InspectionStatus where Id = 1);

insert InspectionStatus select 2, 'Closed'
where not exists (select 1 from InspectionStatus where Id = 2);

insert InspectionStatus select 3, 'Inspecting'
where not exists (select 1 from InspectionStatus where Id = 3);

insert InspectionStatus select 4, 'Open'
where not exists (select 1 from InspectionStatus where Id = 4);