﻿namespace Mago.Services.AuthAPI.Models.Dto
{
    public class RegisterRoleRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
