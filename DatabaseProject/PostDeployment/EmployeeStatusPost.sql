print 'Employee status script aan het uitvoeren :)';


insert EmployeeStatus select 1, 'Beschikbaar'
where not exists (select 1 from EmployeeStatus where Id = 1);

insert EmployeeStatus select 2, 'Ziek'
where not exists (select 1 from EmployeeStatus where Id = 2);

insert EmployeeStatus select 3, 'Verlof'
where not exists (select 1 from EmployeeStatus where Id = 3);

insert EmployeeStatus select 4, 'Ontslagen'
where not exists (select 1 from EmployeeStatus where Id = 4);

insert EmployeeStatus select 5, 'Dood'
where not exists (select 1 from EmployeeStatus where Id = 5);