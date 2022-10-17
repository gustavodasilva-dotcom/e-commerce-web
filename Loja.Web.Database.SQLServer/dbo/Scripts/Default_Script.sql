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

-- *************************************************************************
-- Script to be executed to insert, at the database, the card's issuers:
-- *************************************************************************
INSERT INTO CardIssuers
	(GuidID,	Name,			Length,					Prefixes,										CheckDigit)
VALUES
	 (NEWID(), 'Visa',			'13,16',				'4',											1)
	,(NEWID(), 'MasterCard',	'16',					'51,52,53,54,55',								1)
	,(NEWID(), 'DinersClub',	'14,16',				'36,38,54,55',									1)
	,(NEWID(), 'CarteBlanche',	'14',					'300,301,302,303,304,305',						1)
	,(NEWID(), 'AmEx',			'15',					'34,37',										1)
	,(NEWID(), 'Discover',		'16',					'6011,622,64,65',								1)
	,(NEWID(), 'JCB',			'16',					'35',											1)
	,(NEWID(), 'enRoute',		'15',					'2014,2149',									1)
	,(NEWID(), 'Solo',			'16,18,19',				'6334,6767',									1)
	,(NEWID(), 'Switch',		'16,18,19',				'4903,4905,4911,4936,564182,633110,6333,6759',	1)
	,(NEWID(), 'Maestro',		'12,13,14,15,16,18,19', '5018,5020,5038,6304,6759,6761,6762,6763',		1)
	,(NEWID(), 'VisaElectron',	'16',					'4026,417500,4508,4844,4913,4917',				1)
	,(NEWID(), 'LaserCard',		'16,17,18,19',			'6304,6706,6771,6709',							1);