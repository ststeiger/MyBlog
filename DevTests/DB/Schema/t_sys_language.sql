-- Table: public.t_sys_language

-- DROP TABLE public.t_sys_language;

CREATE TABLE public.t_sys_language
(
  syslang_lcid integer NOT NULL,
  syslang_culturename character varying(255),
  syslang_name character varying(255),
  syslang_ietflanguagetag character varying(255),
  syslang_twoletterisolanguagename character varying(255),
  syslang_threeletterisolanguagename character varying(255),
  syslang_threeletterwindowslanguagename character varying(255),
  syslang_englishname character varying(255),
  syslang_nativename character varying(255),
  syslang_displayname character varying(255),
  syslang_nativecalendarname character varying(255),
  syslang_fulldatetimepattern character varying(255),
  syslang_rfc1123pattern character varying(255),
  syslang_sortabledatetimepattern character varying(255),
  syslang_universalsortabledatetimepattern character varying(255),
  syslang_dateseparator character varying(255),
  syslang_longdatepattern character varying(255),
  syslang_shortdatepattern character varying(255),
  syslang_longtimepattern character varying(255),
  syslang_shorttimepattern character varying(255),
  syslang_yearmonthpattern character varying(255),
  syslang_monthdaypattern character varying(255),
  syslang_pmdesignator character varying(255),
  syslang_amdesignator character varying(255),
  syslang_calendar character varying(255),
  syslang_isneutralculture boolean,
  syslang_isrighttoleft boolean,
  syslang_parentlcid integer,
  syslang_ansicodepage integer,
  syslang_ebcdiccodepage integer,
  syslang_maccodepage integer,
  syslang_oemcodepage integer,
  syslang_listseparator character varying(255),
  syslang_numberdecimalseparator character varying(255),
  syslang_numbergroupseparator character varying(255),
  syslang_numbernegativepattern character varying(255),
  syslang_currencydecimalseparator character varying(255),
  syslang_currencygroupseparator character varying(255),
  syslang_currencysymbol character varying(255),
  syslang_currencynegativepattern character varying(255),
  syslang_currencypositivepattern character varying(255),
  syslang_percentdecimalseparator character varying(255),
  syslang_percentgroupseparator character varying(255),
  syslang_percentnegativepattern character varying(255),
  syslang_percentpositivepattern character varying(255),
  syslang_coruse boolean
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.t_sys_language
  OWNER TO postgres;
