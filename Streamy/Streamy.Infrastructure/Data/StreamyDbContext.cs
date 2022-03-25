using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Streamy.Infrastructure.Models;

namespace Streamy.Infrastructure.Data
{
    public class StreamyDbContext : IdentityDbContext
    {
        public StreamyDbContext(DbContextOptions<StreamyDbContext> options)
            : base(options)
        {

        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<SongPlaylist> SongPlaylists { get; set; }
        public DbSet<SongArtist> SongArtists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Song>()
                .HasOne<Genre>(s => s.Genre)
                .WithMany(g => g.Song)
                .HasForeignKey(s => s.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SongArtist>()
                  .HasKey(sa => new { sa.ArtistId, sa.SongId });

            modelBuilder.Entity<SongPlaylist>()
                 .HasKey(sp => new { sp.PlaylistId, sp.SongId });
        }
    }
}