using Microsoft.EntityFrameworkCore;
using NftShirt.Server.Data.Entities;
namespace NftShirt.Server.Data;
public class NftShirtContext : DbContext
{
    public DbSet<Iten> Itens { get; set; } = null!;
    public DbSet<Nftag> Nftags { get; set; } = null!;
    public DbSet<Nft> Nfts { get; set; } = null!;
    public DbSet<Collection> Colections { get; set; } = null!;
    public DbSet<Wallet> Wallets { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public AbiConfigure _abiConfigure = new AbiConfigure();
    public NftShirtContext(DbContextOptions<NftShirtContext> options)
    : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        var colection = modelBuilder.Entity<Collection>();
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
            .HasMany(c => c.Nfts)
            .WithOne(n => n.Collection)
            .HasForeignKey(n => n.ColectionID);
        colection
            .HasOne(c=> c.Contract)
            .WithMany(n => n.Collections)
            .HasForeignKey(n => n.ContractAdress);
        
        var iten = modelBuilder.Entity<Iten>();
        iten.ToTable("Itens")
            .HasKey(i => i.Id);

        iten.Property(i => i.Nome)
            .HasMaxLength(100)
            .IsRequired();

        iten.Property(i => i.Descricao)
            .IsRequired();

        iten.HasMany(i => i.Nftags)
            .WithOne(n => n.Iten)
            .HasForeignKey(n => n.ItenId);

        var nft = modelBuilder.Entity<Nft>();

        nft.ToTable("Nfts")
            .HasKey(n => n.TokenId);

        nft.Property(n => n.Metadata);

        nft.Property(n => n.TokenId); 

        nft.Property(n => n.TokenURI);

        nft.Property(n => n.OwnerAddress);
        
        nft.HasOne(n => n.Collection)
            .WithMany(c => c.Nfts)
            .HasForeignKey(n => n.ColectionID);

        nft.HasOne(n => n.Wallet)
            .WithMany(w => w.Nfts)
            .HasForeignKey(n => n.WalletID);

        nft.HasMany(n => n.Nftags)
            .WithOne(nt => nt.Nft)
            .HasForeignKey(nt => nt.NftHash);
        
        var nftag = modelBuilder.Entity<Nftag>();

            nftag.ToTable("Nftags")
                .HasKey(nt => nt.LinkTag);

            nftag.Property(nt => nt.LinkTag)
                .IsRequired();

            nftag.HasOne(nt => nt.Iten)
                .WithMany(i => i.Nftags)
                .HasForeignKey(nt => nt.ItenId);

            nftag.HasOne(nt => nt.Nft)
                .WithMany(n => n.Nftags)
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

            wallet.HasMany(w => w.Nfts)
                .WithOne(n => n.Wallet)
                .HasForeignKey(n => n.WalletID);

            var contract = modelBuilder.Entity<Contract>();

            contract.ToTable("Contracts")   
                .HasKey(c => c.Adress);

            contract.Property(c => c.Abi)
                .HasColumnType("jsonb")
                .IsRequired();

            contract.HasMany(c => c.Collections)
                .WithOne(c => c.Contract)
                .HasForeignKey(c=> c.ContractAdress);


        modelBuilder.Entity<Contract>().HasData(
            new Contract
            {
                Adress = "0x18214613dCc65311Fb7471B61a02825537F87a52",
                Abi =_abiConfigure.setarAbiFirst()
            }
        );
        modelBuilder.Entity<Collection>().HasData(
            new Collection
            {
                Id = 1,
                Name = "Capy WorkFound",
                Description = "Nftshirt Teste",
                ContractAdress = "0x18214613dCc65311Fb7471B61a02825537F87a52"

            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
