using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;

namespace senai.inlock.webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EstudioController : ControllerBase
    {
        private IEstudioRepository _estudioRepository { get; set; }

        public  EstudioController()
        {
            _estudioRepository = new EstudioRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebera os dados das aquisições
                List<EstudioDomain> ListaEstudio = _estudioRepository.ListarTodos();

                //Retorna um status code e a lista
                return StatusCode(200, ListaEstudio);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem de erro
                return BadRequest(erro.Message);
            }
        }
    }
}
