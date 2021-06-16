namespace Git.Controllers
{
    using Git.Services.Repositories;
    using Git.Services.Validation;
    using Git.ViewModels.Repositories;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class RepositoriesController : Controller
    {
        private readonly IModelValidation modelValidation;
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IModelValidation modelValidation, IRepositoriesService repositoriesService)
        {
            this.modelValidation = modelValidation;
            this.repositoriesService = repositoriesService;
        }


        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(InputRepositoryViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            this.modelValidation.ValidateRepositoryData(input, this.GetUserId());
            if (!this.modelValidation.IsValid)
            {
                return this.Error(this.modelValidation.Message);
            }

            this.repositoriesService.CreateRepository(input, this.GetUserId());


            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var repos = this.repositoriesService.All();
            return this.View(repos);
        }
    }
}
