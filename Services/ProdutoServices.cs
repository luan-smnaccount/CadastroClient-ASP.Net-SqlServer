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

        public async Task<ProdutoModel> CreateUserAsync()
        {
            Console.Write("Informe o nome :");
            string nome = Console.ReadLine()!;

            Console.Write("Informe o email: ");
            string email = Console.ReadLine()!;

            Console.Write("Informe a senha: ");
            string password = Console.ReadLine()!;

            Console.Write("Informe a data de nascimento: ");
            string DataNascimento = Console.ReadLine()!;

            var usuario = new ProdutoModel
            {
                Nome = nome,
                Email = email,
                Senha = password,
                DataNascimento = DataNascimento
            };

            await _context.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }
    }
}
