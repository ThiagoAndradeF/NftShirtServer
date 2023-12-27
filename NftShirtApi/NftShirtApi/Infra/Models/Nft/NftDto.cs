using System.Text.Json.Nodes;
using NftShirt.Server.Data.Entities;

namespace  NftShirt.Server.Infra.Models;
public class NftDto{
    public string? TokenId { get; set; } = string.Empty;
    public string? TokenUri { get; set; } = string.Empty;
    public int? ColectionId { get; set; }
    public int? WalletId { get; set; }
}