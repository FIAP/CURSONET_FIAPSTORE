using FiapStore.Entidades;

namespace FiapStore.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}
