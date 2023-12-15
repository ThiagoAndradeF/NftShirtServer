namespace NftShirt.Server.Data.Entities;
public class Nftag
{
    public string LinkTag { get; set; } = string.Empty; //primaryKey
    public int ItenId { get; set; }
    public int ActiveId { get; set; }
    public virtual Iten Iten { get; set; } = default!;
    public string NftHash { get; set; }= string.Empty;
    public virtual Nft Nft { get; set; } = default!;
}