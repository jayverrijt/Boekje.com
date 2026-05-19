namespace Boekje.Data.DTO;

public class TransactionDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public string Type { get; set; }
        = string.Empty;

    public string Category { get; set; }
        = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}