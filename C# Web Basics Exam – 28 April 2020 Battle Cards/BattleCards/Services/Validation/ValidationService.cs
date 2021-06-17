namespace BattleCards.Services.Validation
{
    using BattleCards.Data;
    using BattleCards.Services.PasswordEncoding;
    using BattleCards.ViewModels.Cards;
    using BattleCards.ViewModels.Users;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ValidationService : IValidationService
    {
        private const int UsernameMinLenght = 5;
        private const int UsernameMaxLenght = 20;

        private const int PasswordMinLenght = 6;
        private const int PasswordMaxLenght = 20;

        private const int CardNameMinLenght = 5;
        private const int CardNameMaxLenght = 15;

        private const int DescriptionMinLenght = 1;
        private const int DescriptionMaxLenght = 200;


        private readonly ApplicationDbContext applicationDbContext;
        private readonly IPasswordEncoder passwordEncoder;
        private readonly EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();

        public ValidationService(ApplicationDbContext applicationDbContext, IPasswordEncoder passwordEncoder)
        {
            this.applicationDbContext = applicationDbContext;
            this.passwordEncoder = passwordEncoder;
        }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public string ValidateRegisterData(InputRegisterViewModel input)
        {
            this.IsValid = true;

            var existingUsernames = this.applicationDbContext.Users.ToList().Select(x => x.Username).ToList();
            var existingEmails = this.applicationDbContext.Users.ToList().Select(x => x.Email).ToList();

            var username = input.Username;
            var email = input.Email;
            var password = input.Password;
            var confirmPassword = input.ConfirmPassword;

            if (!IsValidLenght(username, UsernameMinLenght, UsernameMaxLenght))
            {
                this.IsValid = false;
                return this.Message = $"Username must be between {UsernameMinLenght} and {UsernameMaxLenght} characters!";
            }

            if (existingUsernames.Any(x => x == username))
            {
                this.IsValid = false;
                return this.Message = "The username is already in use!";
            }

            if (!emailAddressAttribute.IsValid(email))
            {
                this.IsValid = false;
                return this.Message = "Invalid user email!";
            }

            if (existingEmails.Any(x => x == input.Email))
            {
                this.IsValid = false;
                return this.Message = "The email is already in use!";

            }

            if (!IsValidLenght(password, PasswordMinLenght, PasswordMaxLenght))
            {
                this.IsValid = false;
                return this.Message = $"Password must be between {PasswordMinLenght} and {PasswordMaxLenght} characters!";
            }

            if (password != confirmPassword)
            {
                this.IsValid = false;
                return this.Message = "The password does not match the confirmation!";
            }


            return this.Message;
        }

        public string ValidateLoginData(InputLoginViewModel input)
        {
            this.IsValid = true;

            var username = input.Username;
            var password = input.Password;

            var user = this.applicationDbContext.Users.ToList().FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                this.IsValid = false;
                return this.Message = "Wrong username or password!";
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                this.IsValid = false;
                return this.Message = "Fields are required!";
            }

            if (user.Password != this.passwordEncoder.ComputeHash(password))
            {
                this.IsValid = false;
                return this.Message = "Wrong username or password!";
            }

            return this.Message;
        }

        public string ValidateCardData(InputCardViewModel input)
        {
            this.IsValid = true;

            var name = input.Name;
            var imageUrl = input.Image;
            var keyword = input.Keyword;
            var attack = input.Attack;
            var health = input.Health;
            var description = input.Description;

            if (IsValidLenght(name, CardNameMaxLenght, CardNameMaxLenght))
            {
                this.IsValid = false;
                return this.Message = $"The name must be between {CardNameMinLenght} and {CardNameMaxLenght} character!";
            }

            //if (string.IsNullOrEmpty(imageUrl) || Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            //{
            //    this.IsValid = false;
            //    return this.Message = "Invalid URL!";
            //}

            if (string.IsNullOrEmpty(keyword))
            {
                this.IsValid = false;
                return this.Message = "Keyword is required!";
            }

            if (attack < 1 || health < 1)
            {
                this.IsValid = false;
                return this.Message = "The value cannot be used negatively!";
            }

            if (IsValidLenght(description, DescriptionMinLenght, DescriptionMaxLenght))
            {
                this.IsValid = false;
                return this.Message = $"The description must be between {DescriptionMinLenght} and {DescriptionMaxLenght} character!";
            }

            return this.Message;
        }

        public string CheckCurrentCard(int cardId, string userId)
        {
            this.IsValid = true;

            var card = this.applicationDbContext.Cards.ToList().FirstOrDefault(x => x.Id == cardId);
            var userCards = this.applicationDbContext.UserCards.ToList().Where(x => x.UserId == userId);

            if (userId == null)
            {
                this.IsValid = false;
                return this.Message = "Unauthorized user!";
            }

            if (userCards.Any(x => x.Card.Name == card.Name))
            {
                this.applicationDbContext.Cards.Remove(card);
                this.applicationDbContext.SaveChanges();

                this.IsValid = false;
                return this.Message = "Card with this name has already been added!";
            }

            return this.Message;
        }

        private bool IsValidLenght(string parameter, int minValue, int maxValue)
        {
            if (string.IsNullOrEmpty(parameter) || parameter.Length < minValue || parameter.Length > maxValue)
            {
                return false;
            }

            return true;
        }
    }


}
