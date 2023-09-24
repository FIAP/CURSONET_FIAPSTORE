using Dapper;
using FiapStore.Entidades;
using FiapStore.Interface;
using System.Data.Common;
using System.Data.SqlClient;

namespace FiapStore.Repository
{
    public class UsuarioRepository : DapperRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Alterar(Usuario entidade)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                string query = "UPDATE Usuario SET Nome = @Nome WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(query, entidade);
            }
        }

        public override void Cadastrar(Usuario entidade)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Usuario (Nome) VALUES(@Nome)";
                dbConnection.Open();
                dbConnection.Execute(query, entidade);
            }
        }

        public override void Deletar(int id)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM Usuario"
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public override Usuario ObterPorId(int id)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM Usuario WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Usuario>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public override IList<Usuario> ObterTodos()
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                var usuarios = dbConnection.Query<Usuario>("SELECT * FROM Usuario");
                return usuarios.ToList();
            }
        }

        public Usuario ObterComPedidos(int id)
        {
            using var dbConnection = new SqlConnection(ConnectionString);
            var query = "SELECT Usuario.Id, Usuario.Nome, Pedido.Id, Pedido.NomeProduto, Pedido.UsuarioId"
                + " FROM Usuario LEFT JOIN Pedido ON Usuario.Id = Pedido.UsuarioId WHERE Usuario.Id = @Id";
            var resultado = new Dictionary<int, Usuario>();

            var parametros = new { Id = id };

            dbConnection.Query<Usuario, Pedido, Usuario>(query, (usuario, pedido) =>
            {
                if (!resultado.TryGetValue(usuario.Id, out var usuarioExistente))
                {
                    usuarioExistente = usuario;
                    usuarioExistente.Pedidos = new List<Pedido>();
                    resultado.Add(usuario.Id, usuarioExistente);
                }

                if (pedido != null)
                    usuarioExistente.Pedidos.Add(pedido);

                return usuarioExistente;
            }, parametros, splitOn: "Id");

            var usuario = resultado.Values.FirstOrDefault();
            return usuario;
        }

        public Usuario ObterPorNomeUsuarioESenha(string nomeUsuario, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
