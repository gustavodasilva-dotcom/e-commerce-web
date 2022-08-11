CREATE TABLE [dbo].[Neighborhoods]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(150)		NOT NULL,
	[CityID]		INT					NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_Neighborhood_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_Neighborhood_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_Neighborhood_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_Neighborhoods] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Neighborhoods_CityID] FOREIGN KEY([CityID])
	REFERENCES Cities([ID])
)
