using Microsoft.EntityFrameworkCore;
using Task_Management_API.Models;
using Task = Task_Management_API.Models.Task;

namespace Task_Management_API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
}