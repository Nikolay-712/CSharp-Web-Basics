namespace Git.Data.Configuration
{
    using Git.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class RepositoryConfiguration : IEntityTypeConfiguration<Repository>
    {
        public void Configure(EntityTypeBuilder<Repository> repository)
        {
            repository
                .HasKey(x => x.Id);

            repository
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(10);

            repository
                .Property(x => x.IsPublic)
                .IsRequired()
                .HasDefaultValue<bool>(value: true);

            repository
                .HasMany(x => x.Commits)
                .WithOne(x => x.Repository)
                .HasForeignKey(x => x.RepositoryId);
        }
    }
}
