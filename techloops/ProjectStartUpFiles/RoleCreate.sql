USE [SSD_GLAB1];

INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Admin');
INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Manager');

INSERT INTO AspNetUserRoles
  VALUES((SELECT Id FROM AspNetUsers WHERE UserName = 'admin@techloops.ca'),
	     (SELECT Id FROM AspNetRoles WHERE [Name] = 'Admin'));
INSERT INTO AspNetUserRoles
  VALUES((SELECT Id FROM AspNetUsers WHERE UserName = 'admin@techloops.ca'),
	     (SELECT Id FROM AspNetRoles WHERE [Name] = 'Manager'));
INSERT INTO AspNetUserRoles
  VALUES((SELECT Id FROM AspNetUsers WHERE UserName = 'manager@techloops.ca'),
	     (SELECT Id FROM AspNetRoles WHERE [Name] = 'Manager'));
