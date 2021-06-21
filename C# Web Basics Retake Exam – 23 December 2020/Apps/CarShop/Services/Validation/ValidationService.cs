namespace CarShop.Services.Validation
{
    using CarShop.Data;
    using CarShop.Services.PasswordEncoding;
    using CarShop.ViewModels.Cars;
    using CarShop.ViewModels.Issues;
    using CarShop.ViewModels.Users;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
    using static ValidationConstants;

    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordEncoder passwordEncoder;
        private readonly EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();
        private readonly string[] supportedRoles = new string[] { "mechanic", "client" };
        public ValidationService(ApplicationDbContext dbContext, IPasswordEncoder passwordEncoder)
        {
            this.dbContext = dbContext;
            this.passwordEncoder = passwordEncoder;
        }

        public bool IsValid { get; set; }
        public string Message { get; set; }

        public string ValidateRegisterData(InputRegisterViewModel input)
        {
            this.IsValid = true;

            var existingNames = this.dbContext.Users.Select(x => x.Username).ToList();
            var existingEmails = this.dbContext.Users.Select(x => x.Email).ToList();

            var usernameLenght = input.Username.Length;
            var passwordLenght = input.Password.Length;

            if (existingEmails.Any(x => x == input.Email))
            {
                this.IsValid = false;
                return this.Message = $"Email {input.Email} is already registered";
            }

            if (existingNames.Any(x => x == input.Username))
            {
                this.IsValid = false;
                return this.Message = $"Username {input.Username} is already registered";
            }

            if (!emailAddressAttribute.IsValid(input.Email))
            {
                this.IsValid = false;
                return this.Message = "Invalid email address";
            }

            if (usernameLenght < UsernameMinLenght || usernameLenght > UsernameMaxLenght)
            {
                this.IsValid = false;
                return this.Message = $"Username must be between {UsernameMinLenght} and {UsernameMaxLenght} characters";
            }

            if (passwordLenght < PasswordMinLenght || passwordLenght > PasswordMaxLenght)
            {
                this.IsValid = false;
                return this.Message = $"The password must be between {PasswordMinLenght} and {PasswordMaxLenght} characters";
            }

            if (input.Password != input.ConfirmPassword)
            {
                this.IsValid = false;
                return this.Message = "Password does not match";
            }

            if (!this.supportedRoles.Contains(input.UserType.ToLower()))
            {
                this.IsValid = false;
                return this.Message = "This role is not supported by the system";
            }

            return this.Message;

        }

        public string ValidateLoginData(InputLoginViewModel input)
        {
            this.IsValid = true;

            var user = this.dbContext.Users.Where(x => x.Username == input.Username &&
            x.Password == this.passwordEncoder.ComputeHash(input.Password)).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(input.Username) || string.IsNullOrWhiteSpace(input.Password))
            {
                this.IsValid = false;
                return this.Message = "Invalid username or password";
            }

            if (user == null)
            {
                this.IsValid = false;
                return this.Message = "Invalid username or password";
            }


            return this.Message;
        }

        public string ValdateCarData(InputCarViewModel input, string userId)
        {
            this.IsValid = true;

            var validPlateNumber = "^[A-Z]{2}[0-9]{4}[A-Z]{2}$";
            var currentUser = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            var carModelLenght = input.Model.Length;

            if (currentUser.IsMechanic)
            {
                this.IsValid = false;
                return this.Message = "Access denied";
            }

            if (carModelLenght < CarModelMinLenght || carModelLenght > CarModelMaxLenght)
            {
                this.IsValid = false;
                return this.Message = $"Model must be between {CarModelMinLenght} and {CarModelMaxLenght} characters";
            }

            if (input.Year < YearMinValue || input.Year > YearMaxValue)
            {
                this.IsValid = false;
                return this.Message = $"car year must be in range ({YearMinValue} - {YearMaxValue}) ";
            }

            if (!Uri.IsWellFormedUriString(input.Image, UriKind.Absolute))
            {
                this.IsValid = false;
                return this.Message = "This URL is not valid";
            }

            if (!Regex.IsMatch(input.PlateNumber, validPlateNumber))
            {
                this.IsValid = false;
                return this.Message = "Invalid plate number";
            }

            return this.Message;
        }

        public string ValidateIssueData(InputIssueViewModel input)
        {
            this.IsValid = true;

            if (input.Description.Length < IssueDescriptionMinLenght)
            {
                this.IsValid = false;
                return this.Message = $"Must be more than {IssueDescriptionMinLenght} characters";
            }

            if (input.CarId == null)
            {
                this.IsValid = false;
                return this.Message = "There is no such car";
            }

            return this.Message;
        }

        public string UserRole(string userId)
        {
            this.IsValid = true;

            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);

            if (!user.IsMechanic)
            {
                this.IsValid = false;
                return this.Message = "Access denied";
            }

            return this.Message;
        }

    }
}
