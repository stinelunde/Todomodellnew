/* using Serilog;

public class TodosService : ITodosService
{
    private static List<Todo> todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Vaske huset", Discription = "Vaske hele huset nøye før bursdag.", IsCompleted = true},
            new Todo { Id = 2, Title = "Handle til helga", Discription = "taco fredag, lasagne lørdag og tomatsuppe på søndag.", IsCompleted = false}
        };
    
    public IEnumerable<Todo> GetTodos() => todos;       //henter alle gjøremål, lambda-utrykk, pilen betyr det samme som return books

    public Todo? GetTodoById(int id) => todos.FirstOrDefault(t => t.Id == id); //?? new Todo(); 

    public void AddTodo(Todo todo){
        todo.Id = todos.Max(t => t.Id) + 1;         //legger til gjøremål, og genererer ny ID basert på maks id fra todo
        todo.IsCompleted = false;                   //sørger for at IsCompleted alltid er false
        todos.Add(todo); 
        Log.Information($"Gjøremål er lagt til, {todo.Title}");
    }

    public void UpdateTodo(int id, Todo updatedTodo)
    {
        var todo = GetTodoById(id);             //oppdaterer todo, hvis todo er null - return 
        if (todo == null) return; 

        todo.Title = updatedTodo.Title;
        todo.Discription = updatedTodo.Discription;
        todo.IsCompleted = false;               //tvinger IsCompleted til å være false. 
        Log.Information($"Gjøremål {todo.Title} er oppdatert."); 
    }

    public void DeleteTodo(int id)          //sletter gjøremål basert på ID 
    {
        var todo = GetTodoById(id); 
        if(todo != null) todos.Remove(todo);
    }
}
*/ 

using TodomodellNew.Data;
using Serilog;

public class TodosService : ITodosService
{
//    private readonly AppDbContext _context;
    private readonly ITodosRepository _repository; 

    public TodosService(ITodosRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Todo> GetTodos() => _repository.GetAll();

    public Todo? GetTodoById(int id) => _repository.GetById(id);

    public void AddTodo(Todo todo)
    {
        todo.IsCompleted = false; // Sørger for at IsCompleted alltid er false
        _repository.Add(todo);
        _repository.Save(); 
        Log.Information($"Gjøremål er lagt til, {todo.Title}");
    }

    public void UpdateTodo(int id, Todo updatedTodo)
    {
        var todo = _repository.GetById(id);
        if (todo == null) return;

        todo.Title = updatedTodo.Title;
        todo.Discription = updatedTodo.Discription;
        todo.IsCompleted = updatedTodo.IsCompleted;

        _repository.Update(todo);
        _repository.Save(); 
        Log.Information($"Gjøremål {todo.Title} er oppdatert.");
    }

    public void DeleteTodo(int id)
    {
        _repository.Delete(id);
        _repository.Save(); 
    }

    public IEnumerable<Todo> GetTodosByCategory(int categoryId) =>
        _repository.GetAll().Where(t => t.CategoryId == categoryId);

    public int GetTodoCountByCategory(int categoryId) =>
        _repository.GetAll().Count(t => t.CategoryId == categoryId);

    public IEnumerable<Todo> GetCompletedTodosWithCategories() =>
        _repository.GetAll()
        .Where(t => t.IsCompleted)
        .ToList();

}