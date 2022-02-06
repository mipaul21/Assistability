print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Becky Baenziger
-- Created: 2021/03/02
--
-- Creates test data for HabitualGoal table
-- </summary>
--
-- <remarks>
-- Updater Name:
-- Update Date:
--
-- </remarks>
print '' print '*** creating test data for HabitualGoal table ***'
GO
INSERT INTO [dbo].[HabitualGoal]
		([UserID_client], [UserID_admin], [HabGoalName], [HabGoalDescription],
		[HabGoalTargetDate], [HabGoalEntryDate], [RoutineFrequency], 
		[Active], [AwardName], [RoutineName])
	VALUES
		(3, 1, 'Mornings', 'Develop morning routine', '20210404', '20210202', 7, 1, "Award number 2", 'FirstRoutine')
		, (3, 1, 'After School', 'Homework after school', '20210404', '20210202', 7, 0, "Award number 1", 'FirstRoutine')
		, (1, 1, 'Chores', 'Weekly Chores', '20210404', '20210202', 1, 1, "Award number 4", 'FirstRoutine')
GO
