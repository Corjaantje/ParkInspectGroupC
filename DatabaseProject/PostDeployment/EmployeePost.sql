print 'Employee script aan het uitvoeren :)';

SET IDENTITY_INSERT [Employee] ON;

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, IsManager) select 3, 'Jaqueline', NULL, 'Irkenbeem', 'F', 'Berkenenschot', 'Mulkenshofweg 3', '4933 NJ', '06 818 113 139', 'upijnenborg@parkinspect.nl', 2, 1, 0, 1
where not exists (select 1 from Employee where Id = 3);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, ManagerId) select 1, 'Jan', 'de', 'Visser', 'M', 'Haaren', 'Kantstraat 2a', '5076 OI', '06 303 43 021', 'jvisser@parkinspect.nl', 1, 1, 1, 3
where not exists (select 1 from Employee where Id = 1);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, ManagerId) select 2, 'Urther', NULL, 'Pijnenborg', 'M', 'Tilburg', 'Berkel-Enschot 50', '5056 AC', '06 818 113 139', 'upijnenborg@parkinspect.nl', 1, 3, 1, 3
where not exists (select 1 from Employee where Id = 2);
-- new
insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, ManagerId) select 4, 'Ana', 'de', 'Windt', 'F', 'Maastricht', 'Akerstraat 34', '6221 CL', '06 434 163 193', 'awindt@parkinspect.nl', 7, 1, 1, 3
where not exists (select 1 from Employee where Id = 4);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, ManagerId) select 5, 'Bob', NULL, 'Bouwman', 'M', 'Tilburg', 'Amperstraat 67', '5021 NE', '06 321 567 235', 'bbouwman@parkinspect.nl', 2, 1, 1, 3
where not exists (select 1 from Employee where Id = 5);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, IsManager) select 6, 'Martijn', NULL, 'Vogels', 'M', 'Haaren', 'Beekweg 5', '5076 PT', '06 234 421 897', 'mvogels@parkinspect.nl', 1, 1, 0, 1
where not exists (select 1 from Employee where Id = 6);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, ManagerId) select 7, 'Michael', NULL, 'Schagen', 'M', 'Lelystad', 'Apolloweg 6', '8239 DB', '06 456 378 225', 'mschagen@parkinspect.nl', 9, 1, 1, 5
where not exists (select 1 from Employee where Id = 7);
insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, ManagerId) select 8, 'Stijn', NULL, 'Meersen', 'M', 'Rotterdam', 'Achterharingvliet 123', '3011 TD', '06 167 789 368', 'smeersen@parkinspect.nl', 8, 1, 1, 5
where not exists (select 1 from Employee where Id = 8);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, ManagerId) select 9, 'Bas', NULL, 'Bakker', 'M', 'Amsterdam', 'Barndesteeg 27', '1012 BV', '06 232 343 637', 'bbakker@parkinspect.nl', 5, 1, 1, 5
where not exists (select 1 from Employee where Id = 9);

insert Employee(Id, FirstName, Prefix, SurName, Gender, City, Address, ZipCode, Phonenumber, Email, RegionId, EmployeeStatusId, IsInspecter, IsManager) select 10, 'Max', 'de', 'Jong', 'M', 'Tilburg', 'Reeshof 38', '5045 DR', '06 783 267 493', 'mdjong@parkinspect.nl', 2, 1, 0, 1
where not exists (select 1 from Employee where Id = 10);

SET IDENTITY_INSERT [Employee] OFF;