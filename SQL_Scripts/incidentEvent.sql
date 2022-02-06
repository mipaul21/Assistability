-- <summary>
-- Nick Loesel
-- Created: 2021/04/16
--
-- Script for the creation of the IncidentEvent table
-- and related stored procedures
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/04/16
--
-- Definition of the Incident event table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating Incident Event table ***'
GO
CREATE TABLE [dbo].[IncidentEvent]
(
	[IncidentEventID]			[INT]Identity(1,1)		NOT NULL,
	[IncidentName]				[NVARCHAR](50)			NOT NULL,
	[DateOfOccurence]			[DATETIME]				NOT NULL,
	[PersonsInvolved]     		[NVARCHAR](250)			NOT NULL,
	[EventDescription]			[NVARCHAR](500)			NOT NULL,
	[EventConsequence]			[NVARCHAR](250)				NOT NULL,
    [EventEditDate]            	[DATETIME]              NULL,
	[UserIDClient]				[INT]					NOT NULL,
	[UserIDAdmin]				[INT]					NOT NULL
	
	CONSTRAINT [pk_IncidentEvent]		PRIMARY KEY([IncidentEventID] ASC),
	CONSTRAINT [fk_IncidentName]	FOREIGN KEY([IncidentName])
	REFERENCES [dbo].[Incident]([IncidentName]),
     CONSTRAINT [fk_UserID_Client_IncidentEvent] FOREIGN KEY([UserIDClient])
	REFERENCES [dbo].[UserAccount]([UserID]),
     CONSTRAINT [fk_UserID_Admin_IncidentEvent]		FOREIGN KEY([UserIDAdmin])
	REFERENCES [dbo].[UserAccount]([UserID]),
		
)
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/04/22
--
-- Creates sp_select_incident_events_by_incident_name stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_incident_events_by_incident_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_incident_events_by_incident_name]
	(
		@IncidentName		[NVARCHAR](50)	
	)
AS
	BEGIN
		SELECT [IncidentEventID],[IncidentName], [DateOfOccurence], [PersonsInvolved], [EventDescription],
          [EventConsequence],[EventEditDate],[UserIDClient], [UserIDAdmin]
		FROM IncidentEvent
		WHERE [IncidentName] = @IncidentName
		ORDER BY IncidentName ASC
	END
GO

-- <summary>
--
-- Nick Loesel
-- Created: 2021/04/25
--
-- The stored procedure for 
-- inserting an IncidentEvent.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_insert_incidentEvent ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_incidentEvent]
	(
		  @IncidentName				[nvarchar](50)
		, @DateOfOccurence			[datetime]
		, @PersonsInvolved			[nvarchar](250)
		, @EventDescription			[nvarchar](500)
		, @EventConsequence			[nvarchar](250)
		, @UserIDClient				[int]
		, @UserIDAdmin				[int]
		
	)
AS
	BEGIN
		INSERT INTO [dbo].[IncidentEvent] (
				[IncidentName],
				[DateOfOccurence],
				[PersonsInvolved],
				[EventDescription], 
				[EventConsequence],
				[UserIDClient],
				[UserIDAdmin]
				)
		VALUES (
				@IncidentName,
				@DateOfOccurence,
				@EventDescription, 
				@PersonsInvolved,
				@EventConsequence,
				@UserIDClient,
				@UserIDAdmin
				)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/04/25
--
-- Creates sp_update_incidentEvent stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_update_incidentEvent ***'
GO
CREATE PROCEDURE [dbo].[sp_update_incidentEvent]
	(
		@IncidentName        		    [NVARCHAR](50),
	    @OldEventDescription		    [NVARCHAR](500),	
		@OldPersonsInvolved				[NVARCHAR](250),
		@OldEventConsequence			[NVARCHAR](250),
		@OldDateOfOccurence				[DATETIME],
		
	    @NewEventDescription		    [NVARCHAR](500),	
		@NewPersonsInvolved				[NVARCHAR](250),
		@NewEventConsequence			[NVARCHAR](250),
		@NewDateOfOccurence				[DATETIME],
		@NewEventEditDate				[DATETIME]
	)
AS
	BEGIN
		UPDATE IncidentEvent
			SET 
				EventDescription = @NewEventDescription,
				DateOfOccurence = @NewDateOfOccurence,
				PersonsInvolved = @NewPersonsInvolved,
				EventConsequence = @NewEventConsequence,
				EventEditDate = @NewEventEditDate
			WHERE IncidentName = @IncidentName
				AND EventDescription = @OldEventDescription
				AND PersonsInvolved = @OldPersonsInvolved
				AND EventConsequence = @OldEventConsequence
				AND DateOfOccurence = @OldDateOfOccurence
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/04/26
--
-- Creates sp_delete_incidentEvent stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_delete_incidentEvent ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_incidentEvent]
	(
		@IncidentEventID        		    [INT]
	)
AS
	BEGIN
		DELETE FROM IncidentEvent
			WHERE IncidentEventID = @IncidentEventID
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/04/30
--
-- Creates sp_select_incidentEvent_by_incidentEventId stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_incidentEvent_by_incidentEventId ***'
GO
CREATE PROCEDURE [dbo].[sp_select_incidentEvent_by_incidentEventId]
	(
		@IncidentEventID		[INT]	
	)
AS
	BEGIN
		SELECT [IncidentEventID],[IncidentName], [DateOfOccurence], [PersonsInvolved], [EventDescription],
          [EventConsequence],[EventEditDate],[UserIDClient], [UserIDAdmin]
		FROM IncidentEvent
		WHERE [IncidentEventID] = @IncidentEventID
		ORDER BY IncidentName ASC
	END
GO
