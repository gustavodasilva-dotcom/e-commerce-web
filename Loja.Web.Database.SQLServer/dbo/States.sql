CREATE TABLE [dbo].[States]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Initials]		VARCHAR(3)			NOT NULL,
	[CountryID]		INT					NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_State_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_State_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_State_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_States_CountryID] FOREIGN KEY([CountryID])
	REFERENCES Countries([ID])
)
