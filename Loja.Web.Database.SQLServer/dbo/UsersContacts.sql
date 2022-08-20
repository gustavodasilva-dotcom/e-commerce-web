CREATE TABLE [dbo].[UsersContacts]
(
	[ID]			INT			NOT NULL IDENTITY(1, 1),
	[UserID]		INT			NOT NULL,
	[ContactID]		INT			NOT NULL,
	[Active]		BIT			CONSTRAINT [DF_UserContact_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT			CONSTRAINT [DF_UserContact_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME	CONSTRAINT [DF_UserContact_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_UsersContacts] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_UsersContacts_UserID] FOREIGN KEY ([UserID])
	REFERENCES Users([ID]),

	CONSTRAINT [FK_UsersContacts_ContactID] FOREIGN KEY ([ContactID])
	REFERENCES Contacts([ID])
)
