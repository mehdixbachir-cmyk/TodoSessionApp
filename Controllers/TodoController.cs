using Microsoft.AspNetCore.Mvc;
using TodoSessionApp.Models;

namespace TodoSessionApp.Controllers
{
    public class TodoController : Controller
    {
        private const string SessionKey = "Todos";

        private List<Todo> GetTodos()
        {
            var todosJson = HttpContext.Session.GetString(SessionKey);

            if (string.IsNullOrEmpty(todosJson))
            {
                return new List<Todo>();
            }

            return System.Text.Json.JsonSerializer.Deserialize<List<Todo>>(todosJson);
        }

        private void SaveTodos(List<Todo> todos)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(todos);
            HttpContext.Session.SetString(SessionKey, json);
        }

        public IActionResult Index()
        {
            var todos = GetTodos();
            return View(todos);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Todo todo)
        {
            var todos = GetTodos();
            todos.Add(todo);
            SaveTodos(todos);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var todos = GetTodos();
            todos.RemoveAt(id);
            SaveTodos(todos);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var todos = GetTodos();
            var todo = todos[id];
            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(int id, Todo updated)
        {
            var todos = GetTodos();
            todos[id] = updated;
            SaveTodos(todos);
            return RedirectToAction("Index");
        }
    }
}
