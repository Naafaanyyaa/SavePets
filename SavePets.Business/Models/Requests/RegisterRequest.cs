﻿using System.ComponentModel.DataAnnotations;
using SavePets.Business.Models.Enums;

namespace SavePets.Business.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Firstname { set; get; } = string.Empty;
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Lastname { set; get; } = string.Empty;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { set; get; } = string.Empty;
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string Password { set; get; } = string.Empty;
        public RoleEnum Role { set; get; }
    }
}