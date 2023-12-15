namespace NftShirt.Server.Data.Entities;
public class Colection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public virtual IEnumerable<Nft> Nfts { get; set; } = default!;
}