using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    public class SortedController: Controller
    {
        private readonly MusicApiContext _context;

        public SortedController(MusicApiContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoutes.SortList.GetAll)]
        public ActionResult<IEnumerable<SortDetail>> GetAll()
        {
            return _context.SortDetails.ToList();
        }

        [HttpGet(ApiRoutes.SortList.GetByUser)]
        public ActionResult<IEnumerable<SortDetail>> GetByUser(int id)
        {
            var result = _context.SortDetails.Where(x => x.UserId == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound("No records");
            }
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.SortList.DeleteByUser)]
        public async Task<IActionResult> DeleteByUser(int id)
        {
            //var result = await _context.SortDetails.FindAsync(id);
            var result = _context.SortDetails.Where(x => x.UserId == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            _context.SortDetails.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete(ApiRoutes.SortList.Delete)]
        public async Task<IActionResult> DeleteSort(int id)
        {
            var result = await _context.SortDetails.FindAsync(id);
            //var result = _context.SortDetails.Where(x => x.UserId == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            _context.SortDetails.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost(ApiRoutes.SortList.Create)]
        public async Task<ActionResult<SortDetail>> PostSorted([FromBody] SortDetail sort)
        {
            _context.SortDetails.Add(sort);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAll", new { id = sort.Id }, sort);
        }

        [HttpPut(ApiRoutes.SortList.Update)]
        public async Task<IActionResult> PutSort(int id, [FromBody] SortDetail sort)
        {
            if (id != sort.Id)
            {
                return BadRequest();
            }

            _context.Entry(sort).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SortExists(id))
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

        [HttpPatch(ApiRoutes.SortList.PatchByUser)]
        public async Task<ActionResult> PatchByUser(int id, [FromBody] JsonPatchDocument<SortDetail> patchSort)
        {
            if (patchSort == null)
            {
                return BadRequest();
            }

            var sortFromDB = await _context.SortDetails.Where(x => x.UserId == id).FirstOrDefaultAsync();

            if (sortFromDB == null)
            {
                return NotFound();
            }

            patchSort.ApplyTo(sortFromDB, ModelState);

            var isValid = TryValidateModel(sortFromDB);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut(ApiRoutes.SortList.PutByUser)]
        public async Task<IActionResult> PutSortByUser(int id, [FromBody] SortDetail sort)
        {

            if (id != sort.Id)
            {
                return BadRequest();
            }

            //var sortToUpdate = await _context.SortDetails.Where(c => c.Id == id).FirstOrDefaultAsync();

            _context.Entry(sort).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SortExists(sort.Id))
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

        [HttpDelete(ApiRoutes.SortList.Delete)]
        public async Task<IActionResult> DeleteSort(long id)
        {
            var result = await _context.SortDetails.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.SortDetails.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SortExists(int id)
        {
            return _context.SortDetails.Any(e => e.Id == id);
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(m => m.CourseID == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }
        //    PopulateDepartmentsDropDownList(course.DepartmentID);
        //    return View(course);
        //}

        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditPost(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var courseToUpdate = await _context.Courses
        //        .FirstOrDefaultAsync(c => c.CourseID == id);

        //    if (await TryUpdateModelAsync<Course>(courseToUpdate,
        //        "",
        //        c => c.Credits, c => c.DepartmentID, c => c.Title))
        //    {
        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    PopulateDepartmentsDropDownList(courseToUpdate.DepartmentID);
        //    return View(courseToUpdate);
        //}

        //private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        //{
        //    var departmentsQuery = from d in _context.Departments
        //                           orderby d.Name
        //                           select d;
        //    ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        //}

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .Include(c => c.Department)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(m => m.CourseID == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(course);
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .Include(c => c.Department)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(m => m.CourseID == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(course);
        //}
    }
}
