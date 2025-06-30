USE MiniBank;

-- Insert Users
INSERT INTO Users (Name) VALUES 
('Noa'),     -- ID 1
('Alona'),   -- ID 2
('Sagi'),    -- ID 3
('Or'),      -- ID 4
('Romi'),    -- ID 5
('Tahel'),   -- ID 6
('Yishai'),  -- ID 7
('Inbar');   -- ID 8

-- Insert Accounts
INSERT INTO Accounts (Balance, Type) VALUES 
(1000.00, 'Simple'),   -- Account ID 1
(5000.00, 'VIP'),      -- Account ID 2
(200.00,  'Simple'),   -- Account ID 3
(3000.00, 'VIP'),      -- Account ID 4
(150.00,  'Simple'),   -- Account ID 5
(4200.00, 'VIP');      -- Account ID 6

-- Associate Users with Accounts
INSERT INTO UserAccounts (UserId, AccountId) VALUES 
(1, 1),  -- Noa -> Simple
(1, 2),  -- Noa -> VIP (shared)
(2, 3),  -- Alona -> Simple
(2, 2),  -- Alona -> VIP (shared)
(3, 4),  -- Sagi -> VIP
(4, 5),  -- Or -> Simple
(5, 6),  -- Romi -> VIP
(6, 5),  -- Tahel -> Simple (shared with Or)
(7, 6),  -- Yishai -> VIP (shared with Romi)
(8, 1);  -- Inbar -> Simple (shared with Noa)
