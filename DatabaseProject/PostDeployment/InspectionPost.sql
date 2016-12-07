print 'Inspection script aan het uitvoeren :)';

insert Inspection(Id, AssignmentId, RegionId, Location, StartDate, EndDate, StatusId, InspectorId) select 1, 1, 1, 'Haaren', '2016-12-02 00:00:00.000', '2016-12-31 00:00:00.000', 3, 1
where not exists(select 1 from Inspection where Id = 1);

insert Inspection(Id, AssignmentId, RegionId, Location, StartDate, EndDate, StatusId, InspectorId) select 2, 2, 10, 's-Hertogenbosch', '2016-12-02 00:00:00.000', '2016-12-31 00:00:00.000', 4, 8
where not exists(select 1 from Inspection where Id = 2);

insert Inspection(Id, AssignmentId, RegionId, Location, StartDate, EndDate, StatusId, InspectorId) select 3, 3, 10, 's-Hertogenbosch', '2016-12-02 00:00:00.000', '2016-12-31 00:00:00.000', 4, 7
where not exists(select 1 from Inspection where Id = 3);

insert Inspection(Id, AssignmentId, RegionId, Location, StartDate, EndDate, StatusId, InspectorId) select 4, 1, 1, 'Haaren', '2016-12-02 00:00:00.000', '2016-12-31 00:00:00.000', 4, 2
where not exists(select 1 from Inspection where Id = 4);

insert Inspection(Id, AssignmentId, RegionId, Location, StartDate, EndDate, StatusId, InspectorId) select 5, 4, 10, 's-Hertogenbosch', '2016-12-02 00:00:00.000', '2016-12-11 00:00:00.000', 4, 4
where not exists(select 1 from Inspection where Id = 5);

insert Inspection(Id, AssignmentId, RegionId, Location, StartDate, EndDate, StatusId, InspectorId) select 6, 5, 2, 'Tilburg', '2016-12-02 00:00:00.000', '2017-01-11 00:00:00.000', 4, 5
where not exists(select 1 from Inspection where Id = 6);
