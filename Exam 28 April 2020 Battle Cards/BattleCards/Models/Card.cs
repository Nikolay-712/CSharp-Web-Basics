namespace BattleCards.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Card
    {
        public Card(string name, string imageUrl, string keyword, int attack, int health, string description)
        {
            this.UserCards = new HashSet<UserCard>();
            this.Name = name;
            this.ImageUrl = imageUrl;
            this.Keyword = keyword;
            this.Attack = attack;
            this.Health = health;
            this.Description = description;
        }

        public int Id { get; private set; }

        [MaxLength(15), MinLength(5)]
        public string Name { get; private set; }

        public string ImageUrl { get; private set; }

        public string Keyword { get; private set; }

        [Range(1, int.MaxValue)]
        public int Attack { get; private set; }

        [Range(1, int.MaxValue)]
        public int Health { get; private set; }

        public string Description { get; private set; }

        public ICollection<UserCard> UserCards { get; private set; }

    }
}
