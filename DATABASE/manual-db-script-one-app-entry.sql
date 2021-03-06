USE [MitekMonitoring]

CREATE TABLE [dbo].[Application](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ShortDesc] [nvarchar](200) NOT NULL,
	[LongDesc] [nvarchar](500) NULL,
	[AuthToken] [nvarchar](500) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Monitoring](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AppID] [int] NOT NULL,
	[AppArea] [nvarchar](200) NULL,
	[Information] [nvarchar](max) NULL,
	[Comments] [nvarchar](500) NULL,
	[Error] [bit] default(0),
	[DateModified] [date] NOT NULL
) ON [PRIMARY] 



INSERT [dbo].[Application] ([ID], [ShortDesc], [LongDesc], [AuthToken]) VALUES (1, N'ConnDes', N'ConnectionDesign', N'E1C878EB623D49FB8244FE74778E7335')
