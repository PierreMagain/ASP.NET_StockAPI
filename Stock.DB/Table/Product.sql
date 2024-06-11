CREATE TABLE [dbo].[Product]
(
  [Id] INT PRIMARY KEY IDENTITY(1,1),
  [Name] VARCHAR(100) NOT NULL,
  [Brand] VARCHAR(100) NULL,
  [Description] VARCHAR(MAX) NULL,
  [ExpiryDate] DATE NULL,
  [PriceExcludingTax] DECIMAL(18, 2) NOT NULL,
  [VAT] DECIMAL(18, 2) NOT NULL,
  [QuantityInStock] INT NOT NULL,
  [QuantityInShelf] INT NOT NULL,
  CONSTRAINT CK_Product__Name CHECK (LEN(TRIM(Name)) >= 1),
  CONSTRAINT CK_Product__Price CHECK (PriceExcludingTax >= 0),
  CONSTRAINT CK_Product__VAT CHECK (VAT >= 0)
);
