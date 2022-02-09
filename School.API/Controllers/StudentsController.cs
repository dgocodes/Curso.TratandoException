#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.API.Context;
using School.API.Exceptions;
using School.API.Exceptions.Filters;
using School.API.Models;
using School.API.ViewModels;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        private readonly AppDBContext _context;

        public StudentsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        [CustomExceptionFilter]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            throw new Exception("Testando o CustomExceptionFilter");
            return await CreateResponse(async () => await _context.Students.ToListAsync());
        }


        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(CreateStudentViewModel student)
        {
            try
            {
                var studentByEmail = _context.Students.FirstOrDefault(x => x.Email == student.Email);

                if (studentByEmail != null)
                    throw new EmailAlreadyRegistredException("E-mail already registred", student.Email);

                if (student.Age <= 5)
                    throw new MinimumAgeException($"Age invalid", student.Age);

                var newStudent = student.FromModel();

                _context.Students.Add(newStudent);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetStudent", new { id = newStudent.Id }, newStudent);
            }
            catch (EmailAlreadyRegistredException ex)
            {
                return BadRequest($"{ex.Email} already registred.");
            }
            catch (MinimumAgeException ex)
            {
                return BadRequest($"Age must be greater than 5 - current age {ex.Age}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
