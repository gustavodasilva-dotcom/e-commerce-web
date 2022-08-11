CREATE TABLE [dbo].[Manufacturers]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]				VARCHAR(200)		NOT NULL,
	[CAGE]				VARCHAR(20)			NOT NULL,
	[NCAGE]				VARCHAR(20)			NOT NULL,
	[SEC]				INT					NOT NULL,
	[ContactID]			INT					NOT NULL,
	[AddressID]			INT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_Manufacturer_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_Manufacturer_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_Manufacturer_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Manufacturers_ContactID] FOREIGN KEY([ContactID])
	REFERENCES Contacts([ID]),

	CONSTRAINT [FK_Manufacturers_AddressID] FOREIGN KEY([AddressID])
	REFERENCES Addresses([ID])
)
