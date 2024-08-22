SELECT * FROM [GEC].dbo.OrderItems;
SELECT * FROM [GEC].dbo.Orders;
SELECT * FROM [GEC].dbo.Products;
SELECT * FROM [GEC].dbo.Users;
SELECT * FROM [GEC].dbo.__EFMigrationsHistory;

INSERT INTO [GEC].dbo.Products (ProductId, Name, Description, Price, Stock, CreatedOn, UpdatedOn, Status)
VALUES (NEWID(), 'Tshirt1', 'Tshirts', 20.5, 5, GETDATE(), GETDATE(), 1);
INSERT INTO [GEC].dbo.Products (ProductId, Name, Description, Price, Stock, CreatedOn, UpdatedOn, Status)
VALUES (NEWID(), 'Tshirt2', 'Tshirts', 20.5, 5, GETDATE(), GETDATE(), 1);
INSERT INTO [GEC].dbo.Products (ProductId, Name, Description, Price, Stock, CreatedOn, UpdatedOn, Status)
VALUES (NEWID(), 'Tshirt3', 'Tshirts', 20.5, 5, GETDATE(), GETDATE(), 1);
INSERT INTO [GEC].dbo.Products (ProductId, Name, Description, Price, Stock, CreatedOn, UpdatedOn, Status)
VALUES (NEWID(), 'Tshirt4', 'Tshirts', 20.5, 5, GETDATE(), GETDATE(), 1);
INSERT INTO [GEC].dbo.Products (ProductId, Name, Description, Price, Stock, CreatedOn, UpdatedOn, Status)
VALUES (NEWID(), 'Tshirt5', 'Tshirts', 20.5, 5, GETDATE(), GETDATE(), 1);

DELETE FROM [GEC].dbo.Users;