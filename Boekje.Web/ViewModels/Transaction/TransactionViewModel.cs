namespace Boekje.Web.ViewModels.Transaction;

public class TransactionViewModel
{
    public int Id { get; set; }

    public decimal Amount
    {
        get;
        set;
    }

    public string Type
    {
        get;
        set;
    } = string.Empty;

    public string Category
    {
        get;
        set;
    } = string.Empty;

    public string? Description
    {
        get;
        set;
    }

    public DateTime CreatedAt
    {
        get;
        set;
    }
}