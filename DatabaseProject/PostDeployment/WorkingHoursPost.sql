-- Availability
print 'Availability script aan het uitvoeren :)';

insert Availability(EmployeeId, Date, StartTime, EndTime) select 1, '2016-12-2', '10:20', '20:00'
where not exists (select 1 from Availability where EmployeeId = 1 and Date = '2016-12-02');

insert Availability(EmployeeId, Date, StartTime, EndTime) select 1, '2016-12-10', '10:20', '20:00'
where not exists (select 1 from Availability where EmployeeId = 1 and Date = '2016-12-10');

insert Availability(EmployeeId, Date, StartTime, EndTime) select 1, '2016-12-12', '10:20', '20:00'
where not exists (select 1 from Availability where EmployeeId = 1 and Date = '2016-12-12');

insert Availability(EmployeeId, Date, StartTime, EndTime) select 2, '2016-12-13', '9:45', '17:00'
where not exists (select 1 from Availability where EmployeeId = 2 and Date = '2016-12-13');

insert Availability(EmployeeId, Date, StartTime, EndTime) select 2, '2016-12-20', '9:45', '17:00'
where not exists (select 1 from Availability where EmployeeId = 2 and Date = '2016-12-20');

insert Availability(EmployeeId, Date, StartTime, EndTime) select 2, '2016-12-25', '15:00', '17:00'
where not exists (select 1 from Availability where EmployeeId = 2 and Date = '2016-12-25');

-- Working
print 'Working script aan het uitvoeren :)';

insert WorkingHours(EmployeeId, Date, StartTime, EndTime) select 1, '2016-12-10', '15:00', '17:00'
where not exists (select 1 from WorkingHours where EmployeeId = 1 and Date = '2016-12-10');

insert WorkingHours(EmployeeId, Date, StartTime, EndTime) select 1, '2016-12-12', '12:00', '18:00'
where not exists (select 1 from WorkingHours where EmployeeId = 1 and Date = '2016-12-12');

insert WorkingHours(EmployeeId, Date, StartTime, EndTime) select 2, '2016-12-13', '12:00', '18:00'
where not exists (select 2 from WorkingHours where EmployeeId = 2 and Date = '2016-12-13');
