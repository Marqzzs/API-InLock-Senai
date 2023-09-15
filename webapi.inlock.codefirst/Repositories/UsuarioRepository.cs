using webapi.inlock.codefirst.Contexts;
using webapi.inlock.codefirst.Domains;
using webapi.inlock.codefirst.Interfaces;
using webapi.inlock.codefirst.Uteis;

namespace webapi.inlock.codefirst.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        /// <summary>
        /// Variavel privada somente para leitura para armazenar os dados do contexto
        /// </summary>
        private readonly InlockContext ctx;
        /// <summary>
        /// Construtor do repositorio
        /// Toda vez que o repositorio for iinstanciado
        /// Ele tera acesso aos dados fornecidos pelo contexto
        /// </summary>
        public UsuarioRepository()
        {
            ctx = new InlockContext();
        }

        public Usuario BuscarUsuario(string email, string senha)
        {
            try
            {
                Usuario usuarioBuscado = ctx.Usuario.FirstOrDefault(u => u.Email == email)!;

                if (usuarioBuscado != null)
                {
                    bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha!);

                    if (confere)
                    {
                        return usuarioBuscado;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            try
            {

            usuario.Senha = Criptografia.GerarHash(usuario.Senha!);

            ctx.Usuario.Add(usuario);

            ctx.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Usuario Login(string email, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
