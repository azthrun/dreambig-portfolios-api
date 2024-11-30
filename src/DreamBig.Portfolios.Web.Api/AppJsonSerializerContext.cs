using System.Text.Json.Serialization;
using DreamBig.Portfolios.Web.Application.Post.Dtos;
using DreamBig.Portfolios.Web.Application.Profile.Dtos;
using DreamBig.Portfolios.Web.Domain.Entities;
using Mediator;

[JsonSerializable(typeof(Contact))]
[JsonSerializable(typeof(ContactType))]
[JsonSerializable(typeof(Experience))]
[JsonSerializable(typeof(Post))]
[JsonSerializable(typeof(Profile))]
[JsonSerializable(typeof(ContactDto))]
[JsonSerializable(typeof(ExperienceDto))]
[JsonSerializable(typeof(ProfileDto))]
[JsonSerializable(typeof(PostDto[]))]
[JsonSerializable(typeof(IMediator))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}