namespace Boekje.Domain.Entities;

public class BudgetAdvice
{
    private readonly List<string> _warnings = new();

    public decimal RecommendedMaxRent { get; private set; }

    public decimal RecommendedMinimumSavings { get; private set; }

    public IReadOnlyList<string> Warnings
        => _warnings;

    public BudgetAdvice(
        decimal recommendedMaxRent,
        decimal recommendedMinimumSavings)
    {
        if (recommendedMaxRent < 0)
        {
            throw new Exception(
                "Recommended rent cannot be negative.");
        }

        if (recommendedMinimumSavings < 0)
        {
            throw new Exception(
                "Recommended savings cannot be negative.");
        }

        RecommendedMaxRent =
            recommendedMaxRent;

        RecommendedMinimumSavings =
            recommendedMinimumSavings;
    }

    public void AddWarning(string warning)
    {
        if (string.IsNullOrWhiteSpace(warning))
        {
            throw new Exception(
                "Warning cannot be empty.");
        }

        _warnings.Add(warning);
    }

    public bool HasWarnings()
    {
        return _warnings.Count > 0;
    }
}