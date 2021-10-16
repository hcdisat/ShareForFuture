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

            modelBuilder.Entity<DeviceCategory>()
                .HasMany(c => c.DeviceCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Offerings
            modelBuilder.Entity<Offering>()
                   .HasOne(o => o.Borrower)
                   .WithMany(b => b.BorrowerOfferings)
                   .HasForeignKey(o => o.BorrowerId)
                   .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Offering>()
                .HasOne(o => o.Lender)
                .WithMany(b => b.LenderOfferings)
                .HasForeignKey(o => o.LenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Offering>()
                .HasOne(o => o.Device)
                .WithMany(d => d.Offerings)
                .HasForeignKey(o => o.DeviceId);

            modelBuilder.Entity<OfferingNotification>()
                .HasOne(on => on.Offering)
                .WithMany(o => o.Notifications)
                .HasForeignKey(on => on.OfferingId);

            modelBuilder.Entity<Offering>()
                .HasMany(o => o.Statuses)
                .WithMany(s => s.Offerings);

            modelBuilder.Entity<OfferingNotificationType>()
                .HasMany(sn => sn.Notifications)
                .WithOne(n => n.Type)
                .HasForeignKey(n => n.TypeId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Borrower)
                .WithMany(b => b.BorrowerReviews)
                .HasForeignKey(r => r.BorrowerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Lender)
                .WithMany(b => b.LenderReviews)
                .HasForeignKey(r => r.LenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Offering)
                .WithMany(o => o.Reviews)
                .HasForeignKey(r => r.OfferingId);
            #endregion

            #region Complains
            modelBuilder.Entity<Complain>()
                    .HasOne(c => c.Offering)
                    .WithMany(o => o.Complains)
                    .HasForeignKey(c => c.OfferingId);

            modelBuilder.Entity<Complain>()
                .HasOne(c => c.Filler)
                .WithMany(f => f.FillerComplains)
                .HasForeignKey(c => c.FillerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Complain>()
                .HasOne(c => c.AgainstTo)
                .WithMany(a => a.AgainstToComplains)
                .HasForeignKey(c => c.AgainsToId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Complain>()
                .HasOne(c => c.ComplainStatus)
                .WithMany(s => s.Complains)
                .HasForeignKey(c => c.ComplainStatusId);

            modelBuilder.Entity<Complain>()
                .HasMany(c => c.Asignments)
                .WithOne(a => a.Complain)
                .HasForeignKey(a => a.ComplainId);

            modelBuilder.Entity<Complain>()
                .HasMany(c => c.Notes)
                .WithOne(n => n.Complain)
                .HasForeignKey(n => n.ComplainId);

            modelBuilder.Entity<Complain>()
                .HasMany(c => c.Images)
                .WithOne(i => i.Complain)
                .HasForeignKey(i => i.ComplainId);
            #endregion

            #region Columns Attributes
            modelBuilder.Entity<User>()
                   .HasIndex(u => u.Email)
                   .IsUnique();

            modelBuilder.Entity<EmailVerification>()
                .HasIndex(ev => ev.Token)
                .IsUnique();

            modelBuilder.Entity<IdentityProvider>()
                .HasIndex(ev => ev.Identifier)
                .IsUnique();

            modelBuilder.Entity<Group>()
                .HasIndex(ev => ev.Name)
                .IsUnique();

            modelBuilder.Entity<DeviceCategory>()
                .HasIndex(ev => ev.Name)
                .IsUnique();

            modelBuilder.Entity<DeviceStatus>()
                .HasIndex(ev => ev.Name)
                .IsUnique();

            modelBuilder.Entity<DeviceAvailability>()
               .HasIndex(ev => ev.Name)
               .IsUnique();

            modelBuilder.Entity<DeviceTag>()
               .HasIndex(ev => ev.Name)
               .IsUnique();

            modelBuilder.Entity<OfferingNotificationType>()
               .HasIndex(ev => ev.Name)
               .IsUnique();

            modelBuilder.Entity<OfferingStatus>()
               .HasIndex(ev => ev.Name)
               .IsUnique(); 
            #endregion
        }
    }
}
