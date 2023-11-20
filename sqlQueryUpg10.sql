USE NorthWind
GO

/*Prints all avaliable products ordered by category then name.*/
SELECT * FROM Products
ORDER BY CategoryID, ProductName

/*Prints amount of customer orders and the company name in descending order by amount of orders.*/
SELECT Count(Orders.OrderID) as NumberOfOrders, CompanyName FROM Orders
JOIN Customers ON Orders.CustomerID = Customers.CustomerID
GROUP BY CompanyName
ORDER BY NumberOfOrders DESC

/*Prints all employees and what territorie they work within*/
SELECT E.FirstName, E.LastName, T.TerritoryDescription FROM Employees E
JOIN EmployeeTerritories ET ON E.EmployeeID = ET.EmployeeID
JOIN Territories T ON T.TerritoryID = ET.TerritoryID
ORDER BY TerritoryDescription