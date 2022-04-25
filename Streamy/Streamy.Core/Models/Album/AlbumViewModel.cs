namespace Streamy.Core.Models
{
    public class AlbumViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Duration { get; set; }

        public short GenreId { get; set; }
        public GenreViewModel Genre { get; set; }
    }
}