﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class SignUpUser
{
    [Required]
    [NotNull]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Compare("ConfirmPassword")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "At least 8 symbols", MinimumLength = 8)]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
}
