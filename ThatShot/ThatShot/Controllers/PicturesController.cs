using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThatShot.Data;
using ThatShot.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using ThatShot.Migrations;

namespace ThatShot.Controllers
{
    public class PicturesController : Controller
    {
        private readonly UserManager<TSUser> _userManager;

        private readonly IHostingEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public PicturesController(IHostingEnvironment IHostingEnvironment, ApplicationDbContext context, UserManager<TSUser> userManager)
        {
            _context = context;
            _environment = IHostingEnvironment;
            _userManager = userManager;
        }

        // GET: Pictures

        // Some code for practice

        //  List <Picture> pictures
        //  List<Genre> Genres=Genre.select(item => new Genre { Name= item.genre})

        public async Task<IActionResult> Index(string pictureGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Pictures
                                            orderby m.Genre
                                            select m.Genre;

            var pictures = from m in _context.Pictures where m.show == false
                           select m;



            if (!String.IsNullOrEmpty(searchString))
            {
                pictures = pictures.Where(s => s.Name.Contains(searchString));
                pictures = pictures.Where(s => s.Description.Contains(searchString));
                pictures = pictures.Where(s => s.User.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(pictureGenre))
            {
                pictures = pictures.Where(x => x.Genre == pictureGenre);
            }

            var movieGenreVM = new PictureGenreViewModel();
            movieGenreVM.Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            movieGenreVM.pictures = await pictures.ToListAsync();
            movieGenreVM.SearchString = searchString;

            return View(movieGenreVM);
        }

        // GET: All/Pictures admin

        // Some code for practice

        //  List <Picture> pictures
        //  List<Genre> Genres=Genre.select(item => new Genre { Name= item.genre})

        public async Task<IActionResult> AdminView(string pictureGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Pictures
                                            orderby m.Genre
                                            select m.Genre;

            var pictures = from m in _context.Pictures
                           select m;



            if (!String.IsNullOrEmpty(searchString))
            {
                pictures = pictures.Where(s => s.Name.Contains(searchString));
                pictures = pictures.Where(s => s.Description.Contains(searchString));
                pictures = pictures.Where(s => s.User.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(pictureGenre))
            {
                pictures = pictures.Where(x => x.Genre == pictureGenre);
            }

            var movieGenreVM = new PictureGenreViewModel();
            movieGenreVM.Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            movieGenreVM.pictures = await pictures.ToListAsync();
            movieGenreVM.SearchString = searchString;

            return View(movieGenreVM);
        }


        // GET: Pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // GET: Pictures/Create
        [Authorize/*(Roles = "Admin")*/]
        public IActionResult Create()
        {
            ViewBag.username = _userManager.GetUserName(HttpContext.User);

            if (User.Identity.Name == @ViewBag.username)
            { 
                return View();
            }
            return NotFound();
        }



        // GET: Pictures/Admin/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // POST: Pictures/Admin/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Admin(int id, [Bind("Id,File,Name,Description,User,Genre,show")] Picture picture)
        {
            ViewBag.username = _userManager.GetUserName(HttpContext.User);

            if (id != picture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(picture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PictureExists(picture.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(picture);
        }




        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,File,Name,Description,User,Genre")] Picture picture)
        {
            var newFileName = string.Empty;
            string pathDB = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + ".jpg";

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "demoImages") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        pathDB = "demoImages/" + newFileName;


                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                ViewBag.username = _userManager.GetUserName(HttpContext.User);
                picture.File = pathDB;
                picture.User = ViewBag.username;
                _context.Add(picture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(picture);
        }

        // GET: Pictures/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null )
            {
                return NotFound();
            }

            var picture = await _context.Pictures.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }
            
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,File,Name,Description,User,Genre")] Picture picture)
        {
            ViewBag.username = _userManager.GetUserName(HttpContext.User);

            if (id != picture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(picture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PictureExists(picture.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var picture = await _context.Pictures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (picture == null)
            {
                return NotFound();
            }

            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var picture = await _context.Pictures.FindAsync(id);
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        

        private bool PictureExists(int id)
        {
            return _context.Pictures.Any(e => e.Id == id);
        }
    }
}
