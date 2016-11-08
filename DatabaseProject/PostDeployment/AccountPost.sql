print 'Account script aan het uitvoeren :)';

insert Account select 1, 'jvisser', '3A694CEC5B81F115519AD40CE15DD5CDD1A4D003', '3c12f1e1-e29d-4366-99c7-a7ba33ebadc3', 1
where not exists (select 1 from Account where Id = 1);

insert Account select 2, 'upijnenborg', 'FF0A24741128B134A9BCC6E0025B2A792D386522', '1b477366-b642-472a-b547-8943741ab347', 2
where not exists (select 1 from Account where Id = 2);

insert Account select 3, 'jirkenbeem', '34E65CFFEE32BC9C978B52DDB86656A45FBFDD06', 'e19dcf0e-8e5e-4232-94f7-5e54269b3c82', 3
where not exists (select 1 from Account where Id = 3);