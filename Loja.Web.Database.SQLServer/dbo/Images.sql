CREATE TABLE [dbo].[Images]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Base64]		VARCHAR(MAX)		NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_Image_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_Image_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_Image_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]	INT					NULL,
	[Deleted_at]	DATETIME			NULL,
	[Deleted_by]	INT					NULL

	CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ([ID] ASC)
)
