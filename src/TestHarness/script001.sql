-- Database: myDataBase

-- DROP DATABASE "myDataBase";

--CREATE DATABASE "myDataBase"
--    WITH 
--    OWNER = postgres
--    ENCODING = 'UTF8'
--    LC_COLLATE = 'English_United Kingdom.1252'
--    LC_CTYPE = 'English_United Kingdom.1252'
--    TABLESPACE = pg_default
--    CONNECTION LIMIT = -1;

INSERT INTO article (cola,colb)
SELECT x.id, 'article #' || x.id
  FROM generate_series(10,10000000) AS x(id);