namespace DreamBig.Portfolios.Web.Domain.Entities;

/// <summary>
/// Contacts Table
/// </summary>
public partial class Contact : IEntity
{
    /// <summary>
    /// Contact Type ID
    /// </summary>
    public Guid ContactTypeId { get; set; }

    /// <summary>
    /// URL Link
    /// </summary>
    public string Link { get; set; } = null!;

    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public bool Deleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}
