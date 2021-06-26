namespace SharedTrip.Services.Users
{
    using SharedTrip.Data;
    using SharedTrip.Models;
    using SharedTrip.Services.Encoder;
    using SharedTrip.ViewModels;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IPasswordEncoder passwordEncoder;

        public UserService(ApplicationDbContext applicationDbContext, IPasswordEncoder passwordEncoder)
        {
            this.applicationDbContext = applicationDbContext;
            this.passwordEncoder = passwordEncoder;
        }

        public string Login(InputLoginViewModel input)
        {
            var user = this.applicationDbContext.Users.ToList()
                .FirstOrDefault(x => x.Username == input.Username &&  x.Password == passwordEncoder.ComputeHash(input.Password) );


            return user.Id;
        }

        public void Register(InputRegisterViewModel input)
        {
            var user = new User
            {
                Email = input.Email,
                Username = input.Username,
                Password = this.passwordEncoder
                .ComputeHash(input.Password),
            };

            this.applicationDbContext.Users.Add(user);
            this.applicationDbContext.SaveChanges();
        }
    }
}
