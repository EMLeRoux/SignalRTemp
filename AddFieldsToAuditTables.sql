DECLARE @SqlCursor NVARCHAR (MAX)
DECLARE @CheckTableExistsSql NVARCHAR (500)
DECLARE @TableName AS VARCHAR(50)
DECLARE @DbStepName AS VARCHAR(50)
DECLARE @DatabaseName varchar(100)
DECLARE @LastColumnNameBeforeInsert AS VARCHAR(50)
DECLARE @InsertedFields AS VARCHAR(MAX)
DECLARE @SqlTableCreate NVARCHAR (MAX)
DECLARE @SqlInsertStatement NVARCHAR (MAX)
DECLARE @SqlInsertStatementLines VARCHAR (MAX) = ''
DECLARE @SqlInsertPrimaryKeyStatement NVARCHAR (MAX)
DECLARE @SqlInsertPrimaryKeyStatementLines VARCHAR (MAX) = ''

DECLARE @SqlDropTableStatement NVARCHAR (MAX)
DECLARE @SqlRenameTableStatement NVARCHAR (MAX)
DECLARE @SqlBackupDatabaseStatement NVARCHAR (MAX)
DECLARE @SqlNewName NVARCHAR (MAX)
DECLARE @SqlOldName NVARCHAR (MAX)

DECLARE @SqlColumnLine VARCHAR (MAX)
DECLARE @ColumnName VARCHAR (100)
DECLARE @CharacterMaximumLength int
DECLARE @DataType VARCHAR (100)
DECLARE @IsNullable VARCHAR (100)
DECLARE @IsPrimaryKeyInd VARCHAR (1)
DECLARE @IsLastColumnBeforeInsertInd VARCHAR (1)
DECLARE @CRLF char(2)


SET @CRLF = char(10) 

--1)TABLE TO BE CHANGED  
SET @TableName = 'COMPANY'
--2)LAST COLUMN BEFORE INSERT OF NEW COLUMN/S 
SET @LastColumnNameBeforeInsert = 'UI19_EMPLOYER_PERSON_ID_NO'
--3)COLUMN/S TO BE INSERTED
SET @InsertedFields = 'LOCKOUT_BREAKS_FACILITY_ON_CLOCK_IND varchar(1) NULL,' + @CRLF

--Extract First New Column from @InsertedFields
SET @ColumnName = LEFT(@InsertedFields, CHARINDEX(' ', @InsertedFields) - 1)  
PRINT @ColumnName



SET @DatabaseName = 'InteractPayroll_00001'

--Now Working
SET @SqlRenameTableStatement = @DatabaseName + '.dbo.sp_rename ''' + @TableName + '_Tmp'',''' + @TableName + ''''
print @SqlRenameTableStatement

EXEC(@SqlRenameTableStatement)

return


--PRINT '@SqlNewName = ' + @SqlNewName
--PRINT '@SqlOldName = ' + @SqlOldName

EXEC [InteractPayroll_00001].dbo.sp_rename 'COMPANY','COMPANY_Tmp'

return


--EXEC sp_rename 'old_table_name', 'new_table_name'















--1) Cleanup Logs
DELETE FROM InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS

DECLARE Database_Cursor CURSOR FOR
SELECT name FROM sys.databases
WHERE name LIKE 'InteractPayroll[_]%'
ORDER BY
name

OPEN Database_Cursor   

FETCH NEXT FROM Database_Cursor INTO 
@DatabaseName

WHILE @@FETCH_STATUS = 0   
BEGIN   

	SET @SqlBackupDatabaseStatement = 'BACKUP DATABASE ' + @DatabaseName + ' TO DISK = ''C:\Interact Software\DB\' + @DatabaseName + '_' + FORMAT(GETDATE(),'yyyyMMdd_HHmm') + '_Test'''
	--PRINT @SqlBackupDatabaseStatement

	--2) Backup Database
	BEGIN TRY
		
		SET @DbStepName = 'Database Backup'

		INSERT INTO InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS
		(DB_NAME
		,DB_STEP_NAME
		,DB_SUCCESS_IND)
		VALUES
		(@DatabaseName
		,@DbStepName
		,'N')
	
		EXEC(@SqlBackupDatabaseStatement)

		UPDATE InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS
		SET DB_SUCCESS_IND = 'Y'
		WHERE DB_NAME = @DatabaseName
		AND DB_STEP_NAME = @DbStepName

	END TRY
	BEGIN CATCH

	   GOTO Close_Database_Cursor
	   RETURN

	END CATCH
	
	SET @CheckTableExistsSql = ' SELECT' + @CRLF 
	SET @CheckTableExistsSql += ' TABLE_NAME' + @CRLF 
	SET @CheckTableExistsSql += ' FROM ' + @DatabaseName + '.INFORMATION_SCHEMA.COLUMNS' + @CRLF 
	SET @CheckTableExistsSql += ' WHERE TABLE_NAME = ''' + @TableName + '''' + @CRLF 
	SET @CheckTableExistsSql += ' AND COLUMN_NAME = ''' + @LastColumnNameBeforeInsert + '''' + @CRLF 
	--PRINT @CheckTableExistsSql

	--3 Check Table and Column Exists
	BEGIN TRY

	    SET @DbStepName = 'Check Last Column before Insert Exists'
		
		INSERT INTO InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS
		(DB_NAME
		,DB_STEP_NAME
		,DB_SUCCESS_IND)
		VALUES
		(@DatabaseName
		,@DbStepName
		,'N')

		EXEC (@CheckTableExistsSql)

		IF @@ROWCOUNT = 0
		BEGIN

			GOTO Close_Database_Cursor
			RETURN

		END

		UPDATE InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS
		SET DB_SUCCESS_IND = 'Y'
		WHERE DB_NAME = @DatabaseName
		AND DB_STEP_NAME = @DbStepName

	END TRY
	BEGIN CATCH

	   GOTO Close_Database_Cursor
	   RETURN

	END CATCH

	SET @CheckTableExistsSql = ' SELECT' + @CRLF 
	SET @CheckTableExistsSql += ' TABLE_NAME' + @CRLF 
	SET @CheckTableExistsSql += ' FROM ' + @DatabaseName + '.INFORMATION_SCHEMA.COLUMNS' + @CRLF 
	SET @CheckTableExistsSql += ' WHERE TABLE_NAME = ''' + @TableName + '''' + @CRLF 
	SET @CheckTableExistsSql += ' AND COLUMN_NAME = ''' + @ColumnName + '''' + @CRLF 
	--PRINT @CheckTableExistsSql

	--4 Check New Column Does Not Exists
	BEGIN TRY

	    SET @DbStepName = 'Check Inserted Column Does NOT Exist'
		
		INSERT INTO InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS
		(DB_NAME
		,DB_STEP_NAME
		,DB_SUCCESS_IND)
		VALUES
		(@DatabaseName
		,@DbStepName
		,'N')

		EXEC (@CheckTableExistsSql)

		IF @@ROWCOUNT > 0
		BEGIN

			GOTO Close_Database_Cursor
			RETURN

		END

		UPDATE InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS
		SET DB_SUCCESS_IND = 'Y'
		WHERE DB_NAME = @DatabaseName
		AND DB_STEP_NAME = @DbStepName

	END TRY
	BEGIN CATCH

	   GOTO Close_Database_Cursor
	   RETURN

	END CATCH

	SET @SqlTableCreate = 'CREATE TABLE ' + @DatabaseName + '.dbo.' + @TableName + '_Tmp (' + @CRLF 

	SET @SqlInsertStatement = 'INSERT INTO ' + @DatabaseName + '.dbo.' + @TableName + '_Tmp ' + @CRLF 
	SET @SqlInsertStatement += '(' + @CRLF 

	SET @SqlCursor = 'DECLARE Table_Create_Field_Cursor CURSOR FOR' + @CRLF 
	SET @SqlCursor += ' SELECT' + @CRLF
	SET @SqlCursor += ' C.COLUMN_NAME' + @CRLF
	SET @SqlCursor += ',C.CHARACTER_MAXIMUM_LENGTH' + @CRLF
	SET @SqlCursor += ',C.DATA_TYPE' + @CRLF
	SET @SqlCursor += ',C.IS_NULLABLE' + @CRLF
	SET @SqlCursor += ',IS_PRIMARY_KEY_IND = ' + @CRLF
	SET @SqlCursor += ' CASE ' + @CRLF
	SET @SqlCursor += ' WHEN PRIMARY_KEY_TABLE.COLUMN_NAME IS NULL THEN ''N''' + @CRLF
	SET @SqlCursor += ' ELSE ''Y''' + @CRLF
	SET @SqlCursor += ' END ' + @CRLF 

	SET @SqlCursor += ',IS_LAST_COLUMN_BEFORE_INSERT_IND = ' + @CRLF
	SET @SqlCursor += ' CASE ' + @CRLF
	SET @SqlCursor += ' WHEN NOT COLUMN_TABLE.COLUMN_NAME IS NULL THEN ''Y''' + @CRLF
	SET @SqlCursor += ' ELSE ''N''' + @CRLF
	SET @SqlCursor += ' END ' + @CRLF 
				
	SET @SqlCursor += ' FROM ' + @DatabaseName + '.INFORMATION_SCHEMA.COLUMNS C' + @CRLF

	SET @SqlCursor += ' LEFT JOIN ' + @CRLF
	SET @SqlCursor += '(SELECT' + @CRLF
	SET @SqlCursor += ' CCU.Column_Name' + @CRLF
	SET @SqlCursor += ' FROM ' + @DatabaseName + '.INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC' + @CRLF
	SET @SqlCursor += ' INNER JOIN ' + @DatabaseName + '.INFORMATION_SCHEMA.KEY_COLUMN_USAGE CCU' + @CRLF
	SET @SqlCursor += ' ON CCU.Constraint_Name = TC.Constraint_Name' + @CRLF
	SET @SqlCursor += ' AND CCU.Table_Name = TC.Table_Name' + @CRLF
	SET @SqlCursor += ' WHERE TC.Constraint_Type = ''PRIMARY KEY''' + @CRLF
	SET @SqlCursor += ' AND TC.TABLE_NAME = ''' + @TableName + ''') AS PRIMARY_KEY_TABLE' + @CRLF
	SET @SqlCursor += ' ON C.COLUMN_NAME = PRIMARY_KEY_TABLE.COLUMN_NAME' + @CRLF

	SET @SqlCursor += ' LEFT JOIN ' + @CRLF
	SET @SqlCursor += '(SELECT' + @CRLF
	SET @SqlCursor += ' C.Column_Name' + @CRLF
	SET @SqlCursor += ' FROM ' + @DatabaseName + '.INFORMATION_SCHEMA.COLUMNS C' + @CRLF
				
	SET @SqlCursor += ' WHERE C.TABLE_NAME = ''' + @TableName + '''' + @CRLF
	SET @SqlCursor += ' AND C.COLUMN_NAME = ''' + @LastColumnNameBeforeInsert + ''') AS COLUMN_TABLE' + @CRLF
	SET @SqlCursor += ' ON C.COLUMN_NAME = COLUMN_TABLE.COLUMN_NAME' + @CRLF
				
	SET @SqlCursor += ' WHERE C.TABLE_NAME = ''' + @TableName + '''' + @CRLF 
	SET @SqlCursor += ' ORDER BY' + @CRLF
	SET @SqlCursor += ' C.ORDINAL_POSITION' + @CRLF

	--PRINT @SqlCursor
		
	EXEC sp_executesql @SqlCursor
			
	OPEN Table_Create_Field_Cursor   

	FETCH NEXT FROM Table_Create_Field_Cursor INTO 
	 @ColumnName
	,@CharacterMaximumLength
	,@DataType
	,@IsNullable
	,@IsPrimaryKeyInd
	,@IsLastColumnBeforeInsertInd

	WHILE @@FETCH_STATUS = 0   
	BEGIN   
	
		--PRINT 'COLUMN_NAME=' + @ColumnName
		--PRINT 'CHARACTER_MAXIMUM_LENGTH=' + CONVERT(VARCHAR,@CharacterMaximumLength)
		--PRINT 'DATA_TYPE=' + @DataType
		--PRINT 'IS_NULLABLE=' + @IsNullable
	
		IF @SqlInsertStatementLines = ''
		BEGIN
			SET @SqlInsertStatementLines = @ColumnName + @CRLF 
		END
		ELSE
		BEGIN
			SET @SqlInsertStatementLines += ',' + @ColumnName  + @CRLF 
		END

		IF @IsPrimaryKeyInd = 'Y'
		BEGIN
			--Primary Key
			IF @SqlInsertPrimaryKeyStatementLines = ''
			BEGIN
				SET @SqlInsertPrimaryKeyStatementLines = @ColumnName + @CRLF 
			END
			ELSE
			BEGIN
				SET @SqlInsertPrimaryKeyStatementLines += ',' + @ColumnName  + @CRLF 
			END
		END

		SET @SqlColumnLine = @ColumnName + ' ' + @DataType

		IF NOT @CharacterMaximumLength IS NULL
		BEGIN 
			IF @CharacterMaximumLength = -1
			BEGIN
				SET @SqlColumnLine += '(MAX)'
			END
			ELSE
			BEGIN
				SET @SqlColumnLine += '(' + CONVERT(VARCHAR,@CharacterMaximumLength) + ')'
			END
			--PRINT '@SqlColumnLine = ' + @SqlColumnLine
		END

		IF @IsNullable = 'YES'
		BEGIN
			SET @SqlColumnLine += ' NULL,' 
		END
		ELSE
		BEGIN
			SET @SqlColumnLine += ' NOT NULL,' 
		END
	
		SET @SqlTableCreate += @SqlColumnLine + @CRLF

		IF @IsLastColumnBeforeInsertInd = 'Y'
		BEGIN
			SET @SqlTableCreate += @InsertedFields 
		END
	
		FETCH NEXT FROM Table_Create_Field_Cursor INTO
		 @ColumnName
		,@CharacterMaximumLength
		,@DataType
		,@IsNullable
		,@IsPrimaryKeyInd
		,@IsLastColumnBeforeInsertInd
	END   

	CLOSE Table_Create_Field_Cursor   
	DEALLOCATE Table_Create_Field_Cursor
	
	FETCH NEXT FROM Database_Cursor INTO
	@DatabaseName
	
END   

Close_Database_Cursor:
CLOSE Database_Cursor   
DEALLOCATE Database_Cursor

RETURN
















SET @SqlNewName = @DatabaseName + '.dbo.' + @TableName + @CRLF 
SET @SqlOldName = @DatabaseName + '.dbo.' + @TableName + '_Tmp' + @CRLF 



INSERT INTO InteractPayroll.dbo.DB_SCRIIPT_STEP_LOGS
(DB_NAME
,DB_STEP_NAME
,DB_SUCCESS_IND)
VALUES
(@DatabaseName
,'Rename ' + @SqlOldName + ' to ' + @SqlNewName
,'Y')



--SET @SqlRenameTableStatement = @DatabaseName


PRINT '@SqlNewName = ' + @SqlNewName
PRINT '@SqlOldName = ' + @SqlOldName
EXEC sp_rename 'COMPANY_Tmp','COMPANY'
return













--1) Create Temp Table and Run
SET @SqlTableCreate = LEFT(@SqlTableCreate, LEN(@SqlTableCreate) - 2) + ')'  + @CRLF

--PRINT '@SqlTableCreate = ' + @SqlTableCreate
--EXEC sp_executesql @SqlTableCreate

--2) Create Insert Statement and Run
SET @SqlInsertStatement += @SqlInsertStatementLines
SET @SqlInsertStatement += ')' + @CRLF
SET @SqlInsertStatement += 'SELECT ' + @CRLF
SET @SqlInsertStatement += @SqlInsertStatementLines

SET @SqlInsertStatement += 'FROM ' + @DatabaseName + '.dbo.' + @TableName + @CRLF 

--PRINT '@SqlInsertStatement = ' + @SqlInsertStatement
--EXEC sp_executesql @SqlInsertStatement

--3 Create Drop Statement and Run
SET @SqlDropTableStatement = 'DROP TABLE ' + @DatabaseName + '.dbo.' + @TableName + @CRLF 
PRINT '@SqlDropTableStatement = ' + @SqlDropTableStatement
--EXEC sp_executesql @SqlDropTableStatement


SET @SqlDropTableStatement = 'sp_rename ''' + @DatabaseName + '.dbo.' + @TableName + '_Tmp,''' + @DatabaseName + '.dbo.' + @TableName + @CRLF 
PRINT '@SqlDropTableStatement = ' + @SqlDropTableStatement


--EXEC sp_executesql @SqlDropTableStatement


--EXEC sp_rename 'old_table_name', 'new_table_name'

RETURN



SET @SqlInsertPrimaryKeyStatement = 'ALTER TABLE ' + @DatabaseName + '.dbo.' + @TableName + ' ADD CONSTRAINT' + @CRLF
SET @SqlInsertPrimaryKeyStatement += 'PK_' + @TableName + ' PRIMARY KEY CLUSTERED' + @CRLF
SET @SqlInsertPrimaryKeyStatement += '(' + @CRLF

SET @SqlInsertPrimaryKeyStatement += @SqlInsertPrimaryKeyStatementLines

SET @SqlInsertPrimaryKeyStatement += ') WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]' + @CRLF

PRINT '@SqlInsertPrimaryKeyStatement = ' + @SqlInsertPrimaryKeyStatement

EXEC sp_executesql @SqlInsertPrimaryKeyStatement

RETURN

