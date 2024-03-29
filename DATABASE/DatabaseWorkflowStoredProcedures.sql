/****** Object:  Table [dbo].[ENTUserAccount]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTUserAccount]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTUserAccount](
	[ENTUserAccountId] [int] IDENTITY(2,1) NOT NULL,
	[WindowsAccountName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FirstName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsActive] [bit] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[ENTUserAccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_UserAccount] UNIQUE NONCLUSTERED 
(
	[WindowsAccountName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ENTWorkflow]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflow]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWorkflow](
	[ENTWorkflowId] [int] IDENTITY(1,1) NOT NULL,
	[WorkflowName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ENTWorkflowObjectName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWorkflow] PRIMARY KEY CLUSTERED 
(
	[ENTWorkflowId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflow]') AND name = N'IX_ENTWorkflow')
CREATE UNIQUE NONCLUSTERED INDEX [IX_ENTWorkflow] ON [dbo].[ENTWorkflow] 
(
	[WorkflowName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  StoredProcedure [dbo].[ENTWFTransitionSelectByFromStateId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransitionSelectByFromStateId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFTransitionSelectByFromStateId]
(
    @FromENTWFStateId int
)
AS
    SET NOCOUNT ON

    DECLARE @SQL varchar(1000)

    SET @SQL = ''SELECT ENTWFTransitionId, ENTWFTransition.ENTWorkflowId, TransitionName, FromENTWFStateId, ToENTWFStateId, PostTransitionMethodName, ENTWFTransition.InsertDate, ENTWFTransition.InsertENTUserAccountId, ENTWFTransition.UpdateDate, ENTWFTransition.UpdateENTUserAccountId, ENTWFTransition.Version,
                ENTWFState.StateName AS FromStateName, ENTWFState1.StateName AS ToStateName
           FROM ENTWFTransition 
     INNER JOIN ENTWFState AS ENTWFState1 
             ON ENTWFState1.ENTWFStateId = ENTWFTransition.ToENTWFStateId 
LEFT OUTER JOIN ENTWFState 
             ON ENTWFState.ENTWFStateId = ENTWFTransition.FromENTWFStateId ''

    IF @FromENTWFStateId IS NULL 
        SET @SQL = @SQL + ''WHERE FromENTWFStateId IS NULL''
    ELSE
        SET @SQL = @SQL + ''WHERE FromENTWFStateID = '' + CONVERT(varchar(15), @FromENTWFStateId)

    EXEC(@SQL)

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupSelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupSelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFOwnerGroupSelectAll]
AS
    SET NOCOUNT ON

    SELECT ENTWFOwnerGroupId, ENTWorkflowId, OwnerGroupName, DefaultENTUserAccountId, IsDefaultSameAsLast, Description, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFOwnerGroup

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupSelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupSelectById]
(
    @ENTWFOwnerGroupId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFOwnerGroupId, ENTWorkflowId, OwnerGroupName, DefaultENTUserAccountId, IsDefaultSameAsLast, Description, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFOwnerGroup
     WHERE ENTWFOwnerGroupId = @ENTWFOwnerGroupId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStateSelectCountByENTWFOwnerGroupId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStateSelectCountByENTWFOwnerGroupId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFStateSelectCountByENTWFOwnerGroupId]
(
	@ENTWFOwnerGroupId int	
)
AS
	SET NOCOUNT ON

	SELECT COUNT(1) AS CountOfStates
	  FROM ENTWFState
	 WHERE ENTWFOwnerGroupId = @ENTWFOwnerGroupId
	
	RETURN' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupDelete]
(
    @ENTWFOwnerGroupId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFOwnerGroup
     WHERE ENTWFOwnerGroupId = @ENTWFOwnerGroupId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupInsert]
(
    @ENTWFOwnerGroupId  int OUTPUT, 
    @ENTWorkflowId  int, 
    @OwnerGroupName  varchar(50), 
    @DefaultENTUserAccountId  int, 
    @IsDefaultSameAsLast  bit, 
    @Description  varchar(255), 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFOwnerGroup (ENTWorkflowId, OwnerGroupName, DefaultENTUserAccountId, IsDefaultSameAsLast, Description, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWorkflowId, 
                @OwnerGroupName, 
                @DefaultENTUserAccountId, 
                @IsDefaultSameAsLast, 
                @Description, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFOwnerGroupId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupUpdate]
(
    @ENTWFOwnerGroupId  int, 
    @ENTWorkflowId  int, 
    @OwnerGroupName  varchar(50), 
    @DefaultENTUserAccountId  int, 
    @IsDefaultSameAsLast  bit, 
    @Description  varchar(255), 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFOwnerGroup
       SET 
           ENTWorkflowId = @ENTWorkflowId, 
           OwnerGroupName = @OwnerGroupName, 
           DefaultENTUserAccountId = @DefaultENTUserAccountId, 
           IsDefaultSameAsLast = @IsDefaultSameAsLast, 
           Description = @Description, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFOwnerGroupId = @ENTWFOwnerGroupId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  Table [dbo].[ENTWFOwnerGroup]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWFOwnerGroup](
	[ENTWFOwnerGroupId] [int] IDENTITY(1,1) NOT NULL,
	[ENTWorkflowId] [int] NOT NULL,
	[OwnerGroupName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DefaultENTUserAccountId] [int] NULL,
	[IsDefaultSameAsLast] [bit] NOT NULL,
	[Description] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWFOwnerGroup] PRIMARY KEY CLUSTERED 
(
	[ENTWFOwnerGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroup]') AND name = N'IX_ENTWFOwnerGroup')
CREATE UNIQUE NONCLUSTERED INDEX [IX_ENTWFOwnerGroup] ON [dbo].[ENTWFOwnerGroup] 
(
	[ENTWorkflowId] ASC,
	[OwnerGroupName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupSelectCountByNameWorkflowId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupSelectCountByNameWorkflowId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFOwnerGroupSelectCountByNameWorkflowId]
(
	@ENTWFOwnerGroupId int,
	@ENTWorkflowId int,
	@OwnerGroupName varchar(50)
)
AS
	SET NOCOUNT ON

	SELECT COUNT(1) AS CountOfNames
	  FROM ENTWFOwnerGroup
	 WHERE OwnerGroupName = @OwnerGroupName
  	   AND ENTWorkflowId = @ENTWorkflowId
	   AND ENTWFOwnerGroupId <> @ENTWFOwnerGroupId
	
	RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupSelectByENTWorkflowId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupSelectByENTWorkflowId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFOwnerGroupSelectByENTWorkflowId]
(
	@ENTWorkflowId int
)
AS
	SET NOCOUNT ON
	
    SELECT ENTWFOwnerGroupId, ENTWorkflowId, OwnerGroupName, DefaultENTUserAccountId, IsDefaultSameAsLast, Description, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFOwnerGroup
     WHERE ENTWorkflowId = @ENTWorkflowId
	
	RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStatePropertySelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStatePropertySelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFStatePropertySelectAll]
AS
    SET NOCOUNT ON

    SELECT ENTWFStatePropertyId, ENTWFStateId, PropertyName, Required, ReadOnly, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFStateProperty

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStatePropertySelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStatePropertySelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStatePropertySelectById]
(
    @ENTWFStatePropertyId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFStatePropertyId, ENTWFStateId, PropertyName, Required, ReadOnly, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFStateProperty
     WHERE ENTWFStatePropertyId = @ENTWFStatePropertyId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStatePropertyDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStatePropertyDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStatePropertyDelete]
(
    @ENTWFStatePropertyId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFStateProperty
     WHERE ENTWFStatePropertyId = @ENTWFStatePropertyId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStatePropertyInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStatePropertyInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStatePropertyInsert]
(
    @ENTWFStatePropertyId  int OUTPUT, 
    @ENTWFStateId  int, 
    @PropertyName  varchar(255), 
    @Required  bit, 
    @ReadOnly  bit, 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFStateProperty (ENTWFStateId, PropertyName, Required, ReadOnly, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWFStateId, 
                @PropertyName, 
                @Required, 
                @ReadOnly, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFStatePropertyId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStatePropertyUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStatePropertyUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStatePropertyUpdate]
(
    @ENTWFStatePropertyId  int, 
    @ENTWFStateId  int, 
    @PropertyName  varchar(255), 
    @Required  bit, 
    @ReadOnly  bit, 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFStateProperty
       SET 
           ENTWFStateId = @ENTWFStateId, 
           PropertyName = @PropertyName, 
           Required = @Required, 
           ReadOnly = @ReadOnly, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFStatePropertyId = @ENTWFStatePropertyId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStatePropertySelectByENTWFStateId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStatePropertySelectByENTWFStateId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStatePropertySelectByENTWFStateId]
(
    @ENTWFStateId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFStatePropertyId, ENTWFStateId, PropertyName, Required, ReadOnly, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFStateProperty
     WHERE ENTWFStateId = @ENTWFStateId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStatePropertyDeleteByENTWFStateId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStatePropertyDeleteByENTWFStateId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStatePropertyDeleteByENTWFStateId]
(
    @ENTWFStateId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFStateProperty
     WHERE ENTWFStateId = @ENTWFStateId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFTransitionDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransitionDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFTransitionDelete]
(
    @ENTWFTransitionId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFTransition
     WHERE ENTWFTransitionId = @ENTWFTransitionId

    RETURN

' 
END
GO
/****** Object:  Table [dbo].[ENTWFState]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWFState](
	[ENTWFStateId] [int] IDENTITY(1,1) NOT NULL,
	[ENTWorkflowId] [int] NOT NULL,
	[StateName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ENTWFOwnerGroupId] [int] NULL,
	[IsOwnerSubmitter] [bit] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWFState] PRIMARY KEY CLUSTERED 
(
	[ENTWFStateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFState]') AND name = N'IX_ENTWFState')
CREATE UNIQUE NONCLUSTERED INDEX [IX_ENTWFState] ON [dbo].[ENTWFState] 
(
	[ENTWorkflowId] ASC,
	[StateName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[ENTWFTransition]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWFTransition](
	[ENTWFTransitionId] [int] IDENTITY(1,1) NOT NULL,
	[ENTWorkflowId] [int] NOT NULL,
	[TransitionName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FromENTWFStateId] [int] NULL,
	[ToENTWFStateId] [int] NOT NULL,
	[PostTransitionMethodName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWFTransition] PRIMARY KEY CLUSTERED 
(
	[ENTWFTransitionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransition]') AND name = N'IX_ENTWFTransition_1')
CREATE UNIQUE NONCLUSTERED INDEX [IX_ENTWFTransition_1] ON [dbo].[ENTWFTransition] 
(
	[FromENTWFStateId] ASC,
	[ToENTWFStateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
/****** Object:  Table [dbo].[ENTWFItem]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWFItem](
	[ENTWFItemId] [int] IDENTITY(1,1) NOT NULL,
	[ENTWorkflowId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[SubmitterENTUserAccountId] [int] NOT NULL,
	[CurrentWFStateId] [int] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWFItem] PRIMARY KEY CLUSTERED 
(
	[ENTWFItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ENTWFItemOwner]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwner]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWFItemOwner](
	[ENTWFItemOwnerId] [int] IDENTITY(1,1) NOT NULL,
	[ENTWFItemId] [int] NOT NULL,
	[ENTWFOwnerGroupId] [int] NOT NULL,
	[ENTUserAccountId] [int] NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWFItemOwner] PRIMARY KEY CLUSTERED 
(
	[ENTWFItemOwnerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ENTWFItemStateHistory]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWFItemStateHistory](
	[ENTWFItemStateHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ENTWFItemId] [int] NOT NULL,
	[ENTWFStateId] [int] NOT NULL,
	[ENTUserAccountId] [int] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWFItemTransition] PRIMARY KEY CLUSTERED 
(
	[ENTWFItemStateHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ENTWFOwnerGroupUserAccount]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccount]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ENTWFOwnerGroupUserAccount](
	[ENTWFOwnerGroupUserAccountId] [int] IDENTITY(1,1) NOT NULL,
	[ENTWFOwnerGroupId] [int] NOT NULL,
	[ENTUserAccountId] [int] NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[InsertENTUserAccountId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateENTUserAccountId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ENTWFOwnerGroupUserAccount] PRIMARY KEY CLUSTERED 
(
	[ENTWFOwnerGroupUserAccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWorkflowSelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflowSelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWorkflowSelectAll]
AS
    SET NOCOUNT ON

    SELECT ENTWorkflowId, WorkflowName, ENTWorkflowObjectName, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWorkflow

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWorkflowSelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflowSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWorkflowSelectById]
(
    @ENTWorkflowId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWorkflowId, WorkflowName, ENTWorkflowObjectName, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWorkflow
     WHERE ENTWorkflowId = @ENTWorkflowId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWorkflowDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflowDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWorkflowDelete]
(
    @ENTWorkflowId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWorkflow
     WHERE ENTWorkflowId = @ENTWorkflowId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWorkflowInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflowInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWorkflowInsert]
(
    @ENTWorkflowId  int OUTPUT, 
    @WorkflowName  varchar(50), 
    @ENTWorkflowObjectName  varchar(255), 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWorkflow (WorkflowName, ENTWorkflowObjectName, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (                
                @WorkflowName, 
                @ENTWorkflowObjectName, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWorkflowId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWorkflowUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflowUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWorkflowUpdate]
(
    @ENTWorkflowId  int, 
    @WorkflowName  varchar(50), 
    @ENTWorkflowObjectName  varchar(255), 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWorkflow
       SET 
           WorkflowName = @WorkflowName, 
           ENTWorkflowObjectName = @ENTWorkflowObjectName, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWorkflowId = @ENTWorkflowId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWorkflowSelectByObjectName]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWorkflowSelectByObjectName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWorkflowSelectByObjectName]
(
    @ENTWorkflowObjectName varchar(255)
)
AS
    SET NOCOUNT ON

    SELECT ENTWorkflowId, WorkflowName, ENTWorkflowObjectName, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWorkflow
     WHERE ENTWorkflowObjectName = @ENTWorkflowObjectName

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemOwnerSelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwnerSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemOwnerSelectById]
(
    @ENTWFItemOwnerId int
)
AS
    SET NOCOUNT ON

         SELECT ENTWFItemOwnerId, ENTWFItemId, ENTWFOwnerGroupId, ENTWFItemOwner.ENTUserAccountId, ENTWFItemOwner.InsertDate, ENTWFItemOwner.InsertENTUserAccountId, ENTWFItemOwner.UpdateDate, ENTWFItemOwner.UpdateENTUserAccountId, ENTWFItemOwner.Version,
		        LastName + '', '' + FirstName AS UserName
           FROM ENTWFItemOwner
LEFT OUTER JOIN ENTUserAccount
             ON ENTWFItemOwner.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
          WHERE ENTWFItemOwnerId = @ENTWFItemOwnerId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemOwnerSelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwnerSelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFItemOwnerSelectAll]
AS
    SET NOCOUNT ON

         SELECT ENTWFItemOwnerId, ENTWFItemId, ENTWFOwnerGroupId, ENTWFItemOwner.ENTUserAccountId, ENTWFItemOwner.InsertDate, ENTWFItemOwner.InsertENTUserAccountId, ENTWFItemOwner.UpdateDate, ENTWFItemOwner.UpdateENTUserAccountId, ENTWFItemOwner.Version,
		        LastName + '', '' + FirstName AS UserName
           FROM ENTWFItemOwner
LEFT OUTER JOIN ENTUserAccount
             ON ENTWFItemOwner.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
              

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemStateHistorySelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistorySelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFItemStateHistorySelectAll]
AS
    SET NOCOUNT ON

    SELECT ENTWFItemStateHistoryId, ENTWFItemId, ENTWFItemStateHistory.ENTWFStateId, ENTWFItemStateHistory.ENTUserAccountId, ENTWFItemStateHistory.InsertDate, ENTWFItemStateHistory.InsertENTUserAccountId, ENTWFItemStateHistory.UpdateDate, ENTWFItemStateHistory.UpdateENTUserAccountId, ENTWFItemStateHistory.Version,
           StateName, 
           ENTUserAccount.LastName + '', '' + ENTUserAccount.FirstName AS OwnerName, 
           Inserted.LastName + '', '' + Inserted.FirstName AS InsertedBy
      FROM ENTWFItemStateHistory
INNER JOIN ENTWFState
        ON ENTWFItemStateHistory.ENTWFStateId = ENTWFState.ENTWFStateId
INNER JOIN ENTUserAccount
        ON ENTWFItemStateHistory.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
INNER JOIN ENTUserAccount Inserted
        ON ENTWFItemStateHistory.InsertENTUserAccountId = Inserted.ENTUserAccountId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemStateHistorySelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistorySelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemStateHistorySelectById]
(
    @ENTWFItemStateHistoryId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFItemStateHistoryId, ENTWFItemId, ENTWFItemStateHistory.ENTWFStateId, ENTWFItemStateHistory.ENTUserAccountId, ENTWFItemStateHistory.InsertDate, ENTWFItemStateHistory.InsertENTUserAccountId, ENTWFItemStateHistory.UpdateDate, ENTWFItemStateHistory.UpdateENTUserAccountId, ENTWFItemStateHistory.Version,
           StateName, 
           ENTUserAccount.LastName + '', '' + ENTUserAccount.FirstName AS OwnerName, 
           Inserted.LastName + '', '' + Inserted.FirstName AS InsertedBy
      FROM ENTWFItemStateHistory
INNER JOIN ENTWFState
        ON ENTWFItemStateHistory.ENTWFStateId = ENTWFState.ENTWFStateId
INNER JOIN ENTUserAccount
        ON ENTWFItemStateHistory.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
INNER JOIN ENTUserAccount Inserted
        ON ENTWFItemStateHistory.InsertENTUserAccountId = Inserted.ENTUserAccountId
     WHERE ENTWFItemStateHistoryId = @ENTWFItemStateHistoryId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupUserAccountSelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccountSelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFOwnerGroupUserAccountSelectAll]
AS
    SET NOCOUNT ON

    SELECT ENTWFOwnerGroupUserAccountId, ENTWFOwnerGroupId, ENTWFOwnerGroupUserAccount.ENTUserAccountId, ENTWFOwnerGroupUserAccount.InsertDate, ENTWFOwnerGroupUserAccount.InsertENTUserAccountId, ENTWFOwnerGroupUserAccount.UpdateDate, ENTWFOwnerGroupUserAccount.UpdateENTUserAccountId, ENTWFOwnerGroupUserAccount.Version,
		   LastName + '', '' + FirstName AS UserName
      FROM ENTWFOwnerGroupUserAccount
INNER JOIN ENTUserAccount
        ON ENTWFOwnerGroupUserAccount.ENTUserAccountId = ENTUserAccount.ENTUserAccountId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupUserAccountSelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccountSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupUserAccountSelectById]
(
    @ENTWFOwnerGroupUserAccountId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFOwnerGroupUserAccountId, ENTWFOwnerGroupId, ENTWFOwnerGroupUserAccount.ENTUserAccountId, ENTWFOwnerGroupUserAccount.InsertDate, ENTWFOwnerGroupUserAccount.InsertENTUserAccountId, ENTWFOwnerGroupUserAccount.UpdateDate, ENTWFOwnerGroupUserAccount.UpdateENTUserAccountId, ENTWFOwnerGroupUserAccount.Version,
		   LastName + '', '' + FirstName AS UserName
      FROM ENTWFOwnerGroupUserAccount
INNER JOIN ENTUserAccount
        ON ENTWFOwnerGroupUserAccount.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
     WHERE ENTWFOwnerGroupUserAccountId = @ENTWFOwnerGroupUserAccountId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemSelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemSelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFItemSelectAll]
AS
    SET NOCOUNT ON

    SELECT ENTWFItemId, ENTWorkflowId, ItemId, SubmitterENTUserAccountId, CurrentWFStateId, ENTWFItem.InsertDate, ENTWFItem.InsertENTUserAccountId, ENTWFItem.UpdateDate, ENTWFItem.UpdateENTUserAccountId, ENTWFItem.Version,
	       LastName + '', '' + FirstName AS SubmitterUserName
      FROM ENTWFItem
INNER JOIN ENTUserAccount
        ON ENTWFItem.SubmitterENTUserAccountId = ENTUserAccount.ENTUserAccountId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemSelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemSelectById]
(
    @ENTWFItemId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFItemId, ENTWorkflowId, ItemId, SubmitterENTUserAccountId, CurrentWFStateId, ENTWFItem.InsertDate, ENTWFItem.InsertENTUserAccountId, ENTWFItem.UpdateDate, ENTWFItem.UpdateENTUserAccountId, ENTWFItem.Version,
	       LastName + '', '' + FirstName AS SubmitterUserName
      FROM ENTWFItem
INNER JOIN ENTUserAccount
        ON ENTWFItem.SubmitterENTUserAccountId = ENTUserAccount.ENTUserAccountId
     WHERE ENTWFItemId = @ENTWFItemId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupUserAccountSelectByENTWFOwnerGroupId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccountSelectByENTWFOwnerGroupId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFOwnerGroupUserAccountSelectByENTWFOwnerGroupId]
(
	@ENTWFOwnerGroupId int
)
AS
	SET NOCOUNT ON
	
	SELECT ENTWFOwnerGroupUserAccountId, ENTWFOwnerGroupId, ENTWFOwnerGroupUserAccount.ENTUserAccountId, ENTWFOwnerGroupUserAccount.InsertDate, ENTWFOwnerGroupUserAccount.InsertENTUserAccountId, ENTWFOwnerGroupUserAccount.UpdateDate, ENTWFOwnerGroupUserAccount.UpdateENTUserAccountId, ENTWFOwnerGroupUserAccount.Version,
		   LastName + '', '' + FirstName AS UserName
      FROM ENTWFOwnerGroupUserAccount
INNER JOIN ENTUserAccount
        ON ENTWFOwnerGroupUserAccount.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
     WHERE ENTWFOwnerGroupId = @ENTWFOwnerGroupId
	
	RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemOwnerSelectByENTWFItemId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwnerSelectByENTWFItemId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFItemOwnerSelectByENTWFItemId]
(
    @ENTWFItemId int
)
AS
    SET NOCOUNT ON

         SELECT ENTWFItemOwnerId, ENTWFItemId, ENTWFOwnerGroupId, ENTWFItemOwner.ENTUserAccountId, ENTWFItemOwner.InsertDate, ENTWFItemOwner.InsertENTUserAccountId, ENTWFItemOwner.UpdateDate, ENTWFItemOwner.UpdateENTUserAccountId, ENTWFItemOwner.Version,
		        LastName + '', '' + FirstName AS UserName
           FROM ENTWFItemOwner
LEFT OUTER JOIN ENTUserAccount
             ON ENTWFItemOwner.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
          WHERE ENTWFItemId = @ENTWFItemId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemStateHistorySelectByENTWFItemId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistorySelectByENTWFItemId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFItemStateHistorySelectByENTWFItemId]
(
    @ENTWFItemId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFItemStateHistoryId, ENTWFItemId, ENTWFItemStateHistory.ENTWFStateId, ENTWFItemStateHistory.ENTUserAccountId, ENTWFItemStateHistory.InsertDate, ENTWFItemStateHistory.InsertENTUserAccountId, ENTWFItemStateHistory.UpdateDate, ENTWFItemStateHistory.UpdateENTUserAccountId, ENTWFItemStateHistory.Version,
           StateName, 
           ENTUserAccount.LastName + '', '' + ENTUserAccount.FirstName AS OwnerName, 
           Inserted.LastName + '', '' + Inserted.FirstName AS InsertedBy
      FROM ENTWFItemStateHistory
INNER JOIN ENTWFState
        ON ENTWFItemStateHistory.ENTWFStateId = ENTWFState.ENTWFStateId
INNER JOIN ENTUserAccount
        ON ENTWFItemStateHistory.ENTUserAccountId = ENTUserAccount.ENTUserAccountId
INNER JOIN ENTUserAccount Inserted
        ON ENTWFItemStateHistory.InsertENTUserAccountId = Inserted.ENTUserAccountId
     WHERE ENTWFItemId = @ENTWFItemId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemSelectByItemId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemSelectByItemId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemSelectByItemId]
(
    @ENTWorkflowId int,
    @ItemId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFItemId, ENTWorkflowId, ItemId, SubmitterENTUserAccountId, CurrentWFStateId, ENTWFItem.InsertDate, ENTWFItem.InsertENTUserAccountId, ENTWFItem.UpdateDate, ENTWFItem.UpdateENTUserAccountId, ENTWFItem.Version,
	       LastName + '', '' + FirstName AS SubmitterUserName
      FROM ENTWFItem
INNER JOIN ENTUserAccount
        ON ENTWFItem.SubmitterENTUserAccountId = ENTUserAccount.ENTUserAccountId
     WHERE ENTWorkflowId = @ENTWorkflowId
       AND ItemId = @ItemId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemOwnerSelectLastUserByGroupId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwnerSelectLastUserByGroupId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemOwnerSelectLastUserByGroupId]
(
    @ENTWFOwnerGroupId int,
    @ENTUserAccountId int
)
AS
    SET NOCOUNT ON

     SELECT TOP(1) ENTUserAccountId
       FROM ENTWFItem
 INNER JOIN ENTWFItemOwner
         ON ENTWFItem.ENTWFItemId = ENTWFItemOwner.ENTWFItemId
      WHERE ENTWFOwnerGroupId = @ENTWFOwnerGroupId
        AND ENTWFItem.InsertENTUserAccountId = @ENTUserAccountId
   ORDER BY ENTWFItem.InsertDate Desc

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemOwnerDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwnerDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemOwnerDelete]
(
    @ENTWFItemOwnerId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFItemOwner
     WHERE ENTWFItemOwnerId = @ENTWFItemOwnerId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemOwnerInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwnerInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemOwnerInsert]
(
    @ENTWFItemOwnerId  int OUTPUT, 
    @ENTWFItemId  int, 
    @ENTWFOwnerGroupId  int, 
    @ENTUserAccountId  int, 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFItemOwner (ENTWFItemId, ENTWFOwnerGroupId, ENTUserAccountId, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWFItemId, 
                @ENTWFOwnerGroupId, 
                @ENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFItemOwnerId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemOwnerUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwnerUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemOwnerUpdate]
(
    @ENTWFItemOwnerId  int, 
    @ENTWFItemId  int, 
    @ENTWFOwnerGroupId  int, 
    @ENTUserAccountId  int, 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFItemOwner
       SET 
           ENTWFItemId = @ENTWFItemId, 
           ENTWFOwnerGroupId = @ENTWFOwnerGroupId, 
           ENTUserAccountId = @ENTUserAccountId, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFItemOwnerId = @ENTWFItemOwnerId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemStateHistoryDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistoryDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemStateHistoryDelete]
(
    @ENTWFItemStateHistoryId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFItemStateHistory
     WHERE ENTWFItemStateHistoryId = @ENTWFItemStateHistoryId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemStateHistoryInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistoryInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemStateHistoryInsert]
(
    @ENTWFItemStateHistoryId  int OUTPUT, 
    @ENTWFItemId  int, 
    @ENTWFStateId  int, 
    @ENTUserAccountId  int, 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFItemStateHistory (ENTWFItemId, ENTWFStateId, ENTUserAccountId, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWFItemId, 
                @ENTWFStateId, 
                @ENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFItemStateHistoryId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemStateHistoryUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistoryUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemStateHistoryUpdate]
(
    @ENTWFItemStateHistoryId  int, 
    @ENTWFItemId  int, 
    @ENTWFStateId  int, 
    @ENTUserAccountId  int, 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFItemStateHistory
       SET 
           ENTWFItemId = @ENTWFItemId, 
           ENTWFStateId = @ENTWFStateId, 
           ENTUserAccountId = @ENTUserAccountId, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFItemStateHistoryId = @ENTWFItemStateHistoryId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemDelete]
(
    @ENTWFItemId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFItem
     WHERE ENTWFItemId = @ENTWFItemId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemInsert]
(
    @ENTWFItemId  int OUTPUT, 
    @ENTWorkflowId  int, 
    @ItemId  int, 
    @SubmitterENTUserAccountId  int, 
    @CurrentWFStateId  int, 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFItem (ENTWorkflowId, ItemId, SubmitterENTUserAccountId, CurrentWFStateId, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWorkflowId, 
                @ItemId, 
                @SubmitterENTUserAccountId, 
                @CurrentWFStateId, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFItemId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemUpdate]
(
    @ENTWFItemId  int, 
    @ENTWorkflowId  int, 
    @ItemId  int, 
    @SubmitterENTUserAccountId  int, 
    @CurrentWFStateId  int, 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFItem
       SET 
           ENTWorkflowId = @ENTWorkflowId, 
           ItemId = @ItemId, 
           SubmitterENTUserAccountId = @SubmitterENTUserAccountId, 
           CurrentWFStateId = @CurrentWFStateId, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFItemId = @ENTWFItemId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStateSelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStateSelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFStateSelectAll]
AS
    SET NOCOUNT ON

    SELECT ENTWFStateId, ENTWorkflowId, StateName, Description, ENTWFOwnerGroupId, IsOwnerSubmitter, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFState

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStateSelectByENTWorkflowId]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStateSelectByENTWorkflowId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStateSelectByENTWorkflowId]
(
    @ENTWorkflowId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFStateId, ENTWorkflowId, StateName, Description, ENTWFOwnerGroupId, IsOwnerSubmitter, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFState
     WHERE ENTWorkflowId = @ENTWorkflowId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFTransitionSelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransitionSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFTransitionSelectById]
(
    @ENTWFTransitionId int
)
AS
    SET NOCOUNT ON

         SELECT ENTWFTransitionId, ENTWFTransition.ENTWorkflowId, TransitionName, FromENTWFStateId, ToENTWFStateId, PostTransitionMethodName, ENTWFTransition.InsertDate, ENTWFTransition.InsertENTUserAccountId, ENTWFTransition.UpdateDate, ENTWFTransition.UpdateENTUserAccountId, ENTWFTransition.Version,
                ENTWFState.StateName AS FromStateName, ENTWFState1.StateName AS ToStateName
           FROM ENTWFTransition 
     INNER JOIN ENTWFState AS ENTWFState1 
             ON ENTWFState1.ENTWFStateId = ENTWFTransition.ToENTWFStateId 
LEFT OUTER JOIN ENTWFState 
             ON ENTWFState.ENTWFStateId = ENTWFTransition.FromENTWFStateId   
          WHERE ENTWFTransitionId = @ENTWFTransitionId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStateSelectById]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStateSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStateSelectById]
(
    @ENTWFStateId int
)
AS
    SET NOCOUNT ON

    SELECT ENTWFStateId, ENTWorkflowId, StateName, Description, ENTWFOwnerGroupId, IsOwnerSubmitter, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId, Version
      FROM ENTWFState
     WHERE ENTWFStateId = @ENTWFStateId

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStateDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStateDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStateDelete]
(
    @ENTWFStateId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFState
     WHERE ENTWFStateId = @ENTWFStateId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStateInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStateInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStateInsert]
(
    @ENTWFStateId  int OUTPUT, 
    @ENTWorkflowId  int, 
    @StateName  varchar(50), 
    @Description  varchar(255), 
    @ENTWFOwnerGroupId  int, 
    @IsOwnerSubmitter  bit, 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFState (ENTWorkflowId, StateName, Description, ENTWFOwnerGroupId, IsOwnerSubmitter, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWorkflowId, 
                @StateName, 
                @Description, 
                @ENTWFOwnerGroupId, 
                @IsOwnerSubmitter, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFStateId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFStateUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFStateUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFStateUpdate]
(
    @ENTWFStateId  int, 
    @ENTWorkflowId  int, 
    @StateName  varchar(50), 
    @Description  varchar(255), 
    @ENTWFOwnerGroupId  int, 
    @IsOwnerSubmitter  bit, 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFState
       SET 
           ENTWorkflowId = @ENTWorkflowId, 
           StateName = @StateName, 
           Description = @Description, 
           ENTWFOwnerGroupId = @ENTWFOwnerGroupId, 
           IsOwnerSubmitter = @IsOwnerSubmitter, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFStateId = @ENTWFStateId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFTransitionSelectAll]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransitionSelectAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ENTWFTransitionSelectAll]
AS
    SET NOCOUNT ON

         SELECT ENTWFTransitionId, ENTWFTransition.ENTWorkflowId, TransitionName, FromENTWFStateId, ToENTWFStateId, PostTransitionMethodName, ENTWFTransition.InsertDate, ENTWFTransition.InsertENTUserAccountId, ENTWFTransition.UpdateDate, ENTWFTransition.UpdateENTUserAccountId, ENTWFTransition.Version,
                ENTWFState.StateName AS FromStateName, ENTWFState1.StateName AS ToStateName
           FROM ENTWFTransition 
     INNER JOIN ENTWFState AS ENTWFState1 
             ON ENTWFState1.ENTWFStateId = ENTWFTransition.ToENTWFStateId 
LEFT OUTER JOIN ENTWFState 
             ON ENTWFState.ENTWFStateId = ENTWFTransition.FromENTWFStateId   

    RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupUserAccountDelete]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccountDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupUserAccountDelete]
(
    @ENTWFOwnerGroupUserAccountId int
)
AS
    SET NOCOUNT ON

    DELETE
      FROM ENTWFOwnerGroupUserAccount
     WHERE ENTWFOwnerGroupUserAccountId = @ENTWFOwnerGroupUserAccountId

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupUserAccountInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccountInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupUserAccountInsert]
(
    @ENTWFOwnerGroupUserAccountId  int OUTPUT, 
    @ENTWFOwnerGroupId  int, 
    @ENTUserAccountId  int, 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFOwnerGroupUserAccount (ENTWFOwnerGroupId, ENTUserAccountId, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWFOwnerGroupId, 
                @ENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFOwnerGroupUserAccountId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFOwnerGroupUserAccountUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccountUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFOwnerGroupUserAccountUpdate]
(
    @ENTWFOwnerGroupUserAccountId  int, 
    @ENTWFOwnerGroupId  int, 
    @ENTUserAccountId  int, 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFOwnerGroupUserAccount
       SET 
           ENTWFOwnerGroupId = @ENTWFOwnerGroupId, 
           ENTUserAccountId = @ENTUserAccountId, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFOwnerGroupUserAccountId = @ENTWFOwnerGroupUserAccountId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFTransitionInsert]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransitionInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[ENTWFTransitionInsert]
(
    @ENTWFTransitionId  int OUTPUT, 
    @ENTWorkflowId  int, 
    @TransitionName  varchar(50), 
    @FromENTWFStateId  int, 
    @ToENTWFStateId  int,     
    @PostTransitionMethodName  varchar(255), 
    @InsertENTUserAccountId  int
)
AS
    SET NOCOUNT ON

    INSERT INTO ENTWFTransition (ENTWorkflowId, TransitionName, FromENTWFStateId, ToENTWFStateId, PostTransitionMethodName, InsertDate, InsertENTUserAccountId, UpdateDate, UpdateENTUserAccountId)
         VALUES (
                @ENTWorkflowId, 
                @TransitionName, 
                @FromENTWFStateId, 
                @ToENTWFStateId, 
                @PostTransitionMethodName, 
                GetDate(), 
                @InsertENTUserAccountId, 
                GetDate(), 
                @InsertENTUserAccountId
                )
    SET @ENTWFTransitionId = Scope_Identity()

    RETURN

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFTransitionUpdate]    Script Date: 07/01/2008 01:04:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFTransitionUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFTransitionUpdate]
(
    @ENTWFTransitionId  int, 
    @ENTWorkflowId  int, 
    @TransitionName  varchar(50), 
    @FromENTWFStateId  int, 
    @ToENTWFStateId  int, 
    @PostTransitionMethodName  varchar(255), 
    @UpdateENTUserAccountId  int, 
    @Version  timestamp
)
AS
    SET NOCOUNT ON

    UPDATE ENTWFTransition
       SET 
           ENTWorkflowId = @ENTWorkflowId, 
           TransitionName = @TransitionName, 
           FromENTWFStateId = @FromENTWFStateId, 
           ToENTWFStateId = @ToENTWFStateId, 
           PostTransitionMethodName = @PostTransitionMethodName, 
           UpdateDate = GetDate(), 
           UpdateENTUserAccountId = @UpdateENTUserAccountId
     WHERE ENTWFTransitionId = @ENTWFTransitionId
       AND Version = @Version

    RETURN @@ROWCOUNT

' 
END
GO
/****** Object:  StoredProcedure [dbo].[ENTWFItemSelectByWorkflowId]    Script Date: 11/14/2008 01:00:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTWFItemSelectByWorkflowId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[ENTWFItemSelectByWorkflowId]
(
    @ENTWorkflowId int    
)
AS
    SET NOCOUNT ON

    SELECT COUNT(1) AS CountOfWFItems
      FROM ENTWFItem
     WHERE ENTWorkflowId = @ENTWorkflowId       

    RETURN
' 
END
GO

/****** Object:  Default [DF_ENTUserAccount_InsertDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTUserAccount_InsertDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTUserAccount]'))
Begin
ALTER TABLE [dbo].[ENTUserAccount] ADD  CONSTRAINT [DF_ENTUserAccount_InsertDate]  DEFAULT (getdate()) FOR [InsertDate]

End
GO
/****** Object:  Default [DF_ENTUserAccount_UpdateDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTUserAccount_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTUserAccount]'))
Begin
ALTER TABLE [dbo].[ENTUserAccount] ADD  CONSTRAINT [DF_ENTUserAccount_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]

End
GO
/****** Object:  Default [DF_ENTWFItem_InsertDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTWFItem_InsertDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItem]'))
Begin
ALTER TABLE [dbo].[ENTWFItem] ADD  CONSTRAINT [DF_ENTWFItem_InsertDate]  DEFAULT (getdate()) FOR [InsertDate]

End
GO
/****** Object:  Default [DF_ENTWFItem_UpdateDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTWFItem_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItem]'))
Begin
ALTER TABLE [dbo].[ENTWFItem] ADD  CONSTRAINT [DF_ENTWFItem_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]

End
GO
/****** Object:  Default [DF_ENTWFItemOwner_InsertDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTWFItemOwner_InsertDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwner]'))
Begin
ALTER TABLE [dbo].[ENTWFItemOwner] ADD  CONSTRAINT [DF_ENTWFItemOwner_InsertDate]  DEFAULT (getdate()) FOR [InsertDate]

End
GO
/****** Object:  Default [DF_ENTWFItemOwner_UpdateDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTWFItemOwner_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwner]'))
Begin
ALTER TABLE [dbo].[ENTWFItemOwner] ADD  CONSTRAINT [DF_ENTWFItemOwner_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]

End
GO
/****** Object:  Default [DF_ENTWFItemTransition_InsertDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTWFItemTransition_InsertDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistory]'))
Begin
ALTER TABLE [dbo].[ENTWFItemStateHistory] ADD  CONSTRAINT [DF_ENTWFItemTransition_InsertDate]  DEFAULT (getdate()) FOR [InsertDate]

End
GO
/****** Object:  Default [DF_ENTWFItemTransition_UpdateDate]    Script Date: 07/01/2008 01:04:11 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ENTWFItemTransition_UpdateDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistory]'))
Begin
ALTER TABLE [dbo].[ENTWFItemStateHistory] ADD  CONSTRAINT [DF_ENTWFItemTransition_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]

End
GO
/****** Object:  ForeignKey [FK_ENTWFState_ENTWFOwnerGroup]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFState_ENTWFOwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFState]'))
ALTER TABLE [dbo].[ENTWFState]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFState_ENTWFOwnerGroup] FOREIGN KEY([ENTWFOwnerGroupId])
REFERENCES [dbo].[ENTWFOwnerGroup] ([ENTWFOwnerGroupId])
GO
ALTER TABLE [dbo].[ENTWFState] CHECK CONSTRAINT [FK_ENTWFState_ENTWFOwnerGroup]
GO
/****** Object:  ForeignKey [FK_ENTWFState_ENTWorkflow]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFState_ENTWorkflow]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFState]'))
ALTER TABLE [dbo].[ENTWFState]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFState_ENTWorkflow] FOREIGN KEY([ENTWorkflowId])
REFERENCES [dbo].[ENTWorkflow] ([ENTWorkflowId])
GO
ALTER TABLE [dbo].[ENTWFState] CHECK CONSTRAINT [FK_ENTWFState_ENTWorkflow]
GO
/****** Object:  ForeignKey [FK_ENTWFTransition_ENTWFState]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFTransition_ENTWFState]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFTransition]'))
ALTER TABLE [dbo].[ENTWFTransition]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFTransition_ENTWFState] FOREIGN KEY([FromENTWFStateId])
REFERENCES [dbo].[ENTWFState] ([ENTWFStateId])
GO
ALTER TABLE [dbo].[ENTWFTransition] CHECK CONSTRAINT [FK_ENTWFTransition_ENTWFState]
GO
/****** Object:  ForeignKey [FK_ENTWFTransition_ENTWFState1]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFTransition_ENTWFState1]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFTransition]'))
ALTER TABLE [dbo].[ENTWFTransition]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFTransition_ENTWFState1] FOREIGN KEY([ToENTWFStateId])
REFERENCES [dbo].[ENTWFState] ([ENTWFStateId])
GO
ALTER TABLE [dbo].[ENTWFTransition] CHECK CONSTRAINT [FK_ENTWFTransition_ENTWFState1]
GO
/****** Object:  ForeignKey [FK_ENTWFTransition_ENTWorkflow]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFTransition_ENTWorkflow]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFTransition]'))
ALTER TABLE [dbo].[ENTWFTransition]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFTransition_ENTWorkflow] FOREIGN KEY([ENTWorkflowId])
REFERENCES [dbo].[ENTWorkflow] ([ENTWorkflowId])
GO
ALTER TABLE [dbo].[ENTWFTransition] CHECK CONSTRAINT [FK_ENTWFTransition_ENTWorkflow]
GO
/****** Object:  ForeignKey [FK_ENTWFItem_ENTUserAccount]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItem_ENTUserAccount]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItem]'))
ALTER TABLE [dbo].[ENTWFItem]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItem_ENTUserAccount] FOREIGN KEY([SubmitterENTUserAccountId])
REFERENCES [dbo].[ENTUserAccount] ([ENTUserAccountId])
GO
ALTER TABLE [dbo].[ENTWFItem] CHECK CONSTRAINT [FK_ENTWFItem_ENTUserAccount]
GO
/****** Object:  ForeignKey [FK_ENTWFItem_ENTWorkflow]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItem_ENTWorkflow]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItem]'))
ALTER TABLE [dbo].[ENTWFItem]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItem_ENTWorkflow] FOREIGN KEY([ENTWorkflowId])
REFERENCES [dbo].[ENTWorkflow] ([ENTWorkflowId])
GO
ALTER TABLE [dbo].[ENTWFItem] CHECK CONSTRAINT [FK_ENTWFItem_ENTWorkflow]
GO
/****** Object:  ForeignKey [FK_ENTWFItemOwner_ENTUserAccount]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItemOwner_ENTUserAccount]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwner]'))
ALTER TABLE [dbo].[ENTWFItemOwner]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItemOwner_ENTUserAccount] FOREIGN KEY([ENTUserAccountId])
REFERENCES [dbo].[ENTUserAccount] ([ENTUserAccountId])
GO
ALTER TABLE [dbo].[ENTWFItemOwner] CHECK CONSTRAINT [FK_ENTWFItemOwner_ENTUserAccount]
GO
/****** Object:  ForeignKey [FK_ENTWFItemOwner_ENTWFItem]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItemOwner_ENTWFItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwner]'))
ALTER TABLE [dbo].[ENTWFItemOwner]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItemOwner_ENTWFItem] FOREIGN KEY([ENTWFItemId])
REFERENCES [dbo].[ENTWFItem] ([ENTWFItemId])
GO
ALTER TABLE [dbo].[ENTWFItemOwner] CHECK CONSTRAINT [FK_ENTWFItemOwner_ENTWFItem]
GO
/****** Object:  ForeignKey [FK_ENTWFItemOwner_ENTWFOwnerGroup]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItemOwner_ENTWFOwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemOwner]'))
ALTER TABLE [dbo].[ENTWFItemOwner]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItemOwner_ENTWFOwnerGroup] FOREIGN KEY([ENTWFOwnerGroupId])
REFERENCES [dbo].[ENTWFOwnerGroup] ([ENTWFOwnerGroupId])
GO
ALTER TABLE [dbo].[ENTWFItemOwner] CHECK CONSTRAINT [FK_ENTWFItemOwner_ENTWFOwnerGroup]
GO
/****** Object:  ForeignKey [FK_ENTWFItemStateHistory_ENTUserAccount]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItemStateHistory_ENTUserAccount]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistory]'))
ALTER TABLE [dbo].[ENTWFItemStateHistory]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItemStateHistory_ENTUserAccount] FOREIGN KEY([ENTUserAccountId])
REFERENCES [dbo].[ENTUserAccount] ([ENTUserAccountId])
GO
ALTER TABLE [dbo].[ENTWFItemStateHistory] CHECK CONSTRAINT [FK_ENTWFItemStateHistory_ENTUserAccount]
GO
/****** Object:  ForeignKey [FK_ENTWFItemStateHistory_ENTWFItem]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItemStateHistory_ENTWFItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistory]'))
ALTER TABLE [dbo].[ENTWFItemStateHistory]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItemStateHistory_ENTWFItem] FOREIGN KEY([ENTWFItemId])
REFERENCES [dbo].[ENTWFItem] ([ENTWFItemId])
GO
ALTER TABLE [dbo].[ENTWFItemStateHistory] CHECK CONSTRAINT [FK_ENTWFItemStateHistory_ENTWFItem]
GO
/****** Object:  ForeignKey [FK_ENTWFItemStateHistory_ENTWFState]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFItemStateHistory_ENTWFState]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFItemStateHistory]'))
ALTER TABLE [dbo].[ENTWFItemStateHistory]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFItemStateHistory_ENTWFState] FOREIGN KEY([ENTWFStateId])
REFERENCES [dbo].[ENTWFState] ([ENTWFStateId])
GO
ALTER TABLE [dbo].[ENTWFItemStateHistory] CHECK CONSTRAINT [FK_ENTWFItemStateHistory_ENTWFState]
GO
/****** Object:  ForeignKey [FK_ENTWFOwnerGroupUserAccount_ENTUserAccount]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFOwnerGroupUserAccount_ENTUserAccount]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccount]'))
ALTER TABLE [dbo].[ENTWFOwnerGroupUserAccount]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFOwnerGroupUserAccount_ENTUserAccount] FOREIGN KEY([ENTUserAccountId])
REFERENCES [dbo].[ENTUserAccount] ([ENTUserAccountId])
GO
ALTER TABLE [dbo].[ENTWFOwnerGroupUserAccount] CHECK CONSTRAINT [FK_ENTWFOwnerGroupUserAccount_ENTUserAccount]
GO
/****** Object:  ForeignKey [FK_ENTWFOwnerGroupUserAccount_ENTWFOwnerGroup]    Script Date: 07/01/2008 01:04:11 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ENTWFOwnerGroupUserAccount_ENTWFOwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[ENTWFOwnerGroupUserAccount]'))
ALTER TABLE [dbo].[ENTWFOwnerGroupUserAccount]  WITH CHECK ADD  CONSTRAINT [FK_ENTWFOwnerGroupUserAccount_ENTWFOwnerGroup] FOREIGN KEY([ENTWFOwnerGroupId])
REFERENCES [dbo].[ENTWFOwnerGroup] ([ENTWFOwnerGroupId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ENTWFOwnerGroupUserAccount] CHECK CONSTRAINT [FK_ENTWFOwnerGroupUserAccount_ENTWFOwnerGroup]
GO
