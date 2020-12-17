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
    public class SongsPlatlistController: Controller
    {
        private readonly MusicApiContext _context;

        public SongsPlatlistController(MusicApiContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoutes.SongsPlayList.GetAll)]
        public ActionResult<IEnumerable<SongsPlatlistDetail>> GetAll()
        {
            return _context.SongsPlatlistDetails.ToList();
        }

        [HttpGet(ApiRoutes.SongsPlayList.GetBy)]
        public ActionResult<IEnumerable<SongsPlatlistDetail>> GetBy(string id)
        {
            var result = _context.SongsPlatlistDetails.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet(ApiRoutes.SongsPlayList.GetByPlaylist)]
        public ActionResult<IEnumerable<SongsPlatlistDetail>> GetByPlaylist(int id)
        {
            var result = _context.SongsPlatlistDetails.Where(x => x.Playlist == id).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet(ApiRoutes.SongsPlayList.GetByUser)]
        public ActionResult<IEnumerable<SongsPlatlistDetail>> GetByUser(int id)
        {
            var result = new List<SongsPlatlistDetail>();
            var playlist = _context.PlaylistDetails.Where(x => x.UserId == id).ToList();
            foreach (var item in playlist)
            {
                var song = _context.SongsPlatlistDetails.Where(x => x.Playlist == item.Id).ToList();
                foreach (var o in song)
                {
                    result.Add(o);
                }
            }
            //var result = _context.SongsPlatlistDetails.Where(x => x.Id == id).ToList();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPost(ApiRoutes.SongsPlayList.Create)]
        public async Task<ActionResult<SongsPlatlistDetail>> PostSongPlatlist([FromBody] SongsPlatlistDetail song)
        {
            _context.SongsPlatlistDetails.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = song.Id }, song);
        }

        [HttpPut(ApiRoutes.SongsPlayList.Update)]
        public async Task<IActionResult> PutSongPlatlist(int id, [FromBody] SongsPlatlistDetail song)
        {
            if (id != song.Id)
            {
                return BadRequest("Cannot update a not existing term.");
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongsPlatlistExists(id))
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

        [HttpDelete(ApiRoutes.SongsPlayList.DeleteBySong)]
        public async Task<IActionResult> DeleteBySong(int id)
        {
            var result = await _context.SongsPlatlistDetails.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _context.SongsPlatlistDetails.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete(ApiRoutes.SongsPlayList.DeleteByPlaylist)]
        public async Task<IActionResult> DeleteByPlaylist(int id)
        {
            //var result = await _context.SongsPlatlistDetails.FindAsync(id);
            var result = _context.SongsPlatlistDetails.Where(x => x.Playlist == id).ToList();
            if (result == null)
            {
                return NotFound();
            }
            foreach (var p in result)
            {
                _context.SongsPlatlistDetails.Remove(p);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool SongsPlatlistExists(int id)
        {
            return _context.SongsPlatlistDetails.Any(e => e.Id == id);
        }
    }
}
