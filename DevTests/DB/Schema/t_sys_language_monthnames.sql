-- Table: public.t_sys_language_monthnames

-- DROP TABLE public.t_sys_language_monthnames;

CREATE TABLE public.t_sys_language_monthnames
(
  sysmonths_syslang_lcid integer NOT NULL,
  sysmonths_monthindexbasezero integer NOT NULL,
  sysmonths_monthindexbaseone integer,
  sysmonths_syslang_ietflanguagetag character varying(255),
  sysmonths_name character varying(255),
  sysmonths_lowercasename character varying(255),
  sysmonths_uppercasename character varying(255),
  sysmonths_titlecasename character varying(255),
  sysmonths_genitivename character varying(255),
  sysmonths_lowercasegenitivename character varying(255),
  sysmonths_uppercasegenitivename character varying(255),
  sysmonths_titlecasegenitivename character varying(255),
  sysmonths_abbreviatedname character varying(255),
  sysmonths_lowercaseabbreviatedname character varying(255),
  sysmonths_uppercaseabbreviatedname character varying(255),
  sysmonths_titlecaseabbreviatedname character varying(255),
  sysmonths_abbreviatedgenitivename character varying(255),
  sysmonths_lowercaseabbreviatedgenitivename character varying(255),
  sysmonths_uppercaseabbreviatedgenitivename character varying(255),
  sysmonths_titlecaseabbreviatedgenitivename character varying(255)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.t_sys_language_monthnames
  OWNER TO postgres;
