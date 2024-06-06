-- Insertion des catégories
INSERT INTO Categories (Name) VALUES 
    ('Électronique'),
    ('Vêtements'),
    ('Alimentation'),
    ('Sport'),
    ('Maison'),
    ('Jouets');

-- Insertion des produits
INSERT INTO Product (Name, Brand, Description, ExpiryDate, PriceExcludingTax, VAT, QuantityInStock, QuantityInShelf)
VALUES 
    ('Smartphone', 'Samsung', 'Modèle Galaxy S20', '2025-12-31', 800.00, 0.21, 50, 20),
    ('T-shirt', 'Nike', 'Taille M, couleur noir', NULL, 25.00, 0.21, 100, 50),
    ('Pain', NULL, 'Pain de blé entier', '2024-06-30', 2.00, 0.06, 200, NULL),
    ('Soccer Ball', 'Adidas', 'Official FIFA ball', NULL, 30.00, 0.21, 50, NULL),
    ('TV', 'Sony', '55" Smart TV', '2026-01-01', 1200.00, 0.21, 30, 10),
    ('Jeans', 'Jack&Jones', 'Taille 32, couleur bleu', NULL, 50.00, 0.21, 80, 40),
    ('Baguette', NULL, 'French baguette', '2024-06-30', 1.50, 0.06, 150, NULL),
    ('Basketball', 'Nike', 'Official NBA ball', NULL, 40.00, 0.21, 40, NULL),
    ('Laptop', 'Apple', 'MacBook Pro', '2025-12-31', 1500.00, 0.21, 20, 5),
    ('Dress', 'Zara', 'Size M, color red', NULL, 60.00, 0.21, 70, 30);

-- Insertion des relations entre produits et catégories
INSERT INTO ProductCategories (ProductId, CategoryId) VALUES
    (1, 1),  -- Smartphone -> Électronique
    (2, 2),  -- T-shirt -> Vêtements
    (3, 3),  -- Pain -> Alimentation
    (4, 4),  -- Soccer Ball -> Sport
    (5, 1),  -- TV -> Électronique
    (6, 2),  -- Jeans -> Vêtements
    (7, 3),  -- Baguette -> Alimentation
    (8, 4),  -- Basketball -> Sport
    (9, 1),  -- Laptop -> Électronique
    (10, 2); -- Dress -> Vêtements
