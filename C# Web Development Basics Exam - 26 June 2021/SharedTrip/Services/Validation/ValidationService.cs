namespace SharedTrip.Services.Validation
{
    using SharedTrip.Data;
    using SharedTrip.Services.Encoder;
    using SharedTrip.ViewModels;
    using SharedTrip.ViewModels.Trips;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;

    class ValidationService : IValidationService
    {
        private const int UsernameMinLenght = 5;
        private const int UsernameMaxLenght = 20;

        private const int PasswordMinLenght = 6;
        private const int PasswordMaxLenght = 20;


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


        public string ValideateTripInputData(InputTripViewModel input)
        {
            this.IsValid = true;

            if (string.IsNullOrEmpty(input.StartPoint) || string.IsNullOrEmpty(input.EndPoint))
            {
                this.IsValid = false;
                return this.Message = $"Fields are required {nameof(input.StartPoint)} - {nameof(input.EndPoint)}";
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                this.IsValid = false;
                return this.Message ="Seats should be between 2 and 6";
            }

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 80)
            {
                this.IsValid = false;
                return this.Message = "Description is required ";
            }

            if (!DateTime.TryParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                this.IsValid = false;
                return this.Message = "Invalid format";
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
