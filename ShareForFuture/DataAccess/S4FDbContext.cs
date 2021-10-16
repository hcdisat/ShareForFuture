global using Microsoft.EntityFrameworkCore;

namespace ShareForFuture.DataAccess
{
    public class S4FDbContext: DbContext
    {
        public S4FDbContext(DbContextOptions<S4FDbContext> options): base(options) { }

        #region Users
        public DbSet<User>? Users { get; set; }

        public DbSet<EmailVerification>? EmailVerifications { get; set; }

        public DbSet<LoginHistory>? LoginHistories { get; set; }

        public DbSet<IdentityProvider>? IdentityProviders { get; set; }

        public DbSet<Group>? Groups { get; set; } 
        #endregion

        #region Divices
        public DbSet<Device>? Devices { get; set; }

        public DbSet<DeviceImage>? DeviceImages { get; set; }

        public DbSet<DeviceCategory>? DeviceCategories { get; set; }

        public DbSet<DeviceVerification>? DeviceVerifications { get; set; }

        public DbSet<DeviceStatus>? DeviceStatuses { get; set; }

        public DbSet<DeviceAvailability>? DeviceAvailabilities { get; set; }

        public DbSet<DeviceTag>? DeviceTags { get; set; } 
        #endregion

        #region Offerings
        public DbSet<Offering>? Offerings { get; set; }

        public DbSet<OfferingNotification>? OfferingNotifications { get; set; }

        public DbSet<OfferingStatus>? OfferingStatuses { get; set; }

        public DbSet<OfferingNotificationType>? OfferingNotificationTypes { get; set; }

        public DbSet<Review>? Reviews { get; set; } 
        #endregion

        #region Complains
        public DbSet<Complain>? Complains { get; set; }

        public DbSet<ComplainStatus>? ComplainStatuses { get; set; }

        public DbSet<ComplainAsignment>? ComplainAsignments { get; set; }

        public DbSet<ComplainNote>? ComplainNotes { get; set; }

        public DbSet<ComplainImage>? ComplainImages { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relationship
            #region User
            modelBuilder.Entity<User>()
                    .HasOne(u => u.EmailVerification)
                    .WithOne(ev => ev.User)
                    .HasForeignKey<EmailVerification>(e => e.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.LoginHistories)
                .WithOne(lh => lh.User)
                .HasForeignKey(lh => lh.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.IdentityProviders)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users);
            #endregion

            #region Device
            modelBuilder.Entity<Device>()
                    .HasOne(d => d.User)
                    .WithMany(d => d.Devices)
                    .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Category)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.CategoryId);

            modelBuilder.Entity<Device>().HasMany(d => d.Images);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.DeviceVerification)
                .WithOne(dv => dv.Device)
                .HasForeignKey<DeviceVerification>(dv => dv.DeviceId);

            modelBuilder.Entity<DeviceAvailability>()
                .HasKey(da => new { da.DeviceId, da.StatusId });

            modelBuilder.Entity<DeviceAvailability>()
                .HasOne(da => da.Device)
                .WithMany(d => d.Availabilities)
                .HasForeignKey(da => da.DeviceId);

            modelBuilder.Entity<DeviceAvailability>()
                .HasOne(da => da.Status)
                .WithMany(s => s.Availabilities)
                .HasForeignKey(da => da.StatusId);    
            #endregion



            // columns attributes
        }
    }
}
