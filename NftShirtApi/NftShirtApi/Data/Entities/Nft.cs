namespace NftShirt.Server.Data.Entities;
public class Nft
{
    public string TokenId { get; set; } = string.Empty;
    public string TokenURI { get; set; } = string.Empty;
    public string OwnerAddress { get; set; } = string.Empty;
    public int ColectionID { get; set; }
    public int WalletID { get; set; }
    public string Metadata { get; set; } = string.Empty;
    public virtual Collection Collection { get; set; } = default!;
    public virtual Wallet Wallet { get; set; } = default!;
    // Relacionamento com NFTag
    public virtual IEnumerable<Nftag> Nftags { get; set; } = default!;
}