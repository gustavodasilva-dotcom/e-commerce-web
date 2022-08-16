CREATE TABLE [dbo].[Addresses]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Number]		VARCHAR(10)			NOT NULL,
	[Comment]		VARCHAR(100),
	[StreetID]		INT					NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_Address_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_Address_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_Address_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Addresses_StreetID] FOREIGN KEY([StreetID])
	REFERENCES Streets([ID])
)
