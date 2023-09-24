using FiapStore.Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapStore.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // Gera o token
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Secret")); // Transforma a chave em um array de bytes
            var tokenDescriptor = new SecurityTokenDescriptor() // Descreve tudo oq nosso token tem
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NomeUsuario), // configura User.Identity.Name
                    new Claim(ClaimTypes.Role, (usuario.Permissao - 1).ToString()) // configura User.IsInRole()
                    // Pode colocar quantos Claims vc quiser da maneira que quiser ex:
                    //new Claim("Id", usuario.Id.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(8), // Tempo de expiração do token
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
