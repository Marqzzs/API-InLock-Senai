using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    /// <summary>
    /// Interface para o repositório de jogos.
    /// </summary>
    public interface IJogoRepository
    {
        /// <summary>
        /// Cadastra um novo jogo.
        /// </summary>
        /// <param name="novoJogo">O objeto JogoDomain a ser cadastrado.</param>
        void Cadastrar(JogoDomain novoJogo);

        /// <summary>
        /// Lista todos os jogos cadastrados.
        /// </summary>
        /// <returns>Uma lista de objetos JogoDomain representando os jogos cadastrados.</returns>
        List<JogoDomain> ListarTodos();

        /// <summary>
        /// Atualiza as informações de um jogo.
        /// </summary>
        /// <param name="jogo">O objeto JogoDomain com as informações atualizadas.</param>
        void Atualizar(JogoDomain jogo);

        /// <summary>
        /// Deleta um jogo pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do jogo a ser deletado.</param>
        void Deletar(int id);

        /// <summary>
        /// Busca um jogo pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do jogo a ser buscado.</param>
        /// <returns>O objeto JogoDomain encontrado ou nulo se não for encontrado.</returns>
        JogoDomain BuscarId(int id);
    }
}

