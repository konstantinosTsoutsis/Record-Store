USE [Chinook]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[StoredProcedure6]
@date DATE
AS
BEGIN
		

		SELECT Track.Name, Artist.Name as 'Name1',
		Year=DATEPART(yy,Invoice.InvoiceDate),
		Sales1 = SUM(CASE WHEN DATEPART(q,Invoice.InvoiceDate)=1 THEN InvoiceLine.Quantity ELSE 0 end),
		Sales2 =SUM(CASE WHEN DATEPART(q,Invoice.InvoiceDate)=2 THEN InvoiceLine.Quantity ELSE 0 end),
		Sales3 = SUM(CASE WHEN DATEPART(q,Invoice.InvoiceDate)=3 THEN InvoiceLine.Quantity ELSE 0 end),
		Sales4 = SUM(CASE WHEN DATEPART(q,Invoice.InvoiceDate)=4 THEN InvoiceLine.Quantity ELSE 0 end)

		FROM  Invoice inner join InvoiceLine
		on Invoice.InvoiceId = InvoiceLine.InvoiceId 
		inner join Track
		on InvoiceLine.TrackId = Track.TrackId
		inner join Album 
		on Album.AlbumId = Track.AlbumId
		inner join artist
		on Album.ArtistId = Artist.ArtistId 
		inner join MediaType
		on Track.MediaTypeId = MediaType.MediaTypeId
		inner join Genre
		on Track.GenreId = Genre.GenreId

		where DATEPART (YY , Invoice.InvoiceDate) = DATEPART (YY,@date)

		GROUP BY Track.Name, Artist.Name , Invoice.InvoiceDate , InvoiceLine.Quantity

		ORDER BY Track.Name ASC , Artist.Name ASC
END

