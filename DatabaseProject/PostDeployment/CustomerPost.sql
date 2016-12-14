print 'Customer script aan het uitvoeren :)';

SET IDENTITY_INSERT [Customer] ON;

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 1, 'H. Piepschuim', 'Hogeschoollaan 2915', 'Den Bosch', '088 525 7500', 'piepschuim@avans.nl'
where not exists (select 1 from Customer where Id = 1);

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 2, 'Ger Haris', 'SarisLaan 1', 'Den Bosch', '06 666 666', 'gharis@avans.nl'
where not exists (select 1 from Customer where Id = 2);

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 3, 'Henk Parktem', 'Parkeerlaan 4a', 'Haaren', '06 304 201 2', 'fampartkem@gmail.com'
where not exists (select 1 from Customer where Id = 3);

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 4, 'Derek Plaats', 'Parkeerlaan 5', 'Haaren', '06 748 173 212', 'park@hotmail.nl'
where not exists (select 1 from Customer where Id = 4);

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 5, 'Jeroen de Vries', 'Henri Dunanstraat 1', 's-Hertogenbosch', '07 355 532 000', 'jdevries@hotmail.nl'
where not exists (select 1 from Customer where Id = 5);

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 6, 'Max Driessen', 'Spoorlaan 7', 'Oisterwijk', '09 004 466 880', 'maxd@hotmail.nl'
where not exists (select 1 from Customer where Id = 6);

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 7, 'Alexander Saase', 'Burgemeester loeffplein 70', 'Tilburg', '06 346 236 889', 'alexander-Saase@hotmail.nl'
where not exists (select 1 from Customer where Id = 7);

insert Customer(Id, Name, Address, Location, Phonenumber, Email) select 8, 'Suzie Klaassen', 'Maijweg', 's-Hertogenbosch', '06 478 193 256', 'sklaassen@hotmail.nl'
where not exists (select 1 from Customer where Id = 8);

SET IDENTITY_INSERT [Customer] OFF;