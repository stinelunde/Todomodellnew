using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

public class Category
{
    public int Id {get; set;}

    [Required]
    public string Name {get; set;}

    public ICollection<Todo> Todos {get; set;} = new List<Todo>(); 
}