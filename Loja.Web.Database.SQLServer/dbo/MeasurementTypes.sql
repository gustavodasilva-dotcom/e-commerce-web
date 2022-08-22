CREATE TABLE [dbo].[MeasurementTypes]
(
	[ID]			INT					NOT NULL IDENTITY(1, 1),
	[GuidID]		UNIQUEIDENTIFIER	NOT NULL,
	[Name]			VARCHAR(50)			NOT NULL,
	[Active]		BIT					CONSTRAINT [DF_MeasuramentType_Active]		DEFAULT (1) NOT NULL,
	[Deleted]		BIT					CONSTRAINT [DF_MeasuramentType_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]	DATETIME			CONSTRAINT [DF_MeasuramentType_Created_at]	DEFAULT (GETDATE()) NOT NULL

	CONSTRAINT [PK_MeasurementTypes] PRIMARY KEY CLUSTERED ([ID] ASC)
)
