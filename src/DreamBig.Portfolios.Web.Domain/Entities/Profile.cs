namespace DreamBig.Portfolios.Web.Domain.Entities;

public sealed class Profile : BaseEntity
{
    public string? OwnerName { get; set; }
    public string? Description { get; set; }
}