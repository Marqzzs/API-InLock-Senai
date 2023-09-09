using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System.Data.SqlClient;

namespace senai.inlock.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public string StringConexao = "Data Source = NOTE09-S14; Initial Catalog = inlock_games_manha; User Id = SA; Pwd = Senai@134";
        public UsuarioDomain Login(string email, string senha)
        {
            // Cria uma nova conexão com o banco de dados.
            using (SqlConnection connection = new SqlConnection(StringConexao))
            {
                // Define a consulta SQL para buscar o usuário com base no email e senha fornecidos.
                string queryUsuario = "SELECT IdUsuario, Email, Titulo FROM Usuario WHERE Email = @Email AND Senha = @Senha";

                // Abre a conexão com o banco de dados.
                connection.Open();

                // Cria um novo comando SQL usando a consulta e a conexão.
                using (SqlCommand cmd = new SqlCommand(queryUsuario, connection))
                {
                    // Adiciona parâmetros ao comando para substituir os marcadores na consulta.
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    // Executa a consulta SQL e obtém o resultado.
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Verifica se há dados no resultado.
                    if (rdr.Read())
                    {
                        // Cria um objeto UsuarioDomain e popula com os dados do resultado.
                        UsuarioDomain usuario = new UsuarioDomain
                        {
                            IdUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            Email = rdr["Email"].ToString(),
                            Titulo = rdr["Titulo"].ToString()
                        };
                        return usuario; // Retorna o objeto do usuário encontrado.
                    }
                    return null; // Retorna null se nenhum usuário for encontrado.
                }
            }
        }
    }
}
