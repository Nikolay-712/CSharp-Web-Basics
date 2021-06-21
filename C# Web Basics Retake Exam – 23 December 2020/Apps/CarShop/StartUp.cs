﻿using CarShop.Data;
using CarShop.Services;
using CarShop.Services.Cars;
using CarShop.Services.Issues;
using CarShop.Services.PasswordEncoding;
using CarShop.Services.Validation;
using Microsoft.EntityFrameworkCore;

using SUS.HTTP;
using SUS.MvcFramework;

using System.Collections.Generic;

namespace CarShop
{
    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ICarService, CarsService>();
            serviceCollection.Add<IIssuesService, IssuesService>();
            serviceCollection.Add<IValidationService, ValidationService>();
            serviceCollection.Add<IPasswordEncoder, PasswordEncoder>();
        }
    }
}
