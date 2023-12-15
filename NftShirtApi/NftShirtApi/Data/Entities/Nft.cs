namespace NftShirt.Server.Data.Entities;
public class Nft
{
    public string NftHash { get; set; } = string.Empty;
    public int ColectionID { get; set; }
    public int WalletID { get; set; }
    public string Metadata { get; set; } = string.Empty;
    public virtual Colection Colection { get; set; } = default!;
    public virtual Wallet Wallet { get; set; } = default!;


    // Relacionamento com NFTag
    public virtual IEnumerable<Nftag> Nftags { get; set; } = default!;
}