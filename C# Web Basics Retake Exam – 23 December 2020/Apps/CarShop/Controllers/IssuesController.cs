namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Services.Issues;
    using CarShop.Services.Validation;
    using CarShop.ViewModels.Issues;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Linq;

    public class IssuesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IValidationService validationService;
        private readonly IIssuesService issuesService;

        public IssuesController(
            ApplicationDbContext dbContext,
            IValidationService validationService,
            IIssuesService issuesService)
        {
            this.dbContext = dbContext;
            this.validationService = validationService;
            this.issuesService = issuesService;
        }


        public HttpResponse CarIssues(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var car = this.dbContext.Cars.FirstOrDefault(x => x.Id == carId);

            var info = new CarInfoViewModel
            {
                CarId = carId,
                Model = car.Model,
                Year = car.Year,
                Issues = this.dbContext
                .Issues
                .Where(x => x.CarId == carId)
                .ToList(),
            };

            return this.View(info);
        }

        public HttpResponse Add(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View(carId);
        }

        [HttpPost]
        public HttpResponse Add(InputIssueViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.validationService.ValidateIssueData(input);
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.issuesService.CreateIssue(input);

            return this.Redirect($"/Issues/CarIssues?carId={input.CarId}");
        }

        public HttpResponse Delete(string issueId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.issuesService.DeleteIssue(issueId);
            return this.Redirect("/");
        }

        public HttpResponse Fix(string issueId)
        {

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.validationService.UserRole(this.GetUserId());
            if (!this.validationService.IsValid)
            {
                return this.Error(this.validationService.Message);
            }

            this.issuesService.FixIssue(issueId);
            return this.Redirect("/");
        }


    }
}
