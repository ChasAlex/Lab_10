SELECT P.ProductName,P.UnitPrice, C.CategoryName
FROM Products AS P
JOIN Categories AS C ON P.CategoryID = C.CategoryID
ORDER BY C.CategoryName,P.ProductName;


SELECT Customers.CustomerID, Customers.ContactName, COUNT(Orders.OrderID) AS TotalOrders
FROM Customers
LEFT JOIN Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.CustomerID, Customers.ContactName
ORDER BY TotalOrders DESC;


SELECT Employees.EmployeeID, Employees.FirstName, Employees.LastName, Territories.TerritoryDescription
FROM Employees
INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID;
