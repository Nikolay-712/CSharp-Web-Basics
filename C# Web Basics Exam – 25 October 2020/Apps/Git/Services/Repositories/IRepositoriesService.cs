namespace Git.Services.Repositories
{
    using Git.ViewModels.Repositories;
    using System.Collections.Generic;

    public interface IRepositoriesService
    {
        void CreateRepository(InputRepositoryViewModel input, string userId);

        IEnumerable<RepositoryViewModel> All();
    }
}
