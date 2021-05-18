USE [Chinook]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[StoredProcedure2]
	(@date1 DATE,@date2 DATE )
AS
BEGIN
	

	SELECT TOP 10  Track.Name ,COUNT(InvoiceLine.TrackId) AS 'Top10Tracks'
	From Artist inner join Album
	on Artist.ArtistId = Album.ArtistId
	inner join Track
	on Track.AlbumId = Album.AlbumId
	inner join InvoiceLine
	on Track.TrackId = InvoiceLine.TrackId
	inner join Invoice
	on Invoice.InvoiceId = InvoiceLine.InvoiceId
	inner join Customer
	on Customer.CustomerId = Invoice.CustomerId
	inner join MediaType
	on Track.MediaTypeId = MediaType.MediaTypeId
	inner join Genre
	on Track.GenreId = Genre.GenreId
	
	WHERE InvoiceDate > @date1 AND InvoiceDate < @date2
	GROUP BY Track.Name  
	ORDER BY Top10Tracks DESC
   
END