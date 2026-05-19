using Boekje.Data.DTO;
using Boekje.Domain.Entities;

namespace Boekje.Data.Mappers;

public static class UserMapper
{
    public static User ToDomain(
        UserDto dto)
    {
        var user =
            new User(
                dto.Name,
                dto.Email,
                dto.PasswordHash);

        user.SetId(
            dto.Id);

        return user;
    }

    public static UserDto ToDto(
        User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PasswordHash =
                user.PasswordHash
        };
    }
}