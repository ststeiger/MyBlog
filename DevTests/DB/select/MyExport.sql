
-- https://stackoverflow.com/questions/50276474/dump-postgresql-table-to-xml-file
-- https://stackoverflow.com/questions/19007884/import-xml-files-to-postgresql/33211885#33211885



SELECT table_to_xml('t_blogpost', true, false, '');

SELECT table_to_xml('t_sys_language', true, false, '');
SELECT table_to_xml('t_sys_language_daynames', true, false, '');
SELECT table_to_xml('t_sys_language_monthnames', true, false, '');



SELECT table_to_xml('geoip.geoip_locations_temp', true, false, '');
SELECT table_to_xml('geoip.geoip_blocks_temp', true, false, '');


COPY (SELECT table_to_xml('geoip.geoip_blocks_temp', true, false, '')) to 'D:/file.xml';


SELECT 
	CASE WHEN ordinal_position = 1 THEN ' ' ELSE ',' END 
     || '(xpath(''//' || column_name || '/text()'', myTempTable.myXmlColumn))[1]::text::' || data_type || ' AS ' || column_name 
    -- ,column_name 
    -- ,is_nullable
    ,data_type 
FROM information_schema.columns 
WHERE table_schema = 'geoip' 
AND table_name = 'geoip_blocks_temp' 
ORDER BY ordinal_position 
