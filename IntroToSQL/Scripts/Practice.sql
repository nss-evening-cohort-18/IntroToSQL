--get all customers
SELECT *
FROM Customer;

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
ORDER BY Customer.Id, [Order].Id;

--Get customers and all order/transaction/item info, but only if they have an order
SELECT c.Name AS CustomerName,
	   o.Id AS OrderNumber,
	   i.Name AS ItemName,
	   i.Description AS ItemDescription,
	   ot.ExtendedPrice AS ItemPrice,
	   ot.Qty AS ItemQty
FROM Customer c
INNER JOIN [Order] o ON o.CustomerId = c.Id
INNER JOIN OrderTransaction ot ON ot.OrderId = o.Id
INNER JOIN Item i ON i.Id = ot.ItemId
ORDER BY o.Id, ot.Id;