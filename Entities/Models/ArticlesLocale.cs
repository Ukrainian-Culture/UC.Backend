﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ArticlesLocale
{
    public int Id { get; init; }
    [ForeignKey(nameof(Culture))] public int CultureId { get; init; }
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;


    public Culture Culture { get; set; } = null!;
}