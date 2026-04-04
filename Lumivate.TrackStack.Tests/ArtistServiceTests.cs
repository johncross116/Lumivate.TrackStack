// TODO: Create unit tests for ArtistService
//
// You have done this before with TurtleServiceTests! Follow the same pattern:
//
// 1. Add using statements for:
//    using Lumivate.TrackStack.Data;
//    using Lumivate.TrackStack.Models;
//    using Lumivate.TrackStack.Services;
//    using Microsoft.EntityFrameworkCore;
//    using Xunit;
//
// 2. Create a test class called ArtistServiceTests
//
// 3. Add the GetInMemoryContext() helper (same pattern as TurtleServiceTests):
//    private TrackStackContext GetInMemoryContext()
//    {
//        var options = new DbContextOptionsBuilder<TrackStackContext>()
//            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//            .Options;
//        return new TrackStackContext(options);
//    }
//
// 4. Write tests for at least:
//    - AddArtist_IncreasesCount
//    - GetArtistById_WithValidId_ReturnsArtist
//    - GetArtistById_WithInvalidId_ReturnsNull
//    - DeleteArtist_RemovesArtist
//    - GetArtistById_IncludesSongs (add an artist, add a song for that artist,
//      then verify GetArtistById returns the artist WITH the song in the Songs list)
//
// The last test is new - it verifies the one-to-many relationship works correctly.
// You will need to add the song directly to _context.Songs and SaveChanges()
// before calling GetArtistById.

using Lumivate.TrackStack.Data;
using Lumivate.TrackStack.Models;
using Lumivate.TrackStack.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lumivate.TrackStack.Tests
{
    // Your test class goes here

    public class ArtistServiceTests
    {
        private TrackStackContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TrackStackContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new TrackStackContext(options);
        }

        [Fact]
        public void AddArtist_IncreasesCount()
        {
            var context = GetInMemoryContext();
            var service = new ArtistService(context);

            service.AddArtist(new Artist { Name = "Test Artist", Genre = "Rock" });

            Assert.Single(service.GetAllArtists());
        }

        [Fact]
        public void GetArtistById_WithValidId_ReturnsArtist()
        {
            var context = GetInMemoryContext();
            var service = new ArtistService(context);
            var artist = new Artist { Name = "Test Artist", Genre = "Rock" };
            service.AddArtist(artist);

            var result = service.GetArtistById(artist.Id);

            Assert.NotNull(result);
            Assert.Equal("Test Artist", result.Name);
        }

        [Fact]
        public void GetArtistById_WithInvalidId_ReturnsNull()
        {
            var context = GetInMemoryContext();
            var service = new ArtistService(context);

            var result = service.GetArtistById(999);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteArtist_RemovesArtist()
        {
            var context = GetInMemoryContext();
            var service = new ArtistService(context);
            var artist = new Artist { Name = "Test Artist", Genre = "Rock" };
            service.AddArtist(artist);

            service.DeleteArtist(artist.Id);

            Assert.Empty(service.GetAllArtists());
        }

        [Fact]
        public void GetArtistById_IncludesSongs()
        {
            var context = GetInMemoryContext();
            var service = new ArtistService(context);
            var artist = new Artist { Name = "Test Artist", Genre = "Rock" };
            service.AddArtist(artist);

            context.Songs.Add(new Song { Title = "Test Song", DurationSeconds = 200, ArtistId = artist.Id });
            context.SaveChanges();

            var result = service.GetArtistById(artist.Id);

            Assert.NotNull(result);
            Assert.NotNull(result.Songs);
            Assert.Single(result.Songs);
            Assert.Equal("Test Song", result.Songs[0].Title);
        }
    }
}
