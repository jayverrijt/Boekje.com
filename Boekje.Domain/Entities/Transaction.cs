namespace Boekje.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = "";
    public string Category { get; set; } = "";
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}