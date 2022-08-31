CREATE TABLE [dbo].[Orders]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Total]				MONEY				NOT NULL,					
	[UserID]			INT					NOT NULL,
	[PaymentMethodID]	INT					NOT NULL,
	[OrderStatusID]		INT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_Order_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_Order_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_Order_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Orders_UserID] FOREIGN KEY([UserID])
	REFERENCES Users([ID]),

	CONSTRAINT [FK_Orders_OrderStatusID] FOREIGN KEY([OrderStatusID])
	REFERENCES OrdersStatus([ID]),
)
