using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class LoginViewModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(8, ErrorMessage = "Identifier too long (8 character limit).")]
    public string Password { get; set; }
}