CREATE TABLE [Order] (
  [Id] int PRIMARY KEY,
  [CustomerId] int,
  [DatePlaced] datetime,
  [DateCompleted] datetime,
  [TotalAmt] money
)
GO

CREATE TABLE [OrderTransaction] (
  [Id] int PRIMARY KEY,
  [OrderId] int,
  [ItemId] int,
  [Qty] int,
  [ExtendedPrice] money
)
GO

CREATE TABLE [Item] (
  [Id] int PRIMARY KEY,
  [Name] varchar(63),
  [Description] varchar(255),
  [Cost] money,
  [Price] money,
  [Status] varchar(8)
)
GO

CREATE TABLE [Customer] (
  [Id] int PRIMARY KEY,
  [Name] varchar(255),
  [Address] varchar(255),
  [Phone] varchar(10),
  [Email] varchar(255),
  [IsVerified] bit,
  [LastOnline] datetime
)
GO

ALTER TABLE [OrderTransaction] ADD FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id])
GO

ALTER TABLE [OrderTransaction] ADD FOREIGN KEY ([ItemId]) REFERENCES [Item] ([Id])
GO

ALTER TABLE [Order] ADD FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id])
GO
