CREATE DATABASE [JobsBookingAppDb]

GO
USE [JobsBookingAppDb]
GO

CREATE TABLE [Employees]
(
[EmployeeId] INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(120) NOT NULL,
[Email] VARCHAR(100) UNIQUE NOT NULL,
[Username] VARCHAR(40) UNIQUE NOT NULL,
[Password] VARCHAR(256) NOT NULL
)

CREATE TABLE [Workplaces]
(
[WorkplaceId] INT PRIMARY KEY IDENTITY,
[Floor] INT NOT NULL,
[Zone] VARCHAR(50) NOT NULL,
[HasMonitor] BIT NOT NULL,
[HasDock] BIT NOT NULL,
[IsNearWindow] BIT NOT NULL,
[IsNearPrinter] BIT NOT NULL,
[IsAvailable] BIT NOT NULL
)

CREATE TABLE [Reservations]
(
[ReservationId] INT PRIMARY KEY IDENTITY,
[EmployeeId] INT FOREIGN KEY REFERENCES [Employees]([EmployeeId]) NOT NULL,
[StartTime] DATE NOT NULL,
[EndTime] DATE NOT NULL,
[WorkplaceId] INT FOREIGN KEY REFERENCES [Workplaces]([WorkplaceId]) NOT NULL,
[CreatedAt] DATETIME NOT NULL
)

CREATE TABLE [EmployeeFavorites]
(
    [EmployeeId] INT NOT NULL,
    [WorkplaceId] INT NOT NULL,
    PRIMARY KEY ([EmployeeId], [WorkplaceId]),
    FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([EmployeeId]),
    FOREIGN KEY ([WorkplaceId]) REFERENCES [Workplaces]([WorkplaceId])
)

INSERT INTO [Employees] ([Name], [Email], [Username], [Password]) VALUES
('Alice Dimitrova', 'alice.dimitrova@example.com', 'alice.dimitrova', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Boris Ivanov', 'boris.ivanov@example.com', 'boris.ivanov', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Clara Stoyanova', 'clara.stoyanova@example.com', 'clara.stoyanova', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Dimitar Georgiev', 'dimitar.georgiev@example.com', 'dimitar.georgiev', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Elena Petrova', 'elena.petrova@example.com', 'elena.petrova', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('John Smith', 'john.smith@example.com', 'john.smith', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Emily Johnson', 'emily.johnson@example.com', 'emily.johnson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Michael Brown', 'michael.brown@example.com', 'michael.brown', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Sarah Davis', 'sarah.davis@example.com', 'sarah.davis', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('David Wilson', 'david.wilson@example.com', 'david.wilson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Laura Moore', 'laura.moore@example.com', 'laura.moore', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('James Taylor', 'james.taylor@example.com', 'james.taylor', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Olivia Anderson', 'olivia.anderson@example.com', 'olivia.anderson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Daniel Thomas', 'daniel.thomas@example.com', 'daniel.thomas', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Grace Jackson', 'grace.jackson@example.com', 'grace.jackson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Matthew White', 'matthew.white@example.com', 'matthew.white', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Sophie Harris', 'sophie.harris@example.com', 'sophie.harris', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Andrew Martin', 'andrew.martin@example.com', 'andrew.martin', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Chloe Thompson', 'chloe.thompson@example.com', 'chloe.thompson', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA'),
('Benjamin Lee', 'benjamin.lee@example.com', 'benjamin.lee', 'A109E36947AD56DE1DCA1CC49F0EF8AC9AD9A7B1AA0DF41FB3C4CB73C1FF01EA')

INSERT INTO [Workplaces] ([Floor], [Zone], [HasMonitor], [HasDock], [IsNearWindow], [IsNearPrinter], [IsAvailable]) VALUES
(1, 'A', 1, 1, 1, 0, 1),
(1, 'A', 1, 0, 0, 1, 1),
(1, 'B', 0, 1, 1, 1, 1),
(2, 'B', 1, 1, 0, 0, 1),
(2, 'C', 0, 0, 1, 0, 1),
(2, 'C', 1, 0, 1, 1, 1),
(3, 'D', 1, 1, 1, 1, 1),
(3, 'D', 0, 1, 0, 1, 1),
(3, 'E', 1, 0, 0, 0, 1),
(4, 'E', 1, 1, 1, 0, 1),
(4, 'F', 0, 0, 0, 1, 1),
(4, 'F', 1, 1, 0, 0, 1),
(5, 'G', 1, 0, 1, 1, 1),
(5, 'G', 0, 1, 0, 1, 1),
(5, 'H', 1, 1, 1, 1, 1);