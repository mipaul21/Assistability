-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Script for the creation of the Role table
-- and related stored procedures
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Definition of the Role table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating Role table ***'
GO
CREATE TABLE [dbo].[Role]
(
	[RoleID]			[INT]			IDENTITY(1,1)		NOT NULL,
	[RoleName]		[VARCHAR](50)						NOT NULL,
	[RoleDescription]	[NVARCHAR](100)					NOT NULL,
	[RoleAccepted]		[DATETIME]						NOT NULL
					DEFAULT GETDATE(),
	[Active]			[BIT]							NOT NULL
					DEFAULT(1)
	CONSTRAINT	[pk_RoleID]	PRIMARY KEY([RoleID] ASC),
	CONSTRAINT	[ak_RoleName]	UNIQUE([RoleName] ASC)
)
GO

-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Creates data for the Role table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating Role data ***'
GO
INSERT INTO [dbo].[Role]
		([RoleName], [RoleDescription])
	VALUES
		('Admin', 'User Administrator'),
		('Caregiver', 'Caregiver User'),
		('Client', 'Client User')
GO
