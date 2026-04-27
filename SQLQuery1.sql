IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = N'Status'
    AND Object_ID = Object_ID(N'Invoices')
)
BEGIN
    ALTER TABLE Invoices
    ADD Status NVARCHAR(20) NOT NULL DEFAULT 'Draft';
END