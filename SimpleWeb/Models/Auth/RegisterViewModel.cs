﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWeb.Models.Auth
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 4)]
        public string DisplayName { get; set; }

        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character @$!%*?&")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Confirm Password"), Compare(nameof(ConfirmPassword))]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
    }
}
