-- <summary>
-- Nick Loesel
-- Created: 2021/03/17
--
-- Script for the creation of the Incident table
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
-- Created: 2021/03/17
--
-- Definition of the Incident table
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating Incident table ***'
GO
CREATE TABLE [dbo].[Incident]
(
	[IncidentName]			[NVARCHAR](50)			NOT NULL,
	[IncidentDescription]	[NVARCHAR](250)			NOT NULL,
	[DesiredConsequence]	[NVARCHAR](250)			NOT NULL,
	[IncidentEntryDate]     [DATETIME]				NOT NULL,
	[IncidentEditDate]		[DATETIME]				NULL,
	[IncidentRemovalDate]	[DATETIME]				NULL,
    [Active]            	[BIT]                   NOT NULL,
	[UserID_Client]			[INT]					NOT NULL,
	[UserID_Admin]			[INT]					NOT NULL
	
	CONSTRAINT [pk_IncidentName]		
	PRIMARY KEY([IncidentName] ASC )
     CONSTRAINT [fk_Incident_UserID_Client]		FOREIGN KEY([UserID_Client])
	REFERENCES [dbo].[UserAccount]([UserID])ON UPDATE CASCADE,
     CONSTRAINT [fk_Incident_UserID_Admin]		FOREIGN KEY([UserID_Admin])
	REFERENCES [dbo].[UserAccount]([UserID])
)
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/03/17
--
-- Creates sp_select_incidents_by_useraccountid stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_incidents_by_useraccountid ***'
GO
CREATE PROCEDURE [dbo].[sp_select_incidents_by_useraccountid]
	(
		@UserID_Client			[INT]
	)
AS
	BEGIN
		SELECT [IncidentName], [IncidentDescription], [DesiredConsequence], [IncidentEntryDate],
          [IncidentEditDate],[IncidentRemovalDate],[Active], [UserID_Client], [UserID_Admin]
		FROM Incident
		WHERE [UserID_Client] = @UserID_Client
		ORDER BY IncidentName ASC
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/18/03
--
-- Creates sp_insert_new_incident stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_insert_new_incident ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_new_incident]
	(
	 @IncidentName			[nvarchar] (50),
	 @IncidentDescription	[nvarchar] (250),
	 @DesiredConsequence	[nvarchar] (250),
	 @IncidentEntryDate		[DATETIME],
	 @Active				[BIT],
	 @UserID_Client			[INT],
	 @UserID_Admin			[INT]
	)
AS
	BEGIN
		INSERT INTO Incident
			( [IncidentName], [IncidentDescription],[DesiredConsequence],[IncidentEntryDate], [Active], [UserID_Client], [UserID_Admin])
		  VALUES
			(@IncidentName, @IncidentDescription, @DesiredConsequence, @IncidentEntryDate, @Active, @UserID_Client, @UserID_Admin)
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Mitchell Paul
-- Created: 2021/19/03
--
-- Creates sp_update_incident stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_update_incident ***'
GO
CREATE PROCEDURE [dbo].[sp_update_incident]
	(
		@IncidentName        		     [NVARCHAR](50),
	    @OldIncidentDescription		    [NVARCHAR](250),	
		@OldDesiredConsequence			[NVARCHAR](250),
	    @NewIncidentDescription			[NVARCHAR](250),	
		@NewDesiredConsequence			[NVARCHAR](250),
		@NewIncidentEditDate 				[DATETIME]
	)
AS
	BEGIN
		UPDATE Incident
			SET 
				IncidentDescription = @NewIncidentDescription,
				DesiredConsequence = @NewDesiredConsequence,
				IncidentEditDate = @NewIncidentEditDate
			WHERE IncidentName = @IncidentName
				AND IncidentDescription = @OldIncidentDescription
				AND DesiredConsequence = @OldDesiredConsequence
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/03/25
--
-- Creates sp_deactivate_incident stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_deactivate_incident ***'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_incident]
	(
		@UserID_Client			[INT],
		@IncidentName			[NVARCHAR](50),
		@IncidentRemovalDate		[DATETIME]
	)
AS
	BEGIN
		UPDATE Incident
			SET Active = 0,
				IncidentRemovalDate = @IncidentRemovalDate
			WHERE @UserID_Client = UserID_Client
			AND		@IncidentName = IncidentName
			RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/03/25
--
-- Creates sp_reactivate_incident stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_reactivate_incident ***'
GO
CREATE PROCEDURE [dbo].[sp_reactivate_incident]
	(
		@UserID_Client			[INT],
		@IncidentName			[NVARCHAR](50),
		@IncidentRemovalDate		[DATETIME]
	)
AS
	BEGIN
		UPDATE Incident
			SET Active = 1,
				IncidentRemovalDate = null
			WHERE @UserID_Client = UserID_Client
			AND		@IncidentName = IncidentName
			RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Nick Loesel
-- Created: 2021/03/25
--
-- Creates sp_select_incidents_by_active stored procedure
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_select_incidents_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_incidents_by_active]
	(
		@UserID_Client			[INT],
		@Active					[BIT]
	)
AS
	BEGIN
		SELECT [IncidentName], [IncidentDescription], [DesiredConsequence], [IncidentEntryDate],
          [IncidentEditDate],[IncidentRemovalDate],[Active], [UserID_Client], [UserID_Admin]
		FROM Incident
		WHERE [UserID_Client] = @UserID_Client
		AND	[Active] = @Active
		ORDER BY IncidentName ASC
	END
GO