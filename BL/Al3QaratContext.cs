using System;
using Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BL
{
    public partial class Al3QaratContext : IdentityDbContext<ApplicationUser>
    {
        public Al3QaratContext()
        {
        }

        public Al3QaratContext(DbContextOptions<Al3QaratContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbInquiry> TbInquiries { get; set; }
        public virtual DbSet<TbLogHistory> TbLogHistories { get; set; }
        public virtual DbSet<TbOffer> TbOffers { get; set; }
        public virtual DbSet<TbOfferBooking> TbOfferBookings { get; set; }
        public virtual DbSet<TbOfferImage> TbOfferImages { get; set; }
        public virtual DbSet<TbOfferNote> TbOfferNotes { get; set; }
        public virtual DbSet<TbOfferVideo> TbOfferVideos { get; set; }
        public virtual DbSet<TbRegion> TbRegions { get; set; }
        public virtual DbSet<TbRequest> TbRequests { get; set; }
        public virtual DbSet<TbRequestNote> TbRequestNotes { get; set; }
        public virtual DbSet<TbSalesRepPoint> TbSalesRepPoints { get; set; }
        public virtual DbSet<TbUnit> TbUnits { get; set; }

        public virtual DbSet<TbSetting> TbSettings { get; set; }
        public virtual DbSet<TbRealTimeNotifcation> TbRealTimeNotifcations { get; set; }


        public virtual DbSet<TbMap> TbMaps { get; set; }


        public virtual DbSet<TbActiveUsers> TbActiveUserss { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TbInquiry>(entity =>
            {
                entity.HasKey(e => e.InquiryId);

                entity.ToTable("TbInquiry");

                entity.Property(e => e.InquiryId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.InquiryOwnerName).HasMaxLength(200);

                entity.Property(e => e.InquiryOwnerPhoneNumber).HasMaxLength(200);

                entity.Property(e => e.InquirySyntax).HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.ReceptionRepId).HasMaxLength(200);

                entity.Property(e => e.ReceptionRepName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });


            modelBuilder.Entity<TbMap>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.ToTable("TbMap");

                entity.Property(e => e.id).HasColumnType("int");

              

              

                entity.Property(e => e.customer_name).HasMaxLength(50);

                entity.Property(e => e.lat).HasColumnType("NUMERIC(38, 16)");

                entity.Property(e => e.lng).HasColumnType("NUMERIC(38, 16)");

                entity.Property(e => e.contract_type).HasMaxLength(50);

                entity.Property(e => e.contract_number).HasColumnType("int");

                entity.Property(e => e.icon).HasMaxLength(255);

            
            });


            modelBuilder.Entity<TbRealTimeNotifcation>(entity =>
            {
                entity.HasKey(e => e.RealTimeNotifcationId);

                entity.Property(e => e.RealTimeNotifcationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NotificationType).HasMaxLength(200);

                entity.Property(e => e.NotificationSyntax).HasMaxLength(200);



                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");


            });


            modelBuilder.Entity<TbSetting>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.ToTable("TbSetting");

                entity.Property(e => e.SettingId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NoOfBookingDays).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasMaxLength(200);

            

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

              
            });

            modelBuilder.Entity<TbLogHistory>(entity =>
            {
                entity.HasKey(e => e.LogHistoryId);

                entity.ToTable("TbLogHistory");

                entity.Property(e => e.LogHistoryId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.LoggedUserName).HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbActiveUsers>(entity =>
            {
                entity.HasKey(e => e.LoggingId);

                entity.ToTable("TbActiveUsers");

                entity.Property(e => e.LoggingId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.LoggedUserId).HasMaxLength(200);

                entity.Property(e => e.LoggedUserName).HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOffer>(entity =>
            {
                entity.HasKey(e => e.OfferId);

                entity.ToTable("TbOffer");

                entity.Property(e => e.OfferId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude).HasMaxLength(200);

                entity.Property(e => e.LatitudeField).HasMaxLength(200);

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.LocationField).HasMaxLength(200);

                entity.Property(e => e.Longitute).HasMaxLength(200);

                entity.Property(e => e.LongituteField).HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.OfferEndTime).HasMaxLength(200);

                entity.Property(e => e.OfferEndTimeField).HasMaxLength(200);

                entity.Property(e => e.OfferStartTime).HasMaxLength(200);

                entity.Property(e => e.OfferStartTimeFiled).HasMaxLength(200);

                entity.Property(e => e.OfferStatus).HasMaxLength(200);

                entity.Property(e => e.OfferStatusField).HasMaxLength(200);

                entity.Property(e => e.OfferSyntax).HasMaxLength(200);

                entity.Property(e => e.OwnerName).HasMaxLength(200);

                entity.Property(e => e.OwnerNameField).HasMaxLength(200);

                entity.Property(e => e.OwnerPhoneNumber).HasMaxLength(200);

                entity.Property(e => e.OwnerPhoneNumberField).HasMaxLength(200);

                entity.Property(e => e.PropertyDocumentPath).HasMaxLength(200);

                entity.Property(e => e.PropertyDocumentPathField).HasMaxLength(200);

                entity.Property(e => e.ReceptionRepId).HasMaxLength(200);

                entity.Property(e => e.ReceptionRepName).HasMaxLength(200);

                entity.Property(e => e.ReceptionRepNameField).HasMaxLength(200);

                entity.Property(e => e.RegionName).HasMaxLength(200);

                entity.Property(e => e.RegionNameField).HasMaxLength(200);

                entity.Property(e => e.SalesRepId).HasMaxLength(200);

                entity.Property(e => e.SalesRepName).HasMaxLength(200);

                entity.Property(e => e.SalesRepNameField).HasMaxLength(200);

                entity.Property(e => e.SellingStatus).HasMaxLength(200);

                entity.Property(e => e.SellingStatusField).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(200);

                entity.Property(e => e.UnitNameField).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOfferBooking>(entity =>
            {
                entity.HasKey(e => e.OfferBookingId);

                entity.ToTable("TbOfferBooking");

                entity.Property(e => e.OfferBookingId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CashOrCredit).HasMaxLength(200);

                entity.Property(e => e.SellingStatus).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.CustomerName).HasMaxLength(200);

                entity.Property(e => e.CustomerPhoneNumber).HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.SalesRepId).HasMaxLength(200);

                entity.Property(e => e.SalesRepName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValueToBePaid).HasMaxLength(200);
            });

            modelBuilder.Entity<TbOfferImage>(entity =>
            {
                entity.HasKey(e => e.OfferImageId);

                entity.ToTable("TbOfferImage");

                entity.Property(e => e.OfferImageId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.OfferImagePath).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbOfferNote>(entity =>
            {
                entity.HasKey(e => e.OfferNotesId);

                entity.Property(e => e.OfferNotesId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.NoteSyntax).HasColumnType("nvarchar(max)");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.ReadByAdmin).HasMaxLength(200);

                entity.Property(e => e.ReadByReceptionEmployee).HasMaxLength(200);

                entity.Property(e => e.ReadBySalesEmployee).HasMaxLength(200);

                entity.Property(e => e.RecieverDepartment).HasMaxLength(200);

                entity.Property(e => e.RecieverId).HasMaxLength(200);

                entity.Property(e => e.RecieverName).HasMaxLength(200);

                entity.Property(e => e.SenderDepartment).HasMaxLength(200);

                entity.Property(e => e.SenderId).HasMaxLength(200);

                entity.Property(e => e.SenderName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Visibility).HasMaxLength(200);
            });

            modelBuilder.Entity<TbOfferVideo>(entity =>
            {
                entity.HasKey(e => e.OfferVideoId);

                entity.ToTable("TbOfferVideo");

                entity.Property(e => e.OfferVideoId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("(getdate())")
                    .IsFixedLength(true);

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.OfferVideoPath).HasMaxLength(200);

                entity.Property(e => e.OfferVideoYouTubeLink).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TbRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.ToTable("TbRegion");

                entity.Property(e => e.RegionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.RegionName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("TbRequest");

                entity.Property(e => e.RequestId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Area).HasMaxLength(200);

                entity.Property(e => e.CashOrCredit).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Latitude).HasMaxLength(200);

                entity.Property(e => e.Location).HasMaxLength(200);

                entity.Property(e => e.Longitute).HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.RegionName).HasMaxLength(200);

                entity.Property(e => e.RequestText).HasMaxLength(200);

                entity.Property(e => e.RequesterName).HasMaxLength(200);

                entity.Property(e => e.RequesterNumber).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValueOfPurchase).HasMaxLength(200);
            });

            modelBuilder.Entity<TbRequestNote>(entity =>
            {
                entity.HasKey(e => e.RequestNotesId);

                entity.Property(e => e.RequestNotesId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.NoteSyntax).HasMaxLength(200);

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.ReadByAdmin).HasMaxLength(200);

                entity.Property(e => e.ReadByReceptionEmployee).HasMaxLength(200);

                entity.Property(e => e.ReadBySalesEmployee).HasMaxLength(200);

                entity.Property(e => e.RecieverDepartment).HasMaxLength(200);

                entity.Property(e => e.RecieverId).HasMaxLength(200);

                entity.Property(e => e.RecieverName).HasMaxLength(200);

                entity.Property(e => e.SenderDepartment).HasMaxLength(200);

                entity.Property(e => e.SenderId).HasMaxLength(200);

                entity.Property(e => e.SenderName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Visibility).HasMaxLength(200);
            });

            modelBuilder.Entity<TbSalesRepPoint>(entity =>
            {
                entity.HasKey(e => e.SalesRepPointId);

                entity.ToTable("TbSalesRepPoint");

                entity.Property(e => e.SalesRepPointId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Cause).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.Points).HasMaxLength(200);

                entity.Property(e => e.SalesRepName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbUnit>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.ToTable("TbUnit");

                entity.Property(e => e.UnitId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedBy).HasMaxLength(200);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrentState).HasDefaultValueSql("((1))");

                entity.Property(e => e.Notes).HasMaxLength(200);

                entity.Property(e => e.UnitImage).HasMaxLength(200);

                entity.Property(e => e.UnitName).HasMaxLength(200);

                entity.Property(e => e.UpdatedBy).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
