using Microsoft.EntityFrameworkCore;
using webapi.inlock.codefirst.Contexts;
using webapi.inlock.codefirst.Domains;
using webapi.inlock.codefirst.Interfaces;

namespace webapi.inlock.codefirst.Repositories
{
    public class EstudioRepository : IEstudioRepository
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
        public EstudioRepository()
        {
            ctx = new InlockContext();
        }

        public void Atualizar(Guid id, Estudio estudio)
        {
            Estudio estudioBuscado = ctx.Estudio.Find(id)!;

            if (estudioBuscado != null)
            {
                estudioBuscado.Nome = estudio.Nome;
            }

            ctx.Estudio.Update(estudioBuscado!);

            ctx.SaveChanges();
        }

        public Estudio BuscarPorId(Guid id)
        {
            return ctx.Estudio.FirstOrDefault(e => e.IdEstudio == id)!;
        }

        public void Cadastrar(Estudio estudio)
        {
            ctx.Estudio.Add(estudio);

            ctx.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            Estudio estudioBuscado = ctx.Estudio.Find(id)!;

            ctx.Estudio.Remove(estudioBuscado);

            ctx.SaveChanges();
        }

        public List<Estudio> Listar()
        {
            return ctx.Estudio.ToList();
        }

        public List<Estudio> ListarComJogos()
        {
            return ctx.Estudio.Include(e => e.Jogos).ToList();
        }
    }
}
