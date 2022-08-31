CREATE TABLE [dbo].[PaymentMethods]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]				VARCHAR(60)			NOT NULL,
	[IsCard]			BIT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_PaymentMethod_Active]			DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_PaymentMethod_Deleted]			DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_PaymentMethod_Created_at]		DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_PaymentMethods] PRIMARY KEY([ID])
)
