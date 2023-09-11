using senai.inlock.webApi.Domains;
using System.Collections.Generic;

namespace senai.inlock.webApi.Interfaces
{
    public interface IEstudioRepository
    {
        /// <summary>
        /// Cadastra um novo estúdio.
        /// </summary>
        /// <param name="estudio">Objeto EstudioDomain a ser cadastrado.</param>
        void Cadastrar(EstudioDomain estudio);

        /// <summary>
        /// Lista todos os estúdios.
        /// </summary>
        /// <returns>Uma lista de objetos EstudioDomain.</returns>
        List<EstudioDomain> ListarTodos();

        /// <summary>
        /// Atualiza os dados de um estúdio.
        /// </summary>
        /// <param name="estudio">Objeto EstudioDomain com os dados atualizados.</param>
        void Atualizar(EstudioDomain estudio);

        /// <summary>
        /// Deleta um estúdio pelo seu ID.
        /// </summary>
        /// <param name="id">ID do estúdio a ser deletado.</param>
        void Deletar(int id);

        /// <summary>
        /// Busca um estúdio pelo seu ID.
        /// </summary>
        /// <param name="id">ID do estúdio a ser buscado.</param>
        /// <returns>Objeto EstudioDomain correspondente ao ID informado ou null se não encontrado.</returns>
        EstudioDomain Buscarid(int id);
    }
}
