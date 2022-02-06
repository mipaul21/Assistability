print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Becky Baenziger
-- Created: 2021/03/28
--
-- Creates test data for AttainmentGoal table
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating test data for AttainmentGoal table ***'
GO
INSERT INTO [dbo].[AttainmentGoal]
		([UserID_client], [UserID_admin], [AttGoalName], [AttGoalDescription],
		[AttGoalTargetDate], [AttGoalEntryDate], [PerformanceFrequency], [Active], [AwardName], 
		[PerformanceName])
	VALUES
		(3, 1, 'Thank Yous', 'Say thank you', '20210404', '20200202', 1, 5, "Award number 1", 'fake')
		, (1, 1, 'Sportsmanship', 'Say Good Game', '20210404', '20210202', 0, 7, "Award number 3", 'different')
GO