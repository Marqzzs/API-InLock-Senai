using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade(tabela) Usuario
    /// </summary>
    public class UsuarioDomain
    {
        // Declaração da propriedade para armazenar o ID do usuário
        public int IdUsuario { get; set; }

        // Declaração da propriedade para armazenar o ID do tipo de usuário
        public int IdTipoUsuario { get; set; }

        // Declaração da propriedade para armazenar o endereço de email do usuário (pode ser nulo)
        [Required(ErrorMessage = "O email é obrigatório!")]
        public string? Email { get; set; }

        // Declaração da propriedade para armazenar a senha do usuário com restrições de tamanho
        [StringLength(20, MinimumLength = 3, ErrorMessage = "A senha deve ter de 3 a 20 caracteres")]
        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string? Senha { get; set; }

        // Declaração da propriedade para representar o tipo de usuário associado (pode ser nulo)
        public TiposUsuarioDomain? TiposUsuario { get; set; }

    }
}
