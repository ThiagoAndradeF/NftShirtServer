using System.Text.Json.Nodes;
using Microsoft.AspNetCore.SignalR.Protocol;
using NftShirt.Server.Data.Entities;

namespace  NftShirt.Server.Infra.Models;
public class NftWithCollectionDto{
    public string? TokenId { get; set; } = string.Empty;
    public string? TokenURI { get; set; } = string.Empty;
    public int? ColectionID { get; set; }
    public int? WalletID { get; set; }
    public string? Metadata { get; set; } = string.Empty;
    public CollectionDto? Collection {get;set;}
    public ContractDto ? ContractDto{get;set;}

}