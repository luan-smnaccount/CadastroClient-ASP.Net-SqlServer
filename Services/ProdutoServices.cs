using CadastroClient_ASP.Net_SqlServer.Data;
using CadastroClient_ASP.Net_SqlServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroClient_ASP.Net_SqlServer.Services
{
    public class ProdutoServices
    {
        private readonly AppDBContext _context;

        public ProdutoServices(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoModel>> GetUserAsync()
        {
            return await _context.Produto.ToListAsync();
        }

        public async Task<ProdutoModel> GetByIdAsync(int idUser)
        {
            var usuario = await _context.Produto.FindAsync(idUser);
            if (usuario == null)
            {
                Console.WriteLine("Usuário não cadastrado!");
                Thread.Sleep(1000);
                return null;
            }

            return usuario;
        }
        
        public async Task<ProdutoModel> CreateUserAsync(ProdutoModel user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
