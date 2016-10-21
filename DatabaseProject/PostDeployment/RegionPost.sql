print 'Regio script aan het uitvoeren :)';

insert Region select 1, 'Haaren'
where not exists (select 1 from Region where Id = 1);

insert Region select 2, 'Tilburg'
where not exists (select 1 from Region where Id = 2);

insert Region select 3, 'Groningen'
where not exists (select 1 from Region where Id = 3);

insert Region select 4, 'Heereveen'
where not exists (select 1 from Region where Id = 4);

insert Region select 5, 'Amsterdam'
where not exists (select 1 from Region where Id = 5);

insert Region select 6, 'Biesbosch'
where not exists (select 1 from Region where Id = 6);

insert Region select 7, 'Maastricht'
where not exists (select 1 from Region where Id = 7);

insert Region select 8, 'Rotterdam'
where not exists (select 1 from Region where Id = 8);

insert Region select 9, 'Lelystad'
where not exists (select 1 from Region where Id = 9);