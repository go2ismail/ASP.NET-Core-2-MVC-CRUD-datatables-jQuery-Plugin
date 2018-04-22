using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public IActionResult GetTodo()
        {
            return Json(new { data = _context.Todo.ToList() });
        }


        // POST: api/Todo
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (todo.todoId == 0)
            {
                _context.Todo.Add(todo);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Add new data success." });
            }
            else
            {
                _context.Update(todo);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Edit data success." });
            }

            

        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Todo.SingleOrDefaultAsync(m => m.todoId == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Delete success." });
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.todoId == id);
        }
    }
}