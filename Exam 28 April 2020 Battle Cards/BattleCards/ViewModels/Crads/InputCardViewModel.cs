namespace BattleCards.ViewModels.Crads
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InputCardViewModel
    {
        [Required]
        [MaxLength(15), MinLength(5)]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Keyword { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Attack { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Health { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
