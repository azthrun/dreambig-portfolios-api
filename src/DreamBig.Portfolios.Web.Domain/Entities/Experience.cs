namespace DreamBig.Portfolios.Web.Domain.Entities;

/// <summary>
/// Experiences Table
/// </summary>
public partial class Experience : IEntity
{
    /// <summary>
    /// Profile ID
    /// </summary>
    public Guid ProfileId { get; set; }

    /// <summary>
    /// Company Name
    /// </summary>
    public string Company { get; set; } = null!;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Start Date
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End Date
    /// </summary>
    public DateTime? EndDate { get; set; }

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}
