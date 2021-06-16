namespace Git.Services.Repositories
{
    using Git.Data;
    using Git.Models;
    using Git.ViewModels.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public RepositoriesService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void CreateRepository(InputRepositoryViewModel input, string userId)
        {
            var repositoryType = true;

            if (input.RepositoryType.ToLower() == "private")
            {
                repositoryType = false;
            }


            var repository = new Repository
            {
                Name = input.Name,
                IsPublic = repositoryType,
                CreatedOn = DateTime.UtcNow,
                OwnerId = userId,
            };

            this.applicationDbContext.Repositories.Add(repository);
            this.applicationDbContext.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> All()
        {
            var repsitores = this.applicationDbContext
                .Repositories
                .Where(x => x.IsPublic == true)
                .Select(x => new RepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy/HH:mm"),
                    Owner = this.applicationDbContext.Users.Where(u => x.OwnerId == u.Id).FirstOrDefault(),
                    CommitsCount = x.Commits.Count,

                }).ToList();


            return repsitores;
        }
    }
}
