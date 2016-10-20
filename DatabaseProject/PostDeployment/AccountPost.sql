print 'Account scripts aan het uitvoeren :)';

insert Account select 1, 'piepschuimer', '1234'
where not exists (select 1 from Account where Id = 1);

insert Account select 2, 'javaloverger', 'csharpsucks'
where not exists (select 1 from Account where Id = 2);

insert Account select 3, 'HenkParktem', '1020304050'
where not exists (select 1 from Account where Id = 3);

insert Account select 4, 'darkplats', 'parkeertmscheef'
where not exists (select 1 from Account where Id = 4);

