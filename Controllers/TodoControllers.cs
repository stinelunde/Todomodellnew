using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TodomodellNew.Data;
//using TodomodellNew; 

namespace todomodell.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/todos - Henter alle todo-oppgaver
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            var todos = _context.Todos.ToList();
            return Ok(todos);
        }

        // GET: api/todos/{id} - Henter én todo-oppgave basert på ID
        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodoById(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                Log.Information($"Todo med ID {id} ble ikke funnet.");
                return NotFound();
            }
            return Ok(todo);
        }

        // POST: api/todos - Oppretter en ny todo-oppgave
        [HttpPost]
        public ActionResult<Todo> CreateTodo(Todo newTodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Todos.Add(newTodo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.Id }, newTodo);
        }

        // PUT: api/todos/{id} - Oppdaterer en eksisterende todo-oppgave
        [HttpPut("{id}")]
        public ActionResult UpdateTodo(int id, Todo updatedTodo)
        {
            if (id != updatedTodo.Id)
            {
                return BadRequest("ID i forespørsel matcher ikke ID i dataobjektet.");
            }

            var existingTodo = _context.Todos.Find(id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            existingTodo.Title = updatedTodo.Title;
            existingTodo.IsCompleted = updatedTodo.IsCompleted;

            _context.Entry(existingTodo).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/todos/{id} - Sletter en todo-oppgave
        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}