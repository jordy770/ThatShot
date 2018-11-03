using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ThatShot.Models
{
    public class PictureGenreViewModel
    {
        public List<Picture> pictures;
        public SelectList Genres;
        public string PictureGenre { get; set; }

        public string SearchString { get; set; }
    }
}