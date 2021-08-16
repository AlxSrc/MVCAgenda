﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCAgenda.Core.AccountModels
{
    public class LoginViewModel
    {
        [DisplayName("E-mail")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Parola")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
