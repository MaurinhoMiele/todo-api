using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { 
    }
    public DbSet<Tarefa> Tarefas { get; set; }

}
