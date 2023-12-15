using Microsoft.EntityFrameworkCore;
using NftShirt.Server.Data.Entities;
namespace NftShirt.Server.Data;
public class NftShirtContext : DbContext
{
    public DbSet<Iten> Itens { get; set; } = null!;
    public DbSet<NFTag> NFTags { get; set; } = null!;
    public DbSet<NFT> NFTs { get; set; } = null!;
    public DbSet<Colection> Colections { get; set; } = null!;
    public DbSet<Wallet> Wallets { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public NftShirtContext(DbContextOptions<NftShirtContext> options)
    : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        var colection = modelBuilder.Entity<Colection>();
        colection
            .ToTable("Colections")
            .HasKey(c => c.Id);

        colection
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        colection
            .Property(c => c.Description)
            .IsRequired();

        colection
            .HasMany(c => c.NFTs)
            .WithOne(n => n.Colection)
            .HasForeignKey(n => n.ColectionID);
        
        var iten = modelBuilder.Entity<Iten>();
        iten.ToTable("Itens")
            .HasKey(i => i.Id);

        iten.Property(i => i.Nome)
            .HasMaxLength(100)
            .IsRequired();

        iten.Property(i => i.Descricao)
            .IsRequired();

        iten.HasMany(i => i.NFTags)
            .WithOne(n => n.Iten)
            .HasForeignKey(n => n.ItenId);

        var nft = modelBuilder.Entity<NFT>();

        nft.ToTable("NFTs")
            .HasKey(n => n.NftHash);

        nft.Property(n => n.Metadata);

        nft.HasOne(n => n.Colection)
            .WithMany(c => c.NFTs)
            .HasForeignKey(n => n.ColectionID);

        nft.HasOne(n => n.Wallet)
            .WithMany(w => w.NFTs)
            .HasForeignKey(n => n.WalletID);

        nft.HasMany(n => n.NFTags)
            .WithOne(nt => nt.NFT)
            .HasForeignKey(nt => nt.NftHash);

        var nftag = modelBuilder.Entity<NFTag>();

            nftag.ToTable("NFTags")
                .HasKey(nt => nt.LinkTag);

            nftag.Property(nt => nt.LinkTag)
                .IsRequired();

            nftag.HasOne(nt => nt.Iten)
                .WithMany(i => i.NFTags)
                .HasForeignKey(nt => nt.ItenId);

            nftag.HasOne(nt => nt.NFT)
                .WithMany(n => n.NFTags)
                .HasForeignKey(nt => nt.NftHash);

         var user = modelBuilder.Entity<User>();

            user.ToTable("Users")
                .HasKey(u => u.Id);

            user.Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired();

            user.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            user.HasMany(u => u.Wallets)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId);
                
            var wallet = modelBuilder.Entity<Wallet>();

            wallet.ToTable("Wallets")
                .HasKey(w => w.Id);

            wallet.Property(w => w.Adress)
                .IsRequired();

            wallet.HasOne(w => w.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.UserId);

            wallet.HasMany(w => w.NFTs)
                .WithOne(n => n.Wallet)
                .HasForeignKey(n => n.WalletID);


        
        base.OnModelCreating(modelBuilder);
    }
}
