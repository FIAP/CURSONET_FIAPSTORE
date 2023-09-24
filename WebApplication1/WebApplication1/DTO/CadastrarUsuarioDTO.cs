using FiapStore.Enum;

namespace FiapStore.DTO
{
    public class CadastrarUsuarioDTO
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        public TipoPermissao Permissao { get; set; }
    }
}
