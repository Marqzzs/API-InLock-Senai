using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System.Data;

namespace senai.inlock.webApi.Controllers
{
    //Define que a rota de uma requisição será no seguinte formato
    //dominio/api/nomeController
    //ex: http://localhost:/api/jogo
    [Route("api/[controller]")]
    //Define que é um controller api
    [ApiController]
    //Define que vamos trabalhar com JSON

    [Produces("application/json")]
    //Classe construtora que herda do repository jogo
    public class JogoController : ControllerBase
    {
        /// <summary>
        /// Cria o objeto que tera acesso aos metodos do JogoRepository
        /// </summary>
        private IJogoRepository _jogoRepository { get; set; }
        /// <summary>
        /// INstancia o objeto que tera acesso aos metodos
        /// </summary>
        public JogoController()
        {
            _jogoRepository = new JogoRepository();
        }

        /// <summary>
        /// End point que aciona o metodo de listar todos os jogos de sua lista
        /// </summary>
        /// <returns>Retorna a lista</returns>
        [HttpGet]
        [Authorize(Roles = "1, 2")]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebera os dados das aquisições
                List<JogoDomain> ListaJogo = _jogoRepository.ListarTodos();

                //Retorna um status code e a lista
                return StatusCode(200, ListaJogo);
            }
            catch (Exception erro) 
            {
                //Retorna um status code BadRequest(400) e a mensagem de erro
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// End point que aciona o metodo de cadastar
        /// </summary>
        /// <param name="novoJogo">Objeto que sera cadastrado</param>
        /// <returns>Retorna o objeto para o front end</returns>
        [HttpPost]
        [Authorize(Roles = "2")]
        public IActionResult Post(JogoDomain novoJogo)
        {
            try
            {
                //Aciona o metodo de cadastrar
                _jogoRepository.Cadastrar(novoJogo);
                //Retorna o status code de criacao
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// End point que acionara o metodo de deletar
        /// </summary>
        /// <param name="id">id do objeto a ser deletado</param>
        /// <returns>Retorna um status code e para o front end</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult Delete(int id)
        {
            try
            {
                _jogoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        
        /// <summary>
        /// End point que acionara o metodo de buscar por id
        /// </summary>
        /// <param name="id">id do objeto a ser buscado</param>
        /// <returns>Retorna para o front end</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "1, 2")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                JogoDomain jogo = _jogoRepository.BuscarId(id);

                if (jogo != null)
                {
                    return Ok(jogo); // Retorna o gênero encontrado com status 200 OK
                }
                else
                {
                    return NotFound(); // Retorna 404 Not Found se o gênero não for encontrado
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message); // Retorna erro 400 Bad Request em caso de exceção
            }
        }

        /// <summary>
        /// End point que atualizara um objeto jogo pelo seu corpo
        /// </summary>
        /// <param name="id">id do objeto a ser atualizado</param>
        /// <returns>Retorna um objeto com navas informações</returns>
        [HttpPut]
        [Authorize(Roles = "2")]
        public IActionResult Atualizar(int id, JogoDomain jogo)
        {
            try
            {
                // Busca o jogo pelo ID
                JogoDomain jogoBuscado = _jogoRepository.BuscarId(id);

                if (jogoBuscado != null)
                {
                    // Atualiza as propriedades do jogo buscado com os dados do jogo atualizado
                    jogoBuscado.Nome = jogo.Nome;
                    jogoBuscado.IdEstudio = jogo.IdEstudio;

                    try
                    {
                        _jogoRepository.Atualizar(jogoBuscado);
                        return Ok(); // Retorna um status code de sucesso
                    }
                    catch (Exception erro)
                    {
                        return BadRequest(erro.Message); // Retorna um status code de erro
                    }
                }
                else
                {
                    return NotFound("Filme não encontrado"); // Retorna um status code 404 se o jogo não for encontrado
                }
            }
            catch
            {
                return NotFound("Filme não encontrado");
            }
        }
    }
}
