CREATE TABLE [dbo].[OrdersCardsInfos]
(
	[ID]			INT			NOT NULL IDENTITY(1, 1),
	[OrderID]		INT			NOT NULL,
	[CardInfoID]	INT			NOT NULL,
	[Active]		BIT			CONSTRAINT [DF_OrderCardInfo_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT			CONSTRAINT [DF_OrderCardInfo_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME	CONSTRAINT [DF_OrderCardInfo_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_OrdersCardsInfos] PRIMARY KEY CLUSTERED ([ID] ASC)
)
