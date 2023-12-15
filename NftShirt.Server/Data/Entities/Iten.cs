using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NftShirt.Server.Data.Entities;

public class Iten
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    // Relacionamento com NFTag
    public virtual ICollection<NFTag> NFTags { get; set; } = default!;
}