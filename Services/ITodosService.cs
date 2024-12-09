public interface ITodosService
{
    IEnumerable<Todo> GetTodos();           //Denne metoden henter alle gjøremål
    Todo? GetTodoById(int id);               //Henter ett gjøremål basert på ID 

    void AddTodo(Todo todo);                //Legger til gjøremål 

    void UpdateTodo(int id, Todo todo);     //Oppdaterer et eksisterende gjøremål 

    void DeleteTodo(int id);                //Sletter et gjøremål basert på ID 

    IEnumerable<Todo> GetTodosByCategory(int categoryId);
    int GetTodoCountByCategory(int categoryId);
    IEnumerable<Todo> GetCompletedTodosWithCategories();

}