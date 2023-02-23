#MySQL

USE cat_adoption_db;

DROP PROCEDURE IF EXISTS InsertCatIfNotExistsProcedure;

DELIMITER $$
CREATE PROCEDURE InsertCatIfNotExistsProcedure (IN NameParam VARCHAR(30), BreedParam VARCHAR(30), WeightParam DOUBLE, ColorParam VARCHAR(30), AgeParam INT, GenderParam CHAR(1))
BEGIN
DECLARE erro_sql TINYINT DEFAULT FALSE;
DECLARE CONTINUE HANDLER FOR SQLEXCEPTION SET erro_sql = TRUE;
START TRANSACTION;
	SET @hasCat = (SELECT count(*) FROM cats WHERE (
		Name = NameParam AND 
        Breed = BreedParam AND 
        Weight = WeightParam AND
        Color = ColorParam AND
        Age = AgeParam AND
        Gender = GenderParam));
    
    IF (@hasCat != 0) THEN
		SET @message = concat("O gatinho ", NameParam, ", com esses atributos, já existe");
		SELECT @message;
	ELSE
		INSERT INTO cats (Name, Breed, Weight, Color, Age, Gender)
		VALUES(NameParam, BreedParam, WeightParam, ColorParam, AgeParam, GenderParam);
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

CALL InsertCatIfNotExistsProcedure("Pingo", "Viralata", 2.5, "Preto", 2, 'M');
CALL InsertCatIfNotExistsProcedure("Josefina", "Viralata", 2, "Branco", 1, 'F');
CALL InsertCatIfNotExistsProcedure("Carlito", "Viralata", 3, "Marrom", 5, 'M');
CALL InsertCatIfNotExistsProcedure("Filomena", "Viralata", 2.6, "Mesclado (branco e preto)", 4, 'F');
CALL InsertCatIfNotExistsProcedure("Polenta", "Viralata", 2.1, "Marrom", 1, 'F');