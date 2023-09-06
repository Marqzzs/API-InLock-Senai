using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade(tabela) Jogo
    /// </summary>
    public class JogoDomain
    {
        // Declaração da propriedade para armazenar o ID do jogo
        public int IdJogo { get; set; }
        [Required(ErrorMessage ="Estudio obrigatorio")]

        // Declaração da propriedade para armazenar o ID do estúdio associado ao jogo
        public int IdEstudio { get; set; }
        [Required(ErrorMessage ="O nome é obrigatorio")]

        // Declaração da propriedade para armazenar o nome do jogo (pode ser nulo)
        public string? Nome { get; set; }
        [Required(ErrorMessage ="Descrição do jogo é obrigatorio")]

        // Declaração da propriedade para armazenar a descrição do jogo (pode ser nulo)
        public string? Descricao { get; set; }
        [Required(ErrorMessage ="Data de lançamento é obrigatorio")]

        // Declaração da propriedade para armazenar a data de lançamento do jogo (pode ser nula)
        public DateTime? DataLancamento { get; set; }

        [Required(ErrorMessage ="Valor obrigatorio")]
        // Declaração da propriedade para armazenar o valor do jogo (pode ser nulo)
        public float? Valor { get; set; }

        // Declaração da propriedade para representar uma associação com um estúdio (pode ser nula)
        public EstudioDomain? Estudio { get; set; }

    }
}
