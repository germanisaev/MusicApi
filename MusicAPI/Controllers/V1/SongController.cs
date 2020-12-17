using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    class Item
    {
        public int Id;
        public string Name;
    }

    class ItemEqualityComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            // Two items are equal if their keys are equal.
            return x.Name == y.Name;
        }

        public int GetHashCode(Item obj)
        {
            return obj.Name.GetHashCode();
        }
    }

    public class SongController: Controller
    {
        private readonly MusicApiContext _context;

        public SongController(MusicApiContext context)
        {
            _context = context;
        }

        private List<T> RemoveDuplicatesSet<T>(List<T> items)
        {
            // Use HashSet to remember items seen.
            var result = new List<T>();
            var set = new HashSet<T>();
            for (int i = 0; i < items.Count; i++)
            {
                // Add if needed.
                if (!set.Contains(items[i]))
                {
                    result.Add(items[i]);
                    set.Add(items[i]);
                }
            }
            return result;
        }

        

        [HttpGet(ApiRoutes.SongList.GetAll)]
        public ActionResult<IEnumerable<SongDetail>> GetAll()
        {
            return _context.SongDetails.ToList();
        }

        [HttpGet(ApiRoutes.SongList.GetBy)]
        public ActionResult<IEnumerable<SongDetail>> Get(string id)
        {
            var result = _context.SongDetails.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet(ApiRoutes.SongList.GetByAlbum)]
        public ActionResult<IEnumerable<SongDetail>> GetByAlbum(int id)
        {
            var result = _context.SongDetails.Where(x => x.Album == id).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet(ApiRoutes.SortList.GetArtists)]
        public ActionResult<IEnumerable<SongDetail>> GetArtists()
        {
            //var result = _context.SongDetails.AsEnumerable().Select(r => r.Name).ToList();
            var result = new List<object>();
            
            var list = _context.SongDetails.AsEnumerable().Select(x => new { x.Id, x.Name }).ToList();
            
            for (int i = 0; i < list.Count; i++)
            {
                bool duplicate = false;
                for (int z = 0; z < i; z++)
                {
                    if (list[z].Name == list[i].Name)
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate)
                {
                    result.Add(list[i]);
                }
            }
            
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet(ApiRoutes.SortList.GetCreateds)]
        public ActionResult<IEnumerable<SongDetail>> GetCreateds(string name)
        {
            var result = _context.SongDetails.AsEnumerable().Where(x => x.Name == name).Select(r => r.Created).ToList();
    //        var dataset = entities.processlists
    //.Where(x => x.environmentID == environmentid && x.ProcessName == processname && x.RemoteIP == remoteip && x.CommandLine == commandlinepart)
    //.Select(x => new { x.ServerName, x.ProcessID, x.Username }).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost(ApiRoutes.SongList.Create)]
        public async Task<ActionResult<SongDetail>> PostAlbum([FromBody] SongDetail song)
        {
            _context.SongDetails.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = song.Id }, song);
        }

        [HttpPut(ApiRoutes.SongList.Update)]
        public async Task<IActionResult> PutSong(int id, [FromBody] SongDetail song)
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
                if (!SongExists(id))
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

        [HttpDelete(ApiRoutes.SongList.Delete)]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var result = await _context.SongDetails.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _context.SongDetails.Remove(result);
            await _context.SaveChangesAsync();

            //return NoContent();
            return Ok("Success");
        }

        private bool SongExists(int id)
        {
            return _context.SongDetails.Any(e => e.Id == id);
        }
    }
}
