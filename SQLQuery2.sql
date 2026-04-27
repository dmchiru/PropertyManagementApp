IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MaintenanceProjects')
BEGIN
CREATE TABLE MaintenanceProjects (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100),
    Description NVARCHAR(255),
    Status NVARCHAR(20) DEFAULT 'Open',
    EstimatedCost DECIMAL(10,2),
    ActualCost DECIMAL(10,2),
    CreatedDate DATETIME DEFAULT GETDATE(),
    PropertyID INT NULL
)
END