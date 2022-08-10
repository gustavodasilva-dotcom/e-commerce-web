CREATE TABLE [dbo].[ErrorLogs]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[Controller]	VARCHAR(100)		NULL,
	[Request]		VARCHAR(MAX)		NULL,
	[Message]		VARCHAR(300)		NULL,
	[UserLogged]	BIT					CONSTRAINT [DF_ErrorLog_UserLogged] DEFAULT (1) NOT NULL,
	[UserID]		INT					NULL,
	[Date]			DATETIME			CONSTRAINT [DF_ErrorLog_Date] DEFAULT (GETDATE()) NOT NULL,

	CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([ID] ASC)
)
