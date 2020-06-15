-- Function: public.datecast(character varying, character varying)

-- DROP FUNCTION public.datecast(character varying, character varying);

CREATE OR REPLACE FUNCTION public.datecast(
    hexval character varying,
    castas character varying)
  RETURNS timestamp without time zone AS
$BODY$
DECLARE
    result timestamp;
BEGIN
    result := ('19000101'::timestamp + (CAST(('x' || lpad(substring(hexval, 3,8), 8, '0'))::bit(32)::integer as varchar(20)) || ' days')::interval)  + (CAST(('x' || lpad(substring(hexval, 11,8), 8, '0'))::bit(32)::integer* 10 / 3.0 as varchar(20)) || ' milliseconds')::interval;
	-- date + time from Char
    
    RETURN result;
END;
$BODY$
  LANGUAGE plpgsql IMMUTABLE STRICT
  COST 100;
ALTER FUNCTION public.datecast(character varying, character varying)
  OWNER TO postgres;
