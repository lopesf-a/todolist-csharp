-- Exo 1 : Débutant: Création de table, connexion à une base de données, et requêtes basiques
CREATE DATABASE IF NOT EXIST exo1
CREATE TABLE Livres (
    id  int NOT NULL,
    titre  varchar(255),
    auteur  varchar(255),
    genre  varchar(255),
    annee_publication int,
    PRIMARY KEY (id)
); 

CREATE TABLE Emprunts (
    id  int NOT NULL,
    date_emprunt  DATE,
    date_retour DATE,
    id_livre  int,
    PRIMARY KEY (id),
    FOREIGN KEY (id_livre) REFERENCES Livres(id)
); 

INSERT INTO Livres (id, titre, auteur, genre, annee_publication) VALUES
(1, 'Le Seigneur des Anneaux', 'J.R.R. Tolkien', 'Fantasy', 1954),
(2, 'Harry Potter et la Pierre Philosophale', 'J.K. Rowling', 'Fantasy', 1997),
(3, '1984', 'George Orwell', 'Science-Fiction', 2000),
(4, 'Le Petit Prince', 'Antoine de Saint-Exupéry', 'Fiction', 2023),
(5, 'Don Quichotte', 'Miguel de Cervantes', 'Aventure', 2024);

INSERT INTO Emprunts (id, date_emprunt, date_retour, id_livre) VALUES
(1, '2023-10-01', '2023-10-15', 1),
(2, '2023-10-05', '2023-10-20', 2),
(3, '2022-10-10', '2022-10-25', 3),
(4, '2023-10-15', '2023-10-30', 4),
(5, '2018-10-20', '2019-11-04', 5);


SELECT L.titre, L.annee_publication
FROM Livres L
WHERE L.annee_publication> 2020;

-- Exo 2 : Intermédiaire: Jointures et statistiques

-- 1. Écrivez une requête pour afficher tous les livres empruntés en 2022.
SELECT L.titre, E.date_emprunt
FROM Livres L
INNER JOIN Emprunts E ON L.id = E.id_livre
WHERE YEAR(E.date_emprunt) = 2022;
-- 2. Écrivez une requête pour afficher le nombre total de livres empruntés.
SELECT COUNT(*) AS nombre_total_de_livres_empruntes
FROM Emprunts;
-- 3. Écrivez une requête pour afficher combien de fois chaque genre de livre a été emprunté.
SELECT L.genre, COUNT(*) AS nombre_emprunts
FROM Livres L
JOIN Emrpunts E ON L.id = E.id_livre
GROUP BY L.genre;