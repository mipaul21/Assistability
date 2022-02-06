-- <summary>
-- William Clark
-- Created: 2021/03/12
--
-- Script for the creation of the RoutineStepCompletion table
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
-- Created: 2021/03/12
--
-- Definition of the RoutineStepCompletion table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating RoutineStepCompletion table ***'
GO
CREATE TABLE [dbo].[RoutineStepCompletion]
(
	[UserID_Client]		[INT]					NOT NULL,
	[RoutineStepId] 	[INT]					NOT NULL,
	[RoutineName]	 [NVARCHAR]	(50)			NOT NULL,
	[RoutineStepCompletionDate]		[DATETIME] DEFAULT GETDATE() NOT NULL
	CONSTRAINT	[fk_RoutineStepCompletion_UserID_Client]	FOREIGN KEY([UserID_Client])
	REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT	[fk_RoutineStepCompletion_RoutineStepId]		FOREIGN KEY([RoutineStepId])
	REFERENCES [dbo].[RoutineStep]([RoutineStepId]),
	CONSTRAINT	[fk_RoutineStepCompletion_RoutineName]		FOREIGN KEY([RoutineName])
	REFERENCES [dbo].[Routines]([RoutineName])
)
GO

-- <summary>
-- William Clark
-- Created: 2021/03/12
--
-- Creates a RoutineStepCompletion
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_create_routinestepcompletion ***'
GO
CREATE PROCEDURE [dbo].[sp_create_routinestepcompletion]
	(
		@UserID_Client			[INT],
		@RoutineStepID			[INT],
		@RoutineName			[NVARCHAR](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[RoutineStepCompletion]
			([UserID_Client], [RoutineStepId], [RoutineName])
		VALUES
			(@UserID_Client, @RoutineStepID, @RoutineName)
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/08/12
--
-- Selects completed RoutineStepIDs by a selected day
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_completed_routinestepids_by_day ***'
GO
CREATE PROCEDURE [dbo].[sp_select_completed_routinestepids_by_day]
	(
		@SelectedDate			[DATETIME]
	)
AS
	BEGIN
		SELECT DISTINCT [RoutineStepID]
		FROM RoutineStepCompletion
		WHERE (SELECT
			COUNT([RoutineStepID])
			FROM RoutineStepCompletion
			WHERE [RoutineStepCompletionDate] > DATEADD(day, -1, @SelectedDate)
			AND [RoutineStepCompletionDate]  < DATEADD(day, 1, @SelectedDate)) > 0
	END
GO
