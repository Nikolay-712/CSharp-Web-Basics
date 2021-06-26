namespace SharedTrip
{
    using System.Threading.Tasks;

    using MyWebServer;
    using MyWebServer.Controllers;

    using Controllers;
    using MyWebServer.Results.Views;
    using SharedTrip.Services.Users;
    using SharedTrip.Services.Validation;
    using SharedTrip.Services.Encoder;
    using SharedTrip.Services.Trips;

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                .Add<IUserService, UserService>()
                .Add<IValidationService,ValidationService>()
                .Add<IPasswordEncoder,PasswordEncoder>()
                .Add<ITripService,TripService>())
                .Start();
    }
}
