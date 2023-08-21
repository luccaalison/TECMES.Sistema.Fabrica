using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class AplicacaoContext : DbContext
{
    public AplicacaoContext(DbContextOptions<AplicacaoContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Maquina> Maquinas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<OrdemProducao> OrdensProducao { get; set; }
    public DbSet<Producao> Producoes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .ToTable("Clientes")
            .HasKey(a => a.Id);
        modelBuilder.Entity<Cliente>()
            .HasMany(f => f.OrdensProducao)
            .WithOne(fk => fk.Cliente)
            .HasForeignKey(fk => fk.ClienteId);

        modelBuilder.Entity<Produto>().ToTable("Produtos").HasKey(a => a.Id);
        modelBuilder.Entity<Produto>().ToTable("Produtos")
            .HasMany(f => f.OrdensProducao)
            .WithOne(fk => fk.Produto)
            .HasForeignKey(fk => fk.ProdutoId);

        modelBuilder.Entity<Maquina>().ToTable("Maquinas").HasKey(a => a.Id);
        modelBuilder.Entity<Maquina>().ToTable("Maquinas")
            .HasMany(f => f.Producoes)
            .WithOne(fk => fk.Maquina)
            .HasForeignKey(fk => fk.MaquinaId);


        modelBuilder.Entity<Producao>().ToTable("Producoes").HasKey(a => a.Id);
        modelBuilder.Entity<Producao>().ToTable("Producoes")
            .HasOne<Maquina>(f => f.Maquina)
            .WithMany(fk => fk.Producoes)
            .HasForeignKey(fk => fk.MaquinaId);
        modelBuilder.Entity<Producao>().ToTable("Producoes")
            .HasOne<Produto>(f => f.Produto)
            .WithMany(fk => fk.Producoes)
            .HasForeignKey(fk => fk.ProdutoId);
        modelBuilder.Entity<Producao>().ToTable("Producoes")
            .HasOne<OrdemProducao>(f => f.OrdemProducao)
            .WithOne(fk => fk.Producao);

        modelBuilder.Entity<OrdemProducao>().ToTable("OrdensProducao").HasKey(a => a.Id);
        modelBuilder.Entity<OrdemProducao>().ToTable("OrdensProducao")
            .HasOne<Produto>(f => f.Produto)
            .WithMany(fk => fk.OrdensProducao)
            .HasForeignKey(fk => fk.ProdutoId);
        modelBuilder.Entity<OrdemProducao>().ToTable("OrdensProducao")
            .HasOne<Cliente>(f => f.Cliente)
            .WithMany(fk => fk.OrdensProducao)
            .HasForeignKey(fk => fk.ClienteId);

        // pedidos
        modelBuilder.Entity<Pedido>().ToTable("Pedidos").HasKey(a => a.Id);
        modelBuilder.Entity<Pedido>().ToTable("Pedidos")
            .HasOne<Produto>(f => f.Produto)
            .WithMany(fk => fk.Pedidos)
            .HasForeignKey(fk => fk.ProdutoId);
        modelBuilder.Entity<Pedido>().ToTable("Pedidos")
            .HasOne<Cliente>(f => f.Cliente)
            .WithMany(fk => fk.Pedidos)
            .HasForeignKey(fk => fk.ClienteId);
        modelBuilder.Entity<Pedido>().ToTable("Pedidos")
            .HasOne<OrdemProducao>(f => f.OrdemDeProducao)
            .WithOne(fk => fk.Pedido);
        

        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
    }
}