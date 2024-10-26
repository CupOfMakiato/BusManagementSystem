CREATE TABLE RouteTicket (
    RouteTicketId INT PRIMARY KEY IDENTITY,
    RouteId INT NOT NULL,
    TicketId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    ModifiedAt DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (RouteId) REFERENCES Route(RouteId),
    FOREIGN KEY (TicketId) REFERENCES Ticket(TicketId)
);
