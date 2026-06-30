using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace NewPortfolio.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required,Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
