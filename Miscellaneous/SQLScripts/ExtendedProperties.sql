USE [Selero]

-- NonQualifiedPrescription Table Extended Properties
/*
SELECT tbl.name AS 'Table Name', prop.name AS 'Extended Property', prop.value AS 'Extended Property Value'
FROM sys.tables tbl
	INNER JOIN sys.extended_properties prop ON prop.major_id=tbl.object_id AND prop.minor_id=0
WHERE
	tbl.name='NonQualifiedPrescription'
	--prop.name='DTG_BaseTable'
*/

--IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_BaseTable')
--	EXEC sys.sp_dropextendedproperty @name=N'DTG_BaseTable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription'
--EXEC sys.sp_addextendedproperty @name=N'DTG_BaseTable', @value=N'ManufacturerFileFormat', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription'

IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_Data_Namespace')
	EXEC sys.sp_dropextendedproperty @name=N'DTG_Data_Namespace', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription'
EXEC sys.sp_addextendedproperty @name=N'DTG_Data_Namespace', @value=N'Test.Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription'

IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_Data_RootNamespace')
	EXEC sys.sp_dropextendedproperty @name=N'DTG_Data_RootNamespace', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription'
EXEC sys.sp_addextendedproperty @name=N'DTG_Data_RootNamespace', @value=N'Test', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription'

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- NonQualifiedPrescription Column Extended Properties

/*
-- Drops all column extended properties
--SELECT 'EXEC sp_dropextendedproperty
--	@name = ' + QUOTENAME(ep.name, '''') + '
--	,@level0type = ''schema''
--	,@level0name = ''dbo''' + '
--	,@level1type = ''table''
--	,@level1name = ' + QUOTENAME(OBJECT_NAME(sc.[object_id]), '''') + '
--	,@level2type = ''column''
--	,@level2name = ' + QUOTENAME(sc.name, '''') + ';'
--FROM
--	sys.extended_properties ep
--	INNER JOIN sys.columns sc ON sc.[object_id] = ep.major_id AND sc.column_id = ep.minor_id
--WHERE
--	ep.name LIKE 'DTG_%'
*/

/*
SELECT tbl.name AS 'Table Name', col.name AS 'Column Name', prop.name AS 'Extended Property', prop.value AS 'Extended Property Value'
FROM sys.tables tbl
	INNER JOIN sys.all_columns col ON col.object_id=tbl.object_id
	INNER JOIN sys.extended_properties prop ON prop.major_id=tbl.object_id AND prop.minor_id=col.column_id AND prop.class=1
WHERE
	tbl.name='NonQualifiedPrescription'
	AND prop.name LIKE 'DTG%'
*/

-- CreatedMachine
IF EXISTS (SELECT column_id from sys.columns where name='CreatedMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id = (SELECT column_id from sys.columns where name='CreatedMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedMachine'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedMachine'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultMachine' AND minor_id = (SELECT column_id from sys.columns where name='CreatedMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultMachine', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedMachine'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultMachine', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedMachine'
END

-- CreatedApp
IF EXISTS (SELECT column_id from sys.columns where name='CreatedApp' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id = (SELECT column_id from sys.columns where name='CreatedApp' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedApp'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedApp'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultApp' AND minor_id = (SELECT column_id from sys.columns where name='CreatedApp' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultApp', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedApp'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultApp', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedApp'
END

-- CreatedDate
IF EXISTS (SELECT column_id from sys.columns where name='CreatedDate' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id = (SELECT column_id from sys.columns where name='CreatedDate' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedDate'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedDate'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultDate' AND minor_id = (SELECT column_id from sys.columns where name='CreatedDate' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultDate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedDate'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultDate', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedDate'
END

-- CreatedBy
IF EXISTS (SELECT column_id from sys.columns where name='CreatedBy' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id = (SELECT column_id from sys.columns where name='CreatedBy' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedBy'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedBy'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultUser' AND minor_id = (SELECT column_id from sys.columns where name='CreatedBy' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultUser', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedBy'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultUser', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'CreatedBy'
END

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- StatusMachine
IF EXISTS (SELECT column_id from sys.columns where name='StatusMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusMachine'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusMachine'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultMachine' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultMachine', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusMachine'	
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultMachine', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusMachine'
END

-- StatusApp
IF EXISTS (SELECT column_id from sys.columns where name='StatusApp' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusApp' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusApp'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusApp'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultApp' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusApp' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultApp', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusApp'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultApp', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusApp'
END

-- StatusDate
IF EXISTS (SELECT column_id from sys.columns where name='StatusDate' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusDate' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusDate'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusDate'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultDate' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusDate' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultDate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusDate'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultDate', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusDate'
END

-- StatusBy
IF EXISTS (SELECT column_id from sys.columns where name='StatusBy' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_AddOnly' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusBy' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_AddOnly', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusBy'
	EXEC sys.sp_addextendedproperty @name=N'DTG_AddOnly', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusBy'

	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultUser' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='StatusBy' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultUser', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusBy'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultUser', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'StatusBy'
END

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- UpdatedMachine
IF EXISTS (SELECT column_id from sys.columns where name='UpdatedMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultMachine' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='UpdatedMachine' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultMachine', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedMachine'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultMachine', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedMachine'
END

-- UpdatedApp
IF EXISTS (SELECT column_id from sys.columns where name='UpdatedApp' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultApp' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='UpdatedApp' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultApp', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedApp'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultApp', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedApp'
END

-- UpdatedDate
IF EXISTS (SELECT column_id from sys.columns where name='UpdatedDate' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultDate' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='UpdatedDate' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultDate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedDate'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultDate', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedDate'
END

-- UpdatedBy
IF EXISTS (SELECT column_id from sys.columns where name='UpdatedBy' AND object_id=OBJECT_ID('NonQualifiedPrescription'))
BEGIN
	IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('NonQualifiedPrescription') AND name='DTG_DefaultUser' AND minor_id=(SELECT column_id FROM sys.columns WHERE name='UpdatedBy' AND object_id=OBJECT_ID('NonQualifiedPrescription')))
		EXEC sys.sp_dropextendedproperty @name=N'DTG_DefaultUser', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedBy'
	EXEC sys.sp_addextendedproperty @name=N'DTG_DefaultUser', @value=N'Y', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'NonQualifiedPrescription', @level2type=N'COLUMN', @level2name=N'UpdatedBy'
END
