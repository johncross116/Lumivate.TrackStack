using Lumivate.TrackStack.Models;
using Lumivate.TrackStack.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lumivate.TrackStack.Controllers
{
    // TODO: Create the ArtistsController
    //
    // 1. Make this class extend Controller
    //
    // 2. Inject IArtistService via the constructor
    //    (you'll need using Lumivate.TrackStack.Services)
    //
    // 3. Add the following actions:
    //
    //   Index (GET) - get all artists and return the view
    //
    //   Details (GET) - accept an int id, get artist by id (includes songs),
    //                   return NotFound() if null, otherwise return the view
    //
    //   Create (GET) - just return the view
    //
    //   Create (POST) - accept an Artist parameter, call AddArtist, redirect to Index
    //                   (you'll need using Lumivate.TrackStack.Models and [HttpPost])
    //
    //   Edit (GET) - accept an int id, get artist by id, return NotFound() or view
    //
    //   Edit (POST) - accept an Artist parameter, call UpdateArtist, redirect to Index
    //
    //   Delete (GET) - accept an int id, get artist by id, return NotFound() or view
    //
    //   DeleteConfirmed (POST) - accept an int id, call DeleteArtist, redirect to Index
    //                            Use [HttpPost, ActionName("Delete")] attribute
    //
    // Hint: This is the same CRUD pattern as TurtlesController.
    // The Details page is extra useful here because it shows the artist's songs.

    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        public IActionResult Index()
        {
            var artists = _artistService.GetAllArtists();
            return View(artists);
        }

        public IActionResult Details(int id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            _artistService.AddArtist(artist);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            _artistService.UpdateArtist(artist);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var artist = _artistService.GetArtistById(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _artistService.DeleteArtist(id);
            return RedirectToAction("Index");
        }
    }
}
