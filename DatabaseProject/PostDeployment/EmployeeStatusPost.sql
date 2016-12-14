print 'Employee status script aan het uitvoeren :)';

SET IDENTITY_INSERT [EmployeeStatus] ON;

insert EmployeeStatus(Id, Description) select 1, 'Beschikbaar'
where not exists (select 1 from EmployeeStatus where Id = 1);

insert EmployeeStatus(Id, Description) select 2, 'Ziek'
where not exists (select 1 from EmployeeStatus where Id = 2);

insert EmployeeStatus(Id, Description) select 3, 'Verlof'
where not exists (select 1 from EmployeeStatus where Id = 3);

insert EmployeeStatus(Id, Description) select 4, 'Ontslagen'
where not exists (select 1 from EmployeeStatus where Id = 4);

SET IDENTITY_INSERT [EmployeeStatus] OFF;