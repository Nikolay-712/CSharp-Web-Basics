namespace Git.Services.Validation
{
    using Git.Data;
    using Git.Models;
    using Git.PasswordEncoding;
    using Git.ViewModels.Comits;
    using Git.ViewModels.Repositories;
    using Git.ViewModels.Users;
    using System.Linq;

    public class ModelValidation : IModelValidation
    {
        private const int UsernameMinLenght = 5;
        private const int UsernameMaxLenght = 20;

        private const int PasswordMinLenght = 6;
        private const int PasswordMaxLenght = 20;

        private const int RepositoryNameMinLenght = 3;
        private const int RepositoryNameMaxLenght = 10;
        private string[] RepsitoryTypes = new string[] { "private", "public" };

        private const int CommitDescriptionMinLenght = 5;

        private const string ExistEmail = "This email is already registered!";
        private const string ExistUsername = "This name is already registered!";
        private const string InvalidUsernamelenght = "The name must be between {0} and {1} characters!";

        private const string InvalidEmail = "Email is required!";

        private const string InvalidPassword = "Password is required nad must be between {0} and {1} characters!";
        private const string MismatchedPasswords = "Password does not match!";

        private const string InvalidLoginData = "Field {0} is required!";
        private const string InvalidLogin = "Wrong username or password!";

        private const string InvalidRepoName = "The name must be between {0} and {1} characters!";
        private const string InvalidRepoType = "The type must be {0} or {1} !";
        private const string ExsistingRepoName = "The name already exists!";

        private const string InvalidCommitDescription = "Must be greater than {0} characters!";
        private const string AccessDenied = "You cannot delete this entry!";

        private readonly ApplicationDbContext applicationDbContext;

        public ModelValidation(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public string Message { get; set; }

        public bool IsValid { get; set; }

        public string ValidateRegisterData(InputRegisterViewModel input)
        {
            this.IsValid = true;

            var registeredUsernames = this.applicationDbContext.Users.ToList().Select(x => x.Username).ToList();
            var registeredUserEmails = this.applicationDbContext.Users.ToList().Select(x => x.Email).ToList();

            var usernameLenght = input.Username.Length;
            var passwordLenght = input.Password.Length;

            //Exist Email
            if (registeredUserEmails.Contains(input.Email))
            {
                this.IsValid = false;
                return this.Message = ExistEmail;
            }

            //Exist Username
            if (registeredUsernames.Contains(input.Username))
            {
                this.IsValid = false;
                return this.Message = ExistUsername;
            }

            //Invalid Username lenght
            if (string.IsNullOrEmpty(input.Username) || usernameLenght < UsernameMinLenght || usernameLenght > UsernameMaxLenght)
            {
                this.IsValid = false;
                return this.Message = string.Format(InvalidUsernamelenght, UsernameMinLenght, UsernameMaxLenght);
            }

            //Invalid Email
            if (string.IsNullOrEmpty(input.Email))
            {
                this.IsValid = false;
                return this.Message = InvalidEmail;
            }

            //Invalid Password
            if (string.IsNullOrEmpty(input.Password) || passwordLenght < PasswordMinLenght || passwordLenght > PasswordMaxLenght)
            {
                this.IsValid = false;
                return this.Message = string.Format(InvalidPassword, PasswordMinLenght, PasswordMaxLenght);
            }

            //Mismatched Passwords
            if (input.Password != input.ConfirmPassword)
            {
                this.IsValid = false;
                return this.Message = MismatchedPasswords;
            }

            return this.Message;
        }

        public string ValidateLoginData(InputLoginViewModel input)
        {
            this.IsValid = true;

            if (string.IsNullOrEmpty(input.Username))
            {
                this.IsValid = false;
                return this.Message = string.Format(InvalidLoginData, nameof(input.Username));
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                this.IsValid = false;
                return this.Message = string.Format(InvalidLoginData, nameof(input.Password));
            }


            var currentUser = this.applicationDbContext.Users.ToList().FirstOrDefault(x => x.Username == input.Username);

            if (currentUser == null)
            {
                this.IsValid = false;
                return this.Message = InvalidLogin;
            }
            else if (currentUser.Password != EncodingPassword.ComputeHash(input.Password))
            {
                this.IsValid = false;
                return this.Message = InvalidLogin;
            }


            return this.Message;
        }

        public string ValidateRepositoryData(InputRepositoryViewModel input, string userId)
        {
            var repsitries = this.applicationDbContext
                .Users.Where(x => x.Id == userId).
                Select(x => new
                {
                    Repsitries = x.Repositories.Select(x => x.Name)
                }
                ).ToList()
                .FirstOrDefault();

            this.IsValid = true;

            if (string.IsNullOrEmpty(input.Name) || input.Name.Length < RepositoryNameMinLenght ||
                input.Name.Length > RepositoryNameMaxLenght)
            {
                this.IsValid = false;
                return this.Message = string.Format(InvalidRepoName, RepositoryNameMinLenght, RepositoryNameMaxLenght);
            }

            if (!RepsitoryTypes.Contains(input.RepositoryType.ToLower()))
            {
                this.IsValid = false;
                return this.Message = string.Format(InvalidRepoType, RepsitoryTypes[0], RepsitoryTypes[1]);
            }

            if (repsitries.Repsitries.Contains(input.Name))
            {
                this.IsValid = false;
                return this.Message = ExsistingRepoName;
            }

            return this.Message;
        }

        public string ValidateCommitData(InputCommitViewModel input)
        {
            this.IsValid = true;

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length < CommitDescriptionMinLenght)
            {
                this.IsValid = false;
                return this.Message = string.Format(InvalidCommitDescription, CommitDescriptionMinLenght);
            }

            return this.Message;
        }

        public string CheckCoomitUserAccess(string userId, string commitId)
        {
            this.IsValid = true;

            var commit = this.applicationDbContext.Commits.ToList().FirstOrDefault(x => x.CreatorId == userId);

            if (commit.CreatorId != userId)
            {
                this.IsValid = false;
                return this.Message = "AccessDenied";
            }

            return this.Message;
        }
    }
}
