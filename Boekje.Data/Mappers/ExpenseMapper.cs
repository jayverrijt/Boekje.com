using Boekje.Data.DTO;
using Boekje.Domain.Entities;

namespace Boekje.Data.Mappers;

public static class ExpenseMapper
{
    public static Expense ToDomain(
        ExpenseDto dto)
    {
        return new Expense(
            dto.Type,
            dto.Amount,
            dto.Name);
    }

    public static ExpenseDto ToDto(
        Expense expense,
        int budgetId)
    {
        return new ExpenseDto
        {
            BudgetId = budgetId,
            Type = expense.Type,
            Amount = expense.Amount,
            Name = expense.Name
        };
    }
}