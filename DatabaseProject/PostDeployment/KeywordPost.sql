--Keyword categories

print 'Keyword Category script aan het uitvoeren :)';

SET IDENTITY_INSERT [KeywordCategory] ON;

insert KeywordCategory(Id, Description) select 1, 'Schade'
where not exists (select 1 from KeywordCategory where Id = 1);

insert KeywordCategory(Id, Description) select 2, 'Voertuig'
where not exists (select 1 from KeywordCategory where Id = 2);

insert KeywordCategory(Id, Description) select 1, 'Schade'
where not exists (select 1 from KeywordCategory where Id = 1);

insert KeywordCategory(Id, Description) select 3, 'Overtreding'
where not exists (select 1 from KeywordCategory where Id = 3);

SET IDENTITY_INSERT [KeywordCategory] OFF;

--Keywords

print 'Keyword Category script aan het uitvoeren :)';

SET IDENTITY_INSERT [Keyword] ON;

insert Keyword(Id, CategoryId, Description) select 1, 1, 'Autoruit'
where not exists (select 1 from Keyword where Id = 1);

insert Keyword(Id, CategoryId, Description) select 2, 1, 'Vuurschade'
where not exists (select 1 from Keyword where Id = 2);

insert Keyword(Id, CategoryId, Description) select 3, 1, 'Banden'
where not exists (select 1 from Keyword where Id = 3);

insert Keyword(Id, CategoryId, Description) select 4, 1, 'Numberbord'
where not exists (select 1 from Keyword where Id = 4);

insert Keyword(Id, CategoryId, Description) select 5, 1, 'Spiegels'
where not exists (select 1 from Keyword where Id = 5);

insert Keyword(Id, CategoryId, Description) select 6, 1, 'Lampen'
where not exists (select 1 from Keyword where Id = 6);

insert Keyword(Id, CategoryId, Description) select 7, 2, 'Auto'
where not exists (select 1 from Keyword where Id = 7);

insert Keyword(Id, CategoryId, Description) select 8, 2, 'Vrachtwagen'
where not exists (select 1 from Keyword where Id = 8);

insert Keyword(Id, CategoryId, Description) select 9, 2, 'Trike'
where not exists (select 1 from Keyword where Id = 9);

insert Keyword(Id, CategoryId, Description) select 10, 2, 'Fiets'
where not exists (select 1 from Keyword where Id = 10);

insert Keyword(Id, CategoryId, Description) select 11, 2, 'Motor'
where not exists (select 1 from Keyword where Id = 11);

insert Keyword(Id, CategoryId, Description) select 12, 3, 'Koekblik op wielen'
where not exists (select 1 from Keyword where Id = 12);

insert Keyword(Id, CategoryId, Description) select 13, 3, 'Dubbel geparkeerd'
where not exists (select 1 from Keyword where Id = 13);

insert Keyword(Id, CategoryId, Description) select 14, 3, 'Illegaal geparkeerd'
where not exists (select 1 from Keyword where Id = 14);

SET IDENTITY_INSERT [Keyword] OFF;