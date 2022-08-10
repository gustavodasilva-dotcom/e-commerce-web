CREATE TABLE [dbo].[Users]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(100)		NOT NULL,
	[Email]			VARCHAR(100)		NOT NULL,
	[Login]			VARCHAR (255)		NOT NULL,
	[Password]		VARCHAR (255)		NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_User_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_User_Deleted]	DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_User_Created_at] DEFAULT (GETDATE()) NOT NULL,
	[Created_by]	INT					NULL,
	[Deleted_at]	DATETIME			NULL,
	[Deleted_by]	INT					NULL,
	[UserRoleID]	INT					NOT NULL

	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Users_UserRoleID] FOREIGN KEY ([UserRoleID])
	REFERENCES UserRoles([ID])
)
