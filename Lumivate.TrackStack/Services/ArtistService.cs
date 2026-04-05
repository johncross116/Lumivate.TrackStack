using Lumivate.TrackStack.Data;
using Lumivate.TrackStack.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumivate.TrackStack.Services
{
    // TODO: Create the ArtistService class that implements IArtistService
    //
    // 1. Inject TrackStackContext via the constructor (you'll need a using for Lumivate.TrackStack.Data)
    //
    // 2. Implement all methods from IArtistService:
    //   - GetAllArtists(): return all artists from the database
    //   - GetArtistById(int id): return a single artist INCLUDING their songs
    //     Hint: Use .Include(a => a.Songs) to eagerly load the related songs
    //     You will need: using Microsoft.EntityFrameworkCore;
    //   - AddArtist(Artist artist): add to context and save
    //   - UpdateArtist(Artist artist): find existing, update properties, save
    //   - DeleteArtist(int id): find by id, remove, save
    //
    // Remember: This is the same pattern as TurtleService, but GetArtistById
    // needs .Include() because of the one-to-many relationship with Songs.

    public class ArtistService : IArtistService
    {
        private readonly TrackStackContext _context;

        public ArtistService(TrackStackContext context)
        {
            _context = context;
        }

        public List<Artist> GetAllArtists()
        {
            return _context.Artists.ToList();
        }

        public Artist? GetArtistById(int id)
        {
            return _context.Artists
                .Include(a => a.Songs)
                .FirstOrDefault(a => a.Id == id);
        }

        public void AddArtist(Artist artist)
        {
            _context.Artists.Add(artist);
            _context.SaveChanges();
        }

        public void UpdateArtist(Artist artist)
        {
            var existing = _context.Artists.Find(artist.Id);
            if (existing != null)
            {
                existing.Name = artist.Name;
                existing.Genre = artist.Genre;
                _context.SaveChanges();
            }
        }

        public void DeleteArtist(int id)
        {
            var artist = _context.Artists.Find(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                _context.SaveChanges();
            }
        }
    }
}
