using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NftShirt.Server.Data.Entities;
public class NFTag
{
    [Key, Column(Order = 0)]
    public int ItemId { get; set; }

    [Key, Column(Order = 1)]
    public string LinkTag { get; set; } = string.Empty;
    public string NftHash { get; set; }= string.Empty;
    public int ActiveId { get; set; }

    // Chave estrangeira para Item
    [ForeignKey("ItemId")]
    public virtual Iten Iten { get; set; } = default!;

    // Chave estrangeira para NFT
    [ForeignKey("NftHash")]
    public virtual NFT NFT { get; set; } = default!;
}