using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.inlock.codefirst.Domains
{
    [Table("Usuario")]
    [Index(nameof(Email), IsUnique = true)] //Cria um indice para email para ser campo unico
    public class Usuario
    {
        [Key]
        public Guid IdUsuario { get; set; } = Guid.NewGuid();

        [Column(TypeName ="VARCHAR(100)")]
        [Required(ErrorMessage ="O email e obrigatorio")]
        public string? Email{ get; set; }

        [Column(TypeName ="VARCHAR(100)")]
        [Required(ErrorMessage ="Senha obrigatoio")]
        [StringLength(100, MinimumLength =6, ErrorMessage ="A senha deve ter de 6 a 20 caracteres")]
        public string? Senha{ get; set; }

        //Referencia atabela estrangeira (TiposUsuario)
        [Required(ErrorMessage ="Tipo de usuario obrigatorio")]
        public Guid IdTipoUsuario { get; set; }

        [ForeignKey("IdTipoUsuario")]
        public TiposUsuario? TipoUsuario { get; set; }

    }
}
