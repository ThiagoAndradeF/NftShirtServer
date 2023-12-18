using System.Text.Json.Nodes;
using NftShirt.Server.Data.Entities;

namespace  NftShirt.Server.Infra.Models;
public class CollectionCreateDto{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ContractCreateDto Contract { get;set; } = default!;

}