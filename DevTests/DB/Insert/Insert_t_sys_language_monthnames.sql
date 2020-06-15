
INSERT INTO public.t_sys_language_monthnames(
            sysmonths_syslang_lcid, sysmonths_monthindexbasezero, sysmonths_monthindexbaseone, 
            sysmonths_syslang_ietflanguagetag, sysmonths_name, sysmonths_lowercasename, 
            sysmonths_uppercasename, sysmonths_titlecasename, sysmonths_genitivename, 
            sysmonths_lowercasegenitivename, sysmonths_uppercasegenitivename, 
            sysmonths_titlecasegenitivename, sysmonths_abbreviatedname, sysmonths_lowercaseabbreviatedname, 
            sysmonths_uppercaseabbreviatedname, sysmonths_titlecaseabbreviatedname, 
            sysmonths_abbreviatedgenitivename, sysmonths_lowercaseabbreviatedgenitivename, 
            sysmonths_uppercaseabbreviatedgenitivename, sysmonths_titlecaseabbreviatedgenitivename
)
SELECT 
	 (xpath('//sysmonths_syslang_lcid/text()', myTempTable.myXmlColumn))[1]::text::integer AS sysmonths_syslang_lcid
	,(xpath('//sysmonths_monthindexbasezero/text()', myTempTable.myXmlColumn))[1]::text::integer AS sysmonths_monthindexbasezero
	,(xpath('//sysmonths_monthindexbaseone/text()', myTempTable.myXmlColumn))[1]::text::integer AS sysmonths_monthindexbaseone
	,(xpath('//sysmonths_syslang_ietflanguagetag/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_syslang_ietflanguagetag
	,(xpath('//sysmonths_name/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_name
	,(xpath('//sysmonths_lowercasename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_lowercasename
	,(xpath('//sysmonths_uppercasename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_uppercasename
	,(xpath('//sysmonths_titlecasename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_titlecasename
	,(xpath('//sysmonths_genitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_genitivename
	,(xpath('//sysmonths_lowercasegenitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_lowercasegenitivename
	,(xpath('//sysmonths_uppercasegenitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_uppercasegenitivename
	,(xpath('//sysmonths_titlecasegenitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_titlecasegenitivename
	,(xpath('//sysmonths_abbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_abbreviatedname
	,(xpath('//sysmonths_lowercaseabbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_lowercaseabbreviatedname
	,(xpath('//sysmonths_uppercaseabbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_uppercaseabbreviatedname
	,(xpath('//sysmonths_titlecaseabbreviatedname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_titlecaseabbreviatedname
	,(xpath('//sysmonths_abbreviatedgenitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_abbreviatedgenitivename
	,(xpath('//sysmonths_lowercaseabbreviatedgenitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_lowercaseabbreviatedgenitivename
	,(xpath('//sysmonths_uppercaseabbreviatedgenitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_uppercaseabbreviatedgenitivename
	,(xpath('//sysmonths_titlecaseabbreviatedgenitivename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS sysmonths_titlecaseabbreviatedgenitivename

    -- ,myTempTable.myXmlColumn as myXmlElement
FROM unnest(
    xpath
    (    '//record'
        ,XMLPARSE(DOCUMENT convert_from(pg_read_binary_file('t_sys_language_monthnames.xml'), 'UTF8'))
    )
) AS myTempTable(myXmlColumn)
;