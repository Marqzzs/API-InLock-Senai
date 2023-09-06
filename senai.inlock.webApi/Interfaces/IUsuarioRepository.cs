using senai.inlock.webApi.Domains;

namespace senai.inlock.webApi.Interfaces
{
    public interface IUsuarioRepository
    {
        UsuarioDomain Login(string email, string senha);

        public interface IUsuarioRepository
        {

            /// <summary>
            /// Método de autenticação de usuário
            /// </summary>
            /// <param name="email"> email do usuario </param>
            /// <param name="senha"> senha do usuario </param>
            /// <returns>retorna um objeto do tipo usuario</returns>s
            UsuarioDomain Login(string email, string senha);
        }
    }
}
