CREATE TABLE [dbo].[ProductCategories]
(
  [Id] INT,
  [ProductId] INT NOT NULL,
  [CategoryId] INT NOT NULL,
  PRIMARY KEY (ProductId, CategoryId),
  CONSTRAINT FK_ProductCategories_Product FOREIGN KEY (ProductId) REFERENCES Product(Id),
  CONSTRAINT FK_ProductCategories_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
)
