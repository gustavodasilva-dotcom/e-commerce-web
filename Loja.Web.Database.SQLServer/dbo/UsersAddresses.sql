CREATE TABLE [dbo].[UsersAddresses]
(
	[ID]			INT			NOT NULL IDENTITY(1, 1),
	[UserID]		INT			NOT NULL,
	[AddressID]		INT			NOT NULL,
	[Active]		BIT			CONSTRAINT [DF_UserAddress_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT			CONSTRAINT [DF_UserAddress_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME	CONSTRAINT [DF_UserAddress_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_UsersAddresses] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_UsersAddresses_UserID] FOREIGN KEY ([UserID])
	REFERENCES Users([ID]),

	CONSTRAINT [FK_UsersAddresses_AddressID] FOREIGN KEY ([AddressID])
	REFERENCES Addresses([ID])
)
