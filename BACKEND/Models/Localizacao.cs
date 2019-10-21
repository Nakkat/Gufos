using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND.Models
{
    public partial class Localizacao
    {
        public Localizacao()
        {
            Eventos = new HashSet<Eventos>();
        }

        [Key]
        public int IdLocalizacao { get; set; }
        [Required]
        [Column("CNPJ")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [StringLength(255)]
        public string RazaoSocial { get; set; }
        [Required]
        [StringLength(255)]
        public string Endereco { get; set; }

        [InverseProperty("IdLocalizacaoNavigation")]
        public virtual ICollection<Eventos> Eventos { get; set; }
    }
}
