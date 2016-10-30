print 'Questions and Module scripts aan het uitvoeren :)';

--QuestionSort
insert QuestionSort select 1, 'DATE'
where not exists (select 1 from QuestionSort where Id = 1);

insert QuestionSort select 2, 'INT'
where not exists (select 1 from QuestionSort where Id = 2);

insert QuestionSort select 3, 'VARCHAR'
where not exists (select 1 from QuestionSort where Id = 3);

insert QuestionSort select 4, 'BIT'
where not exists (select 1 from QuestionSort where Id = 4);

--Modules
insert Module select 1, 'Parkeerproblemen', 'Vragen over parkeerproblemen.'
where not exists (select 1 from Module where Id = 1);

insert Module select 2, 'Parkeerruimte', 'Vragen over parkeerruimte.'
where not exists (select 1 from Module where Id = 2);

insert Module select 3, 'Zichtbaarheid', 'Vragen over zichtbaarheid.'
where not exists (select 1 from Module where Id = 3);

--Questions
insert Question select 1, 2, 'Wat is de breedte van een parkeervak in CM?', 2
where not exists (select 1 from Question where Id = 1);

insert Question select 2, 2, 'Wat is de breedte van een invalide parkeervak in CM?', 2
where not exists (select 1 from Question where Id = 2);

insert Question select 3, 2, 'Hoeveel SUVs staan er geparkeerd?', 1
where not exists (select 1 from Question where Id = 3);

insert Question select 4, 2, 'Hoeveel invalide parkeerplaatsen zijn er bezet?', 2
where not exists (select 1 from Question where Id = 4);

insert Question select 5, 2, 'Hoeveel meter is het zicht op de uitgang van de parkeerplaats?', 3
where not exists (select 1 from Question where Id = 5);

insert Question select 6, 2, 'Hoeveel vrachtwagen parkeerplaatsen zijn er aanwezig?', 3
where not exists (select 1 from Question where Id = 6);

--Questionnaire
insert Questionnaire select 1, 1
where not exists (select 1 from Questionnaire where Id = 1);

insert Questionnaire select 2, 1
where not exists (select 1 from Questionnaire where Id = 2);

insert Questionnaire select 3, 2
where not exists (select 1 from Questionnaire where Id = 3);

insert Questionnaire select 4, 3
where not exists (select 1 from Questionnaire where Id = 4);