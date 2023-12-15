using Microsoft.EntityFrameworkCore;
using NftShirt.Server.Data.Entities;
namespace NftShirt.Server.Data.DbContext;
public class MyDbContext : DbContext
{

    public DbSet<Iten> Itens { get; set; }
    public DbSet<NFTag> NFTags { get; set; }
    public DbSet<NFT> NFTs { get; set; }
    public DbSet<Colection> Colections { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Exemplo de configuração de relacionamento
        // modelBuilder.Entity<NFTag>()
        //     .HasKey(nt => new { nt.ItemId, nt.LinkTag });

        // modelBuilder.Entity<NFTag>()
        //     .HasRequired<Item>(nt => nt.Item)
        //     .WithMany(i => i.NFTags)
        //     .HasForeignKey(nt => nt.ItemId);

        // Adicione outras configurações de modelo aqui, se necessário

        base.OnModelCreating(modelBuilder);
    }
}
