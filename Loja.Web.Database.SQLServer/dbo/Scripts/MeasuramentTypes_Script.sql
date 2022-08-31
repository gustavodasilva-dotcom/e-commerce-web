-- *************************************************************************
-- Script to be executed to insert, at the database, the measurements types:
-- *************************************************************************

INSERT INTO MeasurementTypes (GuidID, Name) VALUES (NEWID(), 'Mass');
INSERT INTO MeasurementTypes (GuidID, Name) VALUES (NEWID(), 'Height');


-- *************************************************************************
-- Script to be executed to insert, at the database, the orders' status:
-- *************************************************************************

INSERT INTO OrdersStatus (GuidID, Name) VALUES (NEWID(), 'Completed');
INSERT INTO OrdersStatus (GuidID, Name) VALUES (NEWID(), 'Cancelled');
INSERT INTO OrdersStatus (GuidID, Name) VALUES (NEWID(), 'Pending');