CREATE TABLE [dbo].[Subcategories]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(60)			NOT NULL,
	[CategoryID]	INT					NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_Subcategory_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_Subcategory_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_Subcategory_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]	INT					NULL,
	[Deleted_at]	DATETIME			NULL,
	[Deleted_by]	INT					NULL

	CONSTRAINT [PK_Subcategories] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Subcategories_CategoryID] FOREIGN KEY([CategoryID])
	REFERENCES Categories([ID])

	CONSTRAINT [UC_Subcategories] UNIQUE ([GuidID], [Name])
)
