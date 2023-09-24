using FiapStore.DTO;
using FiapStore.Enum;

namespace FiapStore.Entidades
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

        public Usuario()
        {
        }

        public Usuario(CadastrarUsuarioDTO usuario)
        {
            Nome = usuario.Nome;
            NomeUsuario = usuario.NomeUsuario;
            Senha = usuario.Senha;
            Permissao = usuario.Permissao;
        }

        public Usuario(AlterarUsuarioDTO usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
        }
    }
}
