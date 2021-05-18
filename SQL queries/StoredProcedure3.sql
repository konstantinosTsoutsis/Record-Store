USE [Chinook]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[StoredProcedure3]
(@timelessGenre  int )
	
AS
BEGIN	
SET NOCOUNT ON;
	 


	SELECT  DISTINCT TOP(@timelessGenre) Genre.Name , Genre.GenreId,COUNT(Genre.GenreId) AS 'BestOfGenres'
	FROM Artist inner join Album
	on Artist.ArtistId = Album.ArtistId
	inner join Track
	on Track.AlbumId = Album.AlbumId
	inner join Genre
	on Genre.GenreId = Track.GenreId
	inner join InvoiceLine
	on Track.TrackId = InvoiceLine.TrackId
	inner join Invoice
	on Invoice.InvoiceId = InvoiceLine.InvoiceId
	inner join Customer 
	on Customer.CustomerId = Invoice.CustomerId
	inner join Employee
	on Customer.SupportRepId = Employee.EmployeeId
	GROUP BY Genre.Name , Genre.GenreId
	ORDER BY BestOfGenres DESC

END
