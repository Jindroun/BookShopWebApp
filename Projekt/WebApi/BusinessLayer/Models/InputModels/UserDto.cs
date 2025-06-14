﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.InputModels;
public class UserDto
{

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
    public string LastName { get; set; }

    public AddressDto Address { get; set; }

}
