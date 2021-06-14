namespace BattleCards.Data.Configuration
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class UserCardConfiguration : IEntityTypeConfiguration<UserCard>
    {
        public void Configure(EntityTypeBuilder<UserCard> userCard)
        {
            userCard.HasKey(x => new { x.UserId, x.CardId });
        }
    }
}
