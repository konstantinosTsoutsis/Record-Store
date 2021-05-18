USE [Chinook]
GO
/****** Object:  StoredProcedure [dbo].[BestArtistsAlbums]    Script Date: 12/2/2021 4:29:13 μμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE  [dbo].[StoredProcedure1]
	(@numberOfArtists int ,@date1 DATE,@date2 DATE )
AS 
BEGIN
	
	


    SELECT top( @numberOfArtists)  Artist.Name  ,count(Album.Title) as 'sales'
	From  Artist inner join Album
	on Artist.ArtistId = Album.ArtistId
	inner join Track
	on Track.AlbumId = Album.AlbumId
	inner join InvoiceLine
	on Track.TrackId = InvoiceLine.TrackId
	inner join Invoice
	on Invoice.InvoiceId = InvoiceLine.InvoiceId
	where InvoiceDate > @date1 AND InvoiceDate < @date2
	group by Artist.Name 
	order by sales desc
END
