using Boekje.Data.DTO;
using Boekje.Domain.Entities;

namespace Boekje.Data.Mappers;

public static class SavingMapper
{
    public static Saving ToDomain(
        SavingDto dto)
    {
        return new Saving(
            dto.Amount,
            dto.Name);
    }

    public static SavingDto ToDto(
        Saving saving,
        int budgetId)
    {
        return new SavingDto
        {
            BudgetId = budgetId,
            Amount = saving.Amount,
            Name = saving.Name
        };
    }
}