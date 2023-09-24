using Microsoft.AspNetCore.Mvc;
using FiapStore.Interface;
using FiapStore.Entidades;
using FiapStore.DTO;
using Microsoft.AspNetCore.Authorization;
using FiapStore.Enum;

namespace FiapStore.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;


        public UsuarioController(
            IUsuarioRepository usuarioRepository,
            ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        [HttpGet()]
        [Authorize]
        public IActionResult ObterTodosUsuario()
        {
            _logger.LogInformation("EXECUTANDO ObterTodosUsuario!");
            var usuario = _usuarioRepository.ObterTodos();
            return Ok(usuario);
        }


        [HttpGet("{id}")]
        [Authorize]
        [Authorize(Roles = Permissoes.Funcionario)]
        public IActionResult ObterUsuarioPorId([FromRoute] int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);
            if (usuario == null)
            {
                _logger.LogWarning("Nenhum usuario encontrado");
                return NoContent();
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Cadastra usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Retorna mensagem dizendo se usuário foi cadastrado ou não</returns>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Todo
        ///     {
        ///        "nome": "nome",
        ///        "nomeUsuario": "nomeUsuario",
        ///        "senha": "senha",
        ///        "permissao": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Retorna sucesso</response>
        /// <response code="401">Se não está autenticado</response>  
        /// <response code="403">Se não está autorizado</response>  
        [HttpPost()]
        [Authorize]
        [Authorize(Roles = Permissoes.Administrador)]
        public IActionResult CriarUsuario([FromBody] CadastrarUsuarioDTO usuario)
        {
            try
            {
                _usuarioRepository.Cadastrar(new Usuario(usuario));
                return Ok("Usuário criado com sucesso");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut()]
        [Authorize]
        [Authorize(Roles = Permissoes.Administrador)]
        public IActionResult AlterarUsuario([FromBody] AlterarUsuarioDTO usuario)
        {
            _usuarioRepository.Alterar(new Usuario(usuario));
            return Ok("Usuario alterado com sucesso");
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Authorize(Roles = Permissoes.Administrador)]
        public IActionResult DeletarUsuario([FromRoute] int id)
        {
            _usuarioRepository.Deletar(id);
            return Ok("Usuario deletado com sucesso");
        }

        [HttpGet("com-pedidos/{id}")]
        [Authorize(Roles = $"{Permissoes.Administrador},{Permissoes.Funcionario}")]
        public IActionResult ObeterUsuarioComPedidos([FromRoute] int id)
        {
            _logger.LogTrace("FiapStore::FiapStore.Controller::UsuarioController::ObeterUsuarioComPedidos");
            return Ok(_usuarioRepository.ObterComPedidos(id));
        }
    }
}
