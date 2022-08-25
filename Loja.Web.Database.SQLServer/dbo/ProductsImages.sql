CREATE TABLE [dbo].[ProductsImages]
(
	[ID]			INT			NOT NULL IDENTITY(1, 1),
	[ProductID]		INT			NOT NULL,
	[ImageID]		INT			NOT NULL,
	[Active]		BIT			CONSTRAINT [DF_ProductImage_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT			CONSTRAINT [DF_ProductImage_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME	CONSTRAINT [DF_ProductImage_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_ProductsImages] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_ProductsImages_ProductID] FOREIGN KEY ([ProductID])
	REFERENCES Products([ID]),

	CONSTRAINT [FK_ProductsImages_ImageID] FOREIGN KEY ([ImageID])
	REFERENCES Images([ID])
)
