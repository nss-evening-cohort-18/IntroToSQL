--switch to the master table
USE [master];
GO

--if the DB already exists, drop it and recreate it fresh
IF db_id('IntroToSQLDemo') IS NOT NULL
BEGIN
	DROP DATABASE [IntroToSQLDemo]
END;

CREATE DATABASE [IntroToSQLDemo];
GO

--short pause just in case
WAITFOR DELAY '00:00:03';
GO

--target the new empty DB
USE [IntroToSQLDemo];
GO

--create tables
CREATE TABLE [Item] (
  [Id] INTEGER PRIMARY KEY IDENTITY,
  [Name] VARCHAR(63) NOT NULL,
  [Description] VARCHAR(255) NOT NULL,
  [Cost] MONEY NOT NULL,
  [Price] MONEY NOT NULL,
  [Status] CHAR(1) NOT NULL,
  [LastUpdated] DATETIME NOT NULL,
  [StockQty] INTEGER NOT NULL
)
GO

CREATE TABLE [Customer] (
  [Id] INTEGER PRIMARY KEY IDENTITY,
  [Name] VARCHAR(255) NOT NULL,
  [Address] VARCHAR(255) NOT NULL,
  [Phone] VARCHAR(10) NOT NULL,
  [Email] VARCHAR(255) NOT NULL,
  [IsVerified] BIT NOT NULL,
  [Created] DATETIME NOT NULL,
  [LastOnline] DATETIME NOT NULL
)
GO

CREATE TABLE [Order] (
  [Id] INTEGER PRIMARY KEY IDENTITY(10001,1), --first Order ID will be 10001 and they will increment by 1 after that
  [CustomerId] INTEGER FOREIGN KEY REFERENCES [Customer]([Id]) NOT NULL,
  [DatePlaced] DATETIME NOT NULL,
  [DateCompleted] DATETIME
)
GO

CREATE TABLE [OrderTransaction] (
  [Id] INTEGER PRIMARY KEY IDENTITY,
  [OrderId] INTEGER FOREIGN KEY REFERENCES [Order]([Id]) NOT NULL,
  [ItemId] INTEGER FOREIGN KEY REFERENCES [Item]([Id]) NOT NULL,
  [Qty] INTEGER NOT NULL,
  [ExtendedPrice] MONEY NOT NULL
)
GO

--Customer data
INSERT INTO [Customer] ([Name],
						[Address],
						[Phone],
						[Email],
						[IsVerified],
						[Created],
						[LastOnline])
VALUES ('Brian Neal',
		'',
		'6155551234',
		'b@b.com',
		1,
		'2020-04-20 12:00:00',
		GETDATE());
INSERT INTO [Customer] ([Name],
						[Address],
						[Phone],
						[Email],
						[IsVerified],
						[Created],
						[LastOnline])
VALUES ('Amy Farrah-Fowler',
		'123 Sheldon Towers Apt H',
		'6151234567',
		'amy@brain.edu',
		1,
		'2021-09-23 11:52:07',
		'2022-12-23 12:00:00');
INSERT INTO [Customer] ([Name],
						[Address],
						[Phone],
						[Email],
						[IsVerified],
						[Created],
						[LastOnline])
VALUES ('Biz Nasty',
		'501 Broadway',
		'6159999999',
		'goon@nhl.com',
		0,
		'2022-03-11 12:00:00',
		'2022-08-25 10:00:00');
INSERT INTO [Customer] ([Name],
						[Address],
						[Phone],
						[Email],
						[IsVerified],
						[Created],
						[LastOnline])
VALUES ('Joe Shmo',
		'',
		'6151119876',
		'joe@shmo.com',
		0,
		GETDATE(),
		GETDATE());

--Item data
INSERT INTO [Item] ([Name],
					[Description],
					[Cost],
					[Price],
					[Status],
					[LastUpdated],
					[StockQty])
VALUES ('Beach Ball',
		'Fun for everyone!',
		1.00,
		5.00,
		'A',
		GETDATE(),
		250);
INSERT INTO [Item] ([Name],
					[Description],
					[Cost],
					[Price],
					[Status],
					[LastUpdated],
					[StockQty])
VALUES ('Reading Glasses',
		'If you can read this, you don''t need them',
		9.00,
		25.00,
		'A',
		GETDATE(),
		15);
INSERT INTO [Item] ([Name],
					[Description],
					[Cost],
					[Price],
					[Status],
					[LastUpdated],
					[StockQty])
VALUES ('iPad Pro',
		'You so fancy.',
		0.90,
		1099.99,
		'A',
		GETDATE(),
		4);
INSERT INTO [Item] ([Name],
					[Description],
					[Cost],
					[Price],
					[Status],
					[LastUpdated],
					[StockQty])
VALUES ('iPad Pro Case',
		'Don''t drop it.',
		0.05,
		50.00,
		'A',
		GETDATE(),
		15);
INSERT INTO [Item] ([Name],
					[Description],
					[Cost],
					[Price],
					[Status],
					[LastUpdated],
					[StockQty])
VALUES ('50 Shades of Gray',
		'Comes with discreet book jacket.',
		7.00,
		15.00,
		'A',
		GETDATE(),
		0);

--Order data
INSERT INTO [Order] ([CustomerId],
					 [DatePlaced],
					 [DateCompleted])
VALUES ((SELECT Id FROM Customer WHERE [Name] LIKE 'Brian%'),
		'2020-04-20 12:05:00',
		'2020-04-21 08:00:00');
INSERT INTO [Order] ([CustomerId],
					 [DatePlaced],
					 [DateCompleted])
VALUES ((SELECT Id FROM Customer WHERE [Name] LIKE 'Amy%'),
		'2021-09-23 11:52:07.000',
		'2021-09-24 10:00:00.000');
INSERT INTO [Order] ([CustomerId],
					 [DatePlaced],
					 [DateCompleted])
VALUES ((SELECT Id FROM Customer WHERE [Name] LIKE 'Joe%'),
		GETDATE(),
		NULL);
INSERT INTO [Order] ([CustomerId],
					 [DatePlaced],
					 [DateCompleted])
VALUES ((SELECT Id FROM Customer WHERE [Name] LIKE 'Brian%'),
		GETDATE(),
		NULL);

--Order Transaction data
INSERT INTO [OrderTransaction] ([ItemId],
								[OrderId],
								[Qty],
								[ExtendedPrice])
VALUES (1,
		10001,
		400,
		3.50);
INSERT INTO [OrderTransaction] ([ItemId],
								[OrderId],
								[Qty],
								[ExtendedPrice])
VALUES (1,
		10004,
		1,
		5.00);
INSERT INTO [OrderTransaction] ([ItemId],
								[OrderId],
								[Qty],
								[ExtendedPrice])
VALUES (2,
		10002,
		1,
		25.00);
INSERT INTO [OrderTransaction] ([ItemId],
								[OrderId],
								[Qty],
								[ExtendedPrice])
VALUES (5,
		10002,
		1,
		12.99);
INSERT INTO [OrderTransaction] ([ItemId],
								[OrderId],
								[Qty],
								[ExtendedPrice])
VALUES (4,
		10003,
		1,
		40.00);
INSERT INTO [OrderTransaction] ([ItemId],
								[OrderId],
								[Qty],
								[ExtendedPrice])
VALUES (3,
		10003,
		1,
		850.00);
