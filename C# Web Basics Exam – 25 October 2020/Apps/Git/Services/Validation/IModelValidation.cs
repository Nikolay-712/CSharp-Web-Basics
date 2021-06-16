namespace Git.Services.Validation
{
    using Git.Models;
    using Git.ViewModels.Comits;
    using Git.ViewModels.Repositories;
    using Git.ViewModels.Users;

    public interface IModelValidation
    {
        string Message { get; set; }

        bool IsValid { get; set; }

        string ValidateRegisterData(InputRegisterViewModel input);

        string ValidateLoginData(InputLoginViewModel input);

        string ValidateRepositoryData(InputRepositoryViewModel input, string userId);

        string ValidateCommitData(InputCommitViewModel input);

        string CheckCoomitUserAccess(string userId, string commitId);
    }
}
