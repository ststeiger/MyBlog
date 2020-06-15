
INSERT INTO geoip.geoip_blocks_temp
(
            network, geoname_id, registered_country_geoname_id, represented_country_geoname_id, 
            is_anonymous_proxy, is_satellite_provider, lower_boundary, upper_boundary
)
SELECT 
 (xpath('//network/text()', myTempTable.myXmlColumn))[1]::text::character varying AS network
,(xpath('//geoname_id/text()', myTempTable.myXmlColumn))[1]::text::bigint AS geoname_id
,(xpath('//registered_country_geoname_id/text()', myTempTable.myXmlColumn))[1]::text::bigint AS registered_country_geoname_id
,(xpath('//represented_country_geoname_id/text()', myTempTable.myXmlColumn))[1]::text::bigint AS represented_country_geoname_id
,(xpath('//is_anonymous_proxy/text()', myTempTable.myXmlColumn))[1]::text::integer AS is_anonymous_proxy
,(xpath('//is_satellite_provider/text()', myTempTable.myXmlColumn))[1]::text::integer AS is_satellite_provider
,(xpath('//lower_boundary/text()', myTempTable.myXmlColumn))[1]::text::character varying AS lower_boundary
,(xpath('//upper_boundary/text()', myTempTable.myXmlColumn))[1]::text::character varying AS upper_boundary

    -- ,myTempTable.myXmlColumn as myXmlElement
FROM unnest(
    xpath
    (    '//record'
        ,XMLPARSE(DOCUMENT convert_from(pg_read_binary_file('geoip_blocks_temp.xml'), 'UTF8'))
    )
) AS myTempTable(myXmlColumn)
;
