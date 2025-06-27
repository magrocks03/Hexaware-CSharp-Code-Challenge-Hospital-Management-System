CREATE DATABASE HospitalDB;
GO

USE HospitalDB;
GO

CREATE TABLE Patient (
    PatientId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    ContactNumber NVARCHAR(15) NOT NULL,
    Address NVARCHAR(200)
);

CREATE TABLE Doctor (
    DoctorId INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL,
    ContactNumber NVARCHAR(15) NOT NULL
);

CREATE TABLE Appointment (
    AppointmentId INT PRIMARY KEY IDENTITY(1,1),
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Description NVARCHAR(500),

    FOREIGN KEY (PatientId) REFERENCES Patient(PatientId),
    FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId)
);

SELECT * FROM Patient;
SELECT * FROM Doctor;
SELECT * FROM Appointment;

INSERT INTO Patient (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address) VALUES
('John', 'Doe', '1985-03-15', 'Male', '9876543210', '123 Main St'),
('Jane', 'Smith', '1990-07-22', 'Female', '9123456789', '456 Oak Ave'),
('Alice', 'Johnson', '1975-11-30', 'Female', '9988776655', '789 Pine Rd'),
('Bob', 'Williams', '1982-01-10', 'Male', '9911223344', '321 Birch Blvd'),
('Emma', 'Brown', '1995-06-18', 'Female', '9877891234', '654 Cedar Ln');

INSERT INTO Doctor (FirstName, LastName, Specialization, ContactNumber) VALUES
('Dr. Sarah', 'Miller', 'Cardiology', '9001122334'),
('Dr. James', 'Wilson', 'Neurology', '9011223344'),
('Dr. Emily', 'Clark', 'Pediatrics', '9021334455'),
('Dr. Robert', 'Taylor', 'Orthopedics', '9031445566'),
('Dr. Olivia', 'Anderson', 'Dermatology', '9041556677');

INSERT INTO Appointment (PatientId, DoctorId, AppointmentDate, Description) VALUES
(1, 1, '2025-07-01 10:30:00', 'Routine heart checkup'),
(2, 2, '2025-07-02 11:00:00', 'Migraine consultation'),
(3, 3, '2025-07-03 09:45:00', 'Child vaccination'),
(4, 4, '2025-07-04 14:00:00', 'Knee pain review'),
(5, 5, '2025-07-05 15:30:00', 'Skin rash diagnosis');



