namespace DreamBig.Portfolios.Web.Domain.Entities;

/// <summary>
/// Posts Table
/// </summary>
public partial class Post : IEntity
{
    /// <summary>
    /// Profile ID
    /// </summary>
    public Guid ProfileId { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// URL Link
    /// </summary>
    public string Link { get; set; } = null!;

    /// <summary>
    /// Added Date
    /// </summary>
    public DateTime AddedDate { get; set; }

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}
