namespace NftShirt.Server.Data.Entities;
public class Contract
{
    public string Adress { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string ABI { get; set; } = string.Empty;
    public IEnumerable<Colection> Colections = default!;
}