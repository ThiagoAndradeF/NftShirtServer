using System;
namespace NftShirt.Server.Data.Entities;

public class Iten
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
  
    // Relacionamento com NFTag
    public virtual ICollection<NFTag> NFTags { get; set; } = default!;
}