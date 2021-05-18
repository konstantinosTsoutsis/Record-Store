USE [Chinook]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[StoredProcedure4]
	@date1 DATE,@date2 DATE 
AS
BEGIN
	


SELECT		Customer.CustomerId,
			FirstName,
			LastName, 
			Phone, 
			Fax, 
			Email, 
			turnovernew,
			SUM(Invoice.Total) AS turnover
	
FROM		Customer,
			Invoice  

where	    Customer.CustomerId = Invoice.CustomerId AND
			Invoice.InvoiceDate > @date1 and Invoice.InvoiceDate < @date2

GROUP BY Customer.CustomerId, 
			FirstName, 
			LastName, 
			Phone, 
			Fax, 
			Email,
			turnovernew 

ORDER BY turnover DESC

   
END
