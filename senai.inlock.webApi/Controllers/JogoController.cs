using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;

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
    }
}
