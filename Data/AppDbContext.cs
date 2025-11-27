using Microsoft.EntityFrameworkCore;
using CrudNpN.Models;

namespace CrudNpN.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Venda> Vendas => Set<Venda>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<VendaProduto> VendasProdutos => Set<VendaProduto>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}