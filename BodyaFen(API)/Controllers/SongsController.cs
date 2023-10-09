using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BodyaFen_API_.Contexts;
using BodyaFen_API_.Models;
using Microsoft.AspNetCore.OData.Query;

namespace BodyaFen_API_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly BodyaFenDbContext _context;

        public SongsController(BodyaFenDbContext context)
        {
            _context = context;
        }

        [HttpGet("odata/Songs")]
        [EnableQuery]
        public IActionResult GetSongsOData()
        {
            return Ok(_context.Songs);
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
          if (_context.Songs == null)
          {
              return NotFound();
          }
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<SongDto>> Create(SongCreationDto songCreationDto)
        {
            if (songCreationDto == null)
            {
                return BadRequest();
            }

            var artist = await _context.Artists.FindAsync(songCreationDto.ArtistId);
            var genre = await _context.Genres.FindAsync(songCreationDto.GenreId);

            if (artist == null || genre == null)
            {
                return NotFound("Artist or Genre not found");
            }

            var song = new Song
            {
                Name = songCreationDto.Name,
                Artist = artist,
                Genre = genre
            };

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            var songDto = new SongDto
            {
                Id = song.Id,
                Name = song.Name,
                ArtistName = artist.Name,
                GenreName = genre.Name
            };

            return CreatedAtAction(nameof(GetSong), new { id = songDto.Id }, songDto);
        }



        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
