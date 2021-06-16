namespace Git.Controllers
{
    using Git.Services.Comits;
    using Git.Services.Validation;
    using Git.ViewModels.Comits;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CommitsController : Controller
    {
        private readonly IModelValidation modelValidation;
        private readonly IComitService comitService;

        public CommitsController(IModelValidation modelValidation, IComitService comitService)
        {
            this.modelValidation = modelValidation;
            this.comitService = comitService;
        }

        public HttpResponse Create(string id)
        {
            var repo = this.comitService.GetCurrentRepository(id);
            return this.View(repo);

        }

        [HttpPost]
        public HttpResponse Create(InputCommitViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            this.modelValidation.ValidateCommitData(input);
            if (!this.modelValidation.IsValid)
            {
                return this.Error(this.modelValidation.Message);
            }
            

            this.comitService.CreateComit(input.Id, this.GetUserId(), input);
            return this.Redirect("/Repositories/All");

        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var commits = this.comitService.GetAllCommits(this.GetUserId());
            return this.View(commits);
        }

        public HttpResponse Delete(string Id)
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            this.modelValidation.CheckCoomitUserAccess(this.GetUserId() , Id);
            if (!this.modelValidation.IsValid)
            {
                return this.Error(this.modelValidation.Message);
            }

            this.comitService.RemoveCommit(Id);

            return this.Redirect("/Commits/All");
        }



    }
}
