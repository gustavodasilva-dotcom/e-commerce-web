CREATE TABLE [dbo].[CardsInfos]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[CardNumber]		VARCHAR(20)			NOT NULL,
	[NameAtTheCard]		VARCHAR(50)			NOT NULL,
	[ExpMonth]			INT					NOT NULL,
	[ExpYear]			INT					NOT NULL,
	[CVV]				VARCHAR(3)			NOT NULL,
	[Quantity]			INT,
	--[BankingBrandID]	INT					NOT NULL,
	[UserID]			INT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_CardInfo_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_CardInfo_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_CardInfo_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_CardsInfos] PRIMARY KEY CLUSTERED ([ID] ASC)

	--CONSTRAINT [FK_CardsInfos_BankingBrandID] FOREIGN KEY([BankingBrandID])
	--REFERENCES BankingBrands([ID]),

	CONSTRAINT [FK_CardsInfos_UserID] FOREIGN KEY([UserID])
	REFERENCES Users([ID])
)
