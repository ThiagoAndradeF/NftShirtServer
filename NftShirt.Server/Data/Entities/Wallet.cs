using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NftShirt.Server.Data.Entities;

public class Wallet
{
    [Key]
    public int Id { get; set; }
    public string Adress { get; set; } = string.Empty;
    public int UserId { get; set; }

    // Chave estrangeira para User
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = default!;

    // Relacionamento com NFT
    public virtual ICollection<NFT> NFTs { get; set; } = default!;
}
