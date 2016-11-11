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

SET IDENTITY_INSERT [Customer] OFF;