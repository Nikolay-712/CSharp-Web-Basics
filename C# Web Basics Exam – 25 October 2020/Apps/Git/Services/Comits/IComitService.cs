namespace Git.Services.Comits
{
    using Git.Models;
    using Git.ViewModels.Comits;
    using System.Collections.Generic;

    public interface IComitService
    {
        Repository GetCurrentRepository(string Id);

        void CreateComit(string repositoryId, string userId, InputCommitViewModel input);

        IEnumerable<CommitViewModel> GetAllCommits(string userId);

        void RemoveCommit(string commitId);
    }
}
