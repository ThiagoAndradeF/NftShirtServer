namespace NftShirt.Server.Data.Entities;
public class Wallet
{
    public int Id { get; set; }
    public string Adress { get; set; } = string.Empty;
    public int UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public virtual IEnumerable<Nft> Nfts { get; set; } = default!;
}
