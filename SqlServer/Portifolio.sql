CREATE DATABASE PORTIFOLIO

USE PORTIFOLIO

CREATE TABLE Pessoa(
	id INTEGER PRIMARY KEY IDENTITY,
	nome VARCHAR(200) NOT NULL,
	cpf VARCHAR(14) UNIQUE NOT NULL,
	email VARCHAR(200)
)

CREATE TABLE Usuario(
	id INTEGER IDENTITY,
	id_pessoa INTEGER,
	usuario VARCHAR(50) NOT NULL,
	senha VARCHAR(50) NOT NULL,
	dica_senha VARCHAR(200),
	token VARCHAR(MAX) UNIQUE,
	token_page VARCHAR(100) UNIQUE,
	FOREIGN KEY (id_pessoa) REFERENCES Pessoa (id)
)

CREATE TABLE Projeto(
	id INTEGER IDENTITY,
	id_usuario INTEGER,
	nome VARCHAR(50) NOT NULL,
	descricao VARCHAR(500) NOT NULL,
	url VARCHAR(MAX) NOT NULL,
	img_url VARCHAR(MAX) NOT NULL
	FOREIGN KEY (id_usuario) REFERENCES Usuario (id)
)

--INSERT INTO Pessoa VALUES ('pessoa admin','111.111.111-11','endereco admin')
--DELETE FROM Pessoa WHERE  id=16
--DELETE FROM Usuario WHERE id=8
--INSERT INTO Usuario VALUES (1,'usadmin','Bueno@12598','Senha do usuario admin')
SELECT * FROM Pessoa
SELECT * FROM Usuario
SELECT * FROM Projeto

--UPDATE Pessoa SET cpf='449.671.218-02', email='gabrielbs98@hotmail.com'

--UPDATE Pessoa SET email='teste@teste.com' WHERE id=17

--UPDATE Usuario SET token_page='lihgbjDSJHHhjHJHBnbs3456Sbjk' WHERE id = 1

SELECT P.* FROM Projeto P INNER JOIN Usuario U ON U.token = 'gMEwqd8d2Ms*zDjI[s4]jgv*0yqtk7Jf0l9AOcjYQQLAvDjQ]GUyHSqDw!!f*tT5e](30ove8v2qBZ!vKEOtf!6ej1AUj|FUSkw8khjmtov!Qvfu(*A*YzoeJuwBB8aIlL!(XOmNiB]FBvCKk!p2Ib' AND U.id = P.id_usuario
