CREATE TABLE [dbo].[Streets]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[PostalCode]		VARCHAR(8)			NOT NULL,
	[Name]				VARCHAR(150)		NOT NULL,
	[NeighborhoodID]	INT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_Street_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_Street_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_Street_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_Streets] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Streets_NeighborhoodID] FOREIGN KEY([NeighborhoodID])
	REFERENCES Neighborhoods([ID])
)
