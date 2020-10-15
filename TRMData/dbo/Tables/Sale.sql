CREATE TABLE [dbo].[Sale]
(
	Id INT NOT NULL PRIMARY KEY identity,
	CashierId nvarchar(128) not null, 
    [SaleDate] DATETIME2 NOT NULL, 
    [SubTotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [Total] MONEY NOT NULL,
)
