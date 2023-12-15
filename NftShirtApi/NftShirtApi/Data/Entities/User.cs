namespace NftShirt.Server.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public virtual IEnumerable<Wallet> Wallets { get; set; } = default!;
}