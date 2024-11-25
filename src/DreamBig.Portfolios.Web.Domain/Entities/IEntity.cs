namespace DreamBig.Portfolios.Web.Domain.Entities;

public interface IEntity
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Create Time
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// Deleted Flag
    /// </summary>
    public bool Deleted { get; set; }

    /// <summary>
    /// Delete Time
    /// </summary>
    public DateTime? DeleteTime { get; set; }
}