using Microsoft.EntityFrameworkCore;
using Streamy.Infrastructure.Models;

namespace Streamy.Core
{
    public class StreamyDbContext : DbContext
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

            modelBuilder.Entity<Album>()
                .HasMany<Song>(a => a.Songs)
                .WithOne(s => s.Album)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SongArtist>()
                  .HasKey(sa => new { sa.ArtistId, sa.SongId });

            modelBuilder.Entity<SongPlaylist>()
                 .HasKey(sp => new { sp.PlaylistId, sp.SongId });
        }
    }
}