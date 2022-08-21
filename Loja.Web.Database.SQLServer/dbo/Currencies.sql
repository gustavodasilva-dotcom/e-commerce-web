CREATE TABLE [dbo].[Currencies]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]				VARCHAR(35)			NOT NULL,
	[Symbol]			VARCHAR(5)			NOT NULL,
	[USExchangeRate]	MONEY				NOT NULL,
	[LastUpdated]		DATETIME			CONSTRAINT [DF_Currency_LastUpdated]	DEFAULT (GETDATE()) NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_Currency_Active]			DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_Currency_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_Currency_Created_at]		DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([ID] ASC)
)
