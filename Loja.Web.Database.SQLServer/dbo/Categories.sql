CREATE TABLE [dbo].[Categories]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(60)			NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_Category_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_Category_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_Category_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]	INT					NULL,
	[Deleted_at]	DATETIME			NULL,
	[Deleted_by]	INT					NULL

	CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [UC_Categories] UNIQUE ([GuidID], [Name])
)
