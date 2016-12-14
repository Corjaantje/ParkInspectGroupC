CREATE TABLE [dbo].[Messages]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [SenderId] INT NOT NULL, 
    [ReceiverId] INT NOT NULL, 
    [Message] VARCHAR(MAX) NOT NULL, 
	[Read] BIT NOT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    CONSTRAINT [FK_MessageSender] FOREIGN KEY ([SenderId]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [FK_MessageReceiver] FOREIGN KEY ([ReceiverId]) REFERENCES [Employee]([Id])
)