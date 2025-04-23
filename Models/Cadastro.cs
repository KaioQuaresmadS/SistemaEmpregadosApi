using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpregadoApi.Models
{
    public class Cadastro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpregadoId { get; set; }

        [Required, MaxLength(50)]
        public string? firstName { get; set; }

        [Required, MaxLength(50)]
        public string? lastName { get; set; }

        [Required, MaxLength(50)]
        public string? email { get; set; }

        [Required, MaxLength(13)]
        public string? phone { get; set; }

        [Required, MaxLength(50)]
        public string? city { get; set; }

        [Required, MaxLength(50)]
        public string? address { get; set; }



    }
}
