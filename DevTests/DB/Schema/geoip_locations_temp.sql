-- Table: geoip.geoip_locations_temp

-- DROP TABLE geoip.geoip_locations_temp;

CREATE TABLE geoip.geoip_locations_temp
(
  geoname_id bigint NOT NULL,
  locale_code character varying(2) NOT NULL,
  continent_code character varying(2),
  continent_name character varying(45),
  country_iso_code character varying(2),
  country_name character varying(45),
  CONSTRAINT pk_geoip_locations_temp PRIMARY KEY (geoname_id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE geoip.geoip_locations_temp
  OWNER TO postgres;
