-- Table: public.t_sys_language_daynames

-- DROP TABLE public.t_sys_language_daynames;

CREATE TABLE public.t_sys_language_daynames
(
  sysdays_syslang_lcid integer NOT NULL,
  sysdays_dayofweekindexbasezero integer NOT NULL,
  sysdays_dayofweekindexbaseone integer,
  sysdays_syslang_ietflanguagetag character varying(255),
  sysdays_name character varying(255),
  sysdays_lowercasename character varying(255),
  sysdays_uppercasename character varying(255),
  sysdays_titlecasename character varying(255),
  sysdays_abbreviatedname character varying(255),
  sysdays_lowercaseabbreviatedname character varying(255),
  sysdays_uppercaseabbreviatedname character varying(255),
  sysdays_titlecaseabbreviatedname character varying(255),
  sysdays_shortestname character varying(255),
  sysdays_lowercaseshortestname character varying(255),
  sysdays_uppercaseshortestname character varying(255),
  sysdays_titlecaseshortestname character varying(255)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.t_sys_language_daynames
  OWNER TO postgres;
