CREATE TABLE [dbo].[ShoppingCartsProducts]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Quantity]			INT					NOT NULL,
	[ProductID]			INT					NOT NULL,
	[ShoppingCartID]	INT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_ShoppingCartProduct_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_ShoppingCartProduct_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_ShoppingCartProduct_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_ShoppingCartsProducts] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_ShoppingCartsProducts_ShoppingCartID] FOREIGN KEY([ShoppingCartID])
	REFERENCES ShoppingCarts([ID]),

	CONSTRAINT [FK_ShoppingCartsProducts_ProductID] FOREIGN KEY([ProductID])
	REFERENCES Products([ID])
)
