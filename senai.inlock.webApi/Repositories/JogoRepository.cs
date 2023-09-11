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
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                // Define a consulta SQL para atualizar os dados do filme com base no ID do filme
                string queryInsert = "UPDATE Jogo SET Nome = @Nome, IdEstudio = @idEstudio, Descricao = @Descricao, DataLancamento = @DataLancamento, Valor = @Valor WHERE IdJogo = @idJogo";

                // Abre a conexão com o banco de dados
                connection.Open();

                // Cria um novo comando SQL usando a consulta e a conexão
                using (SqlCommand cmd = new SqlCommand(queryInsert, connection))
                {
                    // Adiciona parâmetros ao comando para substituir os marcadores na consulta
                    cmd.Parameters.AddWithValue("@idJogo", jogo.IdJogo); // Define o ID do jogo a ser atualizado
                    cmd.Parameters.AddWithValue("@idEstudio", jogo.IdEstudio); // Define o novo ID estudio do jogo
                    cmd.Parameters.AddWithValue("@Nome", jogo.Nome); // Define o novo nome do jogo
                    cmd.Parameters.AddWithValue("@Descricao", jogo.Descricao);
                    cmd.Parameters.AddWithValue("@DataLancamento", jogo.DataLancamento);
                    cmd.Parameters.AddWithValue("@Valor", jogo.Valor);

                    // Executa o comando SQL de atualização no banco de dados
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Busca um jogo pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do jogo a ser buscado.</param>
        /// <returns>O objeto JogoDomain encontrado ou nulo se não for encontrado.</returns>
        public JogoDomain BuscarId(int id)
        {
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                string queryBuscar = "SELECT IdJogo, IdEstudio, Nome, Descricao, DataLancamento, Valor FROM Jogo WHERE IdJogo= @IdJogo";

                using (SqlCommand cmd = new SqlCommand(queryBuscar, connection))
                {

                    cmd.Parameters.AddWithValue("@IdJogo", id);
                    //Abre a conexão com o banco de dados
                    connection.Open();

                    //Declara o SqlDataReader para percorrer a tabela do banco de dados
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            // Cria um objeto FilmeDomain e preenche suas propriedades com os valores do rdr
                            JogoDomain jogo = new JogoDomain()
                            {
                                IdJogo = Convert.ToInt32(rdr["IdJogo"]),
                                Nome = rdr["Nome"].ToString(),
                                Descricao = rdr["Descricao"].ToString(),
                                DataLancamento = Convert.ToDateTime(rdr["DataLancamento"]).Date,
                                Valor = Convert.ToSingle(rdr["Valor"]),
                                IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                                // Cria um novo objeto GeneroDomain e preenche suas propriedades com os valores do rdr
                                Estudio = new EstudioDomain()
                                {
                                    IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                                    Nome = rdr["Nome"].ToString()
                                }
                            };
                            return jogo;
                        }
                        else
                        {
                            //Retorna nulo
                            return null;
                        }

                    }
                }
            }
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
            //Declara a conexão passando a string de conexão como parametro
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                //Declara a query a ser executada
                string queryDelete = "DELETE FROM Jogo WHERE IdJogo = @IdJogo";
                //Declara o SqlCommand passando a query que será execuatada e a conexão com o bd
                using (SqlCommand cmd = new SqlCommand(queryDelete, connection))
                {
                    cmd.Parameters.AddWithValue("@IdJogo", id);

                    //Abre a conexão com o bd
                    connection.Open();

                    //Executar a query(queryInsert)
                    cmd.ExecuteNonQuery();
                }
            }
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
