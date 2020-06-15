-- Function: public.mytest()

-- DROP FUNCTION public.mytest();

CREATE OR REPLACE FUNCTION public.mytest()
  RETURNS SETOF refcursor AS
$BODY$
DECLARE 
  ref1 refcursor;
BEGIN

OPEN ref1 FOR 
 SELECT * FROM t_blogpost;
RETURN NEXT ref1;


RETURN;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000;
ALTER FUNCTION public.mytest()
  OWNER TO postgres;
