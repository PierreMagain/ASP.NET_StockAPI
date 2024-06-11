CREATE TABLE [dbo].[User_]
(
    [Id] INT PRIMARY KEY IDENTITY,
    [Username] VARCHAR(50) NOT NULL UNIQUE,
    [Email] VARCHAR(150) NOT NULL UNIQUE,
    [Password] VARCHAR(255) NOT NULL,
    [Role] VARCHAR(50) NOT NULL CHECK (Role IN ('User', 'Responsable', 'Admin')),
    CONSTRAINT CK_User_Username_Email CHECK 
        (TRIM(Username) != '' AND TRIM(Email) != '')
);
