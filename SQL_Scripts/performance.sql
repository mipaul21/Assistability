-- <summary>
-- Ryan Taylor
-- Created: 2021/03/26
--
-- Script for the creation of the Performance table
-- and related stored procedures
-- </summary>

print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/26
--
-- Definition of the Performance table
-- </summary>
print '' print '*** creating Performance table ***'
GO
CREATE TABLE [dbo].[Performance]
(
	[PerformanceName]			[NVARCHAR](50)	NOT NULL,
	[PerformanceDescription]	[NVARCHAR](255)	NOT NULL,
	[UserID_client]				[INT]			NOT NULL,
	[UserIDCreator]				[INT]			NOT NULL,
	[PerformanceEntryDate]		[DATETIME]		NOT	NULL	DEFAULT GETDATE(),
	[PerformanceEditDate]		[DATETIME]		NULL,
	[PerformanceRemovalDate]	[DATETIME]		NULL,
	[Active]					[BIT]			NOT NULL	DEFAULT 1,
	CONSTRAINT	[pk_UserID_client_PerformanceName]	
	PRIMARY KEY([UserID_client] ASC,[PerformanceName] ASC),
	CONSTRAINT	[fk_UserID_client_Performance]		FOREIGN KEY([UserID_client])
	REFERENCES [dbo].[UserAccount]([UserID]) ON UPDATE CASCADE,
	CONSTRAINT	[fk_UserIDCreator]		FOREIGN KEY([UserIDCreator])
	REFERENCES [dbo].[UserAccount]([UserID])
)
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/28
--
-- A stored procedure for getting all performances by client
-- </summary>
print '' print '*** creating sp_select_performances_by_client ***'
GO
CREATE PROCEDURE [dbo].[sp_select_performances_by_client]
	(
	@UserID_client	[INT]
	)
AS
	BEGIN
		SELECT PerformanceName, PerformanceDescription, UserID_client, UserIDCreator,
		PerformanceEntryDate, PerformanceEditDate, PerformanceRemovalDate, Active
		FROM Performance
		WHERE UserID_client = @UserID_client
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/28
--
-- A stored procedure for getting all performances by client and active
-- </summary>
print '' print '*** creating sp_select_performances_by_client_and_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_performances_by_client_and_active]
	(
	@UserID_client	[INT],
	@Active			[BIT]
	)
AS
	BEGIN
		SELECT PerformanceName, PerformanceDescription, UserID_client, UserIDCreator,
		PerformanceEntryDate, PerformanceEditDate, PerformanceRemovalDate, Active
		FROM Performance
		WHERE UserID_client = @UserID_client
			AND Active = @Active
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/28
--
-- A stored procedure for creating a performance
-- </summary>
print '' print '*** creating sp_insert_new_performance ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_performance]
	(
	@PerformanceName		[NVARCHAR](50),
	@PerformanceDescription	[NVARCHAR](255),
	@UserID_client			[INT],
	@UserIDCreator			[INT]
	)
AS
	BEGIN
		INSERT INTO [dbo].[Performance]
			([PerformanceName], [PerformanceDescription], [UserID_client], [UserIDCreator])
		VALUES
			(@PerformanceName, @PerformanceDescription, @UserID_client, @UserIDCreator)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/28
--
-- A stored procedure for editing a performance
-- </summary>
print '' print '*** creating sp_update_performance ***'
GO
CREATE PROCEDURE [dbo].[sp_update_performance]
	(
	@PerformanceName			[NVARCHAR](50),
	@NewPerformanceDescription	[NVARCHAR](255),
	@NewUserID_client			[INT],
	
	@OldPerformanceDescription	[NVARCHAR](255),
	@OldUserID_client			[INT]
	)
AS
	BEGIN
		UPDATE Performance
			SET PerformanceDescription = @NewPerformanceDescription,
				UserID_client = @NewUserID_client,
				PerformanceEditDate = GETDATE()
			WHERE PerformanceName = @PerformanceName
			And PerformanceDescription = @OldPerformanceDescription
			And UserID_client = @OldUserID_client
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Ryan Taylor
-- Created: 2021/03/28
--
-- A stored procedure for deactivating and reactivating a performance
-- </summary>
print '' print '*** creating sp_deactivate_or_reactivate_performance ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_or_reactivate_performance]
	(
	@PerformanceName	[NVARCHAR](50),
	@UserID_client		[INT],
	@NewActive			[Bit],
	
	@OldActive			[Bit]
	)
AS
	BEGIN
		UPDATE Performance
			SET Active = @NewActive,
				PerformanceRemovalDate = GETDATE()
			WHERE PerformanceName = @PerformanceName
			And UserID_client = @UserID_client
			And Active = @OldActive
		RETURN @@ROWCOUNT
	END
GO
