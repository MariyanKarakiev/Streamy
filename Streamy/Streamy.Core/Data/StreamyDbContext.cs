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

        public DbSet<Song> Songs { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<SongAlbum> AlbumSongs { get; set; }

        public DbSet<SongArtist> ArtistSongs { get; set; }

        public DbSet<SongPlaylist> PlaylistSongs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Song>()
            .HasOne(s => s.Genre)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict); 
            
            modelBuilder.Entity<Song>()
            .HasOne(s => s.Genre)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SongAlbum>(sa =>
            {
                sa.HasKey(k => new { k.SongId, k.AlbumId });
            });


            modelBuilder.Entity<SongArtist>(sa =>
            {
                sa.HasKey(k => new { k.SongId, k.ArtistId });
            });

            modelBuilder.Entity<SongPlaylist>(sa =>
            {
                sa.HasKey(k => new { k.SongId, k.PlaylistId });
            });
        }
    }
}