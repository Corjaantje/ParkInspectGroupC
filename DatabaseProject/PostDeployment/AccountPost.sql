print 'Account script aan het uitvoeren :)';

insert Account select 1, 'piepschuimer', 'FA1C8B6C8F8AFFC60F8128612B0ABDD649904997', '84a71932-47bb-4160-8dff-24b946aad99d'
where not exists (select 1 from Account where Id = 1);

insert Account select 2, 'javaloverger', '86718B168C689CA46E1E6314C86013FB130CD9A7', '3fb53269-0aa4-4a2d-99bb-644fe4cf211e'
where not exists (select 1 from Account where Id = 2);

insert Account select 3, 'HenkParktem', '79119B1441F6DCD402710C93936F6EC9BD9EE0B7', 'e17388e5-f976-4e1a-9844-2c4c441a299d'
where not exists (select 1 from Account where Id = 3);

insert Account select 4, 'darkplats', 'C7D5A2017A3B1E861FC95DDE2581F75FD5741C41', '5ffed576-99f2-4d8d-a006-483bef32a3f0'
where not exists (select 1 from Account where Id = 4);

insert Account select 5, 'jvisser', '3A694CEC5B81F115519AD40CE15DD5CDD1A4D003', '3c12f1e1-e29d-4366-99c7-a7ba33ebadc3'
where not exists (select 1 from Account where Id = 5);

insert Account select 6, 'upijnenborg', 'FF0A24741128B134A9BCC6E0025B2A792D386522', '1b477366-b642-472a-b547-8943741ab347'
where not exists (select 1 from Account where Id = 6);

insert Account select 7, 'jirkenbeem', '34E65CFFEE32BC9C978B52DDB86656A45FBFDD06', 'e19dcf0e-8e5e-4232-94f7-5e54269b3c82'
where not exists (select 1 from Account where Id = 7);