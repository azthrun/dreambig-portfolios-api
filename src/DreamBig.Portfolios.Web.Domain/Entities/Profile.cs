﻿namespace DreamBig.Portfolios.Web.Domain.Entities;

/// <summary>
/// Profiles Table
/// </summary>
public class Profile : IEntity
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// About Me
    /// </summary>
    public string? AboutMe { get; set; }

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}
