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


-- *************************************************************************
-- Script to be executed to insert, at the database, the payment methods:
-- *************************************************************************

INSERT INTO PaymentMethods (GuidID, Name, IsCard) VALUES (NEWID(), 'Credit card', 1);
INSERT INTO PaymentMethods (GuidID, Name, IsCard) VALUES (NEWID(), 'Debit card', 1);