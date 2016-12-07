print 'Regio script aan het uitvoeren :)';

SET IDENTITY_INSERT [Region] ON;

insert Region(Id, Region) select 1, 'Haaren'
where not exists (select 1 from Region where Id = 1);

insert Region(Id, Region) select 2, 'Tilburg'
where not exists (select 1 from Region where Id = 2);

insert Region(Id, Region) select 3, 'Groningen'
where not exists (select 1 from Region where Id = 3);

insert Region(Id, Region) select 4, 'Heereveen'
where not exists (select 1 from Region where Id = 4);

insert Region(Id, Region) select 5, 'Amsterdam'
where not exists (select 1 from Region where Id = 5);

insert Region(Id, Region) select 6, 'Biesbosch'
where not exists (select 1 from Region where Id = 6);

insert Region(Id, Region) select 7, 'Maastricht'
where not exists (select 1 from Region where Id = 7);

insert Region(Id, Region) select 8, 'Rotterdam'
where not exists (select 1 from Region where Id = 8);

insert Region(Id, Region) select 9, 'Lelystad'
where not exists (select 1 from Region where Id = 9);

insert Region(Id, Region) select 10, 's-Hertogenbosch'
where not exists (select 1 from Region where Id = 10);

SET IDENTITY_INSERT [Region] OFF;