using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        // Pre-populated in-memory list
        private static readonly List<ToDoItem> _todos = new()
        {
            new ToDoItem { Id = 1, Title = "Study .NET Web API", IsCompleted = false },
            new ToDoItem { Id = 2, Title = "Complete Week-4 Assignment", IsCompleted = false },
            new ToDoItem { Id = 3, Title = "Learn C#", IsCompleted = true }
        };
        private static int _nextId = 4;

        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetAll() => Ok(_todos);

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetById(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound("To-Do not found");
            return Ok(todo);
        }

        [HttpPost]
        public ActionResult<ToDoItem> Create(ToDoItem newTodo)
        {
            newTodo.Id = _nextId++;
            _todos.Add(newTodo);
            return CreatedAtAction(nameof(GetById), new { id = newTodo.Id }, newTodo);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDoItem updatedTodo)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound("To-Do not found");

            todo.Title = updatedTodo.Title;
            todo.IsCompleted = updatedTodo.IsCompleted;
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound("To-Do not found");

            _todos.Remove(todo);
            return NoContent();
        }
    }
}
