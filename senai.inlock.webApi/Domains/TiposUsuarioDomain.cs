using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade(tabela) TiposUsuario
    /// </summary>
    public class TiposUsuarioDomain
    {
        // Declaração da propriedade para armazenar o ID do tipo de usuário
        public int IdTipoUsuario { get; set; }

        // Declaração da propriedade para armazenar o título do usuário (pode ser nulo)
        [Required]
        public string? Titulo { get; set; }

    }
}
