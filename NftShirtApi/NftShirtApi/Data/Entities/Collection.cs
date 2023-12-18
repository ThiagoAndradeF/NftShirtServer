namespace NftShirt.Server.Data.Entities;
public class Collection
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ContractId {get;set;} = string.Empty;
    public Contract Contract { get;set; } = default!;
    public List<Nft> Nfts { get; set; } = default!;
}