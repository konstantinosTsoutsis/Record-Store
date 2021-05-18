USE [Chinook]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[StoredProcedure5]
	(@CustomerLastName VARCHAR(30) ,@CustomerFirstName VARCHAR(30) ,@EmployeeLastName VARCHAR(30) ,@EmployeeFirstName VARCHAR(30),
	@date1 DATE,@date2 DATE)
AS
BEGIN

	

SELECT Invoice.InvoiceId,
		InvoiceDate,
		Customer.FirstName,
		Customer.LastName,	 
		Employee.FirstName as 'FirstName1',
		Employee.LastName as 'LastName1'
	
	

	FROM Invoice inner join InvoiceLine
	on Invoice.InvoiceId = InvoiceLine.InvoiceId
	inner join Customer
	on Customer.CustomerId = Invoice.CustomerId
	inner join Employee
	on Customer.SupportRepId = Employee.EmployeeId

	WHERE InvoiceDate > @date1 AND InvoiceDate < @date2

	AND Customer.LastName = @CustomerLastName
	AND Customer.FirstName = @CustomerFirstName
	AND Employee.LastName = @EmployeeLastName
	AND Employee.FirstName = @EmployeeFirstName
	AND Customer.SupportRepId = Employee.EmployeeId
	

	GROUP BY Invoice.InvoiceId , InvoiceDate  , Customer.LastName, Customer.FirstName , Employee.LastName, Employee.FirstName 
END

