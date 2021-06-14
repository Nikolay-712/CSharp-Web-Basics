namespace BattleCards.Data.Configuration
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> card)
        {
            card.HasKey(x => x.Id);

            card.Property(x => x.Name).IsRequired().HasMaxLength(15);

            card.Property(x => x.ImageUrl).IsRequired();

            card.Property(x => x.Keyword).IsRequired();

            card.Property(x => x.Attack).IsRequired();

            card.Property(x => x.Health).IsRequired();

            card.Property(x => x.Description).IsRequired().HasMaxLength(200);


        }
    }
}
