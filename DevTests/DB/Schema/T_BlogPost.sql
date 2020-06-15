/*
--  (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_BlogPost_AutoAudit_RowVersion_DF') AND type = 'D')
BEGIN
ALTER TABLE T_BlogPost DROP CONSTRAINT T_BlogPost_AutoAudit_RowVersion_DF
END
GO

--  (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_BlogPost_AutoAudit_ModifiedBy_DF') AND type = 'D')
BEGIN
ALTER TABLE T_BlogPost DROP CONSTRAINT T_BlogPost_AutoAudit_ModifiedBy_DF
END
GO

--  (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_BlogPost_AutoAudit_ModifiedDate_DF') AND type = 'D')
BEGIN
ALTER TABLE T_BlogPost DROP CONSTRAINT T_BlogPost_AutoAudit_ModifiedDate_DF
END
GO

--  (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_BlogPost_AutoAudit_CreatedBy_DF') AND type = 'D')
BEGIN
ALTER TABLE T_BlogPost DROP CONSTRAINT T_BlogPost_AutoAudit_CreatedBy_DF
END
GO

--  (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_BlogPost_AutoAudit_CreatedDate_DF') AND type = 'D')
BEGIN
ALTER TABLE T_BlogPost DROP CONSTRAINT T_BlogPost_AutoAudit_CreatedDate_DF
END
GO


--  (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_BlogPost') AND type in (N'U'))
-- DROP TABLE T_BlogPost
GO
*/

CREATE TABLE IF NOT EXISTS T_BlogPost
(
	 BP_UID uuid NOT NULL 
	,BP_Title national character varying(200) 
	,BP_Content national character varying
	,BP_CreoleText national character varying
	,BP_BBCode national character varying 
	,BP_HtmlContent national character varying
	,BP_EntryDate timestamp without time zone 
	,AutoAudit_CreatedDate timestamp without time zone 
	,AutoAudit_CreatedBy national character varying(128) 
	,AutoAudit_ModifiedDate timestamp without time zone 
	,AutoAudit_ModifiedBy national character varying(128) 
	,AutoAudit_RowVersion int 
	,CONSTRAINT PK_T_BlogPost PRIMARY KEY( BP_UID )
); 


-- ALTER TABLE T_BlogPost ADD  CONSTRAINT T_BlogPost_AutoAudit_CreatedDate_DF  DEFAULT (CURRENT_TIMESTAMP) FOR AutoAudit_CreatedDate; 
ALTER TABLE T_BlogPost ALTER COLUMN AutoAudit_CreatedDate SET DEFAULT CURRENT_TIMESTAMP; 


-- ALTER TABLE T_BlogPost ADD  CONSTRAINT T_BlogPost_AutoAudit_CreatedBy_DF  DEFAULT (SUSER_SNAME()) FOR AutoAudit_CreatedBy; 
ALTER TABLE T_BlogPost ALTER COLUMN AutoAudit_CreatedBy SET DEFAULT CURRENT_USER; 

-- ALTER TABLE T_BlogPost ADD  CONSTRAINT T_BlogPost_AutoAudit_ModifiedDate_DF  DEFAULT (CURRENT_TIMESTAMP) FOR AutoAudit_ModifiedDate; 
ALTER TABLE T_BlogPost ALTER COLUMN AutoAudit_ModifiedDate SET DEFAULT CURRENT_TIMESTAMP; 


-- ALTER TABLE T_BlogPost ADD  CONSTRAINT T_BlogPost_AutoAudit_ModifiedBy_DF  DEFAULT ( SUSER_SNAME()) FOR AutoAudit_ModifiedBy; 
ALTER TABLE T_BlogPost ALTER COLUMN AutoAudit_ModifiedBy SET DEFAULT CURRENT_USER; 


-- ALTER TABLE T_BlogPost ADD  CONSTRAINT T_BlogPost_AutoAudit_RowVersion_DF  DEFAULT ((1)) FOR AutoAudit_RowVersion; 
ALTER TABLE T_BlogPost ALTER COLUMN AutoAudit_RowVersion SET DEFAULT 1; 
