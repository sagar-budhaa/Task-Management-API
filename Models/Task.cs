using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.Models;

public class Task
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime created_at { get; set; } = DateTime.UtcNow;
    public DateTime updated_at { get; set; } = DateTime.UtcNow;
    
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
}