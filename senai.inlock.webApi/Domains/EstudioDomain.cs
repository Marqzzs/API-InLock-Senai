using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade(tabela) Estudio
    /// </summary>
    public class EstudioDomain
    {
        // Declaração da propriedade para armazenar o ID do estúdio
        public int IdEstudio { get; set; }

        // Declaração da propriedade para armazenar o nome do estúdio (pode ser nulo)
        [Required(ErrorMessage = "O nome do estúdio é obrigatório!")]
        public string? Nome { get; set; }

    }
}
