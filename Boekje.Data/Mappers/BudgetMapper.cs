using Boekje.Data.DTO;
using Boekje.Domain.Entities;

namespace Boekje.Data.Mappers;

public static class BudgetMapper
{
    public static Budget ToDomain(
        BudgetDto dto)
    {
        var budget =
            new Budget(
                dto.UserId,
                dto.Income);

        budget.SetId(
            dto.Id);

        return budget;
    }

    public static BudgetDto ToDto(
        Budget budget)
    {
        return new BudgetDto
        {
            Id = budget.Id,
            UserId = budget.UserId,
            Income = budget.Income
        };
    }
}