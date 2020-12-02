
-- SELECT '{ "abc": "def" }'::json

/*

SELECT * FROM (
VALUES('{ "customer": "Lily Bush", "items": {"product": "Diaper","qty": 24}}'),
      ('{ "customer": "Josh William", "items": {"product": "Toy Car","qty": 1}}'),
      ('{ "customer": "Mary Clark", "items": {"product": "Toy Train","qty": 2}}')
      ) AS t 
*/

-- https://www.postgresqltutorial.com/postgresql-json/

-- json in Postgres 9.3+
-- jsonb in Postgres 9.4+
SELECT t.value->'customer' AS customer 
	,t.value->'items' AS items 
	,t.value->'items'->'product' AS customer 
	,(t.value->'items'->'qty')::text::int AS qty
	-- ,json_object_keys (t.value->'items') AS keys -- is like a join
FROM json_array_elements(
 '[ 
  { "customer": "Lily Bush", "items": {"product": "Diaper","qty": 24}}
 ,{ "customer": "Josh William", "items": {"product": "Toy Car","qty": 1}}
 ,{ "customer": "Mary Clark", "items": {"product": "Toy Train","qty": 2}}
]'::json) AS t 
;




-- https://attacomsian.com/blog/export-postgresql-table-data-json
-- https://stackoverflow.com/questions/39224382/how-can-i-import-a-json-file-into-postgresql#:~:text=If%20the%20data%20is%20provided,your%20SQL%20client%20offers)%3A

SELECT array_to_json(array_agg(row_to_json (r))) 
FROM 
(
	SELECT * 
	FROM public.__steuern_2016 
	LIMIT 1
) r;




select * from json_populate_recordset(null::__steuern_2016, 
        '[{"kanton":"ZH","gemeindenummer":261,"gemeinde":"Zürich"
,"latitude":47.376887,"longitude":8.541694,"from_12500":0.74
,"from_15000":1.29,"from_17500":1.83,"from_20000":2.35
,"from_25000":3.41,"from_30000":4,"from_35000":4.73
,"from_40000":5.43,"from_45000":6.04,"from_50000":6.65
,"from_60000":7.63,"from_70000":8.62,"from_80000":9.54
,"from_90000":10.3,"from_100000":11.04,"from_125000":12.38
,"from_150000":13.59,"from_175000":14.72,"from_200000":15.69
,"from_250000":17.31,"from_300000":18.56,"from_400000":20.57
,"from_500000":21.78,"from_1000000":24.19}]'

          );


-- https://stackoverflow.com/questions/25785575/how-to-parse-json-using-json-populate-recordset-in-postgres
CREATE TYPE anoop_type AS (id int, name varchar(100));


select * from json_populate_recordset(null::anoop_type, 
        '[{"id":67272,"name":"EE_Quick_Changes_J_UTP.xlsx"},
          {"id":67273,"name":"16167.txt"},
          {"id":67274,"name":"EE_12_09_2013_Bcum_Searchall.png"}]');