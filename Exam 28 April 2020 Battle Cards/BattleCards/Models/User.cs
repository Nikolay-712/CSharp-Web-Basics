namespace BattleCards.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User(string username, string email, string password)
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserCards = new HashSet<UserCard>();
            this.Username = username;
            this.Email = email;
            this.Password = password;
        }

        public string Id { get; private set; }

        [MaxLength(20), MinLength(5)]
        public string Username { get; private set; }

        public string Email { get; private set; }
        
        public string Password { get; private set; }

        public ICollection<UserCard> UserCards { get; private set; }
    }
}
