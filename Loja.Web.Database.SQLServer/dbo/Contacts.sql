CREATE TABLE [dbo].[Contacts]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Phone]				VARCHAR(12),
	[Cellphone]			VARCHAR(13),
	[Email]				VARCHAR(100),
	[Website]			VARCHAR(150),
	[Active]			BIT					CONSTRAINT [DF_Contact_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_Contact_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_Contact_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED ([ID] ASC)
)
