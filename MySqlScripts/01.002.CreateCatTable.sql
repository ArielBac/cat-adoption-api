#MySQL

USE cat_adoption_db;
    
CREATE TABLE IF NOT EXISTS cats(
	Id INT PRIMARY KEY AUTO_INCREMENT,
	Name VARCHAR(30) NOT NULL, 
	Breed VARCHAR(30),
	Weight DOUBLE NOT NULL,
	Color VARCHAR(30) NOT NULL,
	Age INT NOT NULL,
	Gender CHAR NOT NULL
);