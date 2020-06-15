
INSERT INTO geoip.geoip_locations_temp(
            geoname_id, locale_code, continent_code, continent_name, country_iso_code, 
            country_name
)
SELECT 
     (xpath('//geoname_id/text()', myTempTable.myXmlColumn))[1]::text::bigint AS geoname_id
    ,(xpath('//locale_code/text()', myTempTable.myXmlColumn))[1]::text::character varying AS locale_code
    ,(xpath('//continent_code/text()', myTempTable.myXmlColumn))[1]::text::character varying AS continent_code
    ,(xpath('//continent_name/text()', myTempTable.myXmlColumn))[1]::text::character varying AS continent_name
    ,(xpath('//country_iso_code/text()', myTempTable.myXmlColumn))[1]::text::character varying AS country_iso_code
    ,(xpath('//country_name/text()', myTempTable.myXmlColumn))[1]::text::character varying AS country_name
    
    -- ,myTempTable.myXmlColumn as myXmlElement 
    -- Source: https://en.wikipedia.org/wiki/List_of_DNS_record_types
FROM unnest(xpath('//row', 
 CAST('<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<geoip_locations_temp xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">

<row>
  <geoname_id>49518</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>RW</country_iso_code>
  <country_name>Ruanda</country_name>
</row>

<row>
  <geoname_id>51537</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SO</country_iso_code>
  <country_name>Somalia</country_name>
</row>

<row>
  <geoname_id>69543</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>YE</country_iso_code>
  <country_name>Jemen</country_name>
</row>

<row>
  <geoname_id>99237</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>IQ</country_iso_code>
  <country_name>Irak</country_name>
</row>

<row>
  <geoname_id>102358</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>SA</country_iso_code>
  <country_name>Saudi-Arabien</country_name>
</row>

<row>
  <geoname_id>130758</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>IR</country_iso_code>
  <country_name>Iran (Islamische Republik)</country_name>
</row>

<row>
  <geoname_id>146669</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>CY</country_iso_code>
  <country_name>Zypern</country_name>
</row>

<row>
  <geoname_id>149590</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>TZ</country_iso_code>
  <country_name>Tansania</country_name>
</row>

<row>
  <geoname_id>163843</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>SY</country_iso_code>
  <country_name>Syrien</country_name>
</row>

<row>
  <geoname_id>174982</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>AM</country_iso_code>
  <country_name>Armenien</country_name>
</row>

<row>
  <geoname_id>192950</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>KE</country_iso_code>
  <country_name>Kenia</country_name>
</row>

<row>
  <geoname_id>203312</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>CD</country_iso_code>
  <country_name>Kongo</country_name>
</row>

<row>
  <geoname_id>223816</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>DJ</country_iso_code>
  <country_name>Dschibuti</country_name>
</row>

<row>
  <geoname_id>226074</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>UG</country_iso_code>
  <country_name>Uganda</country_name>
</row>

<row>
  <geoname_id>239880</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>CF</country_iso_code>
  <country_name>Zentralafrikanische Republik</country_name>
</row>

<row>
  <geoname_id>241170</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SC</country_iso_code>
  <country_name>Seychellen</country_name>
</row>

<row>
  <geoname_id>248816</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>JO</country_iso_code>
  <country_name>Jordanien</country_name>
</row>

<row>
  <geoname_id>272103</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>LB</country_iso_code>
  <country_name>Libanon</country_name>
</row>

<row>
  <geoname_id>285570</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>KW</country_iso_code>
  <country_name>Kuwait</country_name>
</row>

<row>
  <geoname_id>286963</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>OM</country_iso_code>
  <country_name>Oman</country_name>
</row>

<row>
  <geoname_id>289688</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>QA</country_iso_code>
  <country_name>Katar</country_name>
</row>

<row>
  <geoname_id>290291</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>BH</country_iso_code>
  <country_name>Bahrain</country_name>
</row>

<row>
  <geoname_id>290557</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>AE</country_iso_code>
  <country_name>Vereinigte Arabische Emirate</country_name>
</row>

<row>
  <geoname_id>294640</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>IL</country_iso_code>
  <country_name>Israel</country_name>
</row>

<row>
  <geoname_id>298795</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>TR</country_iso_code>
  <country_name>Türkei</country_name>
</row>

<row>
  <geoname_id>337996</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>ET</country_iso_code>
  <country_name>Äthiopien</country_name>
</row>

<row>
  <geoname_id>338010</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>ER</country_iso_code>
  <country_name>Eritrea</country_name>
</row>

<row>
  <geoname_id>357994</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>EG</country_iso_code>
  <country_name>Ägypten</country_name>
</row>

<row>
  <geoname_id>366755</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SD</country_iso_code>
  <country_name>Sudan</country_name>
</row>

<row>
  <geoname_id>390903</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>GR</country_iso_code>
  <country_name>Griechenland</country_name>
</row>

<row>
  <geoname_id>433561</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>BI</country_iso_code>
  <country_name>Burundi</country_name>
</row>

<row>
  <geoname_id>453733</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>EE</country_iso_code>
  <country_name>Estland</country_name>
</row>

<row>
  <geoname_id>458258</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>LV</country_iso_code>
  <country_name>Lettland</country_name>
</row>

<row>
  <geoname_id>587116</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>AZ</country_iso_code>
  <country_name>Aserbaidschan</country_name>
</row>

<row>
  <geoname_id>597427</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>LT</country_iso_code>
  <country_name>Litauen</country_name>
</row>

<row>
  <geoname_id>607072</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>SJ</country_iso_code>
  <country_name>Svalbard und Jan Mayen</country_name>
</row>

<row>
  <geoname_id>614540</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>GE</country_iso_code>
  <country_name>Georgien</country_name>
</row>

<row>
  <geoname_id>617790</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>MD</country_iso_code>
  <country_name>Moldau (Republik Moldau)</country_name>
</row>

<row>
  <geoname_id>630336</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>BY</country_iso_code>
  <country_name>Weißrussland</country_name>
</row>

<row>
  <geoname_id>660013</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>FI</country_iso_code>
  <country_name>Finnland</country_name>
</row>

<row>
  <geoname_id>661882</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>AX</country_iso_code>
  <country_name>Alandinseln</country_name>
</row>

<row>
  <geoname_id>690791</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>UA</country_iso_code>
  <country_name>Ukraine</country_name>
</row>

<row>
  <geoname_id>718075</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>MK</country_iso_code>
  <country_name>Ehemalige jugoslawische Republik Mazedonien</country_name>
</row>

<row>
  <geoname_id>719819</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>HU</country_iso_code>
  <country_name>Ungarn</country_name>
</row>

<row>
  <geoname_id>732800</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>BG</country_iso_code>
  <country_name>Bulgarien</country_name>
</row>

<row>
  <geoname_id>783754</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>AL</country_iso_code>
  <country_name>Albanien</country_name>
</row>

<row>
  <geoname_id>798544</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>PL</country_iso_code>
  <country_name>Polen</country_name>
</row>

<row>
  <geoname_id>798549</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>RO</country_iso_code>
  <country_name>Rumänien</country_name>
</row>

<row>
  <geoname_id>831053</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>XK</country_iso_code>
  <country_name>Kosovo</country_name>
</row>

<row>
  <geoname_id>878675</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>ZW</country_iso_code>
  <country_name>Simbabwe</country_name>
</row>

<row>
  <geoname_id>895949</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>ZM</country_iso_code>
  <country_name>Sambia</country_name>
</row>

<row>
  <geoname_id>921929</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>KM</country_iso_code>
  <country_name>Komoren</country_name>
</row>

<row>
  <geoname_id>927384</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>MW</country_iso_code>
  <country_name>Malawi</country_name>
</row>

<row>
  <geoname_id>932692</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>LS</country_iso_code>
  <country_name>Lesotho</country_name>
</row>

<row>
  <geoname_id>933860</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>BW</country_iso_code>
  <country_name>Botswana</country_name>
</row>

<row>
  <geoname_id>934292</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>MU</country_iso_code>
  <country_name>Mauritius</country_name>
</row>

<row>
  <geoname_id>934841</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SZ</country_iso_code>
  <country_name>Swasiland</country_name>
</row>

<row>
  <geoname_id>935317</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>RE</country_iso_code>
  <country_name>Réunion</country_name>
</row>

<row>
  <geoname_id>953987</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>ZA</country_iso_code>
  <country_name>Südafrika</country_name>
</row>

<row>
  <geoname_id>1024031</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>YT</country_iso_code>
  <country_name>Mayotte</country_name>
</row>

<row>
  <geoname_id>1036973</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>MZ</country_iso_code>
  <country_name>Mosambik</country_name>
</row>

<row>
  <geoname_id>1062947</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>MG</country_iso_code>
  <country_name>Madagaskar</country_name>
</row>

<row>
  <geoname_id>1149361</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>AF</country_iso_code>
  <country_name>Afghanistan</country_name>
</row>

<row>
  <geoname_id>1168579</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>PK</country_iso_code>
  <country_name>Pakistan</country_name>
</row>

<row>
  <geoname_id>1210997</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>BD</country_iso_code>
  <country_name>Bangladesch</country_name>
</row>

<row>
  <geoname_id>1218197</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>TM</country_iso_code>
  <country_name>Turkmenistan</country_name>
</row>

<row>
  <geoname_id>1220409</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>TJ</country_iso_code>
  <country_name>Tadschikistan</country_name>
</row>

<row>
  <geoname_id>1227603</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>LK</country_iso_code>
  <country_name>Sri Lanka</country_name>
</row>

<row>
  <geoname_id>1252634</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>BT</country_iso_code>
  <country_name>Bhutan</country_name>
</row>

<row>
  <geoname_id>1269750</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>IN</country_iso_code>
  <country_name>Indien</country_name>
</row>

<row>
  <geoname_id>1282028</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>MV</country_iso_code>
  <country_name>Malediven</country_name>
</row>

<row>
  <geoname_id>1282588</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>IO</country_iso_code>
  <country_name>Britisches Territorium im Indischen Ozean</country_name>
</row>

<row>
  <geoname_id>1282988</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>NP</country_iso_code>
  <country_name>Nepal</country_name>
</row>

<row>
  <geoname_id>1327865</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>MM</country_iso_code>
  <country_name>Birma (Myanmar)</country_name>
</row>

<row>
  <geoname_id>1512440</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>UZ</country_iso_code>
  <country_name>Usbekistan</country_name>
</row>

<row>
  <geoname_id>1522867</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>KZ</country_iso_code>
  <country_name>Kasachstan</country_name>
</row>

<row>
  <geoname_id>1527747</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>KG</country_iso_code>
  <country_name>Kirgistan</country_name>
</row>

<row>
  <geoname_id>1546748</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AN</continent_code>
  <continent_name>Antarktis</continent_name>
  <country_iso_code>TF</country_iso_code>
  <country_name>Französische Süd- und Antarktisgebiete</country_name>
</row>

<row>
  <geoname_id>1547376</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>CC</country_iso_code>
  <country_name>Kokosinseln</country_name>
</row>

<row>
  <geoname_id>1559582</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>PW</country_iso_code>
  <country_name>Palau</country_name>
</row>

<row>
  <geoname_id>1562822</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>VN</country_iso_code>
  <country_name>Vietnam</country_name>
</row>

<row>
  <geoname_id>1605651</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>TH</country_iso_code>
  <country_name>Thailand</country_name>
</row>

<row>
  <geoname_id>1643084</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>ID</country_iso_code>
  <country_name>Indonesien</country_name>
</row>

<row>
  <geoname_id>1655842</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>LA</country_iso_code>
  <country_name>Laos</country_name>
</row>

<row>
  <geoname_id>1668284</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>TW</country_iso_code>
  <country_name>Taiwan</country_name>
</row>

<row>
  <geoname_id>1694008</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>PH</country_iso_code>
  <country_name>Philippinen</country_name>
</row>

<row>
  <geoname_id>1733045</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>MY</country_iso_code>
  <country_name>Malaysia</country_name>
</row>

<row>
  <geoname_id>1814991</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>CN</country_iso_code>
  <country_name>China</country_name>
</row>

<row>
  <geoname_id>1819730</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>HK</country_iso_code>
  <country_name>Hongkong</country_name>
</row>

<row>
  <geoname_id>1820814</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>BN</country_iso_code>
  <country_name>Brunei</country_name>
</row>

<row>
  <geoname_id>1821275</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>MO</country_iso_code>
  <country_name>Macau</country_name>
</row>

<row>
  <geoname_id>1831722</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>KH</country_iso_code>
  <country_name>Kambodscha</country_name>
</row>

<row>
  <geoname_id>1835841</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>KR</country_iso_code>
  <country_name>Südkorea</country_name>
</row>

<row>
  <geoname_id>1861060</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>JP</country_iso_code>
  <country_name>Japan</country_name>
</row>

<row>
  <geoname_id>1873107</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>KP</country_iso_code>
  <country_name>Nordkorea</country_name>
</row>

<row>
  <geoname_id>1880251</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>SG</country_iso_code>
  <country_name>Singapur</country_name>
</row>

<row>
  <geoname_id>1899402</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>CK</country_iso_code>
  <country_name>Cookinseln</country_name>
</row>

<row>
  <geoname_id>1966436</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>TL</country_iso_code>
  <country_name>Timor-Leste</country_name>
</row>

<row>
  <geoname_id>2017370</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>RU</country_iso_code>
  <country_name>Russland</country_name>
</row>

<row>
  <geoname_id>2029969</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>MN</country_iso_code>
  <country_name>Mongolei</country_name>
</row>

<row>
  <geoname_id>2077456</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>AU</country_iso_code>
  <country_name>Australien</country_name>
</row>

<row>
  <geoname_id>2078138</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>CX</country_iso_code>
  <country_name>Weihnachtsinsel</country_name>
</row>

<row>
  <geoname_id>2080185</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>MH</country_iso_code>
  <country_name>Marshall-Inseln</country_name>
</row>

<row>
  <geoname_id>2081918</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>FM</country_iso_code>
  <country_name>Mikronesien</country_name>
</row>

<row>
  <geoname_id>2088628</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>PG</country_iso_code>
  <country_name>Papua-Neuguinea</country_name>
</row>

<row>
  <geoname_id>2103350</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>SB</country_iso_code>
  <country_name>Solomon-Inseln</country_name>
</row>

<row>
  <geoname_id>2110297</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>TV</country_iso_code>
  <country_name>Tuwalu</country_name>
</row>

<row>
  <geoname_id>2110425</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>NR</country_iso_code>
  <country_name>Nauru</country_name>
</row>

<row>
  <geoname_id>2134431</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>VU</country_iso_code>
  <country_name>Vanuatu</country_name>
</row>

<row>
  <geoname_id>2139685</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>NC</country_iso_code>
  <country_name>Neukaledonien</country_name>
</row>

<row>
  <geoname_id>2155115</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>NF</country_iso_code>
  <country_name>Norfolkinsel</country_name>
</row>

<row>
  <geoname_id>2186224</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>NZ</country_iso_code>
  <country_name>Neuseeland</country_name>
</row>

<row>
  <geoname_id>2205218</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>FJ</country_iso_code>
  <country_name>Fidschi</country_name>
</row>

<row>
  <geoname_id>2215636</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>LY</country_iso_code>
  <country_name>Libysch-Arabische Dschamahirija</country_name>
</row>

<row>
  <geoname_id>2233387</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>CM</country_iso_code>
  <country_name>Kamerun</country_name>
</row>

<row>
  <geoname_id>2245662</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SN</country_iso_code>
  <country_name>Senegal</country_name>
</row>

<row>
  <geoname_id>2260494</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>CG</country_iso_code>
  <country_name>Kongo (Republik Kongo)</country_name>
</row>

<row>
  <geoname_id>2264397</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>PT</country_iso_code>
  <country_name>Portugal</country_name>
</row>

<row>
  <geoname_id>2275384</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>LR</country_iso_code>
  <country_name>Liberia</country_name>
</row>

<row>
  <geoname_id>2287781</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>CI</country_iso_code>
  <country_name>Elfenbeinküste</country_name>
</row>

<row>
  <geoname_id>2300660</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>GH</country_iso_code>
  <country_name>Ghana</country_name>
</row>

<row>
  <geoname_id>2309096</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>GQ</country_iso_code>
  <country_name>Äquatorialguinea</country_name>
</row>

<row>
  <geoname_id>2328926</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>NG</country_iso_code>
  <country_name>Nigeria</country_name>
</row>

<row>
  <geoname_id>2361809</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>BF</country_iso_code>
  <country_name>Burkina Faso</country_name>
</row>

<row>
  <geoname_id>2363686</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>TG</country_iso_code>
  <country_name>Togo</country_name>
</row>

<row>
  <geoname_id>2372248</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>GW</country_iso_code>
  <country_name>Guinea-Bissau</country_name>
</row>

<row>
  <geoname_id>2378080</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>MR</country_iso_code>
  <country_name>Mauretanien</country_name>
</row>

<row>
  <geoname_id>2395170</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>BJ</country_iso_code>
  <country_name>Benin</country_name>
</row>

<row>
  <geoname_id>2400553</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>GA</country_iso_code>
  <country_name>Gabun</country_name>
</row>

<row>
  <geoname_id>2403846</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SL</country_iso_code>
  <country_name>Sierra Leone</country_name>
</row>

<row>
  <geoname_id>2410758</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>ST</country_iso_code>
  <country_name>Sao Tomé und Principe</country_name>
</row>

<row>
  <geoname_id>2411586</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>GI</country_iso_code>
  <country_name>Gibraltar</country_name>
</row>

<row>
  <geoname_id>2413451</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>GM</country_iso_code>
  <country_name>Gambia</country_name>
</row>

<row>
  <geoname_id>2420477</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>GN</country_iso_code>
  <country_name>Guinea</country_name>
</row>

<row>
  <geoname_id>2434508</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>TD</country_iso_code>
  <country_name>Tschad</country_name>
</row>

<row>
  <geoname_id>2440476</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>NE</country_iso_code>
  <country_name>Niger</country_name>
</row>

<row>
  <geoname_id>2453866</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>ML</country_iso_code>
  <country_name>Mali</country_name>
</row>

<row>
  <geoname_id>2464461</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>TN</country_iso_code>
  <country_name>Tunesien</country_name>
</row>

<row>
  <geoname_id>2510769</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>ES</country_iso_code>
  <country_name>Spanien</country_name>
</row>

<row>
  <geoname_id>2542007</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>MA</country_iso_code>
  <country_name>Marokko</country_name>
</row>

<row>
  <geoname_id>2562770</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>MT</country_iso_code>
  <country_name>Malta</country_name>
</row>

<row>
  <geoname_id>2589581</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>DZ</country_iso_code>
  <country_name>Algerien</country_name>
</row>

<row>
  <geoname_id>2622320</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>FO</country_iso_code>
  <country_name>Färöer-Inseln</country_name>
</row>

<row>
  <geoname_id>2623032</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>DK</country_iso_code>
  <country_name>Dänemark</country_name>
</row>

<row>
  <geoname_id>2629691</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>IS</country_iso_code>
  <country_name>Island</country_name>
</row>

<row>
  <geoname_id>2635167</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>GB</country_iso_code>
  <country_name>Vereinigtes Königreich</country_name>
</row>

<row>
  <geoname_id>2658434</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>CH</country_iso_code>
  <country_name>Schweiz</country_name>
</row>

<row>
  <geoname_id>2661886</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>SE</country_iso_code>
  <country_name>Schweden</country_name>
</row>

<row>
  <geoname_id>2750405</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>NL</country_iso_code>
  <country_name>Niederlande</country_name>
</row>

<row>
  <geoname_id>2782113</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>AT</country_iso_code>
  <country_name>Österreich</country_name>
</row>

<row>
  <geoname_id>2802361</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>BE</country_iso_code>
  <country_name>Belgien</country_name>
</row>

<row>
  <geoname_id>2921044</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>DE</country_iso_code>
  <country_name>Deutschland</country_name>
</row>

<row>
  <geoname_id>2960313</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>LU</country_iso_code>
  <country_name>Luxemburg</country_name>
</row>

<row>
  <geoname_id>2963597</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>IE</country_iso_code>
  <country_name>Irland</country_name>
</row>

<row>
  <geoname_id>2993457</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>MC</country_iso_code>
  <country_name>Monaco</country_name>
</row>

<row>
  <geoname_id>3017382</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>FR</country_iso_code>
  <country_name>Frankreich</country_name>
</row>

<row>
  <geoname_id>3041565</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>AD</country_iso_code>
  <country_name>Andorra</country_name>
</row>

<row>
  <geoname_id>3042058</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>LI</country_iso_code>
  <country_name>Liechtenstein</country_name>
</row>

<row>
  <geoname_id>3042142</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>JE</country_iso_code>
  <country_name>Jersey</country_name>
</row>

<row>
  <geoname_id>3042225</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>IM</country_iso_code>
  <country_name>Insel Man</country_name>
</row>

<row>
  <geoname_id>3042362</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>GG</country_iso_code>
  <country_name>Guernsey</country_name>
</row>

<row>
  <geoname_id>3057568</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>SK</country_iso_code>
  <country_name>Slowakei (Slowakische Republik)</country_name>
</row>

<row>
  <geoname_id>3077311</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>CZ</country_iso_code>
  <country_name>Tschechien</country_name>
</row>

<row>
  <geoname_id>3144096</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>NO</country_iso_code>
  <country_name>Norwegen</country_name>
</row>

<row>
  <geoname_id>3164670</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>VA</country_iso_code>
  <country_name>Staat der Vatikanstadt</country_name>
</row>

<row>
  <geoname_id>3168068</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>SM</country_iso_code>
  <country_name>San Marino</country_name>
</row>

<row>
  <geoname_id>3175395</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>IT</country_iso_code>
  <country_name>Italien</country_name>
</row>

<row>
  <geoname_id>3190538</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>SI</country_iso_code>
  <country_name>Slowenien</country_name>
</row>

<row>
  <geoname_id>3194884</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>ME</country_iso_code>
  <country_name>Montenegro</country_name>
</row>

<row>
  <geoname_id>3202326</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>HR</country_iso_code>
  <country_name>Kroatien</country_name>
</row>

<row>
  <geoname_id>3277605</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>BA</country_iso_code>
  <country_name>Bosnien und Herzegowina</country_name>
</row>

<row>
  <geoname_id>3351879</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>AO</country_iso_code>
  <country_name>Angola</country_name>
</row>

<row>
  <geoname_id>3355338</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>NA</country_iso_code>
  <country_name>Namibia</country_name>
</row>

<row>
  <geoname_id>3370751</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SH</country_iso_code>
  <country_name>St. Helena</country_name>
</row>

<row>
  <geoname_id>3374084</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>BB</country_iso_code>
  <country_name>Barbados</country_name>
</row>

<row>
  <geoname_id>3374766</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>CV</country_iso_code>
  <country_name>Kap Verde</country_name>
</row>

<row>
  <geoname_id>3378535</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>GY</country_iso_code>
  <country_name>Guyana</country_name>
</row>

<row>
  <geoname_id>3381670</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>GF</country_iso_code>
  <country_name>Französisch-Guayana</country_name>
</row>

<row>
  <geoname_id>3382998</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>SR</country_iso_code>
  <country_name>Suriname</country_name>
</row>

<row>
  <geoname_id>3424932</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>PM</country_iso_code>
  <country_name>St. Pierre und Miquelon</country_name>
</row>

<row>
  <geoname_id>3425505</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>GL</country_iso_code>
  <country_name>Grönland</country_name>
</row>

<row>
  <geoname_id>3437598</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>PY</country_iso_code>
  <country_name>Paraguay</country_name>
</row>

<row>
  <geoname_id>3439705</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>UY</country_iso_code>
  <country_name>Uruguay</country_name>
</row>

<row>
  <geoname_id>3469034</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>BR</country_iso_code>
  <country_name>Brasilien</country_name>
</row>

<row>
  <geoname_id>3474414</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>FK</country_iso_code>
  <country_name>Falklandinseln</country_name>
</row>

<row>
  <geoname_id>3474415</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AN</continent_code>
  <continent_name>Antarktis</continent_name>
  <country_iso_code>GS</country_iso_code>
  <country_name>Südgeorgien und die Südlichen Sandwichinseln</country_name>
</row>

<row>
  <geoname_id>3489940</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>JM</country_iso_code>
  <country_name>Jamaika</country_name>
</row>

<row>
  <geoname_id>3508796</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>DO</country_iso_code>
  <country_name>Dom. Republik</country_name>
</row>

<row>
  <geoname_id>3562981</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>CU</country_iso_code>
  <country_name>Kuba</country_name>
</row>

<row>
  <geoname_id>3570311</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>MQ</country_iso_code>
  <country_name>Martinique</country_name>
</row>

<row>
  <geoname_id>3572887</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>BS</country_iso_code>
  <country_name>Bahamas</country_name>
</row>

<row>
  <geoname_id>3573345</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>BM</country_iso_code>
  <country_name>Bermuda</country_name>
</row>

<row>
  <geoname_id>3573511</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>AI</country_iso_code>
  <country_name>Anguilla</country_name>
</row>

<row>
  <geoname_id>3573591</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>TT</country_iso_code>
  <country_name>Trinidad und Tobago</country_name>
</row>

<row>
  <geoname_id>3575174</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>KN</country_iso_code>
  <country_name>Saint Kitts und Nevis</country_name>
</row>

<row>
  <geoname_id>3575830</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>DM</country_iso_code>
  <country_name>Dominica</country_name>
</row>

<row>
  <geoname_id>3576396</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>AG</country_iso_code>
  <country_name>Antigua und Barbuda</country_name>
</row>

<row>
  <geoname_id>3576468</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>LC</country_iso_code>
  <country_name>St. Lucia</country_name>
</row>

<row>
  <geoname_id>3576916</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>TC</country_iso_code>
  <country_name>Turks- und Caicosinseln</country_name>
</row>

<row>
  <geoname_id>3577279</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>AW</country_iso_code>
  <country_name>Aruba</country_name>
</row>

<row>
  <geoname_id>3577718</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>VG</country_iso_code>
  <country_name>Britische Jungferninseln</country_name>
</row>

<row>
  <geoname_id>3577815</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>VC</country_iso_code>
  <country_name>St. Vincent und die Grenadinen</country_name>
</row>

<row>
  <geoname_id>3578097</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>MS</country_iso_code>
  <country_name>Montserrat</country_name>
</row>

<row>
  <geoname_id>3578421</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>MF</country_iso_code>
  <country_name>St. Martin</country_name>
</row>

<row>
  <geoname_id>3578476</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>BL</country_iso_code>
  <country_name>St. Barthélemy</country_name>
</row>

<row>
  <geoname_id>3579143</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>GP</country_iso_code>
  <country_name>Guadeloupe</country_name>
</row>

<row>
  <geoname_id>3580239</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>GD</country_iso_code>
  <country_name>Grenada</country_name>
</row>

<row>
  <geoname_id>3580718</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>KY</country_iso_code>
  <country_name>Kaimaninseln</country_name>
</row>

<row>
  <geoname_id>3582678</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>BZ</country_iso_code>
  <country_name>Belize</country_name>
</row>

<row>
  <geoname_id>3585968</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>SV</country_iso_code>
  <country_name>El Salvador</country_name>
</row>

<row>
  <geoname_id>3595528</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>GT</country_iso_code>
  <country_name>Guatemala</country_name>
</row>

<row>
  <geoname_id>3608932</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>HN</country_iso_code>
  <country_name>Honduras</country_name>
</row>

<row>
  <geoname_id>3617476</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>NI</country_iso_code>
  <country_name>Nikaragua</country_name>
</row>

<row>
  <geoname_id>3624060</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>CR</country_iso_code>
  <country_name>Costa Rica</country_name>
</row>

<row>
  <geoname_id>3625428</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>VE</country_iso_code>
  <country_name>Venezuela</country_name>
</row>

<row>
  <geoname_id>3658394</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>EC</country_iso_code>
  <country_name>Ecuador</country_name>
</row>

<row>
  <geoname_id>3686110</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>CO</country_iso_code>
  <country_name>Kolumbien</country_name>
</row>

<row>
  <geoname_id>3703430</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>PA</country_iso_code>
  <country_name>Panama</country_name>
</row>

<row>
  <geoname_id>3723988</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>HT</country_iso_code>
  <country_name>Haiti</country_name>
</row>

<row>
  <geoname_id>3865483</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>AR</country_iso_code>
  <country_name>Argentinien</country_name>
</row>

<row>
  <geoname_id>3895114</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>CL</country_iso_code>
  <country_name>Chile</country_name>
</row>

<row>
  <geoname_id>3923057</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>BO</country_iso_code>
  <country_name>Bolivien</country_name>
</row>

<row>
  <geoname_id>3932488</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>SA</continent_code>
  <continent_name>Südamerika</continent_name>
  <country_iso_code>PE</country_iso_code>
  <country_name>Peru</country_name>
</row>

<row>
  <geoname_id>3996063</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>MX</country_iso_code>
  <country_name>Mexiko</country_name>
</row>

<row>
  <geoname_id>4030656</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>PF</country_iso_code>
  <country_name>Französisch-Polynesien</country_name>
</row>

<row>
  <geoname_id>4030699</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>PN</country_iso_code>
  <country_name>Pitcairn</country_name>
</row>

<row>
  <geoname_id>4030945</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>KI</country_iso_code>
  <country_name>Kiribati</country_name>
</row>

<row>
  <geoname_id>4031074</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>TK</country_iso_code>
  <country_name>Tokelau</country_name>
</row>

<row>
  <geoname_id>4032283</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>TO</country_iso_code>
  <country_name>Tonga</country_name>
</row>

<row>
  <geoname_id>4034749</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>WF</country_iso_code>
  <country_name>Wallis und Futuna</country_name>
</row>

<row>
  <geoname_id>4034894</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>WS</country_iso_code>
  <country_name>Samoa</country_name>
</row>

<row>
  <geoname_id>4036232</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>NU</country_iso_code>
  <country_name>Niue</country_name>
</row>

<row>
  <geoname_id>4041468</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>MP</country_iso_code>
  <country_name>Nördliche Marianen</country_name>
</row>

<row>
  <geoname_id>4043988</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>GU</country_iso_code>
  <country_name>Guam</country_name>
</row>

<row>
  <geoname_id>4566966</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>PR</country_iso_code>
  <country_name>Puerto Rico</country_name>
</row>

<row>
  <geoname_id>4796775</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>VI</country_iso_code>
  <country_name>Amerikanische Jungferninseln</country_name>
</row>

<row>
  <geoname_id>5854968</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>UM</country_iso_code>
  <country_name>Amerikanisch-Ozeanien</country_name>
</row>

<row>
  <geoname_id>5880801</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>OC</continent_code>
  <continent_name>Ozeanien</continent_name>
  <country_iso_code>AS</country_iso_code>
  <country_name>Amerikanisch-Samoa</country_name>
</row>

<row>
  <geoname_id>6251999</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>CA</country_iso_code>
  <country_name>Kanada</country_name>
</row>

<row>
  <geoname_id>6252001</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>US</country_iso_code>
  <country_name>USA</country_name>
</row>

<row>
  <geoname_id>6254930</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code>PS</country_iso_code>
  <country_name>Palästinensische Autonomiegebiete</country_name>
</row>

<row>
  <geoname_id>6255147</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AS</continent_code>
  <continent_name>Asien</continent_name>
  <country_iso_code xsi:nil="true"/>
  <country_name xsi:nil="true"/>
</row>

<row>
  <geoname_id>6255148</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code xsi:nil="true"/>
  <country_name xsi:nil="true"/>
</row>

<row>
  <geoname_id>6290252</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>EU</continent_code>
  <continent_name>Europa</continent_name>
  <country_iso_code>RS</country_iso_code>
  <country_name>Serbien</country_name>
</row>

<row>
  <geoname_id>6697173</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AN</continent_code>
  <continent_name>Antarktis</continent_name>
  <country_iso_code>AQ</country_iso_code>
  <country_name>Antarktis</country_name>
</row>

<row>
  <geoname_id>7609695</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>SX</country_iso_code>
  <country_name>Sint Maarten</country_name>
</row>

<row>
  <geoname_id>7626836</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>CW</country_iso_code>
  <country_name>Curaçao</country_name>
</row>

<row>
  <geoname_id>7626844</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>NA</continent_code>
  <continent_name>Nordamerika</continent_name>
  <country_iso_code>BQ</country_iso_code>
  <country_name>Bonaire</country_name>
</row>

<row>
  <geoname_id>7909807</geoname_id>
  <locale_code>de</locale_code>
  <continent_code>AF</continent_code>
  <continent_name>Afrika</continent_name>
  <country_iso_code>SS</country_iso_code>
  <country_name>Südsudan</country_name>
</row>

</geoip_locations_temp>
' AS xml)   
)) AS myTempTable(myXmlColumn)
;