﻿namespace JobsBookingApp.Services.DTOs.Authentication
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public int? EmployeeId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Username { get; set; }
    }
}
