﻿namespace CleanArch.API.DTOs
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}