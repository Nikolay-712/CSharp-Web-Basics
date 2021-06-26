namespace SharedTrip.Services.Validation
{
    using SharedTrip.ViewModels;
    using SharedTrip.ViewModels.Trips;

    public interface IValidationService
    {
        bool IsValid { get; set; }

        string Message { get; set; }

        string ValidateRegisterData(InputRegisterViewModel input);

        string ValidateLoginData(InputLoginViewModel input);

        string ValideateTripInputData(InputTripViewModel input);


    }
}
