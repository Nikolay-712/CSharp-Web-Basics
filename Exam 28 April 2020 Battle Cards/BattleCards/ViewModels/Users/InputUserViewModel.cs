using System.ComponentModel.DataAnnotations;

namespace BattleCards.ViewModels.Users
{
    public class InputUserViewModel
    {
        [Required]
        [MaxLength(20), MinLength(5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20), MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20), MinLength(6)]
        public string ConfirmPassword { get; set; }

    }
}
