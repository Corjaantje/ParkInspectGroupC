print 'Questions and Module scripts aan het uitvoeren :)';

--QuestionSort
insert QuestionSort(Id, Description) select 1, 'DATE'
where not exists (select 1 from QuestionSort where Id = 1);

insert QuestionSort(Id, Description) select 2, 'INT'
where not exists (select 1 from QuestionSort where Id = 2);

insert QuestionSort(Id, Description) select 3, 'VARCHAR'
where not exists (select 1 from QuestionSort where Id = 3);

insert QuestionSort(Id, Description) select 4, 'BOOL'
where not exists (select 1 from QuestionSort where Id = 4);


--Modules
insert Module(Id, Name, Description) select 1, 'Parkeerproblemen', 'Vragen over parkeerproblemen.'
where not exists (select 1 from Module where Id = 1);

insert Module(Id, Name, Description) select 2, 'Parkeerruimte', 'Vragen over parkeerruimte.'
where not exists (select 1 from Module where Id = 2);

insert Module(Id, Name, Description) select 3, 'Zichtbaarheid', 'Vragen over zichtbaarheid.'
where not exists (select 1 from Module where Id = 3);

insert Module(Id, Name, Description) select 4, 'Bezetting', 'Vragen over bezetting.'
where not exists (select 1 from Module where Id = 4);

insert Module(Id, Name, Description) select 5, 'Calamiteiten', 'Vragen over calamiteiten.'
where not exists (select 1 from Module where Id = 5);


-- Questionnaire
insert Questionnaire(Id, InspectionId) select 1, 1
where not exists (select 1 from Questionnaire where Id = 1);

insert Questionnaire(Id, InspectionId) select 2, 3
where not exists (select 1 from Questionnaire where Id = 2);

insert Questionnaire(Id, InspectionId) select 3, 3
where not exists (select 1 from Questionnaire where Id = 3);

insert Questionnaire(Id, InspectionId) select 4, 2
where not exists (select 1 from Questionnaire where Id = 4);


-- QuestionnaireModule
insert QuestionnaireModule(ModuleId, QuestionnaireId) select 3, 1
where not exists (select 1 from QuestionnaireModule where ModuleId = 3 and QuestionnaireId = 1);

insert QuestionnaireModule(ModuleId, QuestionnaireId) select 1, 2
where not exists (select 1 from QuestionnaireModule where ModuleId = 1 and QuestionnaireId = 2);

insert QuestionnaireModule(ModuleId, QuestionnaireId) select 1, 3
where not exists (select 1 from QuestionnaireModule where ModuleId = 1 and QuestionnaireId = 3);

insert QuestionnaireModule(ModuleId, QuestionnaireId) select 2, 4
where not exists (select 1 from QuestionnaireModule where ModuleId = 2 and QuestionnaireId = 4);


--Questions
insert Question(Id,SortId, Description, ModuleId) select 1, 2, 'Wat is de breedte van een parkeervak in CM?', 2
where not exists (select 1 from Question where Id = 1);

insert Question(Id,SortId, Description, ModuleId) select 2, 2, 'Wat is de breedte van een invalide parkeervak in CM?', 2
where not exists (select 1 from Question where Id = 2);

insert Question(Id,SortId, Description, ModuleId) select 3, 2, 'Hoeveel SUVs staan er geparkeerd?', 1
where not exists (select 1 from Question where Id = 3);

insert Question(Id,SortId, Description, ModuleId) select 4, 2, 'Hoeveel invalide parkeerplaatsen zijn er bezet?', 2
where not exists (select 1 from Question where Id = 4);

insert Question(Id,SortId, Description, ModuleId) select 5, 2, 'Hoeveel meter is het zicht op de uitgang van de parkeerplaats?', 3
where not exists (select 1 from Question where Id = 5);

insert Question(Id,SortId, Description, ModuleId) select 6, 2, 'Hoeveel vrachtwagen parkeerplaatsen zijn er aanwezig?', 3
where not exists (select 1 from Question where Id = 6);

insert Question(Id,SortId, Description, ModuleId) select 7, 4, 'Zijn er ongeluk gevallen?', 5
where not exists (select 1 from Question where Id = 7);

insert Question(Id,SortId, Description, ModuleId) select 8, 2, 'Hoeveel ongeluk gevallen zijn er?', 5
where not exists (select 1 from Question where Id = 8);

insert Question(Id,SortId, Description, ModuleId) select 9, 3, 'Op welke dag is het parkeerplaats het meest bezet?', 4
where not exists (select 1 from Question where Id = 9);

-- QuestionAnswer
insert QuestionAnswer(QuestionnaireId,QuestionId, Result)select 1, 3, '20'
where not exists (select 1 from QuestionAnswer where QuestionnaireId = 1 and QuestionId = 3);

insert QuestionAnswer(QuestionnaireId,QuestionId, Result)select 1, 7, 'Ja'
where not exists (select 1 from QuestionAnswer where QuestionnaireId = 1 and QuestionId = 7);

insert QuestionAnswer(QuestionnaireId,QuestionId, Result)select 1, 8, '10'
where not exists (select 1 from QuestionAnswer where QuestionnaireId = 1 and QuestionId = 8);

insert QuestionAnswer(QuestionnaireId,QuestionId, Result)select 1, 9, 'Donderdag'
where not exists (select 1 from QuestionAnswer where QuestionnaireId = 1 and QuestionId = 9);

insert QuestionAnswer(QuestionnaireId,QuestionId, Result)select 2, 7, 'nee'
where not exists (select 1 from QuestionAnswer where QuestionnaireId = 2 and QuestionId = 7);

insert QuestionAnswer(QuestionnaireId,QuestionId, Result)select 2, 8, '0'
where not exists (select 1 from QuestionAnswer where QuestionnaireId = 2 and QuestionId = 8);

insert QuestionAnswer(QuestionnaireId,QuestionId, Result)select 2, 4, '4'
where not exists (select 1 from QuestionAnswer where QuestionnaireId = 2 and QuestionId = 4);



