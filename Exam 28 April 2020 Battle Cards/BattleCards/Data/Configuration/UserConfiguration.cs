namespace BattleCards.Data.Configuration
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(x => x.Id);

            user.Property(x => x.Username).IsRequired().HasMaxLength(20);

            user.Property(x => x.Email).IsRequired();

            user.Property(x => x.Password).IsRequired();
        }
    }
}
