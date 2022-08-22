/*
 * Script to be executed to insert, at the database, the measurements types:
 */

INSERT INTO MeasurementTypes (GuidID, Name) VALUES (NEWID(), 'Mass');
INSERT INTO MeasurementTypes (GuidID, Name) VALUES (NEWID(), 'Height');