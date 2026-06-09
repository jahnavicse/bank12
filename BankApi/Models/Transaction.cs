
public class Transaction
{
    public int Id { get; set; }

    public required string Type { get; set; }

    public required string Category { get; set; }

    public required string Description { get; set; }

    public required string UserId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}

