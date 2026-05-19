using Boekje.Data.DTO;
using Boekje.Domain.Entities;

namespace Boekje.Data.Mappers;

public static class TransactionMapper
{
    public static Transaction ToDomain(
        TransactionDto dto)
    {
        var transaction =
            new Transaction(
                dto.UserId,
                dto.Amount,
                dto.Type,
                dto.Category,
                dto.Description);

        transaction.SetId(
            dto.Id);

        transaction.SetCreatedAt(
            dto.CreatedAt);

        return transaction;
    }

    public static TransactionDto ToDto(
        Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            UserId = transaction.UserId,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Category = transaction.Category,
            Description =
                transaction.Description,
            CreatedAt =
                transaction.CreatedAt
        };
    }
}