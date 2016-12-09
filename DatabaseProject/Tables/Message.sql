CREATE TABLE [dbo].[Message]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [SenderId] INT NOT NULL, 
    [ReceiverId] INT NOT NULL, 
    [Message] VARCHAR(MAX) NOT NULL, 
	[Read] BIT NULL, 
    [DateCreated] DATETIME NOT NULL, 
    CONSTRAINT [FK_Message_Sender] FOREIGN KEY ([SenderId]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [FK_Message_Receiver] FOREIGN KEY ([ReceiverId]) REFERENCES [Employee]([Id])
)