namespace Git.ViewModels.Repositories
{
    using Git.Models;

    public class RepositoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public User Owner { get; set; }

        public string CreatedOn { get; set; }

        public int CommitsCount { get; set; }
    }
}
