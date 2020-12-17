using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Contracts;
using MusicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    public class AlbumController: Controller
    {
        private readonly MusicApiContext _context;

        public AlbumController(MusicApiContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoutes.AlbumList.GetAll)]
        public ActionResult<IEnumerable<AlbumDetail>> GetAll()
        {
            //return Ok(new { name = "German" });
            return _context.AlbumDetails.ToList();
        }

        [HttpGet(ApiRoutes.AlbumList.GetBy)]
        public ActionResult<IEnumerable<AlbumDetail>> Get(string id)
        {
            //return Ok(new { name = "German" });
            var result =  _context.AlbumDetails.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost(ApiRoutes.AlbumList.Create)]
        public async Task<ActionResult<AlbumDetail>> PostAlbum([FromBody] AlbumDetail album)
        {
            _context.AlbumDetails.Add(album);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = album.Id }, album);
        }

        [HttpPut(ApiRoutes.AlbumList.Update)]
        public async Task<IActionResult> PutAlbum(int id, [FromBody] AlbumDetail album)
        {
            if (id != album.Id)
            {
                return BadRequest("Cannot update a not existing term.");
            }

            _context.Entry(album).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Success");
        }

        [HttpDelete(ApiRoutes.AlbumList.Delete)]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await _context.AlbumDetails.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _context.AlbumDetails.Remove(result);
            await _context.SaveChangesAsync();

            //return NoContent();
            return Ok("Success");
        }

        private bool AlbumExists(int id)
        {
            return _context.AlbumDetails.Any(e => e.Id == id);
        }
    }
}
