using System.Text.Json.Serialization;
using DreamBig.Portfolios.Web.Domain.Entities;

[JsonSerializable(typeof(Contact))]
[JsonSerializable(typeof(ContactType))]
[JsonSerializable(typeof(Experience))]
[JsonSerializable(typeof(Post))]
[JsonSerializable(typeof(Profile))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}