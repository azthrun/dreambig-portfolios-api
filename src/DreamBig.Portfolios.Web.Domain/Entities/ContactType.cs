namespace DreamBig.Portfolios.Web.Domain.Entities;

/// <summary>
/// ContactTypes Table
/// </summary>
public partial class ContactType : IEntity
{
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = null!;

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}
