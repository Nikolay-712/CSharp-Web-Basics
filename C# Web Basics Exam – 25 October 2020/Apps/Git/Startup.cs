namespace Git
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Services;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using Git.Services.Validation;
    using Git.Services.Users;
    using Git.Services.Repositories;
    using Git.Services.Comits;

    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IModelValidation, ModelValidation>();
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IRepositoriesService, RepositoriesService>();
            serviceCollection.Add<IComitService, ComitService>();
        }
    }
}
