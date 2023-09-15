using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.inlock.codefirst.Domains
{
    [Table("Jogo")]
    public class Jogo
    {
        [Key]
        public Guid IdJogo{ get; set; } = Guid.NewGuid();

        [Column(TypeName ="VARCHAR(50)")]
        [Required(ErrorMessage ="O nome do jogo é obrigatorio")]
        public string? Nome { get; set; }

        [Column(TypeName = "TEXT")]
        [Required(ErrorMessage ="A descrição é obrigatoria")]
        public string? Descricao { get; set; }

        [Column(TypeName ="DATE")]
        [Required(ErrorMessage ="A data de lançamento é obrigatoria")]
        public DateTime DataLancamento { get; set; }

        [Column(TypeName ="DECIMAL(4,2)")]
        [Required(ErrorMessage ="O valor é obrigatorio")]
        public decimal Preco { get; set; }

        //Referecia da chave estrangeira da tabela Estudio

        [Required(ErrorMessage ="Informe o estudio que produziu o jogo")]
        public Guid IdEstudio { get; set; }

        [ForeignKey("IdEstudio")]
        public Estudio? Estudio { get; set; }
    }
}
