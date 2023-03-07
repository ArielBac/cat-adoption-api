#MySQL

USE cat_adoption_db;

DROP PROCEDURE IF EXISTS InsertVaccineIfNotExistsProcedure;

DELIMITER $$
CREATE PROCEDURE InsertVaccineIfNotExistsProcedure (IN NameParam VARCHAR(30), ProducerParam VARCHAR(50), Applied_atParam DATETIME, CatIdParam INT)
BEGIN
DECLARE erro_sql TINYINT DEFAULT FALSE;
DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET erro_sql = TRUE;
START TRANSACTION;
	SET @hasCat = (SELECT count(*) FROM vaccines WHERE (
		Name = NameParam AND 
        Producer = ProducerParam AND 
        Applied_at = Applied_atParam AND
        CatId = CatIdParam));
    
    IF (@hasCat != 0) THEN
		SET @message = concat("A vaccina ", NameParam, ", aplicada a este gatinho já foi registrada");
		SELECT @message;
	ELSE
		INSERT INTO vaccines (Name, Producer, Applied_at, CatId)
		VALUES(NameParam, ProducerParam, Applied_atParam, CatIdParam);
    END IF;
    
	IF erro_sql = FALSE THEN
		COMMIT;
		SELECT 'Transação efetivada com sucesso.' AS Resultado;
	ELSE
		ROLLBACK;
		SELECT 'Erro na transação' AS Resultado;
	END IF;
END $$
DELIMITER ;

CALL InsertVaccineIfNotExistsProcedure("v5", "Fabricante 1", "2020-05-12T16:30:00", 1);
CALL InsertVaccineIfNotExistsProcedure("v5", "Fabricante 1", "2021-05-12T15:35:00", 2);
CALL InsertVaccineIfNotExistsProcedure("v5", "Fabricante 1", "2022-05-12T06:28:00", 3);
CALL InsertVaccineIfNotExistsProcedure("v4", "Fabricante 2", "2020-05-12T14:30:00", 1);
CALL InsertVaccineIfNotExistsProcedure("v3", "Fabricante 3", "2020-05-12T17:30:00", 1);