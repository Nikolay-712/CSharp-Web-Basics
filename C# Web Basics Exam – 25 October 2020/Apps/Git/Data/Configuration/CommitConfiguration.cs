namespace Git.Data.Configuration
{
    using Git.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CommitConfiguration : IEntityTypeConfiguration<Commit>
    {
        public void Configure(EntityTypeBuilder<Commit> commit)
        {
            commit
                .HasKey(x => x.Id);

            commit
                .Property(x => x.Description)
                .IsRequired();
           
        }
    }
}
