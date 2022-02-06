print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

print '' print '*** creating useraccount test records ***'
GO
INSERT INTO [dbo].[UserAccount]
	([FirstName], [LastName], [UserName], [Email], [Active])
VALUES
	("First", "Administrator", "firstAdmin", "first@Administrator.com", 1),
	("Care", "Giver", "Caregiver", "caregiver@Assistability.com", 1),
	("Client", "Client", "Client", "client@Assistability.com", 1)
GO

print '' print '*** creating routines test records ***'
GO
INSERT INTO [dbo].[Routines]
	([RoutineName], [RoutineDescription], [UserID_Client], [UserID_Admin], [EntryDate], [EditDate], [RemovalDate], [Active])
VALUES
	("FirstRoutine", "First Routine", 3, 1, '20210226', null, null, 1),
	("SecondRoutine", "Second Routine", 1, 1, '20210226', null, null, 1)
GO

print '' print '*** creating RoutineStep test data ***'
GO
INSERT INTO [dbo].[RoutineStep]
		([RoutineName], [RoutineStepName], [RoutineStepDescription], [RoutineStepEntryDate], [RoutineStepEditDate], [RoutineStepRemovalDate], [RoutineStepOrderNumber])
	VALUES
		('FirstRoutine', 'Task1', 'DoStuff1', '20210101', '20210201', null, 1)
		, ('FirstRoutine', 'Task2', 'DoStuff2', '20210101', '20210202', null, 2)
		, ('FirstRoutine', 'Task3', 'DoStuff3', '20210101', null, null, 3)
		, ('FirstRoutine', 'Task4', 'DoStuff4', '20210101', null, null, 4)
		, ('FirstRoutine', 'Task5', 'DoStuff5', '20210101', null, null, 5)
GO

print '' print '*** creating incident test records ***'
GO
INSERT INTO [dbo].[Incident]
	([UserID_Client],[IncidentName],[IncidentDescription], [DesiredConsequence], [IncidentEntryDate], [IncidentEditDate], [IncidentRemovalDate],[Active], [UserID_Admin])
VALUES
	(3, "FirstIncident","First Incident", "No Videogames.", '20210226', null, null, 1, 1),
     (3, "SecondIncident","Second Routine", "No Videogames.", '20210226', null, null, 1, 1),
     (3, "ThirdIncident","Third Incident", "No Videogames.", '20210226', null, null, 1, 1)

GO

print '' print '*** creating performance test records ***'
GO
INSERT INTO [dbo].[Performance]
	([UserIDCreator], [UserID_client], [PerformanceName], [PerformanceDescription])
VALUES
	(1, 3, "keep swimming", "Just keep swimming"),
	(1, 3, "Fake", "Nothing more than a fake"),
	(1, 1, "different", "Something different")
GO

print '' print '*** creating Performance Event test data ***'
GO
INSERT INTO [dbo].[PerformanceEvent]
		([PerformanceName], [DateOfOccurance], [EventDescription], [EventResult], [EventEditDate], [UserIDClient], [UserIDReporter])
	VALUES
		('Fake', '20210101', 'Happy Dance', 'Happy', null, 3, 1)
		, ('Fake', '20210102', 'Saved Whales', 'Heroic', null, 3, 1)
		, ('Fake', '20210103', 'Helped Granny', 'Helpful', '20210104', 3, 1)
		, ('Fake', '20210104', 'Taught Orphans', 'Studious', null, 3, 1)
		, ('Fake', '20210105', 'Rested', 'Relaxed', null, 3, 1)
GO

print '' print '*** creating Incident Event test data ***'
GO
INSERT INTO [dbo].[IncidentEvent]
		([IncidentName], [DateOfOccurence], [PersonsInvolved], [EventDescription],
          [EventConsequence],[EventEditDate],[UserIDClient], [UserIDAdmin])
	VALUES
		('FirstIncident', '20210226', 'Nick', 'Bit someone.', 'No video games.', null, 3, 1)
		, ('SecondIncident', '20210226', 'Nathaniel', 'ate dirt.', 'No video games.', null, 3, 1)
		, ('ThirdIncident', '20210226','Ryan', 'Wouldnt take trash out', 'No video games.', '20210104', 3, 1)
GO