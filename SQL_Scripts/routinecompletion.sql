-- <summary>
-- William Clark
-- Created: 2021/02/23
--
-- Script for the creation of the RoutineCompletion table
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
-- Created: 2021/03/18
--
-- Definition of the RoutineCompletion table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating RoutineCompletion table ***'
GO
CREATE TABLE [dbo].[RoutineCompletion]
(
	[UserID_Client]		[INT]					NOT NULL,
	[RoutineName]	 [NVARCHAR]	(50)			NOT NULL,
	[RoutineCompletionDate]		[DATETIME] DEFAULT GETDATE() NOT NULL
	CONSTRAINT	[fk_RoutineCompletion_UserID_Client]	FOREIGN KEY([UserID_Client])
	REFERENCES [dbo].[UserAccount]([UserID]),
	CONSTRAINT	[fk_RoutineCompletion_RoutineName]		FOREIGN KEY([RoutineName])
	REFERENCES [dbo].[Routines]([RoutineName])
)
GO

-- <summary>
-- William Clark
-- Created: 2021/03/18
--
-- Creates a RoutineCompletion
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_create_routinecompletion ***'
GO
CREATE PROCEDURE [dbo].[sp_create_routinecompletion]
	(
		@UserID_Client			[INT],
		@RoutineName			[NVARCHAR](50)
	)
AS
	BEGIN
		INSERT INTO [dbo].[RoutineCompletion]
			([UserID_Client], [RoutineName])
		VALUES
			(@UserID_Client, @RoutineName)
          RETURN @@ROWCOUNT
	END
GO
