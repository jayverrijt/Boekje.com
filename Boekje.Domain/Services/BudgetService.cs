using Boekje.Domain.Entities;

namespace Boekje.Domain.Services;

public class BudgetService
{
    private readonly BudgetQueryService
        _queryService;

    private readonly BudgetCommandService
        _commandService;

    private readonly BudgetRuleEngine
        _ruleEngine;

    public BudgetService(
        BudgetQueryService queryService,
        BudgetCommandService commandService,
        BudgetRuleEngine ruleEngine)
    {
        _queryService =
            queryService;

        _commandService =
            commandService;

        _ruleEngine =
            ruleEngine;
    }

    /*
     * Queries
     */

    public Budget? GetByUser(
        int userId)
    {
        return _queryService
            .GetByUser(userId);
    }

    /*
     * Compatibility method
     */

    public Budget? GetFullBudget(
        int userId)
    {
        return _queryService
            .GetByUser(userId);
    }

    public bool HasBudget(
        int userId)
    {
        return _queryService
            .HasBudget(userId);
    }

    /*
     * Commands
     */

    public void Save(
        Budget budget)
    {
        _commandService
            .Save(budget);
    }

    /*
     * Compatibility overload
     */

    public void Save(
        Budget budget,
        Dictionary<string, decimal>
            categories)
    {
        _commandService
            .Save(budget);
    }

    public void Delete(
        int userId)
    {
        _commandService
            .Delete(userId);
    }

    /*
     * Rules / Advice
     */

    public BudgetAdvice GenerateAdvice(
        Budget budget)
    {
        return _ruleEngine
            .Generate(budget);
    }
}