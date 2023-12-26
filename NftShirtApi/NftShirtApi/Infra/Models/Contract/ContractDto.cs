using System.Text.Json.Nodes;

namespace  NftShirt.Server.Infra.Models;
public class ContractDto{
    public string Adress { get; set; } = string.Empty;
    public JsonArray Abi { get; set; } = default!;
}