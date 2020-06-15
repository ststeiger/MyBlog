-- Table: geoip.geoip_blocks_temp

-- DROP TABLE geoip.geoip_blocks_temp;

CREATE TABLE geoip.geoip_blocks_temp
(
  network character varying(32) NOT NULL DEFAULT ''::character varying,
  geoname_id bigint,
  registered_country_geoname_id bigint,
  represented_country_geoname_id bigint,
  is_anonymous_proxy integer NOT NULL,
  is_satellite_provider integer NOT NULL,
  lower_boundary character varying(15) NOT NULL DEFAULT ''::character varying,
  upper_boundary character varying(15) NOT NULL DEFAULT ''::character varying,
  CONSTRAINT pk_geoip_blocks_temp PRIMARY KEY (network),
  CONSTRAINT fk_geoip_locations_temp FOREIGN KEY (geoname_id)
      REFERENCES geoip.geoip_locations_temp (geoname_id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH (
  OIDS=FALSE
);
ALTER TABLE geoip.geoip_blocks_temp
  OWNER TO postgres;
