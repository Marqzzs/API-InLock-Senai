using Microsoft.EntityFrameworkCore;
using webapi.inlock.codefirst.Domains;

namespace webapi.inlock.codefirst.Contexts
{
    public class InlockContext : DbContext  
    {
        //Definir as entidades do banco de dados
        public DbSet<Estudio> Estudio { get; set; }

        public DbSet<Jogo> Jogo { get; set; }

        public DbSet<TiposUsuario> TiposUsuario { get; set; }
                                                                                                      
        public DbSet<Usuario> Usuario { get; set; }

        /// <summary>
        /// Define as opções de construção do banco(string de conexão)
        /// </summary>
        /// <param name="optionsBuilder">Objeto com as configurações definidas</param>
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NOTE09-S14; DataBase=inlock_games_codeFirst_manha; User Id=SA; Pwd=Senai@134; TrustServerCertificate=True");
            base.OnConfiguring (optionsBuilder);
        }
    }
}
