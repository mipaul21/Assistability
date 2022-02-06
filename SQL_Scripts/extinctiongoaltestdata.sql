print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Becky Baenziger
-- Created: 2021/04/11
--
-- Creates test data for ExtinctionGoal table
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating test data for ExtinctionGoal table ***'
GO
INSERT INTO [dbo].[ExtinctionGoal]
		([UserID_client], [UserID_admin], [ExtGoalName], [ExtGoalDescription],
		[ExtGoalTargetDate], [ExtGoalEntryDate], [Active], [IncidentFrequency], [AwardName], [IncidentName])
	VALUES
		(3, 1, 'Do not hit', 'Stop hitting', '20210404', '20210202', 1, 2, "Award number 1", 'FirstIncident')
		, (3, 1, 'Do not swear', 'Stop swearing', '20210404', '20210202', 0, 5, "Award number 4", 'SecondIncident')
		, (1, 1, 'Do not drink', 'Stop drinking', '20210404', '20210202', 1, 2, "Award number 3", 'ThirdIncident')
GO