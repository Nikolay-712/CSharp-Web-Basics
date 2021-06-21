namespace CarShop.Data.Configuration
{
    using CarShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> issue)
        {
            issue
                .HasKey(x => x.Id);

            issue
                .Property(x => x.Description)
                .IsRequired();

            issue
                .Property(x => x.IsFixed)
                .IsRequired();

            issue
                .Property(x => x.CarId)
                .IsRequired();
        }
    }
}
