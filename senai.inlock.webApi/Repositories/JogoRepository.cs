using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;
using System.Globalization;

namespace senai.inlock.webApi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class JogoRepository : IJogoRepository
    {
        /// <summary>
        /// string de conexao com o banco de dados que recebe os seguintes parametros
        /// Data Source : nome do servidor
        /// Initial Catalog : nome do banco de dados
        /// Autenticacao :
        ///     - Windows : integrated Security = true;
        ///     - SQL : User id : sa; Pwd : Senha
        /// </summary>
        public string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = inlock_games_manha; User Id = SA; Pwd = Senai@134";

        /// <summary>
        /// Atualiza as informações de um jogo.
        /// </summary>
        /// <param name="jogo">O objeto JogoDomain com as informações atualizadas.</param>
        public void Atualizar(JogoDomain jogo)
        {
            // Implementação do método para atualizar um jogo no repositório.
        }

        /// <summary>
        /// Busca um jogo pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do jogo a ser buscado.</param>
        /// <returns>O objeto JogoDomain encontrado ou nulo se não for encontrado.</returns>
        public JogoDomain BuscarId(int id)
        {
            // Implementação do método para buscar um jogo pelo ID no repositório.
            return null; // Você deve implementar a lógica para retornar o jogo encontrado.
        }

        /// <summary>
        /// Cadastra um novo jogo.
        /// </summary>
        /// <param name="novoJogo">O objeto JogoDomain a ser cadastrado.</param>
        public void Cadastrar(JogoDomain novoJogo)
        {
            //Declara a conexão passando a string de conexão como parametro
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //Declara a query a ser executada
                string queryInsert = "INSERT INTO Jogo(IdEstudio, Nome, Descricao, DataLancamento, Valor) VALUES (@IdEstudio, @Nome, @Descricao, @Datalancamento, @Valor)";

                //Declara o SqlCommand passando a query que será executada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(queryInsert, connection))
                {
                    // Atribui o valor do IdGenero do NovoFilme ao parâmetro @IdGenero
                    cmd.Parameters.AddWithValue("@IdEstudio", novoJogo.IdEstudio);
                    cmd.Parameters.AddWithValue("@Nome", novoJogo.Nome);
                    cmd.Parameters.AddWithValue("@Descricao", novoJogo.Descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", novoJogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", novoJogo.Valor);

                    //Abre a conexão com o bd
                    connection.Open();

                    //Executar a query(queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
        }
 
        /// <summary>
        /// Deleta um jogo pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do jogo a ser deletado.</param>
        public void Deletar(int id)
        {
            // Implementação do método para deletar um jogo pelo ID no repositório.
        }

        /// <summary>
        /// Lista todos os jogos cadastrados.
        /// </summary>
        /// <returns>Uma lista de objetos JogoDomain representando os jogos cadastrados.</returns>
        public List<JogoDomain> ListarTodos()
        {
            //cria uma lista para armazenar os jogos
            List<JogoDomain> ListaJogo = new List<JogoDomain>();

            //Cria uma conexao com o banco de dados utilizando a conexao que fizemos antes
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //FaZ a query select que será executada
                string queryAll = "Select J.IdJogo, J.Nome, J.Descricao, J.DataLancamento, J.Valor, J.IdEstudio, J.Nome AS NomeEstudio FROM Jogo J INNER JOIN Estudio E ON J.IdEstudio = E.IdEstudio";

                //Abre a conexão com o banco de dados
                connection.Open();

                //Declara um objeto data reader que percorrera os objetos
                SqlDataReader rdr;

                //Cria um novo sql command com a query e a conexao
                using(SqlCommand cmd = new SqlCommand(queryAll, connection))
                {
                    //executa o rdr
                    rdr = cmd.ExecuteReader();

                    //Percorre os dados da consulta
                    while (rdr.Read())
                    {
                        //cria um novo objeto de jogo que sera preenchido
                        JogoDomain jogo = new JogoDomain()
                        {
                            IdJogo = Convert.ToInt32(rdr["IdJogo"]),
                            Nome = rdr["Nome"].ToString(),
                            Descricao = rdr["Descricao"].ToString(),
                            DataLancamento = DateTime.ParseExact("2012-09-24", "yyyy-MM-dd", CultureInfo.InvariantCulture), // Converte a data manualmente
                            Valor = Convert.ToSingle(rdr["Valor"]),

                            Estudio = new EstudioDomain()
                            {
                                IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                                Nome = rdr["Nome"].ToString(),
                            }

                        };
                        //Adiciona o objeto jogo a lista
                        ListaJogo.Add(jogo);
                    }
                    //Fecha o rdr
                    rdr.Close();
                    //Retorna a lista
                    return ListaJogo;
                }
            }
        }
    }
}
