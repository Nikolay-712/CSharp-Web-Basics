namespace BattleCards.Data.Configuration
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserCardConfiguration : IEntityTypeConfiguration<UserCard>
    {
        public void Configure(EntityTypeBuilder<UserCard> userCard)
        {
            userCard
                .HasKey(x => new { x.UserId, x.CardId });

            userCard
                .HasOne(x => x.User)
                .WithMany(x => x.UserCards)
                .HasForeignKey(x => x.UserId);

            userCard
                .HasOne(x => x.Card)
                .WithMany(x => x.UserCards)
                .HasForeignKey(x => x.CardId);
        }
    }
}
