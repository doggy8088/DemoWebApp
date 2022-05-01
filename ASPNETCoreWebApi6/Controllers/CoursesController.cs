#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreWebApi6.Models;

namespace ASPNETCoreWebApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ContosouniversityContext _context;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ContosouniversityContext context, 
            ILogger<CoursesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Courses
        [HttpGet(Name = nameof(GetCourseAll))]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseAll()
        {
            List<Course> courses = await _context.Course.AsNoTracking().ToListAsync();

            using (_logger.BeginScope("GetCourseAll"))
            {
                _logger.LogInformation("GetCourseAll: {CourseCount}", courses.Count);
            }

            return courses;
        }

        [HttpGet("~/test")]
        public ActionResult Test()
        {
            return Ok("Test");
        }

        [HttpGet("~/date/{*date:datetime}")]
        public ActionResult GetDate(DateTime date)
        {
            return Ok(date);
        }

        // Open Redirection
        [HttpGet("~/redirect/{*url}")]
        public ActionResult GoTo(string url)
        {
            // "/api/Course/1"
            // "https://xxx.com"
            return LocalRedirect(url);
        }

        // GET: api/Courses/5?name=Will
        [HttpGet("{id:int}", Name = nameof(GetCourseById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Course>> GetCourseById(int id, string name)
        {
            var course = await _context.Course.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
    }
}
