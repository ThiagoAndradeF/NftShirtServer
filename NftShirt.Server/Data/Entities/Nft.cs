using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NftShirt.Server.Data.Entities;

public class NFT
{
    [Key]
    public string NftHash { get; set; } = string.Empty;
    public int ColectionID { get; set; }
    public int WalletID { get; set; }
    public string Metadata { get; set; } = string.Empty;

    // Relacionamentos
    [ForeignKey("ColectionID")]
    public virtual Colection Colection { get; set; } = default!;

    [ForeignKey("WalletID")]
    public virtual Wallet Wallet { get; set; } = default!;

    // Relacionamento com NFTag
    public virtual ICollection<NFTag> NFTags { get; set; } = default!;
}