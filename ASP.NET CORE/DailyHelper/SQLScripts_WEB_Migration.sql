﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF SCHEMA_ID(N'Budget') IS NULL EXEC(N'CREATE SCHEMA [Budget];');

GO

CREATE TABLE [Budget].[Category] (
    [IdCategory] int NOT NULL IDENTITY,
    [Name] nvarchar(150) NOT NULL,
    [CategoryType] int NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([IdCategory])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200428201754_InitialMigration', N'3.1.3');

GO

IF SCHEMA_ID(N'Budget') IS NULL EXEC(N'CREATE SCHEMA [Expense];');

GO

IF SCHEMA_ID(N'Budget') IS NULL EXEC(N'CREATE SCHEMA [Income];');

GO

CREATE TABLE [Budget].[Expense] (
    [IdExpense] int NOT NULL IDENTITY,
    [AddedTime] datetime2 NOT NULL,
    [Description] nvarchar(150) NOT NULL,
    [TotalSum] decimal(18,2) NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Expense] PRIMARY KEY ([IdExpense]),
    CONSTRAINT [FK_Expense_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([IdCategory]) ON DELETE CASCADE
);

GO

CREATE TABLE [Budget].[Income] (
    [IdIncome] int NOT NULL IDENTITY,
    [AddedTime] datetime2 NOT NULL,
    [Description] nvarchar(150) NOT NULL,
    [TotalSum] decimal(18,2) NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Income] PRIMARY KEY ([IdIncome]),
    CONSTRAINT [FK_Income_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([IdCategory]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Expense_CategoryId] ON [Budget].[Expense] ([CategoryId]);

GO

CREATE INDEX [IX_Income_CategoryId] ON [Budget].[Income] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200428202130_AddedIncomeAndExpenseTables', N'3.1.3');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Budget].[Income]') AND [c].[name] = N'AddedTime');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Budget].[Income] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Budget].[Income] DROP COLUMN [AddedTime];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Budget].[Expense]') AND [c].[name] = N'AddedTime');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Budget].[Expense] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Budget].[Expense] DROP COLUMN [AddedTime];

GO

ALTER TABLE [Budget].[Income] ADD [AddedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Budget].[Expense] ADD [AddedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200429112432_ModifyItemsDateColumn', N'3.1.3');

GO

CREATE TABLE [Budget].[History] (
    [IdHistory] int NOT NULL IDENTITY,
    [DeletedDate] datetime2 NOT NULL,
    [Description] nvarchar(150) NOT NULL,
    [TotalSum] decimal(18,2) NOT NULL,
    [CategoryId] int NOT NULL,
    [ItemType] nvarchar(max) NULL,
    CONSTRAINT [PK_History] PRIMARY KEY ([IdHistory]),
    CONSTRAINT [FK_History_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([IdCategory]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_History_CategoryId] ON [Budget].[History] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200429122853_AddedHistoryTable', N'3.1.3');

GO

/*
DROP TABLE Budget.History;

GO ALTER TABLE Expense.Expense
DROP CONSTRAINT FK_Expense_History_ExpenseId;
GO
ALTER TABLE Income.Income
DROP CONSTRAINT FK_Income_History_IncomeId;

ALTER TABLE Expense.Expense
DROP INDEX IX_Expense_ExpenseId;

ALTER TABLE Epxense.Expense
DROP COLUMN ExpenseId;

DROP TABLE Income.Income;
DROP TABLE Expense.Expense;
DROP TABLE Budget.History;

DROP TABLE Budget.Category;	
*/

----------------- DDL ---------------------

------------ Category ---------------
/*internationalization..... */

INSERT INTO Budget.Category VALUES ('Salary', 0), ('Freelance', 0), ('Rent for House', 0), ('Other Incomes', 0);
INSERT INTO Budget.Category VALUES ('Products', 1), ('Transport', 1), ('Mobile Connection', 1), ('Internet', 1), ('Games', 1), ('Education', 1), ('Others', 1);

SELECT * FROM Budget.Category;

------ Income -----

INSERT INTO Budget.Income VALUES ('Website', 500, 3, '2020-04-29'), ('Yunusabad', 300, 4, '2020-04-30'), ('Mirabad', 350, 4, '2020-04-30');

SELECT i.Description, i.TotalSum, i.AddedDate, c.Name as Category
FROM Budget.Income i, Budget.Category c
WHERE i.CategoryId = c.IdCategory;

UPDATE Budget.Income SET Description = 'Yunusabad' WHERE IdIncome = 10;
UPDATE Budget.Income SET Description = 'Mirobod' WHERE IdIncome = 11;

SELECT * FROM Budget.Income;

-------------- Expense ------------------------

INSERT INTO Budget.Expense VALUES ('Weekly', 50, 6, '2020-04-20'), ('Proezdnoj', 50, 7, '2020-04-01'), ('University Fee', 3000, 11, '2020-01-01'), ('Online Courses', 30, 11, '2020-03-15');

SELECT e.Description, e.TotalSum, e.AddedDate, c.Name as Category
FROM Budget.Expense e, Budget.Category c
WHERE e.CategoryId = c.IdCategory;

SELECT * FROM Budget.Expense;

---- History -----

INSERT INTO Budget.History VALUES ('2020-05-05', 'Weekly', 50, 6, 'Expense');

SELECT * FROM Budget.History;

DELETE FROM Budget.History WHERE IdHistory > 1;

--------------- Triggers ---------------

CREATE OR ALTER TRIGGER Budget.CheckCategoryType ON Budget.Income
    AFTER INSERT  
AS  
    IF (ROWCOUNT_BIG() = 0)
RETURN;
    IF EXISTS (SELECT *  
               FROM Budget.Category AS p   
               JOIN inserted AS i   
               ON p.IdCategory = i.CategoryId   
               WHERE p.CategoryType = 1
              )
BEGIN  
    RAISERROR ('Cannot insert Expense category type into Income item', 1, 1);  
    ROLLBACK TRANSACTION;  
    RETURN;
END;

CREATE TRIGGER Budget.CheckCategoryTypeExpense ON Budget.Expense
    AFTER INSERT  
AS  
    IF (ROWCOUNT_BIG() = 0)
RETURN;
IF EXISTS (SELECT *  
           FROM Budget.Category AS p   
           JOIN inserted AS i   
           ON p.IdCategory = i.CategoryId   
           WHERE p.CategoryType = 0
          )
BEGIN  
    RAISERROR ('Cannot insert Expense category type into Income item', 1, 1);  
    ROLLBACK TRANSACTION;  
    RETURN;
END;


CREATE TRIGGER Budget.DeletedIncomeToHistory
    ON Budget.Income
    FOR DELETE
AS
  BEGIN 
    INSERT INTO HISTORY(Description, TotalSum, CategoryId, DeletedDate, ItemType)
       SELECT 
           d.Description, d.TotalSum, d.CategoryId, GETDATE(), 'Income'
       FROM deleted d;

       PRINT 'Successfully inserted into history table';
  END
GO
/*
test
SELECT * FROM Budget.Income;
DELETE FROM Budget.Income WHERE IdIncome = 7;
SELECT * FROM Budget.Income;
SELECT * FROM Budget.History;
*/




CREATE TRIGGER Budget.DeletedExpenseToHistory
    ON Budget.Expense
    FOR DELETE
AS
  BEGIN 
    INSERT INTO HISTORY(Description, TotalSum, CategoryId, DeletedDate, ItemType)
       SELECT 
           d.Description, d.TotalSum, d.CategoryId, GETDATE(), 'Expense'
       FROM deleted d;

       PRINT 'Successfully inserted into history table';
  END
GO

/* test
SELECT * FROM Budget.Expense;
DELETE FROM Budget.Expense WHERE IdExpense = 4;
SELECT * FROM Budget.Expense;
*/


/* Test trigger 
INSERT INTO Budget.Income VALUES (
INSERT INTO Budget.Income VALUES ('Micros', 1500, 6, '2020-04-29')
SELECT * FROM Budget.Category;
SELECT * FROM Budget.History;
*/



---------- Procedures ---------------



CREATE PROCEDURE Budget.AddIncome  (@description   VARCHAR(150),
                                    @totalSum     DECIMAL(18,2),
                                    @categoryId   INT)
AS  
  BEGIN  
        BEGIN  
            IF (Budget.CheckItem(@description, @totalSum, @categoryId, 'Income') > 0)
                RAISERROR ('Already existing item', 1, 2);
            ELSE
                INSERT INTO Budget.Income  
                VALUES     ( @description, @totalSum, @categoryId, GETDATE())  
        END 

        PRINT ('New item was successfully added');
END

/* test
EXECUTE Budget.AddIncome 'Test', 50, 2;
SELECT * FROM Budget.Income;
GO
*/


CREATE PROCEDURE Budget.AddExpense  (@description   VARCHAR(150),
                                      @totalSum      DECIMAL(18,2),
                                      @categoryId    INT)
AS  
  BEGIN  
        BEGIN  
            IF (Budget.CheckItem(@description, @totalSum, @categoryId, 'Income') > 0)
                RAISERROR ('Already existing item', 1, 2);
            ELSE
                INSERT INTO Budget.Expense  
                VALUES     ( @description, @totalSum, @categoryId, GETDATE())  
        END 

        PRINT ('New item was successfully added');
END

/* test
EXECUTE Budget.AddExpense 'Test2', 50, 6;
SELECT * FROM Budget.Expense;
GO
*/


--------- Function ------------
/* Check if we want to add already existing data */

CREATE FUNCTION Budget.CheckItem (@description   VARCHAR(150),
                                  @totalSum      DECIMAL(18,2),
                                  @categoryId    INT,
								  
                                  @itemType      VARCHAR(20))
RETURNS INT
WITH EXECUTE AS CALLER
AS
BEGIN
    DECLARE @countItem INT;

    -- Check if we want to count on Income or Expense table ----
    IF(@itemType='Income')
        SET @countItem = (SELECT COUNT(*)
                          FROM Budget.Income
                          WHERE Description = @description AND TotalSum = @totalSum
                                                    AND CategoryId = @categoryId);

    ELSE IF(@itemType='Expense')
        SET @countItem = (SELECT COUNT(*)
                          FROM Budget.Expense
                          WHERE Description = @description AND TotalSum = @totalSum
                                                    AND CategoryId = @categoryId);
    
    IF @countItem > 0
        RETURN @countItem;
    
    RETURN 0;
END;
GO

/*
SELECT * FROM Budget.Income;
SELECT Budget.CheckItem('Micros', 1500, 2, 'Income') AS 'Number of items';
SELECT Budget.CheckItem('Test', 50, 6, 'Income') AS 'Number of items in expense';

---Stupid Date!!! It should work actually... -----
SELECT * FROM Budget.Expense;
SELECT Budget.CheckItem('Test', 50, 6, 'Expense') AS 'Number of items in expense';
*/

select * from Budget.Category WHERE CategoryType = 1;

SELECT * FROM Budget.Income;
SELECT * FROM Budget.Expense;