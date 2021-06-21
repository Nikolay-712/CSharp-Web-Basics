namespace CarShop.Data.Models
{
    using System;

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

        public bool IsMechanic { get; set; }
    }
}
