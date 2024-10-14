using System.ComponentModel.DataAnnotations;

namespace Bonsai.Models;

public class FormToDo
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public required string Title { get; set; }

    [MaxLength(150)]
    public string? Description { get; set; }
    
    public string Difficulty { get; set; } = Models.Difficulty.Easy;
    public DateTime Date { get; set; } = DateTime.Today;
}