
SELECT 
	 BP_UID
	,BP_Title
	,BP_Content
	,BP_CreoleText
	,BP_BBCode
	,BP_HtmlContent
	,BP_EntryDate
FROM T_BlogPost
WHERE (1=1)


ORDER BY BP_EntryDate DESC 

OFFSET 0 ROWS FETCH NEXT 200 ROWS ONLY 

-- "OFFSET " + 0.ToString() + "" ROWS FETCH NEXT " + 200.ToString() + " ROWS ONLY " 


-- https://weblog.west-wind.com/posts/2019/Mar/16/ASPNET-Core-Hosting-on-IIS-with-ASPNET-Core-22
-- https://www.strathweb.com/
-- https://www.strathweb.com/2020/10/beautiful-and-compact-web-apis-with-c-9-net-5-0-and-asp-net-core/

