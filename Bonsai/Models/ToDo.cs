﻿using SQLite;

namespace Bonsai.Models;

[Table("ToDos")]
public class ToDo
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    
    public bool IsCompleted { get; set; }

    [MaxLength(50)]
    public string Title { get; set; } = "";

    [MaxLength(150)]
    public string? Description { get; set; }
    
    public string Difficulty { get; set; } = Models.Difficulty.Easy;

    public DateTime Date { get; set; } = DateTime.Today;
}