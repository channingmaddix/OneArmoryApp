using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OneArmoryApp.Models
{
    public partial class OneArmoryDataContext : DbContext
    {
        public OneArmoryDataContext()
        {
        }

        public OneArmoryDataContext(DbContextOptions<OneArmoryDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EquipmentType> EquipmentType { get; set; }
        public virtual DbSet<Nomenclature> Nomenclature { get; set; }
        public virtual DbSet<Paygrade> Paygrade { get; set; }
        public virtual DbSet<Platoon> Platoon { get; set; }
        public virtual DbSet<Soldier> Soldier { get; set; }
        public virtual DbSet<Weapon> Weapon { get; set; }
        public virtual DbSet<WeaponStatus> WeaponStatus { get; set; }
        public virtual DbSet<WorkOrder> WorkOrder { get; set; }
        public virtual DbSet<WorkOrderStatus> WorkOrderStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=OneArmoryData;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<EquipmentType>(entity =>
            {
                entity.HasIndex(e => e.EquipmentType1)
                    .HasName("UQ__Equipmen__AA28A8D5EDE3F836")
                    .IsUnique();

                entity.Property(e => e.EquipmentTypeId).HasColumnName("EquipmentTypeID");

                entity.Property(e => e.EquipmentType1)
                    .IsRequired()
                    .HasColumnName("EquipmentType")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Nomenclature>(entity =>
            {
                entity.HasIndex(e => e.Nomenclature1)
                    .HasName("UQ__Nomencla__8F9251AE21A11EDA")
                    .IsUnique();

                entity.Property(e => e.NomenclatureId).HasColumnName("NomenclatureID");

                entity.Property(e => e.Nomenclature1)
                    .IsRequired()
                    .HasColumnName("Nomenclature")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Paygrade>(entity =>
            {
                entity.HasIndex(e => e.Paygrade1)
                    .HasName("UQ__Paygrade__2F2183BE4DF8F84B")
                    .IsUnique();

                entity.Property(e => e.PaygradeId).HasColumnName("PaygradeID");

                entity.Property(e => e.Paygrade1)
                    .IsRequired()
                    .HasColumnName("Paygrade")
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<Platoon>(entity =>
            {
                entity.HasIndex(e => e.Platoon1)
                    .HasName("UQ__Platoon__4AB316381B164680")
                    .IsUnique();

                entity.Property(e => e.PlatoonId).HasColumnName("PlatoonID");

                entity.Property(e => e.Platoon1)
                    .IsRequired()
                    .HasColumnName("Platoon")
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Soldier>(entity =>
            {
                entity.HasIndex(e => e.DoDid)
                    .HasName("UQ__Soldier__3E377F45D3462F7B")
                    .IsUnique();

                entity.Property(e => e.SoldierId).HasColumnName("SoldierID");

                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.Property(e => e.DoDid).HasColumnName("DoDID");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Paygrade).HasMaxLength(3);

                entity.Property(e => e.WeaponId).HasColumnName("WeaponID");

                entity.HasOne(d => d.PaygradeNavigation)
                    .WithMany(p => p.Soldier)
                    .HasPrincipalKey(p => p.Paygrade1)
                    .HasForeignKey(d => d.Paygrade)
                    .HasConstraintName("FK__Soldier__Paygrad__3A81B327");

                entity.HasOne(d => d.Weapon)
                    .WithMany(p => p.Soldier)
                    .HasForeignKey(d => d.WeaponId)
                    .HasConstraintName("FK__Soldier__WeaponI__3B75D760");
            });

            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.HasIndex(e => e.Serial)
                    .HasName("UQ__Weapon__1A00E093730B64CC")
                    .IsUnique();

                entity.Property(e => e.WeaponId).HasColumnName("WeaponID");

                entity.Property(e => e.ArrivalDate).HasColumnType("date");

                entity.Property(e => e.EquipmentType).HasMaxLength(20);

                entity.Property(e => e.Nomenclature).HasMaxLength(50);

                entity.Property(e => e.Platoon).HasMaxLength(5);

                entity.Property(e => e.Serial).HasMaxLength(30);

                entity.HasOne(d => d.EquipmentTypeNavigation)
                    .WithMany(p => p.Weapon)
                    .HasPrincipalKey(p => p.EquipmentType1)
                    .HasForeignKey(d => d.EquipmentType)
                    .HasConstraintName("FK__Weapon__Equipmen__3D5E1FD2");

                entity.HasOne(d => d.NomenclatureNavigation)
                    .WithMany(p => p.Weapon)
                    .HasPrincipalKey(p => p.Nomenclature1)
                    .HasForeignKey(d => d.Nomenclature)
                    .HasConstraintName("FK__Weapon__Nomencla__3C69FB99");

                entity.HasOne(d => d.PlatoonNavigation)
                    .WithMany(p => p.Weapon)
                    .HasPrincipalKey(p => p.Platoon1)
                    .HasForeignKey(d => d.Platoon)
                    .HasConstraintName("FK__Weapon__Platoon__3E52440B");
            });

            modelBuilder.Entity<WeaponStatus>(entity =>
            {
                entity.HasIndex(e => e.WeaponStatus1)
                    .HasName("UQ__WeaponSt__07BCF1374346B5D5")
                    .IsUnique();

                entity.Property(e => e.WeaponStatusId).HasColumnName("WeaponStatusID");

                entity.Property(e => e.WeaponStatus1)
                    .IsRequired()
                    .HasColumnName("WeaponStatus")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.Property(e => e.WorkOrderId).HasColumnName("WorkOrderID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FaultDesc).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.WeaponId).HasColumnName("WeaponID");

                entity.Property(e => e.WeaponStatus).HasMaxLength(30);

                entity.Property(e => e.WorkOrderStatus).HasMaxLength(30);

                entity.HasOne(d => d.Weapon)
                    .WithMany(p => p.WorkOrder)
                    .HasForeignKey(d => d.WeaponId)
                    .HasConstraintName("FK__WorkOrder__Weapo__3F466844");

                entity.HasOne(d => d.WeaponStatusNavigation)
                    .WithMany(p => p.WorkOrder)
                    .HasPrincipalKey(p => p.WeaponStatus1)
                    .HasForeignKey(d => d.WeaponStatus)
                    .HasConstraintName("FK__WorkOrder__Weapo__403A8C7D");

                entity.HasOne(d => d.WorkOrderStatusNavigation)
                    .WithMany(p => p.WorkOrder)
                    .HasPrincipalKey(p => p.WorkOrderStatus1)
                    .HasForeignKey(d => d.WorkOrderStatus)
                    .HasConstraintName("FK__WorkOrder__WorkO__412EB0B6");
            });

            modelBuilder.Entity<WorkOrderStatus>(entity =>
            {
                entity.HasIndex(e => e.WorkOrderStatus1)
                    .HasName("UQ__WorkOrde__FACE9A4F07579B9B")
                    .IsUnique();

                entity.Property(e => e.WorkOrderStatusId).HasColumnName("WorkOrderStatusID");

                entity.Property(e => e.WorkOrderStatus1)
                    .IsRequired()
                    .HasColumnName("WorkOrderStatus")
                    .HasMaxLength(30);
            });
        }
    }
}
