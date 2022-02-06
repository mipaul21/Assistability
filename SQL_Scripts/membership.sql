-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Script for the creation of the Membership table
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
-- Definition of the Membership table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating Membership table ***'
GO
CREATE TABLE [dbo].[Membership]
(
	[GroupID]		[INT]							NOT NULL,
	[UserID]		[INT]							NOT NULL,
	[CreationDate] [DATETIME]	DEFAULT GETDATE()		NOT NULL,
	[ExpirationDate] [DATETIME]						NULL,
	[Active]		[BIT]		DEFAULT 1				NOT NULL
	CONSTRAINT	[pk_MemberShip]	PRIMARY KEY([GroupID], [UserID]),
	CONSTRAINT	[fk_Membership_GroupID]		FOREIGN KEY([GroupID])
	REFERENCES [dbo].[UserGroup]([GroupID]),
	CONSTRAINT	[fk_Membership_UserID]		FOREIGN KEY([UserID])
	REFERENCES [dbo].[UserAccount]([UserID])
)
GO


-- <summary>
-- William Clark
-- Created: 2021/02/25
--
-- Selects all Memberships by UserAccountID
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_memberships_by_useraccountid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_memberships_by_useraccountid]
	(
		@UserAccountID			[INT]
	)
AS
	BEGIN
		SELECT [GroupID], [CreationDate],
		[ExpirationDate], [Active]
		FROM Membership
		WHERE [UserID] = @UserAccountID
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/02/25
--
-- Selects all Memberships by GroupID
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_memberships_by_groupid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_memberships_by_groupid]
	(
		@GroupID			[INT]
	)
AS
	BEGIN
		SELECT [UserID], [CreationDate],
		[ExpirationDate], [Active]
		FROM Membership
		WHERE [GroupID] = @GroupID
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/04/15
--
-- Creates a membership in a usergroup
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating create_membership ***'
GO
CREATE PROCEDURE [dbo].[create_membership]
	(
		@GroupID			[INT],
		@UserID			[INT],
		@Active			[BIT]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Membership]
		 ([GroupID], [UserID], [Active])
		VALUES
		 (@GroupID, @UserID, @Active)
		SELECT SCOPE_IDENTITY()
	END
GO
