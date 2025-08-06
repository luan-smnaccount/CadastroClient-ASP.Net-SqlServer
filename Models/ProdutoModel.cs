namespace CadastroClient_ASP.Net_SqlServer.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Senha { get; set; } = string.Empty;
        public string? DataNascimento { get; set; } = string.Empty;
    }
}