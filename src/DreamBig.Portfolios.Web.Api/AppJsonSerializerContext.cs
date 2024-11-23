using System.Text.Json.Serialization;
using DreamBig.Portfolios.Web.Application.Profile.Dtos;

[JsonSerializable(typeof(ProfileDto))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}