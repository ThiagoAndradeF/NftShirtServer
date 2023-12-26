using System.Text.Json.Nodes;
using Microsoft.AspNetCore.SignalR.Protocol;
using NftShirt.Server.Data.Entities;

namespace  NftShirt.Server.Infra.Models;
public class NftWithCollectionDto{
    public NftDto? Nft { get; set; }
    public CollectionDto? Collection {get;set;}
    public ContractDto ? Contract{get;set;}

}