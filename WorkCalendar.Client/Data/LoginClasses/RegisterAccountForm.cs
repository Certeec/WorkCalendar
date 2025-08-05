using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace WorkCalendar.Client.Data.LoginClasses
{
    public class RegisterAccountForm
    {
        [Required]
        [StringLength(16, ErrorMessage = "Name length can't be more than 16.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
        public string Password { get; set; }


        [Required]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }

    }
}
