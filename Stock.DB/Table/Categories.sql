CREATE TABLE [dbo].[Categories]
(
  [Id] INT PRIMARY KEY IDENTITY(1,1),
  [Name] VARCHAR(100) NOT NULL,
  CONSTRAINT CK_Categories__Name CHECK (LEN(TRIM(Name)) >= 1),
)
