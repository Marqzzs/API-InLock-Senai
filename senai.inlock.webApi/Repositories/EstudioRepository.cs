using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        /// <summary>
        /// 
        /// </summary>
        public string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = inlock_games_manha; User Id = SA; Pwd = Senai@134;MultipleActiveResultSets=true";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="estudio"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Atualizar(EstudioDomain estudio)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public EstudioDomain Buscarid(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="estudio"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Cadastrar(EstudioDomain estudio)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<EstudioDomain> ListarTodos()
        {
            List<EstudioDomain> EstudioList = new List<EstudioDomain>();

            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                string queryAllEstudio = "SELECT E.IdEstudio, E.Nome AS NomeEstudio FROM Estudio E";

                connection.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryAllEstudio, connection))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EstudioDomain estudio = new EstudioDomain()
                        {
                            IdEstudio = Convert.ToInt32(rdr["IdEstudio"]),
                            Nome = rdr["NomeEstudio"].ToString(),
                        };

                        // Agora, crie uma consulta separada para buscar os jogos vinculados a este estúdio
                        estudio.Jogos = BuscarJogosPorEstudio(estudio.IdEstudio, connection);

                        EstudioList.Add(estudio);
                    }

                    rdr.Close(); // Feche o DataReader após a leitura dos estúdios
                }
            }

            return EstudioList;
        }

        private List<JogoDomain> BuscarJogosPorEstudio(int idEstudio, SqlConnection connection)
        {
            List<JogoDomain> jogos = new List<JogoDomain>();

            string queryJogosPorEstudio = "SELECT IdJogo, Nome FROM Jogo WHERE IdEstudio = @IdEstudio";

            using (SqlCommand cmd = new SqlCommand(queryJogosPorEstudio, connection))
            {
                cmd.Parameters.AddWithValue("@IdEstudio", idEstudio);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    JogoDomain jogo = new JogoDomain()
                    {
                        IdJogo = Convert.ToInt32(rdr["IdJogo"]),
                        Nome = rdr["Nome"].ToString(),
                    };

                    jogos.Add(jogo);
                }

                rdr.Close(); // Feche o DataReader após a leitura dos jogos
            }

            return jogos;
        }


    }
}
