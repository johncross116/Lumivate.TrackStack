namespace Lumivate.TrackStack.Models
{
    // TODO: Create the Artist model
    //
    // Properties:
    //   - int Id (primary key)
    //   - string Name (required)
    //   - string Genre (required) - e.g. "Pop", "Rock", "Hip-Hop", "Country", etc.
    //   - List<Song> Songs (navigation property - one artist has many songs)
    //
    // Hint: Remember how the Turtle model looked? This follows the same pattern.
    // The Songs property creates the one-to-many relationship with the Song model.
    // EF Core uses this navigation property to understand the relationship.

    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public List<Song> Songs { get; set; } = new List<Song>();
    }
}
