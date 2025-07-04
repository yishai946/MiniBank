DROP DATABASE IF EXISTS minibank;
CREATE DATABASE minibank;

USE minibank;

DROP TABLE IF EXISTS Users;

CREATE TABLE Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

DROP TABLE IF EXISTS Accounts;

CREATE TABLE Accounts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Balance DECIMAL(10, 2) NOT NULL,
    Type ENUM('Simple', 'VIP') NOT NULL
);

DROP TABLE IF EXISTS UserAccounts;

CREATE TABLE UserAccounts (
    UserId INT,
    AccountId INT,
    PRIMARY KEY (UserId, AccountId),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (AccountId) REFERENCES Accounts(Id) ON DELETE CASCADE
);
