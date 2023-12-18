using System.Text.Json.Nodes;

namespace  NftShirt.Server.Infra.Models;
public class CollectionCreateDto{
    public string Adress { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public JsonArray Abi { get; set; } = default!;

}