﻿namespace SaleManagerWebAPI.Models.MyResponse
{
    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
