CREATE TABLE [dbo].[ProductsRatings]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Rating]			INT					NOT NULL,
	[ProductID]			INT					NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_ProductRating_Created_at] DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL
	
	CONSTRAINT [PK_ProductsRatings] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_ProductsRatings_ProductID] FOREIGN KEY([ProductID])
	REFERENCES Products([ID])
)
