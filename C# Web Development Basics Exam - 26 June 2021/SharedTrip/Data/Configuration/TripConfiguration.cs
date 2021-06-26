namespace SharedTrip.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SharedTrip.Models;
    class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> trip)
        {
            trip.HasKey(x => x.Id);

            trip.Property(x => x.StartPoint).IsRequired();

            trip.Property(x => x.EndPoint).IsRequired();

            trip.Property(x => x.Seats).IsRequired().HasMaxLength(6);

            trip.Property(x => x.Description).IsRequired().HasMaxLength(80);
        }
    }
}
