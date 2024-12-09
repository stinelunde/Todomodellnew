namespace TodomodellNew.Data;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : DbContext
{

    public DbSet<Todo> Todos { get; set; }
    public DbSet<Category> Categories {get; set;}
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder builder)
    /*
    {
        builder.Entity<Todo>()
            .ToTable("todos") // Sikrer samsvar mellom navn i migrasjon og kode
            .HasData(
            new Todo {Id = 3, Title = "Carwash", Discription = "with enginewash, before sale", IsCompleted = false},
            new Todo {Id = 4, Title = "Christmasgift shopping", Discription = "til onkel, bestemor & bestefar, og søskenbarn", IsCompleted = false},
            new Todo {Id = 5, Title = "Pus til dyrlegen", Discription = "Skal til dyrlegen mandag 16. 12", IsCompleted = false}
        );
    }
    */ 

    {
    builder.Entity<Todo>()
        .HasOne(t => t.Category)
        .WithMany(c => c.Todos)
        .HasForeignKey(t => t.CategoryId);

    // Seed data for categories
    builder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Husarbeid" },
        new Category { Id = 2, Name = "Shopping" }
    );

    // Seed data for Todos
    builder.Entity<Todo>().HasData(
        new Todo { Id = 3, Title = "Vaske bilen", Discription = "Vask grundig", IsCompleted = false, CategoryId = 1 },
        new Todo { Id = 4, Title = "Handle gaver", Discription = "Kjøp julegaver", IsCompleted = false, CategoryId = 2 }
    );
    }
}

