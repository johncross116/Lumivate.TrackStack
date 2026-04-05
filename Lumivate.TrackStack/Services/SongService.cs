using Lumivate.TrackStack.Data;
using Lumivate.TrackStack.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumivate.TrackStack.Services
{
    // TODO: Create the SongService class that implements ISongService
    //
    // 1. Inject TrackStackContext via the constructor
    //
    // 2. Implement all methods from ISongService:
    //   - GetAllSongs(): return all songs, including their Artist navigation property
    //     Hint: Use .Include(s => s.Artist) so you can display the artist name
    //   - GetSongsByArtist(int artistId): filter songs by ArtistId, include Artist
    //   - GetSongById(int id): return a single song with its Artist included
    //   - AddSong(Song song): add to context and save
    //   - UpdateSong(Song song): find existing, update properties, save
    //   - DeleteSong(int id): find by id, remove, save
    //
    // Hint: The .Include() calls are important here because without them,
    // the Artist property on each Song will be null when you try to display it.

    public class SongService : ISongService
    {
        private readonly TrackStackContext _context;

        public SongService(TrackStackContext context)
        {
            _context = context;
        }

        public List<Song> GetAllSongs()
        {
            return _context.Songs
                .Include(s => s.Artist)
                .ToList();
        }

        public List<Song> GetSongsByArtist(int artistId)
        {
            return _context.Songs
                .Include(s => s.Artist)
                .Where(s => s.ArtistId == artistId)
                .ToList();
        }

        public Song? GetSongById(int id)
        {
            return _context.Songs
                .Include(s => s.Artist)
                .FirstOrDefault(s => s.Id == id);
        }

        public void AddSong(Song song)
        {
            _context.Songs.Add(song);
            _context.SaveChanges();
        }

        public void UpdateSong(Song song)
        {
            var existing = _context.Songs.Find(song.Id);
            if (existing != null)
            {
                existing.Title = song.Title;
                existing.DurationSeconds = song.DurationSeconds;
                existing.ArtistId = song.ArtistId;
                _context.SaveChanges();
            }
        }

        public void DeleteSong(int id)
        {
            var song = _context.Songs.Find(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
                _context.SaveChanges();
            }
        }
    }
}
