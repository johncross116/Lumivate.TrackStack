// TODO: Create unit tests for SongService
//
// Follow the same pattern as ArtistServiceTests:
//
// 1. Add using statements (same as ArtistServiceTests)
//
// 2. Create a test class called SongServiceTests
//
// 3. Add the GetInMemoryContext() helper
//
// 4. Write tests for at least:
//    - AddSong_IncreasesCount
//    - GetSongById_WithValidId_ReturnsSong
//    - GetSongById_WithInvalidId_ReturnsNull
//    - DeleteSong_RemovesSong
//    - GetSongsByArtist_ReturnsOnlySongsForThatArtist
//      (add songs for two different artists, then verify GetSongsByArtist
//       only returns songs for the specified artist)
//
// Hint: For tests that involve songs, you will need to create an Artist first
// (since Song has a required ArtistId foreign key).

using Lumivate.TrackStack.Data;
using Lumivate.TrackStack.Models;
using Lumivate.TrackStack.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lumivate.TrackStack.Tests
{
    // Your test class goes here

    public class SongServiceTests
    {
        private TrackStackContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TrackStackContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new TrackStackContext(options);
        }

        [Fact]
        public void AddSong_IncreasesCount()
        {
            var context = GetInMemoryContext();
            var service = new SongService(context);
            var artist = new Artist { Name = "Test Artist", Genre = "Rock" };
            context.Artists.Add(artist);
            context.SaveChanges();

            service.AddSong(new Song { Title = "Test Song", DurationSeconds = 200, ArtistId = artist.Id });

            Assert.Single(service.GetAllSongs());
        }

        [Fact]
        public void GetSongById_WithValidId_ReturnsSong()
        {
            var context = GetInMemoryContext();
            var service = new SongService(context);
            var artist = new Artist { Name = "Test Artist", Genre = "Rock" };
            context.Artists.Add(artist);
            context.SaveChanges();
            var song = new Song { Title = "Test Song", DurationSeconds = 200, ArtistId = artist.Id };
            service.AddSong(song);

            var result = service.GetSongById(song.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Song", result.Title);
        }

        [Fact]
        public void GetSongById_WithInvalidId_ReturnsNull()
        {
            var context = GetInMemoryContext();
            var service = new SongService(context);

            var result = service.GetSongById(999);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteSong_RemovesSong()
        {
            var context = GetInMemoryContext();
            var service = new SongService(context);
            var artist = new Artist { Name = "Test Artist", Genre = "Rock" };
            context.Artists.Add(artist);
            context.SaveChanges();
            var song = new Song { Title = "Test Song", DurationSeconds = 200, ArtistId = artist.Id };
            service.AddSong(song);

            service.DeleteSong(song.Id);

            Assert.Empty(service.GetAllSongs());
        }

        [Fact]
        public void GetSongsByArtist_ReturnsOnlySongsForThatArtist()
        {
            var context = GetInMemoryContext();
            var service = new SongService(context);
            var artist1 = new Artist { Name = "Artist 1", Genre = "Rock" };
            var artist2 = new Artist { Name = "Artist 2", Genre = "Pop" };
            context.Artists.AddRange(artist1, artist2);
            context.SaveChanges();

            service.AddSong(new Song { Title = "Song A", DurationSeconds = 200, ArtistId = artist1.Id });
            service.AddSong(new Song { Title = "Song B", DurationSeconds = 180, ArtistId = artist1.Id });
            service.AddSong(new Song { Title = "Song C", DurationSeconds = 220, ArtistId = artist2.Id });

            var result = service.GetSongsByArtist(artist1.Id);

            Assert.Equal(2, result.Count);
            Assert.All(result, s => Assert.Equal(artist1.Id, s.ArtistId));
        }
    }
}
