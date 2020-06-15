
INSERT INTO public.t_sys_language_daynames(
            sysdays_syslang_lcid, sysdays_dayofweekindexbasezero, sysdays_dayofweekindexbaseone, 
            sysdays_syslang_ietflanguagetag, sysdays_name, sysdays_lowercasename, 
            sysdays_uppercasename, sysdays_titlecasename, sysdays_abbreviatedname, 
            sysdays_lowercaseabbreviatedname, sysdays_uppercaseabbreviatedname, 
            sysdays_titlecaseabbreviatedname, sysdays_shortestname, sysdays_lowercaseshortestname, 
            sysdays_uppercaseshortestname, sysdays_titlecaseshortestname
)
SELECT 
	 (xpath('//sysdays_syslang_lcid/text()', myTempTable.myXmlColumn))[1]::text::integer AS sysdays_syslang_lcid
	,(xpath('//sysdays_dayofweekindexbasezero/text()', myTempTable.myXmlColumn))[1]::text::integer AS sysdays_dayofweekindexbasezero
	,(xpath('//sysdays_dayofweekindexbaseone/text()', myTempTable.myXmlColumn))[1]::text::integer AS sysdays_dayofweekindexbaseone
	,(xpath('//sysdays_syslang_ietflanguagetag/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_syslang_ietflanguagetag
	,(xpath('//sysdays_name/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_name
	,(xpath('//sysdays_lowercasename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_lowercasename
	,(xpath('//sysdays_uppercasename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_uppercasename
	,(xpath('//sysdays_titlecasename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_titlecasename
	,(xpath('//sysdays_abbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_abbreviatedname
	,(xpath('//sysdays_lowercaseabbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_lowercaseabbreviatedname
	,(xpath('//sysdays_uppercaseabbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_uppercaseabbreviatedname
	,(xpath('//sysdays_titlecaseabbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_titlecaseabbreviatedname
	,(xpath('//sysdays_shortestname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_shortestname
	,(xpath('//sysdays_lowercaseshortestname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_lowercaseshortestname
	,(xpath('//sysdays_uppercaseshortestname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_uppercaseshortestname
	,(xpath('//sysdays_titlecaseshortestname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysdays_titlecaseshortestname

    -- ,myTempTable.myXmlColumn as myXmlElement
FROM unnest(
    xpath
    (    '//record'
        ,XMLPARSE(DOCUMENT convert_from(pg_read_binary_file('t_sys_language_daynames.xml'), 'UTF8'))
    )
) AS myTempTable(myXmlColumn)
;
