using TodomodellNew.Data; 

public class TodoRepository : ITodosRepository
{
    private readonly AppDbContext _context; 

    public TodoRepository(AppDbContext context)
    {
        _context = context; 
    }


    public IEnumerable<Todo> GetAll() => _context.Todos.ToList(); 

    public Todo? GetById(int id) => _context.Todos.Find(id); 

    public void Add(Todo todo) 
    {
        _context.Todos.Add(todo); 
    }

    public void Update(Todo todo)
    {
        _context.Todos.Update(todo); 
    }

    public void Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo != null)
            _context.Todos.Remove(todo);
    }

    public void Save() => _context.SaveChanges(); 
}