
ALTER VIEW dbo.V_Navigation 
AS 
WITH CTE AS
(
	SELECT 
		 T_Navigation.NAV_UID 
		,T_Navigation.NAV_TargetUID 
		,T_Navigation.NAV_NAV_UID 
		,T_Navigation.NAV_Text 
		,0 AS lvl 
		,CAST(T_Navigation.NAV_Text  AS nvarchar(MAX)) AS NAV_Path 
		,ROW_NUMBER() OVER (ORDER BY T_Navigation.NAV_Text) AS rn  
	FROM T_Navigation
	WHERE NAV_NAV_UID IS NULL 

	UNION ALL 

	SELECT 
		 T_Navigation.NAV_UID 
		,T_Navigation.NAV_TargetUID 
		,T_Navigation.NAV_NAV_UID 
		,T_Navigation.NAV_Text 
		,CTE.lvl + 1 AS lvl 
		,CTE.NAV_Path + '/' + CAST(T_Navigation.NAV_Text  AS nvarchar(MAX)) AS NAV_Path 
		,ROW_NUMBER() OVER (ORDER BY T_Navigation.NAV_Text) AS rn  
	FROM CTE 
	INNER JOIN T_Navigation 
		ON T_Navigation.NAV_NAV_UID = CTE.NAV_TargetUID 
)
SELECT 
	 NAV_UID 
	,NAV_TargetUID 
	,NAV_NAV_UID 
	-- ,NAV_Text 
	,lvl 
	,NAV_Path 
	,rn 
FROM CTE 
