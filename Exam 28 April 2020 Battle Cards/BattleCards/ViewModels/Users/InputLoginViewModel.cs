namespace BattleCards.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class InputLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
