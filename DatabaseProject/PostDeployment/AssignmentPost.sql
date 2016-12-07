print 'Assignment script aan het uitvoeren :)';

SET IDENTITY_INSERT [Assignment] ON;

insert Assignment(Id, CustomerId, ManagerId, Description, StartDate, EndDate) select 1, 3, 3, 'Altijd file, wil graag weten hoe dat kan.', '2016-10-20', '2016-11-03'
where not exists (select 1 from Assignment where Id = 1);

insert Assignment(Id, CustomerId, ManagerId, Description, StartDate, EndDate) select 2, 2, 3, 'Parkeerplaats bij supermarkt staat altijd leeg, wil graag weten hoe dat komt.', '2016-10-25', '2016-11-25'
where not exists (select 1 from Assignment where Id = 2);

insert Assignment(Id, CustomerId, ManagerId, Description, StartDate, EndDate) select 3, 1, 3, 'Er ligt altijd piepschuim op de parkeerplaatsen, wil graag weten waar dat vandaan komt.', '2016-12-04', '2017-02-04'
where not exists (select 1 from Assignment where Id = 3);

insert Assignment(Id, CustomerId, ManagerId, Description, StartDate, EndDate) select 4, 5, 5, 'Krijgt veel klacthen van werknemers dat de parkeerplaats vol zit, wi; weten hoe dit komt.', '2016-12-04', '2017-01-15'
where not exists (select 1 from Assignment where Id = 4);

insert Assignment(Id, CustomerId, ManagerId, Description, StartDate, EndDate) select 5, 7, 5, 'Parkeerplaats staat meestal leeg, wil graag weten hoe dat komt.', '2017-01-10', '2017-02-09'
where not exists (select 1 from Assignment where Id = 5);

SET IDENTITY_INSERT [Assignment] ON;