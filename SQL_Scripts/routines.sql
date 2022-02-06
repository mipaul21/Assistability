-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Script for the creation of the Routines table
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
-- Created: 2021/02/26
--
-- Definition of the Routines table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating Routines table ***'
GO
CREATE TABLE [dbo].[Routines]
(
	[RoutineName]		[NVARCHAR](50)			NOT NULL,
	[RoutineDescription][NVARCHAR](150)			NOT NULL,
	[UserID_Client]		[INT]					NOT NULL,
	[UserID_Admin]     	[INT]					NOT NULL,
	[EntryDate]			[DATETIME]				NOT NULL,
	[EditDate]			[DATETIME]				NULL,
	[RemovalDate]		[DATETIME]				NULL,
     [Active]           [BIT]                   NOT NULL
	CONSTRAINT [pk_RoutineName]		PRIMARY KEY([RoutineName] ASC),
     CONSTRAINT [fk_Routine_UserID_Client]		FOREIGN KEY([UserID_Client])
	REFERENCES [dbo].[UserAccount]([UserID]),
     CONSTRAINT [fk_Routine_UserID_Admin]		FOREIGN KEY([UserID_Admin])
	REFERENCES [dbo].[UserAccount]([UserID])
)
GO

-- <summary>
-- William Clark
-- Created: 2021/03/04
--
-- Updates a Routine
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_update_routine ***'
GO
CREATE PROCEDURE [dbo].[sp_update_routine]
	(
		@RoutineName			[NVARCHAR](50),
		@RoutineDescription		[NVARCHAR](150),
		@UserID_Client			[INT],
		@UserID_Admin			[INT],
		@EditDate				[DATETIME],
		@RemovalDate			[DATETIME],
		@Active				[BIT]
	)
AS
	UPDATE [dbo].[Routines]
	SET [RoutineDescription] = @RoutineDescription,
		[EditDate] = @EditDate,
		[RemovalDate] = @RemovalDate,
		[Active] = @Active
	WHERE [RoutineName] = @RoutineName
		AND [UserID_Client] = @UserID_Client
		AND [UserID_Admin] = @UserID_Admin
	RETURN @@ROWCOUNT
GO

-- <summary>
-- William Clark
-- Created: 2021/03/12
--
-- Selects all active Routines by UserID_Client
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_active_routines_by_useraccountid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_active_routines_by_useraccountid]
	(
		@UserID_Client			[INT]
	)
AS
	BEGIN
		SELECT
		[RoutineName], [RoutineDescription], [UserID_Admin], [EntryDate], [EditDate], [RemovalDate]
		FROM [dbo].[Routines]
		WHERE [UserID_Client] = @UserID_Client
			AND [Active] = 1
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/23/02
--
-- Creates sp_insert_new_routine stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_insert_new_routine ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_routine]
	(
	 @RoutineName			[nvarchar] (50),
	 @RoutineDescription	[nvarchar] (150),
	 @UserID_client			[INT],
	 @UserID_Admin			[INT],
	 @EntryDate				[DATETIME],
	 @Active				[BIT]
	)
AS
	BEGIN
		INSERT INTO Routines
			( [RoutineName], [RoutineDescription],[UserID_client],[UserID_Admin], [EntryDate],[Active])
		  VALUES
			(@RoutineName, @RoutineDescription, @UserID_client, @UserID_Admin, @EntryDate, @Active)
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/23/02
--
-- Creates sp_select_routines_by_useraccountid stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_routines_by_useraccountid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_routines_by_useraccountid]
	(
		@UserID_client			[INT]
	)
AS
	BEGIN
		SELECT [RoutineName], [RoutineDescription], [UserID_client], [UserID_Admin],
          [EntryDate],[EditDate],[RemovalDate], [Active]
		FROM Routines
		WHERE [UserID_client] = @UserID_client
		ORDER BY RoutineName ASC
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/04/01
--
-- Creates sp_select_incomplete_routines_by_useraccountid stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_incomplete_routines_by_useraccountid_by_day ***'
GO
CREATE PROCEDURE [dbo].[sp_select_incomplete_routines_by_useraccountid_by_day]
	(
		@UserID_client			[INT],
		@SelectedDate			[DATETIME]
	)
AS
	BEGIN
		SELECT DISTINCT Routines.[RoutineName], [RoutineDescription], Routines.[UserID_client], [UserID_Admin],
		[EntryDate],[EditDate],[RemovalDate], [Active]
		FROM Routines
		WHERE
			Routines.[UserID_client] = @UserID_client
			AND [RoutineName] NOT IN 
			(SELECT [RoutineName]
			FROM RoutineCompletion
			WHERE [RoutineCompletionDate] > DATEADD(day, -1, @SelectedDate)
			AND [RoutineCompletionDate]  < DATEADD(day, 1, @SelectedDate))
			AND [EntryDate] <= @SelectedDate
			AND Routines.[Active] = 1
		ORDER BY Routines.RoutineName ASC
	END
GO
