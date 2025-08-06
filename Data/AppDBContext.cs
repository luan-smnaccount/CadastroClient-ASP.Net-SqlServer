using CadastroClient_ASP.Net_SqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroClient_ASP.Net_SqlServer.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produto { get; set; }
    }
}