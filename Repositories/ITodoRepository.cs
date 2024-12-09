public interface ITodosRepository
{
    IEnumerable<Todo> GetAll(); 
    Todo? GetById(int id); 
    void Add(Todo todo); 
    void Update(Todo todo); 
    void Delete(int id); 
    void Save(); 
}