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


namespace ThatShot.Controllers
{
    public class PicturesController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly ApplicationDbContext _context;


        public PicturesController(IHostingEnvironment IHostingEnvironment, ApplicationDbContext context)
        {
            _context = context;
            _environment = IHostingEnvironment;
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

            var pictures = from m in _context.Pictures
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                pictures = pictures.Where(s => s.Description.Contains(searchString));
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

        //public async Task<IActionResult> Index(string searchString)
        // {
        //    var pictures = from m in _context.Pictures
        //                   select m;

        //   if (!string.IsNullOrEmpty(searchString))
        //   {
        //        pictures = pictures.Where(s => s.Description.Contains(searchString));
        //}

        //   return View(await pictures.ToListAsync());
        //}

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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,Description,User,Genre")] Picture picture)
        {
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

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
                        newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "demoImages") + $@"\{newFileName}";

                        // if you want to store path of folder in database
                        PathDB = "demoImages/" + newFileName;


                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        //using (var target = new MemoryStream())
                        //{
                        //    file.CopyTo(target);
                        //    return target.ToArray();
                        //}
                    }
                }
              }
        if (ModelState.IsValid)
            {
                _context.Add(picture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,Description,User,Genre")] Picture picture)
        {
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
