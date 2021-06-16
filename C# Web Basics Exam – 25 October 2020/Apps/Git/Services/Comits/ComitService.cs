namespace Git.Services.Comits
{
    using Git.Data;
    using Git.Models;
    using Git.ViewModels.Comits;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class ComitService : IComitService
    {
        private readonly ApplicationDbContext applictionDbContext;

        public ComitService(ApplicationDbContext applictionDbContext)
        {
            this.applictionDbContext = applictionDbContext;
        }

        public Repository GetCurrentRepository(string Id)
        {
            var repo = this.applictionDbContext.Repositories.ToList().FirstOrDefault(x => x.Id == Id);
            return repo;
        }

        public void CreateComit(string repositoryId, string userId, InputCommitViewModel input)
        {
            var repo = this.GetCurrentRepository(repositoryId);

            var commit = new Commit
            {
                CreatedOn = DateTime.UtcNow,
                RepositoryId = repo.Id,
                Description = input.Description,
                CreatorId = userId,
            };

            this.applictionDbContext.Commits.Add(commit);
            this.applictionDbContext.SaveChanges();
        }

        public IEnumerable<CommitViewModel> GetAllCommits(string userId)
        {
            var commits = this.applictionDbContext
                .Commits.Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel 
                {
                    Id = x.Id,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy/HH:mm"),
                    Repository = this.applictionDbContext
                    .Repositories.Where(r => r.Id == x.RepositoryId)
                    .Select(x => x.Name).FirstOrDefault(),
                }).ToList();

            return commits;
        }

        public void RemoveCommit(string commitId)
        {
            var commit = this.applictionDbContext.Commits.ToList().FirstOrDefault(x => x.Id == commitId);

            this.applictionDbContext.Commits.Remove(commit);
            this.applictionDbContext.SaveChanges();
        }
    }
}
