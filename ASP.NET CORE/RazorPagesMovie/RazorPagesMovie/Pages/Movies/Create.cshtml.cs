﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class CreateModel : GenreNamePageModel
    {
        private readonly Data.RazorPagesMovieContext _context;

        public CreateModel(Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public IActionResult OnGet()
        {
            PopelateGenresDropDownList(_context);

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyMovie = new Movie();

            if (await TryUpdateModelAsync<Movie>
                (
                    emptyMovie,
                    "movie",
                    s => s.ID,
                    s => s.GenreId,
                    s => s.Genre,
                    s => s.Title,
                    s => s.ReleaseDate,
                    s => s.Price,
                    s => s.Rating
                ))
            {
                _context.Movie.Add(emptyMovie);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopelateGenresDropDownList(_context);
            return Page();
        }
    }
}
