namespace BattleCards.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<UserCard> UserCards{ get; set; }
    }
}
