
CREATE TABLE dbo.T_BlogPost
(
	 BP_UID uniqueidentifier NOT NULL 
	,BP_Title national character varying(200) NULL 
	,BP_Content national character varying(MAX) NULL 
	,BP_CreoleText national character varying(MAX) NULL 
	,BP_BBCode national character varying(MAX) NULL 
	,BP_HtmlContent national character varying(MAX) NULL 
	,BP_EntryDate datetime NULL 
	,CONSTRAINT PK_T_BlogPost PRIMARY KEY( BP_UID ) 
);
