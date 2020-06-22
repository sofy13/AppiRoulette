using BettingRoulette.Entities;
using Microsoft.EntityFrameworkCore;
namespace BettingRoulette.Context
{
    public partial class RouletteContext : DbContext
    {
        public RouletteContext()
        {
        }
        public RouletteContext(DbContextOptions<RouletteContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Bet> Bet { get; set; }
        public virtual DbSet<Roulette> Roulette { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.IdBet)
                    .HasName("bet_pkey");
                entity.ToTable("bet");
                entity.Property(e => e.IdRouletteBet).HasColumnName("idRouletteBet");
                entity.Property(e => e.idUserBet).HasColumnName("idUserBet");
            });
            modelBuilder.Entity<Roulette>(entity =>
            {
                entity.HasKey(e => e.IdRoulette)
                    .HasName("roulette_pkey");
                entity.ToTable("roulette");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}