CREATE TABLE [dbo].[ShoppingCarts]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[UserID]			INT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_ShoppingCart_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_ShoppingCart_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_ShoppingCart_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_ShoppingCarts_UserID] FOREIGN KEY([UserID])
	REFERENCES Users([ID])
)
