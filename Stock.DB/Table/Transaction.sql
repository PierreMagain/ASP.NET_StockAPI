CREATE TABLE [dbo].[Transaction] 
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [TransactionDate] DATETIME NOT NULL,
    [TransactionType] VARCHAR(50) NOT NULL CONSTRAINT CHK_Transaction_TransactionType CHECK (TransactionType IN ('ADD', 'MOVE', 'REMOVE')),
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL CONSTRAINT CHK_Transaction_Quantity CHECK (Quantity >= 0),
    [Source] VARCHAR(50),
    [Destination] VARCHAR(50),
    [Reason] VARCHAR(255),
    CONSTRAINT FK_Transaction_Product FOREIGN KEY (ProductId) REFERENCES Product(Id)
);
