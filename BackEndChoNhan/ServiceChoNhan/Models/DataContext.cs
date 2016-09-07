namespace ServiceChoNhan.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<ChoManager> ChoManager { get; set; }
        public virtual DbSet<LogActive> LogActive { get; set; }
        public virtual DbSet<LogGH> LogGH { get; set; }
        public virtual DbSet<LogHoaHong> LogHoaHong { get; set; }
        public virtual DbSet<LogPinBuy> LogPinBuy { get; set; }
        public virtual DbSet<LogPH> LogPH { get; set; }
        public virtual DbSet<LogReport> LogReport { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<NhanManager> NhanManager { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<StorePin> StorePin { get; set; }
        public virtual DbSet<MemberRole> MemberRole { get; set; }
        public virtual DbSet<RoomGH> RoomGH { get; set; }
        public virtual DbSet<RoomPH> RoomPH { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChoManager>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<LogActive>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<LogGH>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<LogHoaHong>()
                .Property(e => e.UserCho)
                .IsUnicode(false);

            modelBuilder.Entity<LogHoaHong>()
                .Property(e => e.UserNhan)
                .IsUnicode(false);

            modelBuilder.Entity<LogPinBuy>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<LogPH>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Depth)
                .IsUnicode(false);

            modelBuilder.Entity<RoomGH>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<RoomPH>()
                .Property(e => e.UserName)
                .IsUnicode(false);
        }
    }
}
