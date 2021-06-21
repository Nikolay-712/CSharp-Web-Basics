namespace CarShop.Services.Issues
{
    using System.Linq;
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Issues;

    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext dbContext;

        public IssuesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateIssue(InputIssueViewModel input)
        {
            var issue = new Issue
            {
                Description = input.Description,
                CarId = input.CarId,

            };

            this.dbContext.Issues.Add(issue);
            this.dbContext.SaveChanges();
        }

        public void DeleteIssue(string Id)
        {
            var issue = this.dbContext.Issues.FirstOrDefault(x => x.Id == Id);

            this.dbContext.Issues.Remove(issue);
            this.dbContext.SaveChanges();
        }

        public void FixIssue(string Id)
        {
            var issue = this.dbContext.Issues.FirstOrDefault(x => x.Id == Id);

            issue.IsFixed = true;

            this.dbContext.Update(issue);
            this.dbContext.SaveChanges();
        }
    }
}
