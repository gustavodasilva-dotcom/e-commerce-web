CREATE TABLE [dbo].[Measurements]
(
	[ID]				INT					NOT NULL IDENTITY(1, 1),
	[GuidID]			UNIQUEIDENTIFIER	NOT NULL,
	[Name]				VARCHAR(100)		NOT NULL,
	[MeasurementTypeID]	INT					NOT NULL,
	[Active]			BIT					CONSTRAINT [DF_Measurement_Active]		DEFAULT (1) NOT NULL,
	[Deleted]			BIT					CONSTRAINT [DF_Measurement_Deleted]		DEFAULT (0) NOT NULL,
	[Created_at]		DATETIME			CONSTRAINT [DF_Measurement_Created_at]	DEFAULT (GETDATE()) NOT NULL,
	[Created_by]		INT					NULL,
	[Deleted_at]		DATETIME			NULL,
	[Deleted_by]		INT					NULL

	CONSTRAINT [PK_Measurements] PRIMARY KEY CLUSTERED ([ID] ASC)

	CONSTRAINT [FK_Measurements_MeasurementTypeID] FOREIGN KEY([MeasurementTypeID])
	REFERENCES MeasurementTypes([ID])
)
