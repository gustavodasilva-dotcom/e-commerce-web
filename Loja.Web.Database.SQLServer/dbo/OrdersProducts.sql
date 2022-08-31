CREATE TABLE [dbo].[OrdersProducts]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Quantity]		INT					NOT NULL,
	[Amount]		MONEY				NOT NULL,
	[Unitary]		MONEY				NOT NULL,
	[OrderID]		INT					NOT NULL,
	[ProductID]		INT					NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_OrderProduct_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_OrderProduct_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_OrderProduct_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]	INT					NULL,
	[Deleted_at]	DATETIME			NULL,
	[Deleted_by]	INT					NULL

	CONSTRAINT [PK_OrdersProducts] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_OrdersProducts_OrderID] FOREIGN KEY([OrderID])
	REFERENCES Orders([ID]),

	CONSTRAINT [FK_OrdersProducts_ProductID] FOREIGN KEY([ProductID])
	REFERENCES Products([ID])
)
