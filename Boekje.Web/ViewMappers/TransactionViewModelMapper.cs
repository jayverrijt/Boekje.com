using Boekje.Domain.Entities;
using Boekje.Web.ViewModels.Transaction;

namespace Boekje.Web.ViewMappers;

public static class TransactionViewModelMapper
{
    public static TransactionViewModel
        ToViewModel(
            Transaction transaction)
    {
        return new TransactionViewModel
        {
            Id =
                transaction.Id,

            Amount =
                transaction.Amount,

            Type =
                transaction.Type,

            Category =
                transaction.Category,

            Description =
                transaction.Description,

            CreatedAt =
                transaction.CreatedAt
        };
    }

    public static List<TransactionViewModel>
        ToViewModels(
            IEnumerable<Transaction>
                transactions)
    {
        return transactions
            .Select(ToViewModel)
            .ToList();
    }
}