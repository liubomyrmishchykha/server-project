USE testDb
GO

INSERT INTO Users(Name, Password)
VALUES	('John','password'),
		('Silvia', 'admin'),
		('Haiden', 'pass'),
		('Sem', 'superadmin'),
		('Stiv', '1234567'),
		('Kerol', 'qwerty'),
		('Admin', 'asdfgh'),
		('Pol', 'qazwsx'),
		('Frenk', '0987654321'),
		('Jake', 'qwertyuiop'),
		('Bob', 'adminmnbvcxz'),
		('John','password'),
		('Silvia', 'admin'),
		('Haiden', 'pass'),
		('Sem', 'superadmin'),
		('Stiv', '1234567'),
		('Kerol', 'qwerty'),
		('Admin', 'asdfgh'),
		('Pol', 'qazwsx'),
		('Frenk', '0987654321'),
		('Jake', 'qwertyuiop'),
		('Bob', 'adminmnbvcxz')
GO

INSERT INTO Instances(HostName, InstanceName, Status, Version, Added, Modified, RAM, CPUCount, UserId)
VALUES	('IFCLASS23', 'SQLEXPRESS', 0, '101.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 24, 8, 1),
		('local', 'MySQL', 1, '5.232.1', '2015-11-01T12:00:00', '2015-11-15T13:45:00', 20, 8, 2),
		('Home', 'OracleDB', 0, '1.22.1', '2015-10-01T15:04:20', '2015-10-05T13:45:00', 30, 10, 3),
		('Admin', 'NoSql', 1, '4.32.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 16, 6, 4),
		('IFCLASS24', 'SQLEXPRESS', 0, '1.2.1', '2015-09-01T15:00:00', '2015-09-15T13:45:00', 24, 8, 5),
		('SuperAdmin', 'MongoDb', 1, '6.2.1', '2015-08-11T12:00:00', '2015-10-05T13:45:00', 20, 8, 6),
		('NASA', 'PostgreSql', 0, '1.2.1', '2015-10-21T12:00:00', '2015-11-05T13:45:00', 18, 8, 7),
		('Foo', 'DB2', 1, '21.232.1', '2015-08-01T12:00:00', '2015-12-05T13:45:00', 4, 2, 8),
		('Bar', 'SqlServer', 0, '41.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 8, 2, 9),
		('IFCLASS25', 'PostgreSQL', 1, '41.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 8, 2, 9),
		('IFCLASS26', 'MySql', 0, '101.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 24, 8, 10),
		('localnet', 'MangoDb', 1, '5.232.1', '2015-11-01T12:00:00', '2015-11-15T13:45:00', 20, 8, 11),
		('HomePC', 'CiscoDb', 0, '1.22.1', '2015-10-01T15:04:20', '2015-10-05T13:45:00', 30, 10, 12),
		('AdminA', 'SqlExpress', 1, '4.32.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 16, 6, 13),
		('IFCLASS28', 'DB2', 0, '1.2.1', '2015-09-01T15:00:00', '2015-09-15T13:45:00', 24, 8, 14),
		('SuperAdminFirst', 'PostgreDb', 1, '6.2.1', '2015-08-11T12:00:00', '2015-10-05T13:45:00', 20, 8, 15),
		('NASALocal', 'MySql', 0, '1.2.1', '2015-10-21T12:00:00', '2015-11-05T13:45:00', 18, 8, 16),
		('Foot', 'SQLSERVER', 1, '21.232.1', '2015-08-01T12:00:00', '2015-12-05T13:45:00', 4, 2, 17),
		('Barselona', 'MongoDb', 0, '41.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 8, 2, 18),
		('IFCLASS29', 'NoSQL', 1, '41.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 8, 2, 19),
		('IFCLASS30', 'CiscoDb', 0, '101.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 24, 8, 20),
		('localhost', 'SqlExpress', 1, '5.232.1', '2015-11-01T12:00:00', '2015-11-15T13:45:00', 20, 8, 21),
		('HomeMAC', 'DB2', 0, '1.22.1', '2015-10-01T15:04:20', '2015-10-05T13:45:00', 30, 10, 22),
		('AdminB', 'PostgreSql', 1, '4.32.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 16, 6, 4),
		('IFCLASS31', 'MangoDb', 0, '1.2.1', '2015-09-01T15:00:00', '2015-09-15T13:45:00', 24, 8, 5),
		('SuperAdminSecond', 'NoSQL', 1, '6.2.1', '2015-08-11T12:00:00', '2015-10-05T13:45:00', 20, 8, 6),
		('NASADrop', 'SQLSERVER', 0, '1.2.1', '2015-10-21T12:00:00', '2015-11-05T13:45:00', 18, 8, 7),
		('Footage', 'NoSql', 1, '21.232.1', '2015-08-01T12:00:00', '2015-12-05T13:45:00', 4, 2, 8),
		('Barbados', 'MazeDb', 0, '41.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 8, 2, 9),
		('IFCLASS34', 'PostSQL', 1, '41.232.1', '2015-10-01T12:00:00', '2015-10-05T13:45:00', 8, 2, 9)
GO

INSERT INTO Databases (Name, CreateTime, TotalSize, DbState, InstanceId)
	VALUES	('Customers', '2015-10-01T12:00:00', 5, 0, 1),
			('Orders', '2015-10-01T12:00:00', 5, 0, 2),
			('Items', '2015-10-01T12:00:00', 5, 0, 3),
			('Supliers', '2015-10-01T12:00:00', 5, 0, 4),
			('Input', '2015-10-01T12:00:00', 5, 0, 5),
			('Output', '2015-10-01T12:00:00', 5, 0, 6),
			('Selected', '2015-10-01T12:00:00', 5, 0, 7),
			('Customers', '2015-10-01T12:00:00', 5, 0, 8),
			('Orders', '2015-10-01T12:00:00', 5, 0, 9),
			('Items', '2015-10-01T12:00:00', 5, 0, 10),
			('Supliers', '2015-10-01T12:00:00', 5, 0, 11),
			('Input', '2015-10-01T12:00:00', 5, 0, 12),
			('Output', '2015-10-01T12:00:00', 5, 0, 13),
			('Selected', '2015-10-01T12:00:00', 5, 0, 14),
			('Customers', '2015-10-01T12:00:00', 5, 0, 15),
			('Orders', '2015-10-01T12:00:00', 5, 0, 16),
			('Items', '2015-10-01T12:00:00', 5, 0, 17),
			('Supliers', '2015-10-01T12:00:00', 5, 0, 18),
			('Input', '2015-10-01T12:00:00', 5, 0, 19),
			('Output', '2015-10-01T12:00:00', 5, 0, 20),
			('Selected', '2015-10-01T12:00:00', 5, 0, 21),
			('Customers', '2015-10-01T12:00:00', 5, 0, 22),
			('Orders', '2015-10-01T12:00:00', 5, 0, 23),
			('Items', '2015-10-01T12:00:00', 5, 0, 24),
			('Supliers', '2015-10-01T12:00:00', 5, 0, 25),
			('Input', '2015-10-01T12:00:00', 5, 0, 26),
			('Output', '2015-10-01T12:00:00', 5, 0, 27),
			('Selected', '2015-10-01T12:00:00', 5, 0, 28),
			('Customers', '2015-10-01T12:00:00', 5, 0, 29),
			('Orders', '2015-10-01T12:00:00', 5, 0, 30)
GO

INSERT INTO Tables(Name, ColumnCount, DatabaseId)
	VALUES	('Master',8, 1),
			('Slave', 4, 2),
			('Person', 5, 3),
			('Human', 8, 4),
			('Production', 6, 5),
			('Sales', 4, 6),
			('Credits', 3, 7),
			('Security', 5, 8),
			('Logger', 6, 9),
			('Version', 5, 10),
			('Master', 3, 11),
			('Slave', 6, 12),
			('Person', 3, 13),
			('Human', 2, 14),
			('Production', 3, 15),
			('Sales', 5, 16),
			('Credits', 8, 17),
			('Security', 4, 18),
			('Logger', 7, 19),
			('Version', 3, 20),
			('Master', 4, 21),
			('Slave', 5, 22),
			('Person', 8, 23),
			('Human', 6, 24),
			('Production', 3, 25),
			('Sales', 4, 26),
			('Credits', 4, 27),
			('Security', 3, 28),
			('Logger', 3, 29),
			('Version', 3, 30)
GO


INSERT INTO Options(Interval)
			VALUES(5)
GO