

CREATE TABLE dbo.T_Navigation
(
	 NAV_UID uniqueidentifier NOT NULL 
	,NAV_TargetUID uniqueidentifier NOT NULL 
	,NAV_NAV_UID uniqueidentifier NULL 
	,NAV_Text national character varying(4000) NULL 
	,CONSTRAINT PK_T_Navigation PRIMARY KEY( NAV_UID ) 
)

ALTER TABLE dbo.T_Navigation ADD CONSTRAINT DV_NAV_UID DEFAULT NEWID() FOR NAV_UID;
