CREATE TABLE IF NOT EXISTS Employees (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Surname VARCHAR(255) NOT NULL,
    Phone VARCHAR(20),
    CompanyId INT NOT NULL,
    DepartmentId INT NOT NULL,
    PassportId INT,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    FOREIGN KEY (PassportId) REFERENCES Passports(Id)
);
CREATE INDEX CompanyIdIndex ON "Employees" ("CompanyId");