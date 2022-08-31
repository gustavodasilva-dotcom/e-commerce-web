CREATE TABLE [dbo].[BankingBrands]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]				VARCHAR(100)		NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_BankingBrand_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_BankingBrand_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_BankingBrand_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_BankingBrands] PRIMARY KEY CLUSTERED ([ID] ASC)
)
