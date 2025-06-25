-- Insert Users
INSERT INTO Users (Name) VALUES 
('Alice'),
('Bob'),
('Charlie');

-- Insert Accounts
INSERT INTO Accounts (Balance, Type) VALUES 
(1000.00, 'Simple'),   -- Account ID 1
(5000.00, 'VIP'),      -- Account ID 2 (Joined accounIdt)
(200.00, 'Simple'),    -- Account ID 3
(3000.00, 'VIP');      -- Account ID 4

-- Associate users with accounts
INSERT INTO UserAccounts (UserId, AccountId) VALUES 
(1, 1),  -- Alice -> Simple
(1, 2),  -- Alice -> VIP (Joined account)
(2, 3),  -- Bob -> Simple
(3, 4),  -- Charlie -> VIP
(2, 2);  -- Bob -> VIP (Joined account)
