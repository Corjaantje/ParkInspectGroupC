print 'Employee script aan het uitvoeren :)';

SET IDENTITY_INSERT [Employee] ON;

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, Inspecter, Manager) select 1, 'Jan', 'de', 'Visser', 'M', 'Haaren', 'Kantstraat 2a', '5076OI', '06 303 43 021', 'jvisser@parkinspect.nl', 1, 1, 1, 0
where not exists (select 1 from Employee where Id = 1);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, Inspecter, Manager) select 2, 'Urther', NULL, 'Pijnenborg', 'M', 'Tilburg', 'Berkel-Enschot 50', '5056 AC', '06 818 113 139', 'upijnenborg@parkinspect.nl', 1, 3, 1, 0
where not exists (select 1 from Employee where Id = 2);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, Inspecter, Manager) select 3, 'Jaqueline', NULL, 'Irkenbeem', 'F', 'Berkenenschot', 'Mulkenshofweg 3', '4933NJ', '06 818 113 139', 'upijnenborg@parkinspect.nl', 2, 1, 1, 1
where not exists (select 1 from Employee where Id = 3);

SET IDENTITY_INSERT [Employee] OFF;