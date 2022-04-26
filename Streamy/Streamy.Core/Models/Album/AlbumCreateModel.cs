namespace Streamy.Core.Models
{
    public class AlbumCreateModel
    {
        public AlbumModel Album { get; set; }
        public IEnumerable<ArtistModel> Artists { get; set; }
    }
}