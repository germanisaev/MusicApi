using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    public class PlaylistController: Controller
    {
        private readonly MusicApiContext _context;

        public PlaylistController(MusicApiContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoutes.PlayList.GetAll)]
        public ActionResult<IEnumerable<PlaylistDetail>> GetAll()
        {
            //return Ok(new { name = "German" });
            return _context.PlaylistDetails.ToList();
        }

        [HttpGet(ApiRoutes.PlayList.GetBy)]
        public ActionResult<IEnumerable<PlaylistDetail>> Get(int id)
        {
            //return Ok(new { name = "German" });
            var result = _context.PlaylistDetails.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet(ApiRoutes.PlayList.GetByUser)]
        public ActionResult<IEnumerable<PlaylistDetail>> GetByUser(int id)
        {
            //return Ok(new { name = "German" });
            var result = _context.PlaylistDetails.Where(x => x.UserId == id).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost(ApiRoutes.PlayList.Create)]
        public async Task<ActionResult<PlaylistDetail>> PostPlaylist([FromBody] PlaylistDetail playlist)
        {
            _context.PlaylistDetails.Add(playlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = playlist.Id }, playlist);
        }

        [HttpPut(ApiRoutes.PlayList.Update)]
        public async Task<IActionResult> PutPlaylist(int id, [FromBody] PlaylistDetail playlist)
        {
            if (id != playlist.Id)
            {
                return BadRequest("Cannot update a not existing term.");
            }

            _context.Entry(playlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
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

        [HttpDelete(ApiRoutes.PlayList.Delete)]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var result = await _context.PlaylistDetails.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _context.PlaylistDetails.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaylistExists(int id)
        {
            return _context.PlaylistDetails.Any(e => e.Id == id);
        }
    }
}
