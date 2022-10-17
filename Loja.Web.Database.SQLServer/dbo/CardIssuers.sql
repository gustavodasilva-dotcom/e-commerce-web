CREATE TABLE [dbo].[CardIssuers]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(50)			NOT NULL,
	[Length]		VARCHAR(30)			NOT NULL,
	[Prefixes]		VARCHAR(100)		NOT NULL,
	[Base64]		VARCHAR(MAX)		NULL,
	[CheckDigit]	BIT					CONSTRAINT [DF_CardIssuer_CheckDigit]	DEFAULT (1) NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_CardIssuer_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_CardIssuer_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_CardIssuer_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Deleted_at]	DATETIME			NULL,
)
