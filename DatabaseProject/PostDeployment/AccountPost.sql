print 'Account script aan het uitvoeren :)';

insert Account select 1, 'piepschuimer', '1234', '84a71932-47bb-4160-8dff-24b946aad99d'
where not exists (select 1 from Account where Id = 1);

insert Account select 2, 'javaloverger', 'csharpsucks', '3fb53269-0aa4-4a2d-99bb-644fe4cf211e'
where not exists (select 1 from Account where Id = 2);

insert Account select 3, 'HenkParktem', '1020304050', 'e17388e5-f976-4e1a-9844-2c4c441a299d'
where not exists (select 1 from Account where Id = 3);

insert Account select 4, 'darkplats', 'parkeertmscheef', '5ffed576-99f2-4d8d-a006-483bef32a3f0'
where not exists (select 1 from Account where Id = 4);

insert Account select 5, 'jvisser', 'jvisservistniet', '3c12f1e1-e29d-4366-99c7-a7ba33ebadc3'
where not exists (select 1 from Account where Id = 5);

insert Account select 6, 'upijnenborg', '2bdkcvnwj3kN@dj', '1b477366-b642-472a-b547-8943741ab347'
where not exists (select 1 from Account where Id = 6);

insert Account select 7, 'jirkenbeem', 'ponnylover86', 'e19dcf0e-8e5e-4232-94f7-5e54269b3c82'
where not exists (select 1 from Account where Id = 7);


-- Niet meer geheel bruikbaar na veranderingen
/*
insert Account select 1, 'piepschuimer', '1234'
where not exists (select 1 from Account where Id = 1);

insert Account select 2, 'javaloverger', 'csharpsucks'
where not exists (select 1 from Account where Id = 2);

insert Account select 3, 'HenkParktem', '1020304050'
where not exists (select 1 from Account where Id = 3);

insert Account select 4, 'darkplats', 'parkeertmscheef'
where not exists (select 1 from Account where Id = 4);

insert Account select 5, 'jvisser', 'jvisservistniet'
where not exists (select 1 from Account where Id = 5);

insert Account select 6, 'upijnenborg', '2bdkcvnwj3kN@dj'
where not exists (select 1 from Account where Id = 6);

insert Account select 7, 'jirkenbeem', 'ponnylover86'
where not exists (select 1 from Account where Id = 7);
*/