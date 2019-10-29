using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GUFOS_BackEnd.Domains
{
    public partial class Evento
    {
        public Evento()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        public int IdEvento { get; set; }
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }
        public int? IdCategoria { get; set; }
        [Required]
        public bool? AcessoLivre { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEvento { get; set; }
        public int? IdLocalizacao { get; set; }

        [ForeignKey(nameof(IdCategoria))]
        [InverseProperty(nameof(Categoria.Evento))]
        public virtual Categoria IdCategoriaNavigation { get; set; }
        [ForeignKey(nameof(IdLocalizacao))]
        [InverseProperty(nameof(Localizacao.Evento))]
        public virtual Localizacao IdLocalizacaoNavigation { get; set; }
        [InverseProperty("IdEventoNavigation")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
