print 'Assignment script aan het uitvoeren :)';

insert Assignment select 1, 3, 3, 'Altijd file, wil graag weten hoe dat kan.', '2016-10-20', '2016-11-03'
where not exists (select 1 from Assignment where Id = 1);


insert Assignment select 2, 2, 3, 'Parkeerplaats bij supermarkt staat altijd leeg, wil graag weten hoe dat komt.', '2016-10-25', '2016-11-25'
where not exists (select 1 from Assignment where Id = 2);

insert Assignment select 3, 1, 3, 'Er ligt altijd piepschuim op de parkeerplaatsen, wil graag weten waar dat vandaan komt.', '2016-10-1', '2016-10-15'
where not exists (select 1 from Assignment where Id = 3);