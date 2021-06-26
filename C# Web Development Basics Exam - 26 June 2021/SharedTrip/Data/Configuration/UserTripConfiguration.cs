namespace SharedTrip.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SharedTrip.Models;
    class UserTripConfiguration : IEntityTypeConfiguration<UserTrip>
    {
        public void Configure(EntityTypeBuilder<UserTrip> userTrip)
        {
            userTrip
                .HasKey(x => new { x.UserId, x.TripId });

            //userTrip
            //    .HasOne(x => x.User)
            //    .WithMany(x => x.UserTrips)
            //    .HasForeignKey(x => x.UserId);

            //userTrip
            //    .HasOne(x => x.Trip)
            //    .WithMany(x => x.UserTrips)
            //    .HasForeignKey(x => x.TripId);
        }
    }
}
