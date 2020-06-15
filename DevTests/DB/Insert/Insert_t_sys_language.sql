INSERT INTO public.t_sys_language(
            syslang_lcid, syslang_culturename, syslang_name, syslang_ietflanguagetag, 
            syslang_twoletterisolanguagename, syslang_threeletterisolanguagename, 
            syslang_threeletterwindowslanguagename, syslang_englishname, 
            syslang_nativename, syslang_displayname, syslang_nativecalendarname, 
            syslang_fulldatetimepattern, syslang_rfc1123pattern, syslang_sortabledatetimepattern, 
            syslang_universalsortabledatetimepattern, syslang_dateseparator, 
            syslang_longdatepattern, syslang_shortdatepattern, syslang_longtimepattern, 
            syslang_shorttimepattern, syslang_yearmonthpattern, syslang_monthdaypattern, 
            syslang_pmdesignator, syslang_amdesignator, syslang_calendar, 
            syslang_isneutralculture, syslang_isrighttoleft, syslang_parentlcid, 
            syslang_ansicodepage, syslang_ebcdiccodepage, syslang_maccodepage, 
            syslang_oemcodepage, syslang_listseparator, syslang_numberdecimalseparator, 
            syslang_numbergroupseparator, syslang_numbernegativepattern, 
            syslang_currencydecimalseparator, syslang_currencygroupseparator, 
            syslang_currencysymbol, syslang_currencynegativepattern, syslang_currencypositivepattern, 
            syslang_percentdecimalseparator, syslang_percentgroupseparator, 
            syslang_percentnegativepattern, syslang_percentpositivepattern, 
            syslang_coruse)
SELECT 
	 (xpath('//syslang_lcid/text()', myTempTable.myXmlColumn))[1]::text::integer AS syslang_lcid
	,(xpath('//syslang_culturename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_culturename
	,(xpath('//syslang_name/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_name
	,(xpath('//syslang_ietflanguagetag/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_ietflanguagetag
	,(xpath('//syslang_twoletterisolanguagename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_twoletterisolanguagename
	,(xpath('//syslang_threeletterisolanguagename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_threeletterisolanguagename
	,(xpath('//syslang_threeletterwindowslanguagename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_threeletterwindowslanguagename
	,(xpath('//syslang_englishname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_englishname
	,(xpath('//syslang_nativename/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_nativename
	,(xpath('//syslang_displayname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_displayname
	,(xpath('//syslang_nativecalendarname/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_nativecalendarname
	,(xpath('//syslang_fulldatetimepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_fulldatetimepattern
	,(xpath('//syslang_rfc1123pattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_rfc1123pattern
	,(xpath('//syslang_sortabledatetimepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_sortabledatetimepattern
	,(xpath('//syslang_universalsortabledatetimepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_universalsortabledatetimepattern
	,(xpath('//syslang_dateseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_dateseparator
	,(xpath('//syslang_longdatepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_longdatepattern
	,(xpath('//syslang_shortdatepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_shortdatepattern
	,(xpath('//syslang_longtimepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_longtimepattern
	,(xpath('//syslang_shorttimepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_shorttimepattern
	,(xpath('//syslang_yearmonthpattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_yearmonthpattern
	,(xpath('//syslang_monthdaypattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_monthdaypattern
	,(xpath('//syslang_pmdesignator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_pmdesignator
	,(xpath('//syslang_amdesignator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_amdesignator
	,(xpath('//syslang_calendar/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_calendar
	,(xpath('//syslang_isneutralculture/text()', myTempTable.myXmlColumn))[1]::text::boolean AS syslang_isneutralculture
	,(xpath('//syslang_isrighttoleft/text()', myTempTable.myXmlColumn))[1]::text::boolean AS syslang_isrighttoleft
	,(xpath('//syslang_parentlcid/text()', myTempTable.myXmlColumn))[1]::text::integer AS syslang_parentlcid
	,(xpath('//syslang_ansicodepage/text()', myTempTable.myXmlColumn))[1]::text::integer AS syslang_ansicodepage
	,(xpath('//syslang_ebcdiccodepage/text()', myTempTable.myXmlColumn))[1]::text::integer AS syslang_ebcdiccodepage
	,(xpath('//syslang_maccodepage/text()', myTempTable.myXmlColumn))[1]::text::integer AS syslang_maccodepage
	,(xpath('//syslang_oemcodepage/text()', myTempTable.myXmlColumn))[1]::text::integer AS syslang_oemcodepage
	,(xpath('//syslang_listseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_listseparator
	,(xpath('//syslang_numberdecimalseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_numberdecimalseparator
	,(xpath('//syslang_numbergroupseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_numbergroupseparator
	,(xpath('//syslang_numbernegativepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_numbernegativepattern
	,(xpath('//syslang_currencydecimalseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_currencydecimalseparator
	,(xpath('//syslang_currencygroupseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_currencygroupseparator
	,(xpath('//syslang_currencysymbol/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_currencysymbol
	,(xpath('//syslang_currencynegativepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_currencynegativepattern
	,(xpath('//syslang_currencypositivepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_currencypositivepattern
	,(xpath('//syslang_percentdecimalseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_percentdecimalseparator
	,(xpath('//syslang_percentgroupseparator/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_percentgroupseparator
	,(xpath('//syslang_percentnegativepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_percentnegativepattern
	,(xpath('//syslang_percentpositivepattern/text()', myTempTable.myXmlColumn))[1]::text::character varying AS syslang_percentpositivepattern
	,(xpath('//syslang_coruse/text()', myTempTable.myXmlColumn))[1]::text::boolean AS syslang_coruse

    -- ,myTempTable.myXmlColumn as myXmlElement
FROM unnest(
    xpath
    (    '//record'
        ,XMLPARSE(DOCUMENT convert_from(pg_read_binary_file('t_sys_language.xml'), 'UTF8'))
    )
) AS myTempTable(myXmlColumn)
;
