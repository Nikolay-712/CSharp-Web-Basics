namespace CarShop.Services.Validation
{
    using CarShop.ViewModels.Cars;
    using CarShop.ViewModels.Issues;
    using CarShop.ViewModels.Users;

    public interface IValidationService
    {
        bool IsValid { get; set; }

        string Message { get; set; }

        string ValidateRegisterData(InputRegisterViewModel input);

        string ValidateLoginData(InputLoginViewModel input);

        string ValdateCarData(InputCarViewModel input, string userId);

        string ValidateIssueData(InputIssueViewModel input);

        string UserRole(string userId);
    }
}
