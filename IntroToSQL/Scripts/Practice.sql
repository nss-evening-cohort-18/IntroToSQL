--get all customers
SELECT *
FROM Customer;

--get all orders
SELECT *
FROM [Order];

--Try to DELETE a customer with an order and see what happens. The FOREIGN KEY we defined is being enforced automatically!
DELETE FROM Customer WHERE Id = 3;

-- get customer names
SELECT [Name]
FROM Customer;

--get names of customers who need to provide an address
SELECT [Name]
FROM Customer
WHERE [Address] = '';

--Get ALL customers and any orders (no transaction info), even if they don't have an order
SELECT *
FROM Customer
LEFT OUTER JOIN [Order] ON [Order].CustomerId = Customer.Id
ORDER BY Customer.Id, [Order].Id;

--Get ALL customers and any orders (no transaction info), but only if they don't have an order
SELECT *
FROM Customer
INNER JOIN [Order] ON [Order].CustomerId = Customer.Id
ORDER BY Customer.Id ASC, [Order].Id DESC;

--Get customers and all order/transaction/item info, but only if they have an order
SELECT c.Name AS CustomerName,
	   [Order].Id AS OrderId,
	   i.Name AS ItemName,
	   ExtendedPrice * Qty AS LineTotal
FROM Customer c
INNER JOIN [Order] ON CustomerId = c.Id
INNER JOIN OrderTransaction ON OrderId = [Order].Id
LEFT JOIN Item i ON i.Id = ItemId

SELECT c.Name AS CustomerName, --use the alias to reference the table with shorthand
	   o.Id AS OrderNumber,
	   i.Name AS ItemName,
	   i.Description AS ItemDescription,
	   ot.ExtendedPrice AS ItemPrice,
	   ot.Qty AS ItemQty
FROM Customer c  --ALIAS, a name to reference something
INNER JOIN [Order] o ON o.CustomerId = c.Id
INNER JOIN OrderTransaction ot ON ot.OrderId = o.Id
INNER JOIN Item i ON i.Id = ot.ItemId
ORDER BY o.Id, ot.Id;

--GROUP BY - used with aggregation
--aggregation in SQL - SUM, AVG, COUNT
SELECT COUNT(*) AS OrderCount
FROM [Order]

SELECT COUNT(*) AS OrderCount
FROM [Order]
LEFT JOIN Customer ON CustomerId = Customer.Id

SELECT Customer.Name, COUNT(*) AS OrderCount
FROM [Order]
LEFT JOIN Customer ON CustomerId = Customer.Id
GROUP BY Customer.Name


SELECT AVG(OrderTotal) AS OrderAvg FROM
(SELECT o.Id, SUM(ot.ExtendedPrice * ot.Qty) AS OrderTotal
FROM [Order] o
INNER JOIN OrderTransaction ot ON ot.OrderId = o.Id
GROUP BY o.Id) AS Totals
WHERE OrderTotal < 1400



SELECT * FROM Customer

UPDATE Customer
SET [Name] = 'Mr. Brian',
    Created = '2020-01-01 01:01:01'
WHERE [Name] LIKE 'Brian%'

SELECT DISTINCT i.Name AS ItemName
FROM [Order] o
INNER JOIN OrderTransaction ON OrderId = o.Id
LEFT JOIN Item i ON i.Id = ItemId


--  not equals <>   !=  NOT NULL
SELECT MAX(Qty)
FROM OrderTransaction


SELECT TOP 1 *
FROM Item
ORDER BY Price DESC

SELECT *
FROM Item
WHERE Price = (SELECT MAX(Price) FROM Item)

SELECT *
FROM Item
WHERE Price BETWEEN 20 AND 100

SELECT *
FROM Item
WHERE Name IN ('Beach Ball', 'Reading Glasses')


SELECT c.Name AS CustomerName,
	   [Order].Id AS OrderId,
	   i.Name AS ItemName,
	   ExtendedPrice * Qty AS LineTotal
FROM Customer c
INNER JOIN [Order] ON CustomerId = c.Id
INNER JOIN OrderTransaction ON OrderId = [Order].Id
LEFT JOIN Item i ON i.Id = ItemId
WHERE i.Name IN ('Beach Ball', 'Reading Glasses')

IF EXISTS(SELECT * FROM Item WHERE [Name] = 'Beach Bal')
SELECT 'True'
ELSE SELECT 'False'

SELECT CASE
  WHEN EXISTS(SELECT * FROM Item WHERE [Name] = 'Beach Bal')
  THEN 'TRUE'
  ELSE 'FALSE'
  END

SELECT [Name],
	CASE
		WHEN StockQty = 0
		THEN 1
		ELSE StockQty
	END AS StockQty
FROM Item
