-- <summary>
--
-- Whitney Vinson
-- Created: 2021/02/19
--
-- Script for the creation of the RoutineStep table
-- and related stored procedures.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** using database assistability_db ***'
GO
USE [assistability_db]
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/02/19
--
-- Definition of the RoutineStep table.
--
-- </summary>
-- <remarks>
-- Updater: William Clark
-- Updated: 2021/03/30
-- Update: Removed order number as alternate key
-- </remarks>

print '' print '*** creating RoutineStep table ***'
GO
CREATE TABLE [dbo].[RoutineStep](
	[RoutineStepID]				[int] IDENTITY(1, 1) 	NOT NULL
	, [RoutineName]				[nvarchar](50)			NOT NULL
	, [RoutineStepName]			[nvarchar](50)			NOT NULL
	, [RoutineStepDescription]	[nvarchar](150)			NOT NULL
	, [RoutineStepEntryDate]	[datetime]				NOT NULL
	, [RoutineStepEditDate]		[datetime]				NULL
	, [RoutineStepRemovalDate]	[datetime]				NULL
	, [RoutineStepOrderNumber]	[int]					NOT NULL
	, [Active]					[bit]					NOT NULL DEFAULT 1
	CONSTRAINT [pk_routineStepID] PRIMARY KEY([RoutineStepID] ASC)
	, CONSTRAINT [fk_routineName] FOREIGN KEY([RoutineName])
		REFERENCES [dbo].[Routines]([RoutineName])
)
GO

-- <summary>
-- Whitney Vinson
-- Created: 2021/02/19
--
-- The stored procedure for adding a step
-- </summary>
-- <remarks>
-- Updater: William Clark
-- Updated: 2021/03/30
-- Update: Fixed procedure to include not nullable column RoutineName
-- </remarks>

print '' print '*** creating sp_add_routine_step ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_routine_step]
	(
		@RoutineName			[nvarchar](50)
		, @RoutineStepName			[nvarchar](50)
		, @RoutineStepDescription	[nvarchar](150)
		, @RoutineStepEntryDate		[datetime]
		, @RoutineStepOrderNumber	[int]
	)
AS
	BEGIN
		INSERT INTO [dbo].[RoutineStep] (
				[RoutineName],
				[RoutineStepName],
				[RoutineStepDescription],
				[RoutineStepEntryDate],
				[RoutineStepOrderNumber]
				)
		VALUES (
				@RoutineName,
				@RoutineStepName,
				@RoutineStepDescription,
				@RoutineStepEntryDate,
				@RoutineStepOrderNumber
				)
		SELECT SCOPE_IDENTITY()
	END
GO

-- <summary>
-- Whitney Vinson
-- Created: 2021/02/19
--
-- The stored procedure for updating
-- a Routine Step
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_update_routine_step ***'
GO
CREATE PROCEDURE [dbo].[sp_update_routine_step]
	(
	  @RoutineStepID			[int]
	, @NewRoutineStepName		[nvarchar](50)
	, @NewRoutineStepDescription[nvarchar](150)
	, @NewRoutineStepEditDate	[datetime]	
	, @NewRoutineStepOrderNumber[int]
	, @OldRoutineStepName		[nvarchar](50)
	, @OldRoutineStepDescription[nvarchar](150)
	, @OldRoutineStepEditDate	[datetime]	
	, @OldRoutineStepOrderNumber[int]
	)
AS
	BEGIN
		UPDATE RoutineStep
			SET RoutineStepName = @NewRoutineStepName,
				RoutineStepDescription = @NewRoutineStepDescription,
				RoutineStepEditDate = @NewRoutineStepEditDate,
				RoutineStepOrderNumber = @NewRoutineStepOrderNumber
			WHERE RoutineStepID = @RoutineStepID
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- Whitney Vinson
-- Created: 2021/02/19
--
-- The stored procedure for selecting all routine steps
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_select_all_routine_steps ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_routine_steps]
AS
	BEGIN
		SELECT
			RoutineStepID
			, RoutineName
			, RoutineStepName
			, RoutineStepDescription
			, RoutineStepEntryDate
			, RoutineStepEditDate
			, RoutineStepRemovalDate
			, RoutineStepOrderNumber
			, Active
		FROM RoutineStep
		ORDER BY RoutineStepID ASC
	END
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/02/19
--
-- The stored procedure for selecting all routine steps
-- by routine name.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_select_routine_steps_by_routine_name ***'
GO
CREATE PROCEDURE [dbo].[sp_select_routine_steps_by_routine_name]
	(
		@RoutineName		[NVARCHAR](50),
		@Active				[bit]
	)
AS
	BEGIN
		SELECT
			RoutineStepID
			, RoutineName
			, RoutineStepName
			, RoutineStepDescription
			, RoutineStepEntryDate
			, RoutineStepEditDate
			, RoutineStepRemovalDate
			, RoutineStepOrderNumber
			, Active
		FROM RoutineStep
		WHERE RoutineName = @RoutineName
		AND Active = @Active
		ORDER BY RoutineStepOrderNumber ASC
	END
GO

-- <summary>
--
-- Whitney Vinson
-- Created: 2021/02/19
--
-- The stored procedure for selecting 
-- all routine steps by active.
--
-- </summary>
-- <remarks>
-- </remarks>

print '' print '*** creating sp_select_routine_steps_by_active ***'
GO
CREATE PROCEDURE [dbo].[sp_select_routine_steps_by_active]
(
	@Active			[bit]
)
AS
	BEGIN
		SELECT
			RoutineStepID
			, RoutineName
			, RoutineStepName
			, RoutineStepDescription
			, RoutineStepEntryDate
			, RoutineStepEditDate
			, RoutineStepRemovalDate
			, RoutineStepOrderNumber
			, Active
		FROM RoutineStep
		WHERE Active = @Active
		ORDER BY RoutineStepOrderNumber ASC
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/03/31
--
-- Update a routine step
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_update_routinestep ***'
GO
CREATE PROCEDURE [dbo].[sp_update_routinestep]
(
	@OldRoutineStepID			[INT]
	, @OldRoutineStepName		[NVARCHAR](50)
	, @OldRoutineStepDescription	[NVARCHAR](150)
	, @OldRoutineStepEntryDate	[DATETIME]
	, @OldRoutineStepOrderNumber	[INT]
	, @OldActive				[BIT]
	, @NewRoutineStepName		[NVARCHAR](50)
	, @NewRoutineStepDescription	[NVARCHAR](150)
	, @NewRoutineStepEntryDate	[DATETIME]
	, @NewRoutineStepEditDate	[DATETIME] NULL
	, @NewRoutineStepRemovalDate	[DATETIME] NULL
	, @NewRoutineStepOrderNumber	[INT]
	, @NewActive				[BIT]
)
AS
	BEGIN
		UPDATE RoutineStep
		SET RoutineStepName = @NewRoutineStepName
			, RoutineStepDescription = @NewRoutineStepDescription
			, RoutineStepEntryDate = @NewRoutineStepEntryDate
			, RoutineStepEditDate = @NewRoutineStepEditDate
			, RoutineStepRemovalDate = @NewRoutineStepRemovalDate
			, RoutineStepOrderNumber = @NewRoutineStepOrderNumber
			, Active = @NewActive
		WHERE RoutineStepID = @OldRoutineStepID
		RETURN @@ROWCOUNT
	END
GO

-- <summary>
-- William Clark
-- Created: 2021/03/31
--
-- Procedure for updating a routine step to ensure proper ordering
-- </summary>
-- <remarks>
-- </remarks>
print '' print '*** creating sp_update_routinestep_order ***'
GO
CREATE PROCEDURE [dbo].[sp_update_routinestep_order]
(
	@RoutineStepID			[INT]
	, @RoutineStepOrderNumber	[INT]
)
AS
	BEGIN
		UPDATE RoutineStep
		SET RoutineStepOrderNumber = @RoutineStepOrderNumber
		WHERE RoutineStepID = @RoutineStepID
		RETURN @@ROWCOUNT
	END
GO
