namespace CarShop.Data.Configuration
{
    using CarShop.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> car)
        {
            car
                .HasKey(x => x.Id);

            car
                .Property(x => x.Model)
                .IsRequired()
                .HasMaxLength(20);

            car
                .Property(x => x.Year)
                .IsRequired();

            car
                .Property(x => x.PictureUrl)
                .IsRequired();

            car
                .Property(x => x.PlateNumber)
                .IsRequired()
                .HasMaxLength(8);

            car
                .Property(x => x.OwnerId)
                .IsRequired();
        }
    }
}
